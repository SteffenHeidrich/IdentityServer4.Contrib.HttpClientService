using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.HttpClients;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Interfaces;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Models;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.CommonValues;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientMocks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Contrib.HttpClientService.Tests.Ifrastracture
{
    [TestClass]
    public class IdentityServerServiceTests
    {
        [TestMethod]
        public async Task IdentityServerService_GetTokenResponse_ShouldReturnAccessToken()
        {
            var identityServerService = new IdentityServerService(
                    new IdentityServerHttpClientSelector(
                        new List<IIdentityServerHttpClient> {
                            {
                                new ClientCredentialsHttpClient(
                                    IHttpClientFactoryMocks.Get(
                                        HttpStatusCode.OK,
                                        TokenResponseObjects.GetValidTokenResponseString("live_access_token", 10)
                                    ).CreateClient("test")
                                )
                            }
                        }
                    ),
                    new TokenResponseCacheManager(
                        new MemoryCache(
                            Options.Create(new MemoryCacheOptions())
                        )
                    )
            );

            var tokenServiceOptions = new ClientCredentialsOptions
            {
                Address = "http://localhost/" + Guid.NewGuid(),
                ClientId = "ClientId",
                ClientSecret = "secret",
                Scope = "scope"
            };

            var accessToken = await identityServerService.GetTokenResponseAsync(tokenServiceOptions);
            Assert.AreEqual("live_access_token", accessToken.AccessToken);
        }

    }
}
