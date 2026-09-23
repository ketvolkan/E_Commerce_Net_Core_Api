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