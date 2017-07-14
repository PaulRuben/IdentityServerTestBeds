using Owin;
using System.Web.Http;
using IdentityServer3.AccessTokenValidation;
using Microsoft.Owin;
using Microsoft.Owin.Cors;

//[assembly: OwinStartup(typeof(Api3_DotNetFrameworkWebApi.Startup))]
namespace Api3_DotNetFrameworkWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Allow all origins
            app.UseCors(CorsOptions.AllowAll);

            // token validation
            app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            {
                Authority = "https://localhost:44300",
                
                ClientId = "api",
                ClientSecret = "api-secret",

                RequiredScopes = new[] { "api" }
            });

            // web api configuration
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new AuthorizeAttribute()); // Causes all controlls to require authorization

            app.UseWebApi(config);
        }
    }
}