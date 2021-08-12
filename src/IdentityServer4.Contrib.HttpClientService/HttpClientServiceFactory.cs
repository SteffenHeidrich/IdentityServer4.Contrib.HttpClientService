using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Collections.Generic;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.Core;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.Core.Interfaces;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.HttpClients;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace IdentityServer4.Contrib.HttpClientService
{
    /// <summary>
    /// A factory that creates <see cref="HttpClientService"/> instances for a given logical name.
    /// </summary>
    public sealed class HttpClientServiceFactory : IHttpClientServiceFactory
    {
        private readonly ICoreHttpClient _coreHttpClient;
        private readonly IIdentityServerService _tokenResponseService;
        private readonly IHttpRequestMessageFactory _requestMessageFactory;
        private readonly IConfiguration _configuration;

        private static readonly Lazy<HttpClientServiceFactory> lazyInstance
            = new Lazy<HttpClientServiceFactory>(() => new HttpClientServiceFactory());
        private static readonly object syncLock = new object();

        /// <summary>
        /// Lazy Singleton instantiation for use outside of a DI container.
        /// </summary>
        public static HttpClientServiceFactory Instance
        {
            get
            {
                return lazyInstance.Value;
            }
        }

        private HttpClientServiceFactory()
        {
            //no configuration for Dektop
            _configuration = null;
            var identityServerHttpClient = new HttpClient();

            _coreHttpClient = new CoreHttpClient(
                                new HttpClient()
                              );

            _tokenResponseService = new IdentityServerService(
                                        new IdentityServerHttpClientSelector(
                                            new List<IIdentityServerHttpClient>
                                            {
                                                { new ClientCredentialsHttpClient(identityServerHttpClient) },
                                                { new PasswordHttpClient(identityServerHttpClient) }
                                            }
                                        ),
                                        new TokenResponseCacheManager(
                                            new MemoryCache(
                                                Options.Create(
                                                    new MemoryCacheOptions()
                                                )
                                            )
                                        )
                                    );

            _requestMessageFactory = new HttpRequestMessageFactoryDesktop();
        }

        /// <summary>
        /// Constructor of the <see cref="HttpClientServiceFactory" />.
        /// </summary>
        /// <param name="configuration">Application configuration properties.</param>
        /// <param name="coreHttpClient">An <see cref="ICoreHttpClient"/> implementation that will execute the HTTP requests.</param>
        /// <param name="requestMessageFactory">The <see cref="IHttpRequestMessageFactory"/> to get a new <see cref="HttpRequestMessage"/>.</param>
        /// <param name="tokenResponseService">The <see cref="IIdentityServerService"/> to retrieve a token, if required.</param>
        public HttpClientServiceFactory(IConfiguration configuration, ICoreHttpClient coreHttpClient, IHttpRequestMessageFactory requestMessageFactory, IIdentityServerService tokenResponseService)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            if (coreHttpClient == null)
            {
                throw new ArgumentNullException(nameof(coreHttpClient));
            }

            if (requestMessageFactory == null)
            {
                throw new ArgumentNullException(nameof(requestMessageFactory));
            }

            if (tokenResponseService == null)
            {
                throw new ArgumentNullException(nameof(tokenResponseService));
            }

            _configuration = configuration;
            _coreHttpClient = coreHttpClient;
            _tokenResponseService = tokenResponseService;
            _requestMessageFactory = requestMessageFactory;
        }

        /// <summary>
        /// Creates new <see cref="HttpClientService"/> instances. 
        /// </summary>
        /// <returns>An <see cref="HttpClientService"/> instance.</returns>
        public HttpClientService CreateHttpClientService()
        {
            return new HttpClientService(_configuration, _coreHttpClient, _requestMessageFactory, _tokenResponseService);
        }
    }
}