using System.Net.Http;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.Core.Interfaces;

namespace IdentityServer4.Contrib.HttpClientService.Infrastructure.Core
{
    /// <summary>
    /// A <see cref="HttpRequestMessage"/> factory.
    /// </summary>
    public class HttpRequestMessageFactoryDesktop : IHttpRequestMessageFactory
    {

        /// <summary>
        /// Constructor of the  <see cref="HttpRequestMessageFactory"/>
        /// </summary>
        public HttpRequestMessageFactoryDesktop()
        {
        }

        /// <summary>
        /// Creates and returns a new <see cref="HttpRequestMessage"/>
        /// </summary>
        /// <returns>An <see cref="HttpRequestMessage"/> to be used by an <see cref="HttpClient"/>.</returns>
        public HttpRequestMessage CreateRequestMessage()
        {
            return new HttpRequestMessage();           
        }
    }
}