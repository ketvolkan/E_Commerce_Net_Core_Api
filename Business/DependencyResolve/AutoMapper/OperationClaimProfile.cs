namespace Business.DependencyResolvers.AutoMapper;

using Core.Entities.Concrete;
using Entities.Dtos.Claims;
using global::AutoMapper;

public class OperationClaimProfile : Profile
{
    public OperationClaimProfile()
    {
        CreateMap<OperationClaim, OperationClaimDto>();
        CreateMap<OperationClaimDto, OperationClaim>();
    }
}
