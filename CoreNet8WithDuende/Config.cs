using DataAccess.Model;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Microsoft.Identity.Client;

namespace CoreNet8WithDuende;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("scope1"),
            new ApiScope("api_web")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                AllowedScopes = { "scope1" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("new-secret".Sha256()) },

                AllowedGrantTypes = new List<string>
                        {
                            GrantType.ResourceOwnerPassword,
                            GrantType.AuthorizationCode
                        },
                RedirectUris = { $"{ApplicationSettings.BaseUrl}/signin-oidc" },
                FrontChannelLogoutUri = $"{ApplicationSettings.BaseUrl}/signout-oidc",
                PostLogoutRedirectUris = { $"{ApplicationSettings.BaseUrl}/signout-callback-oidc" },
                AllowOfflineAccess = true,
                RequireClientSecret = true,
                AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            IdentityServerConstants.StandardScopes.OfflineAccess,
                            "api_web"
                        }
            },
        };
}
