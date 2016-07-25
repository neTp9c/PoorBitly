using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Bitly.Business;
using Bitly.Entities;
using Bitly.Web.Services;
using Bitly.Web.ViewModels;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace Bitly.Web.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            BuildMvc(builder);
            BuildWebApi(builder);
            BuildCustom(builder);

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void BuildMvc(ContainerBuilder builder)
        {
            // Register your MVC controllers
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();
        }

        private static void BuildWebApi(ContainerBuilder builder)
        {
            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);
        }

        private static void BuildCustom(ContainerBuilder builder)
        {
            Business.AutofacConfig.Build(builder);
            builder.RegisterType<ShortenPathConverter>().As<IShortenPathConverter>();
            builder.RegisterType<OriginalUrlStandardizer>().As<IOriginalUrlStandardizer>();
            builder.RegisterType<LinkManager>().As<ILinkManager>();
            builder.RegisterType<LinkToLinkViewModelConverter>().As<IConverter<Link, LinkViewModel>>();
        }
    }
}