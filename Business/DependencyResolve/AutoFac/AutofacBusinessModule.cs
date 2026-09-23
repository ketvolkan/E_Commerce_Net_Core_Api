namespace Business.DependencyResolvers.Autofac;

using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.AutoMapper;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using global::Autofac;
using global::Autofac.Extras.DynamicProxy;
using global::AutoMapper;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductManager>().As<IProductService>().InstancePerLifetimeScope();
        builder.RegisterType<EfProductDal>().As<IProductDal>().InstancePerLifetimeScope();

        builder.RegisterType<UserManager>().As<IUserService>().InstancePerLifetimeScope();
        builder.RegisterType<EfUserDal>().As<IUserDal>().InstancePerLifetimeScope();

        builder.RegisterType<AuthManager>().As<IAuthService>().InstancePerLifetimeScope();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>().InstancePerLifetimeScope();

        // Registrations added for generated services and DALs
        builder.RegisterType<CategoryManager>().As<ICategoryService>().InstancePerLifetimeScope();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().InstancePerLifetimeScope();

        builder.RegisterType<BrandManager>().As<IBrandService>().InstancePerLifetimeScope();
        builder.RegisterType<EfBrandDal>().As<IBrandDal>().InstancePerLifetimeScope();

        builder.RegisterType<StoreManager>().As<IStoreService>().InstancePerLifetimeScope();
        builder.RegisterType<EfStoreDal>().As<IStoreDal>().InstancePerLifetimeScope();

        builder.RegisterType<AddressManager>().As<IAddressService>().InstancePerLifetimeScope();
        builder.RegisterType<EfAddressDal>().As<IAddressDal>().InstancePerLifetimeScope();

        builder.RegisterType<FavoriteManager>().As<IFavoriteService>().InstancePerLifetimeScope();
        builder.RegisterType<EfFavoriteDal>().As<IFavoriteDal>().InstancePerLifetimeScope();

        builder.RegisterType<CartManager>().As<ICartService>().InstancePerLifetimeScope();
        builder.RegisterType<EfCartDal>().As<ICartDal>().InstancePerLifetimeScope();

        builder.RegisterType<CartItemManager>().As<ICartItemService>().InstancePerLifetimeScope();
        builder.RegisterType<EfCartItemDal>().As<ICartItemDal>().InstancePerLifetimeScope();

        builder.RegisterType<OrderManager>().As<IOrderService>().InstancePerLifetimeScope();
        builder.RegisterType<EfOrderDal>().As<IOrderDal>().InstancePerLifetimeScope();

        builder.RegisterType<OrderItemManager>().As<IOrderItemService>().InstancePerLifetimeScope();
        builder.RegisterType<EfOrderItemDal>().As<IOrderItemDal>().InstancePerLifetimeScope();

        builder.RegisterType<SubOrderManager>().As<ISubOrderService>().InstancePerLifetimeScope();
        builder.RegisterType<EfSubOrderDal>().As<ISubOrderDal>().InstancePerLifetimeScope();

        builder.RegisterType<ProductImageManager>().As<IProductImageService>().InstancePerLifetimeScope();
        builder.RegisterType<EfProductImageDal>().As<IProductImageDal>().InstancePerLifetimeScope();

        builder.RegisterType<ProductVariantManager>().As<IProductVariantService>().InstancePerLifetimeScope();
        builder.RegisterType<EfProductVariantDal>().As<IProductVariantDal>().InstancePerLifetimeScope();

        builder.RegisterType<ProductReviewManager>().As<IProductReviewService>().InstancePerLifetimeScope();
        builder.RegisterType<EfProductReviewDal>().As<IProductReviewDal>().InstancePerLifetimeScope();

        builder.RegisterType<ProductQuestionManager>().As<IProductQuestionService>().InstancePerLifetimeScope();
        builder.RegisterType<EfProductQuestionDal>().As<IProductQuestionDal>().InstancePerLifetimeScope();

        builder.RegisterType<ClaimManager>().As<IClaimService>().InstancePerLifetimeScope();
        builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().InstancePerLifetimeScope();

        builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().InstancePerLifetimeScope();
        builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().InstancePerLifetimeScope();
      
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();
        builder.RegisterAssemblyTypes(assembly)
            .AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            })
            .SingleInstance();
    }
}