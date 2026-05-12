using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Users
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification(UserSpecificationParams userParams) : base(
            x => (string.IsNullOrEmpty(userParams.Search) || x.UserName!.ToLower().Contains(userParams.Search)
                || x.Surname!.ToLower().Contains(userParams.Search) || x.Email!.ToLower().Contains(userParams.Search)
            )
         )
        {
            ApplyPaging(userParams.PageSize * (userParams.PageIndex - 1), userParams.PageSize);

            if (!string.IsNullOrEmpty(userParams.Sort))
            {
                switch (userParams.Sort)
                {
                    case "nameAsc":
                        AddOrderBy(x => x.Name!);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(x => x.Name!);
                        break;
                    case "surnameAsc":
                        AddOrderBy(x => x.Surname!);
                        break;
                    case "surnameDesc":
                        AddOrderByDescending(x => x.Surname!);
                        break;
                    default:
                        AddOrderBy(x => x.Name!);
                        break;
                }
            }
            else
            {
                AddOrderBy(x => x.Name!);
            }
        }
    }
}
