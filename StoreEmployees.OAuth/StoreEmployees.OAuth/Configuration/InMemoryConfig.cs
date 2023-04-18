using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace StoreEmployees.OAuth.Configuration
{
    public class InMemoryConfig
    {

        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> { new ApiScope("StoreApi", "Store API") };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("StoreApi", "Store API")
                {
                    Scopes = {"StoreApi" }
                }
            };

        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource("roles", "User role(s)", new List<string>{"role"}),
                new IdentityResource("position", "Your position", new List<string> { "position" }),
                new IdentityResource("city", "Your city", new List<string> { "city" })
            };

        public static List<TestUser> GetTestUsers() =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                    Username = "Mick",
                    Password = "Mick@123",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Mick"),
                        new Claim("family_name","Mining"),
                        new Claim("address", "Sunny Street 4"),
                        new Claim("role", "Admin"),
                        new Claim("position", "Administrator"),
                        new Claim("city", "HCM")
                    }
                },
                new TestUser
                {
                    SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                    Username = "Jane",
                    Password = "Jane@123",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Jane"),
                        new Claim("family_name", "Downing"),
                        new Claim("address", "Long Avenue 289"),
                        new Claim("role", "Visitor"),
                        new Claim("position", "Staff"),
                        new Claim("city", "HCM")
                    }
                }
            };


        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "store-employee",
                    ClientSecrets = new [] { new Secret("thiscodesecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "StoreApi" }
               },
               new Client
               {
                   ClientName = "MVC Client",
                   ClientId = "mvc-client",
                   AllowedGrantTypes = GrantTypes.Hybrid,
                   RedirectUris = new List<string>{ "https://localhost:7030/signin-oidc" },
                   RequirePkce = false,
                   AllowedScopes = 
                   {
                       IdentityServerConstants.StandardScopes.OpenId, 
                       IdentityServerConstants.StandardScopes.Profile,
                       IdentityServerConstants.StandardScopes.Address,
                       "roles",
                       "StoreApi",
                       "position",
                       "city"
                   },
                   ClientSecrets = {new Secret("MVCSecret".Sha512())},
                   PostLogoutRedirectUris = new List <string>{ "https://localhost:7030/signout-callback-oidc" },
                   RequireConsent = true //If want to see the consent page for a specific client
               }
            };

    }
}
