using System;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Interfaces;
using Moq;

namespace IdentityServer4.Contrib.HttpClientService.Tests.Helpers.CacheMocks
{
    public static class ITokenResponseCacheManagerMocks
    {
        public static ITokenResponseCacheManager Get(TokenResponse expectedValue)
        {
            var mockAccessTokenCacheManager = new Mock<ITokenResponseCacheManager>();
            mockAccessTokenCacheManager
                .Setup(x => x.AddOrGetExistingAsync(It.IsAny<string>(), It.IsAny<Func<Task<TokenResponse>>>()))
                .Returns(Task.FromResult(expectedValue));
           
            return mockAccessTokenCacheManager.Object;
        }        
    }
}
