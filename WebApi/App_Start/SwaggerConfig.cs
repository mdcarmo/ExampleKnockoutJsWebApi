using System.Web.Http;
using WebActivatorEx;
using WebApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace WebApi
{
    public class SwaggerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.IncludeXmlComments(GetXmlCommentsPath());
                    c.SingleApiVersion("V1", "Contacts Services")
                        .Description("Services available for integration with the Contacts")
                        .TermsOfService("None")
                        .Contact(cc => cc.Name("Marcelo")
                            .Url("http://marcdias.com.br")
                            .Email("marc29dias@yahoo.com.br"));
                })
                .EnableSwaggerUi(c =>
                {
                    c.DisableValidator();
                    c.InjectStylesheet(typeof(SwaggerConfig).Assembly,
                        "WebApi.SwaggerExtensions.theme-material.css");
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\WebApi.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
