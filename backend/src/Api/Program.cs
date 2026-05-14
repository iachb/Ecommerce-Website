using Ecommerce.Api.Middlewares;
using Ecommerce.Application;
using Ecommerce.Application.Extensions;
using Ecommerce.Application.Features.Products.Queries.GetProductList;
using Ecommerce.Domain;
using Ecommerce.Infrastructure;
using Ecommerce.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

// DB connection
builder.Services.AddDbContext<EcommerceDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ConnectionString"),
        sqlOptions => sqlOptions.MigrationsAssembly(typeof(EcommerceDbContext).Assembly.FullName)
    )
);

// MediatR
builder.Services.AddMediatR(typeof(GetProductListQueryHandler).Assembly);

// Controllers with global authorization filter
builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
}).AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

// Register FluentEmail and EmailFluentSettings
builder.Services.AddServiceEmail(builder.Configuration);

// Identity config
IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<User>();
identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);

identityBuilder.AddRoles<IdentityRole>().AddDefaultTokenProviders();
identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<User, IdentityRole>>();

identityBuilder.AddEntityFrameworkStores<EcommerceDbContext>();
identityBuilder.AddSignInManager<SignInManager<User>>();

builder.Services.TryAddSingleton<ISystemClock, SystemClock>();

// JWT Config
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateAudience = false,
            ValidateIssuer = false
        };
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
    );
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var controllerDocNames = typeof(Program).Assembly
        .GetTypes()
        .Where(t => t.IsClass
            && !t.IsAbstract
            && t.Name.EndsWith("Controller", StringComparison.Ordinal))
        .Select(t => t.Name[..^"Controller".Length])
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .OrderBy(n => n)
        .ToList();

    options.SwaggerDoc("All", new OpenApiInfo { Title = "Ecommerce API", Version = "v1" });

    foreach (var docName in controllerDocNames)
    {
        options.SwaggerDoc(docName, new OpenApiInfo { Title = $"{docName} API", Version = "v1" });
    }

    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        if (string.Equals(docName, "All", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        if (!apiDesc.ActionDescriptor.RouteValues.TryGetValue("controller", out var controllerName))
        {
            return false;
        }

        return string.Equals(controllerName, docName, StringComparison.OrdinalIgnoreCase);
    });

    // Keep grouping inside each doc by controller as well
    options.TagActionsBy(api =>
        new[] { api.ActionDescriptor.RouteValues["controller"] ?? api.GroupName ?? "Default" });

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/All/swagger.json", "All");

        var controllerDocNames = typeof(Program).Assembly
            .GetTypes()
            .Where(t => t.IsClass
                && !t.IsAbstract
                && t.Name.EndsWith("Controller", StringComparison.Ordinal))
            .Select(t => t.Name[..^"Controller".Length])
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(n => n)
            .ToList();

        foreach (var docName in controllerDocNames)
        {
            options.SwaggerEndpoint($"/swagger/{docName}/swagger.json", docName);
        }
    });
}

//app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("CorsPolicy");

app.MapControllers();

// Application initialization
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var service = scope.ServiceProvider;
        var loggerFactory = service.GetRequiredService<ILoggerFactory>();
        var configuration = service.GetRequiredService<IConfiguration>();

        try
        {
            var context = service.GetRequiredService<EcommerceDbContext>();
            var userManager = service.GetRequiredService<UserManager<User>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            await context.Database.MigrateAsync();
            await EcommerceDbContextData.LoadDataAsync(context, userManager, roleManager, loggerFactory, configuration);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "Error in migration");
        }
    }
}

app.Run();
