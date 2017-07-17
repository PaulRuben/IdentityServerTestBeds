//[assembly: OwinStartup(typeof(Api3_DotNetFrameworkWebApi.Startup))]
namespace Api3_DotNetFrameworkWebApi
{
    using IdentityServer3.AccessTokenValidation;
    using Microsoft.Owin.Cors;
    using Owin;
    using System.Web.Http;
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

                // For access to the introspection endpoint
                ClientId = "api2",
                ClientSecret = "api-secret2",

                RequiredScopes = new[] { "api2" }
            });

            // web api configuration
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new AuthorizeAttribute()); // Causes all controlls to require authorization

            app.UseWebApi(config);
        }
    }
}