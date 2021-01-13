using IdentityModel;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xw.Zx.Core.Config
{
    public class IdentityServer4Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("AppApi", "Hbzs AppApi", new List<string>(){JwtClaimTypes.Role})
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "App.Manager.Ro",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("DEsjpJFtokIOhMKuE6BVMczYUEEyPGTOLrur3PXw26VMLNwKOfAKFZZgR2vVJDKG".Sha256())
                    },
                    AllowedScopes = { "AppApi" },
                    AllowedCorsOrigins={
                        "http://localhost:8083/",
                        "http://localhost:8080/",
                         "http://localhost:8081",
                         "http://localhost:8082",
                    },
                    AccessTokenLifetime = 3600*24*100,//3600 seconds / 1 hour
                }
            };
        }
    }
}
