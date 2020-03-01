using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using IdentityServer4.Contrib.HttpClientService.Models;
using Microsoft.Extensions.Options;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers;
using System.Net;
using System;

namespace IdentityServer4.Contrib.HttpClientService.Infrastructure.Test
{
    [TestClass]
    public class TokenResponseServiceTests
    {

        [TestMethod]
        public async Task AccessTokenService_GetAccessToken()
        {
            var cachedTokenResponse = await TokenResponseMock.GetValidResponseAsync("cached_access_token", 0);
            var request = new TokenResponseService(
                IHttpClientFactoryMocks.Get(HttpStatusCode.OK,
                @"{
                    ""access_token"": ""live_access_token"",
                    ""expires_in"": 10,
                    ""token_type"": ""Bearer"",
                    ""custom"":  ""liveObject""
                }"),
                IAccessTokenCacheManagerMocks.Get(cachedTokenResponse)
            );

            var tokenServiceOptions = Options.Create(new DefaultClientCredentialOptions
            {
                Address = "http://localhost/" + Guid.NewGuid()
            });

            var accessToken = await request.GetTokenResponseAsync(tokenServiceOptions);
            Assert.AreEqual("cached_access_token", accessToken.AccessToken);
        }

    }
}
