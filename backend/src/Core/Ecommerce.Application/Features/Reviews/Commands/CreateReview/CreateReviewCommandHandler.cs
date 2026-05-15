using AutoMapper;
using Ecommerce.Application.Features.Reviews.Queries.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;

namespace Ecommerce.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewVm>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateReviewCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ReviewVm> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var review = _mapper.Map<Review>(request);
            _unitOfWork.Repository<Review>().AddEntity(review);
            var result = await _unitOfWork.Complete();
            if(result <= 0)
            {
                throw new Exception("Failed to create review");
            }
            return _mapper.Map<ReviewVm>(review);
        }
    }
}
