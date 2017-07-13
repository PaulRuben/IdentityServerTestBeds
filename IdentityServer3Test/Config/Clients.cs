namespace IdentityServer3Test.Config
{
    using IdentityServer3.Core.Models;
    using System.Collections.Generic;

    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "JS Client",
                    ClientId = "js",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "http://localhost:56668/popup.html",
                        "http://localhost:56668/silent-renew.html"
                    },

                    PostLogoutRedirectUris = new List<string>
                    {
                        "http://localhost:56668/index.html"
                    },

                    AllowedCorsOrigins = new List<string>
                    {
                        "http://localhost:56668"
                    },

                    AllowAccessToAllScopes = true,
                    AccessTokenLifetime = 70
                },
                new Client
                {
                    ClientId = "js_oidc_2",
                    ClientName = "JavaScript Client",
                    Flow = Flows.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:2235/callback.html", "http://localhost:2235/silent.html"},
                    PostLogoutRedirectUris = { "http://localhost:2235/index.html" },
                    AllowedCorsOrigins = { "http://localhost:2235" },

                    //AllowedScopes =
                    //{
                    //    IdentityServer3.Core.Constants.StandardScopes.OpenId,
                    //    IdentityServer3.Core.Constants.StandardScopes.Profile,
                    //    "api"
                    //},
                    
                    AllowAccessToAllScopes = true,

                    RequireConsent = false,
                    AccessTokenLifetime = 70,
                },
                new Client
                {
                    ClientId = "mvc_ng",
                    ClientName = "MVC Angular Client",
                    Flow = Flows.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "http://localhost:1615/callback.html", "http://localhost:1615/silent.html"},
                    PostLogoutRedirectUris = { "http://localhost:1615" },
                    AllowedCorsOrigins = { "http://localhost:1615" },

                    //AllowedScopes =
                    //{
                    //    IdentityServer3.Core.Constants.StandardScopes.OpenId,
                    //    IdentityServer3.Core.Constants.StandardScopes.Profile,
                    //    "api"
                    //},
                    
                    AllowAccessToAllScopes = true,

                    RequireConsent = false,
                    AccessTokenLifetime = 70,
                }
                

            };
        }
    }
}