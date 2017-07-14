using System.Collections.Generic;
using System.IdentityModel.Tokens;
using Microsoft.Owin;
using MvcNgClientWithWebApi;
using Owin;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

[assembly: OwinStartup(typeof(Startup))]
namespace MvcNgClientWithWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {

                Authority = "https://localhost:44300/identity",
                ClientId = "mvc_ng_api2",

                //In the scopes, we define 6 Scopes.
                //In the Scope we ask what to include
                Scope = "openid profile api",
                RedirectUri = "http://localhost:14482/",
                ResponseType = "id_token token",
                SignInAsAuthenticationType = "Cookies",

                UseTokenLifetime = false,

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    SecurityTokenValidated = n =>
                    {
                        var id = n.AuthenticationTicket.Identity;

                        var sub = id.FindFirst(IdentityServer3.Core.Constants.ClaimTypes.Subject);
                        var roles = id.FindAll(IdentityServer3.Core.Constants.ClaimTypes.Role);

                        // create new identity and set name and role claim type
                        var nid = new ClaimsIdentity(
                            id.AuthenticationType,
                            IdentityServer3.Core.Constants.ClaimTypes.Name
                            , IdentityServer3.Core.Constants.ClaimTypes.Role
                        );

                        nid.AddClaim(sub);
                        nid.AddClaims(roles);
                        // keep the id_token for logout
                        nid.AddClaim(new Claim("id_token", n.ProtocolMessage.IdToken));
                        n.AuthenticationTicket = new AuthenticationTicket(
                            nid,
                            n.AuthenticationTicket.Properties);

                        return Task.FromResult(0);
                    }
                }
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = IdentityServer3.Core.Constants.ClaimTypes.Subject;
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            //app.CreatePerOwinContext(ApplicationDbContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
        }
    }
}
