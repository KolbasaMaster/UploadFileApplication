using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using System.Net.Http.Formatting;
using System.Reflection;
using Autofac;

using Autofac.Integration.WebApi;
using AutoMapper;
using DAL.Infrastructure;
using DAL.Read;
using Swashbuckle.Application;
using UploadFilesApp.Api.Mapper;
using UploadFilesApp.DAL.Mapper;
using UploadFilesApp.Infrastructure;

using AppContext = UploadFilesApp.Infrastructure.AppContext;

[assembly:OwinStartup(typeof(UploadFilesApp.Startup))]
namespace UploadFilesApp
{
    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            config.EnableSwagger(c => { c.SingleApiVersion("v1", "WebAPI"); }).EnableSwaggerUi();
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<LocalFileStorage>().As<IFileStorage>();
            builder.RegisterType<AppContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<MapperDAL>().AsSelf();
            builder.RegisterType<Read>().As<IRead>();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
//            app.UseAutofacMiddleware(container);   // попробоавть убрать 
            app.UseWebApi(config);

        }
    }
}