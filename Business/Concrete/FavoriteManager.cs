namespace Business.Concrete;

using AutoMapper;
using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Favorites;
using Business.BusinessAspects.Autofac;

using System.Linq;

public class FavoriteManager : IFavoriteService
{
    private readonly IFavoriteDal _favoriteDal;
    private readonly IProductDal _productDal;
    private readonly IMapper _mapper;

    public FavoriteManager(IFavoriteDal favoriteDal, IProductDal productDal, IMapper mapper)
    {
        _favoriteDal = favoriteDal;
        _productDal = productDal;
        _mapper = mapper;
    }

    public IDataResult<List<FavoriteProductDto>> GetMyFavorites()
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorDataResult<List<FavoriteProductDto>>("Kullanıcı oturumu bulunamadı.");

        var favorites = _favoriteDal.GetList(f => f.UserId == userId);
        if (!favorites.Any()) return new SuccessDataResult<List<FavoriteProductDto>>(new List<FavoriteProductDto>());

        var productIds = favorites.Select(f => f.ProductId).ToList();
        var products = _productDal.GetListWithDetails(p => productIds.Contains(p.Id));

        var dtos = favorites.Select(f =>
        {
            var p = products.FirstOrDefault(prod => prod.Id == f.ProductId);
            var minPrice = p?.ProductVariants?.Any() == true ? p.ProductVariants.Min(v => v.Price) : 0;
            var minDiscount = p?.ProductVariants?.Where(v => v.DiscountPrice > 0).Select(v => (decimal?)v.DiscountPrice).Min();

            return new FavoriteProductDto
            {
                FavoriteId = f.Id,
                ProductId = f.ProductId,
                ProductName = p?.Name ?? "Ürün",
                CategoryName = p?.Category?.Name ?? string.Empty,
                BrandName = p?.Brand?.Name ?? string.Empty,
                ImageUrl = p?.ProductImages?.OrderBy(pi => pi.DisplayOrder).FirstOrDefault()?.ImageUrl ?? string.Empty,
                MinPrice = minPrice,
                DiscountPrice = minDiscount
            };
        }).ToList();

        return new SuccessDataResult<List<FavoriteProductDto>>(dtos);
    }

    public IResult ToggleFavorite(int productId)
    {
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (userId <= 0) return new ErrorResult("Kullanıcı oturumu bulunamadı.");

        var existing = _favoriteDal.Get(f => f.UserId == userId && f.ProductId == productId);
        if (existing != null)
        {
            _favoriteDal.Delete(existing);
            return new SuccessResult("Ürün favorilerden kaldırıldı.");
        }

        var newFav = new Favorite
        {
            UserId = userId,
            ProductId = productId
        };
        _favoriteDal.Add(newFav);
        return new SuccessResult("Ürün favorilere eklendi.");
    }

    [SecuredOperation("favorite.get")]
    public IDataResult<List<UpdateFavoriteDto>> GetAll()
    {
        if (Core.Utilities.Security.CurrentUser.IsAdmin())
        {
            var allFavorites = _favoriteDal.GetList();
            return new SuccessDataResult<List<UpdateFavoriteDto>>(_mapper.Map<List<UpdateFavoriteDto>>(allFavorites));
        }

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var userFavorites = _favoriteDal.GetList(f => f.UserId == userId);
        return new SuccessDataResult<List<UpdateFavoriteDto>>(_mapper.Map<List<UpdateFavoriteDto>>(userFavorites));
    }

    public IDataResult<UpdateFavoriteDto> GetById(int id)
    {
        var entity = _favoriteDal.Get(f => f.Id == id);
        if (entity == null) return new ErrorDataResult<UpdateFavoriteDto>(Messages.FavoriteNotFound);
        return new SuccessDataResult<UpdateFavoriteDto>(_mapper.Map<UpdateFavoriteDto>(entity));
    }

    [SecuredOperation("favorite.add")]
    public IResult Add(CreateFavoriteDto createFavoriteDto)
    {
        // Assign ownership from authenticated user, ignore DTO.UserId
        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        var entity = _mapper.Map<Favorite>(createFavoriteDto);
        entity.UserId = userId;
        _favoriteDal.Add(entity);
        return new SuccessResult(Messages.FavoriteAdded);
    }

    [SecuredOperation("favorite.update")]
    public IResult Update(UpdateFavoriteDto updateFavoriteDto)
    {
        var existing = _favoriteDal.Get(f => f.Id == updateFavoriteDto.Id);
        if (existing == null) return new ErrorResult(Messages.FavoriteNotFoundForUpdate);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _mapper.Map(updateFavoriteDto, existing);
        _favoriteDal.Update(existing);
        return new SuccessResult(Messages.FavoriteUpdated);
    }

    [SecuredOperation("favorite.delete")]
    public IResult Delete(int id)
    {
        var existing = _favoriteDal.Get(f => f.Id == id);
        if (existing == null) return new ErrorResult(Messages.FavoriteNotFoundForDelete);

        var userId = Core.Utilities.Security.CurrentUser.GetUserId();
        if (!Core.Utilities.Security.CurrentUser.IsAdmin() && existing.UserId != userId)
            return new ErrorResult(Messages.AuthorizationDenied);

        _favoriteDal.Delete(existing);
        return new SuccessResult(Messages.FavoriteDeleted);
    }
}
