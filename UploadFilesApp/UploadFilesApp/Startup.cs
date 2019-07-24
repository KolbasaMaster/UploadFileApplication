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
using NLog;
using Autofac.Integration.WebApi;
using Swashbuckle.Application;
using UploadFilesApp.Infrastructure;
using AppContext = UploadFilesApp.Infrastructure.AppContext;


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
            builder.RegisterType<FileService>().AsSelf();
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            app.UseAutofacMiddleware(container);   
            app.UseWebApi(config);

        }
    }
}