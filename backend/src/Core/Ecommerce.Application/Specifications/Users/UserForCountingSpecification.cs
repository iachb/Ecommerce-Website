using Ecommerce.Domain;

namespace Ecommerce.Application.Specifications.Users
{
    public class UserForCountingSpecification : BaseSpecification<User>
    {
        public UserForCountingSpecification(UserSpecificationParams userParams) : base(
            x => (string.IsNullOrEmpty(userParams.Search) || x.UserName!.ToLower().Contains(userParams.Search)
                || x.Surname!.ToLower().Contains(userParams.Search) || x.Email!.ToLower().Contains(userParams.Search)
            )
         )
        {
        }
    }
}
