namespace Business.DependencyResolvers.AutoMapper;

using Core.Entities.Concrete;
using Entities.Dtos.UserOperationClaims;
using global::AutoMapper;

public class UserOperationClaimProfile : Profile
{
    public UserOperationClaimProfile()
    {
        CreateMap<UserOperationClaim, UserOperationClaimDto>();
        CreateMap<UserOperationClaimDto, UserOperationClaim>();
    }
}
