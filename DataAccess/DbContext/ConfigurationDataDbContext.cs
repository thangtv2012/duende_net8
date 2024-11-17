using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

using IdentityModel;
using Duende.IdentityServer;
using IdentityModels = Duende.IdentityServer.Models;
using DataAccess.Model;


namespace DataAccess.DbContext
{

    public class ConfigurationDataDbContext : ConfigurationDbContext<ConfigurationDataDbContext>
    {
        enum clientIdList
        {
            client = 1, ro_client, mvc, js, client_web
        };
        enum scopeList
        {
            web_api = 1
        };
        enum secretList
        {
            secret
        };
        enum IdentityResourceIdList
        {
            openid = 1, profile
        }
        public ConfigurationDataDbContext(DbContextOptions<ConfigurationDataDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ClientSeed(modelBuilder);
        }

        private void ClientSeed(ModelBuilder builder)
        {

            builder.Entity<ApiResource>()
                .HasData(
                    new ApiResource
                    {
                        Id = (int)scopeList.web_api,
                        Name = scopeList.web_api.ToString(),
                        DisplayName = "My Web API"
                    }
                );

            builder.Entity<ApiScope>()
                .HasData(
                    new ApiScope
                    {
                        Id = 1,
                        Name = scopeList.web_api.ToString(),
                        DisplayName = scopeList.web_api.ToString(),
                        Description = null,
                        Required = false,
                        Emphasize = false,
                        ShowInDiscoveryDocument = true,
                        //ApiResourceId = 1
                    }
                );

            builder.Entity<IdentityResource>().HasData
                (
                    new IdentityResource()
                    {
                        Id = (int)IdentityResourceIdList.openid,
                        Enabled = true,
                        Name = IdentityServerConstants.StandardScopes.OpenId,
                        DisplayName = "Your user identifier",
                        Description = null,
                        Required = true,
                        Emphasize = false,
                        ShowInDiscoveryDocument = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        NonEditable = false
                    },
                    new IdentityResource()
                    {
                        Id = (int)IdentityResourceIdList.profile,
                        Enabled = true,
                        Name = IdentityServerConstants.StandardScopes.Profile,
                        DisplayName = "User profile",
                        Description = "Your user profile information (first name, last name, etc.)",
                        Required = false,
                        Emphasize = true,
                        ShowInDiscoveryDocument = true,
                        Created = DateTime.UtcNow,
                        Updated = null,
                        NonEditable = false
                    });

            builder.Entity<IdentityResourceClaim>()
                .HasData(
                    new IdentityResourceClaim
                    {
                        Id = 1,
                        IdentityResourceId = (int)IdentityResourceIdList.openid,
                        Type = "sub"
                    },
                    new IdentityResourceClaim
                    {
                        Id = 2,
                        IdentityResourceId = (int)IdentityResourceIdList.profile,
                        Type = "email"
                    },
                    new IdentityResourceClaim
                    {
                        Id = 3,
                        IdentityResourceId = (int)IdentityResourceIdList.profile,
                        Type = "website"
                    },
                    new IdentityResourceClaim
                    {
                        Id = 4,
                        IdentityResourceId = (int)IdentityResourceIdList.profile,
                        Type = "given_name"
                    },
                    new IdentityResourceClaim
                    {
                        Id = 5,
                        IdentityResourceId = (int)IdentityResourceIdList.profile,
                        Type = "family_name"
                    },
                    new IdentityResourceClaim
                    {
                        Id = 6,
                        IdentityResourceId = (int)IdentityResourceIdList.profile,
                        Type = "name"
                    });

            builder.Entity<Client>()
                .HasData(
                    new Client
                    {
                        Id = (int)clientIdList.client,
                        Enabled = true,
                        ClientId = "client",
                        ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect,
                        RequireClientSecret = true,
                        RequireConsent = true,
                        ClientName = null,
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = false
                    },
                    new Client
                    {
                        Id = (int)clientIdList.ro_client,
                        Enabled = true,
                        ClientId = "ro.client",
                        ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect,
                        RequireClientSecret = true,
                        RequireConsent = true,
                        ClientName = null,
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = false
                    },
                    new Client
                    {
                        Id = (int)clientIdList.mvc,
                        Enabled = true,
                        ClientId = "mvc",
                        ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect,
                        RequireClientSecret = true,
                        RequireConsent = true,
                        ClientName = "MVC Client",
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = true
                    },
                    new Client
                    {
                        Id = (int)clientIdList.js,
                        Enabled = true,
                        ClientId = "js",
                        ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect,
                        RequireClientSecret = false,
                        RequireConsent = true,
                        ClientName = "JavaScript client",
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = true,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = false
                    },
                    new Client
                    {
                        Id = (int)clientIdList.client_web,
                        Enabled = true,
                        ClientId = "client_web",
                        ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect,
                        RequireClientSecret = true,
                        RequireConsent = true,
                        ClientName = null,
                        Description = null,
                        AllowRememberConsent = true,
                        AlwaysIncludeUserClaimsInIdToken = false,
                        RequirePkce = false,
                        AllowAccessTokensViaBrowser = false,
                        AllowOfflineAccess = true
                    }
                    );

            builder.Entity<ClientGrantType>()
                .HasData(
                    new ClientGrantType
                    {
                        Id = 1,
                        GrantType = IdentityModels.GrantType.ClientCredentials,
                        ClientId = (int)clientIdList.client
                    },
                    new ClientGrantType
                    {
                        Id = 2,
                        GrantType = IdentityModels.GrantType.ResourceOwnerPassword,
                        ClientId = (int)clientIdList.ro_client
                    },
                    new ClientGrantType
                    {
                        Id = 3,
                        GrantType = IdentityModels.GrantType.Hybrid,
                        ClientId = (int)clientIdList.mvc
                    },
                    new ClientGrantType
                    {
                        Id = 4,
                        GrantType = IdentityModels.GrantType.AuthorizationCode,
                        ClientId = (int)clientIdList.js
                    },
                    new ClientGrantType
                    {
                        Id = 5,
                        GrantType = IdentityModels.GrantType.ResourceOwnerPassword,
                        ClientId = (int)clientIdList.client_web
                    },
                    new ClientGrantType
                    {
                        Id = 6,
                        GrantType = IdentityModels.GrantType.AuthorizationCode,
                        ClientId = (int)clientIdList.client_web
                    }
                    );

            builder.Entity<ClientScope>()
                .HasData(
                    new ClientScope
                    {
                        Id = 1,
                        Scope = IdentityServerConstants.StandardScopes.Profile,
                        ClientId = (int)clientIdList.mvc
                    },
                    new ClientScope
                    {
                        Id = 2,
                        Scope = IdentityServerConstants.StandardScopes.Profile,
                        ClientId = (int)clientIdList.js
                    },
                    new ClientScope
                    {
                        Id = 3,
                        Scope = IdentityServerConstants.StandardScopes.OpenId,
                        ClientId = (int)clientIdList.mvc
                    },
                    new ClientScope
                    {
                        Id = 4,
                        Scope = IdentityServerConstants.StandardScopes.OpenId,
                        ClientId = (int)clientIdList.js
                    },
                    new ClientScope
                    {
                        Id = 5,
                        Scope = scopeList.web_api.ToString(),
                        ClientId = (int)clientIdList.client
                    }
                    ,
                    new ClientScope
                    {
                        Id = 6,
                        Scope = scopeList.web_api.ToString(),
                        ClientId = (int)clientIdList.ro_client
                    }
                    ,
                    new ClientScope
                    {
                        Id = 7,
                        Scope = scopeList.web_api.ToString(),
                        ClientId = (int)clientIdList.mvc
                    }
                    ,
                    new ClientScope
                    {
                        Id = 8,
                        Scope = scopeList.web_api.ToString(),
                        ClientId = (int)clientIdList.js
                    },
                    new ClientScope
                    {
                        Id = 9,
                        Scope = scopeList.web_api.ToString(),
                        ClientId = (int)clientIdList.client_web
                    },
                    new ClientScope
                    {
                        Id = 10,
                        Scope = IdentityServerConstants.StandardScopes.OpenId,
                        ClientId = (int)clientIdList.client_web
                    },
                    new ClientScope
                    {
                        Id = 11,
                        Scope = IdentityServerConstants.StandardScopes.Profile,
                        ClientId = (int)clientIdList.client_web
                    },
                    new ClientScope
                    {
                        Id = 12,
                        Scope = IdentityServerConstants.StandardScopes.OfflineAccess,
                        ClientId = (int)clientIdList.client_web
                    }
                    );

            builder.Entity<ClientSecret>()
                .HasData(
                        new ClientSecret
                        {
                            Id = 1,
                            Value = secretList.secret.ToString().ToSha256(),
                            Type = IdentityServerConstants.SecretTypes.SharedSecret,
                            ClientId = (int)clientIdList.client
                        },
                        new ClientSecret
                        {
                            Id = 2,
                            Value = secretList.secret.ToString().ToSha256(),
                            Type = IdentityServerConstants.SecretTypes.SharedSecret,
                            ClientId = (int)clientIdList.ro_client
                        },
                        new ClientSecret
                        {
                            Id = 3,
                            Value = secretList.secret.ToString().ToSha256(),
                            Type = IdentityServerConstants.SecretTypes.SharedSecret,
                            ClientId = (int)clientIdList.mvc
                        },
                        new ClientSecret
                        {
                            Id = 4,
                            Value = secretList.secret.ToString().ToSha256(),
                            Type = IdentityServerConstants.SecretTypes.SharedSecret,
                            ClientId = (int)clientIdList.js
                        },
                        new ClientSecret
                        {
                            Id = 5,
                            Value = secretList.secret.ToString().ToSha256(),
                            Type = IdentityServerConstants.SecretTypes.SharedSecret,
                            ClientId = (int)clientIdList.client_web
                        });

            builder.Entity<ClientPostLogoutRedirectUri>()
                .HasData(
                new ClientPostLogoutRedirectUri
                {
                    Id = 1,
                    PostLogoutRedirectUri = $"{ApplicationSettings.BaseUrl}/signout-callback-oidc",
                    ClientId = (int)clientIdList.mvc
                },
                new ClientPostLogoutRedirectUri
                {
                    Id = 2,
                    PostLogoutRedirectUri = $"{ApplicationSettings.BaseUrl}/index.html",
                    ClientId = (int)clientIdList.js
                },
                new ClientPostLogoutRedirectUri
                {
                    Id = 3,
                    PostLogoutRedirectUri = $"{ApplicationSettings.BaseUrl}/signout-callback-oidc",
                    ClientId = (int)clientIdList.client_web
                }
                );

            builder.Entity<ClientRedirectUri>()
                .HasData(
                new ClientRedirectUri
                {
                    Id = 1,
                    RedirectUri = $"{ApplicationSettings.BaseUrl}/signin-oidc",
                    ClientId = (int)clientIdList.mvc
                },
                new ClientRedirectUri
                {
                    Id = 2,
                    RedirectUri = $"{ApplicationSettings.BaseUrl}/callback.html",
                    ClientId = (int)clientIdList.js
                },
                new ClientRedirectUri
                {
                    Id = 3,
                    RedirectUri = $"{ApplicationSettings.BaseUrl}/signin-oidc",
                    ClientId = (int)clientIdList.client_web
                },
                new ClientRedirectUri
                {
                    Id = 4,
                    RedirectUri = $"{ApplicationSettings.BaseUrl}/callback.html",
                    ClientId = (int)clientIdList.client_web
                });

            builder.Entity<ClientCorsOrigin>()
                .HasData(
                new ClientCorsOrigin
                {
                    Id = 1,
                    Origin = $"{ApplicationSettings.BaseUrl}",
                    ClientId = (int)clientIdList.js
                },
                new ClientCorsOrigin
                {
                    Id = 2,
                    Origin = $"{ApplicationSettings.BaseUrl}",
                    ClientId = (int)clientIdList.client_web
                });
        }
    }
}
