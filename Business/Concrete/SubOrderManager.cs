namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.SubOrders;

public class SubOrderManager : ISubOrderService
{
    private readonly ISubOrderDal _subOrderDal;
    private readonly IMapper _mapper;

    public SubOrderManager(ISubOrderDal subOrderDal, IMapper mapper)
    {
        _subOrderDal = subOrderDal;
        _mapper = mapper;
    }

    public IDataResult<List<UpdateSubOrderDto>> GetAll()
    {
        // Admin can see all suborders
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var list = _subOrderDal.GetList();
            return new SuccessDataResult<List<UpdateSubOrderDto>>(_mapper.Map<List<UpdateSubOrderDto>>(list));
        }

        // Store owners should see suborders for their stores; regular users see none here (they see Orders)
        var roles = Core.Utilities.Security.CurrentUser.GetRoles();
        if (roles.Contains("store.getall") || roles.Contains("store.getbyid"))
        {
            // find stores by current user id then filter suborders by those stores
            var userId = Core.Utilities.Security.CurrentUser.GetUserId();
            var list = _subOrderDal.GetList(so => so.Store != null && so.Store.UserId == userId);
            return new SuccessDataResult<List<UpdateSubOrderDto>>(_mapper.Map<List<UpdateSubOrderDto>>(list));
        }

        return new SuccessDataResult<List<UpdateSubOrderDto>>(new List<UpdateSubOrderDto>());
    }

    public IDataResult<UpdateSubOrderDto> GetById(int id)
    {
        var entity = _subOrderDal.Get(s => s.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateSubOrderDto>(Messages.SubOrderNotFound);
        return new SuccessDataResult<UpdateSubOrderDto>(_mapper.Map<UpdateSubOrderDto>(entity));
    }

    public IResult Add(CreateSubOrderDto createSubOrderDto)
    {
        // SubOrders are created by system/store after order placement; enforce store ownership if needed
        var entity = _mapper.Map<SubOrder>(createSubOrderDto);
        _subOrderDal.Add(entity);
        return new SuccessResult(Messages.SubOrderAdded);
    }

    public IResult Update(UpdateSubOrderDto updateSubOrderDto)
    {
        var existing = _subOrderDal.Get(s => s.Id == updateSubOrderDto.Id);
        if (existing == null) return new ErrorResult(Messages.SubOrderNotFoundForUpdate);
        _mapper.Map(updateSubOrderDto, existing);
        _subOrderDal.Update(existing);
        return new SuccessResult(Messages.SubOrderUpdated);
    }

    public IResult Delete(int id)
    {
        var existing = _subOrderDal.Get(s => s.Id == id);
        if (existing == null) return new ErrorResult(Messages.SubOrderNotFoundForDelete);
        _subOrderDal.Delete(existing);
        return new SuccessResult(Messages.SubOrderDeleted);
    }
}
