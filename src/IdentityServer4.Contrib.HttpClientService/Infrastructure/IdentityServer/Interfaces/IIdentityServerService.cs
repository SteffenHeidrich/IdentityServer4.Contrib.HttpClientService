using System.Threading.Tasks;
using IdentityModel.Client;

namespace IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Interfaces
{
    /// <summary>
    /// Abstraction for an access token service.
    /// </summary>
    public interface IIdentityServerService
    {
        /// <summary>
        /// Retrieves a <see cref="TokenResponse"/>  from the configured Identity Server
        /// </summary>
        /// <param name="options">The token service options</param>
        /// <returns>A <see cref="TokenResponse"/> instance.</returns>
        Task<TokenResponse> GetTokenResponseAsync(IIdentityServerOptions options);

    }
}