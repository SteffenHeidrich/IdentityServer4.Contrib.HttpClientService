﻿using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.Core;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.HttpClients;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Interfaces;
using IdentityServer4.Contrib.HttpClientService.Models;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.CacheMocks;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.CommonValues;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientMocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientServiceMocks
{
    public static class HttpClientServiceInstances
    {
        public static async Task<HttpClientService> GetNew(HttpStatusCode coreStatusCode, string coreContent, bool validTokenResponse)
        {
            var httpClientService = new HttpClientServiceFactory(
                GetConfigurationMock("{}"),
                new CoreHttpClient(
                    IHttpClientFactoryMocks.Get(coreStatusCode, coreContent).CreateClient()
                ),
                new HttpRequestMessageFactory(
                    IHttpContextAccessorMocks.Get(),
                    Options.Create(new HttpClientServiceOptions())
                ),
                new IdentityServerService(
                    new IdentityServerHttpClientSelector(
                        new List<IIdentityServerHttpClient> {
                            {
                                new ClientCredentialsHttpClient(
                                    IHttpClientFactoryMocks.Get(HttpStatusCode.OK).CreateClient()
                                )
                            },
                            {
                                new PasswordHttpClient(
                                    IHttpClientFactoryMocks.Get(HttpStatusCode.OK).CreateClient()
                                )
                            }
                        }
                    ),
                    ITokenResponseCacheManagerMocks.Get(
                        validTokenResponse
                        ? await TokenResponseObjects.GetValidTokenResponseAsync("access_token", 5)
                        : await TokenResponseObjects.GetInvalidTokenResponseAsync("invalid_client")
                    )
                )
            ).CreateHttpClientService();

            return httpClientService;
        }

        public static async Task<HttpClientService> GetNew(HttpStatusCode coreStatusCode, string coreContent, bool validTokenResponse, string jsonConfiguration)
        {
            var httpClientService = new HttpClientServiceFactory(
                GetConfigurationMock(jsonConfiguration),
                new CoreHttpClient(
                    IHttpClientFactoryMocks.Get(coreStatusCode, coreContent).CreateClient()
                ),
                new HttpRequestMessageFactory(
                    IHttpContextAccessorMocks.Get(),
                    Options.Create(new HttpClientServiceOptions())
                ),
                new IdentityServerService(
                    new IdentityServerHttpClientSelector(
                        new List<IIdentityServerHttpClient> {
                            {
                                new ClientCredentialsHttpClient(
                                    IHttpClientFactoryMocks.Get(HttpStatusCode.OK).CreateClient()
                                )
                            },
                            {
                                new PasswordHttpClient(
                                    IHttpClientFactoryMocks.Get(HttpStatusCode.OK).CreateClient()
                                )
                            }
                        }
                    ),
                    ITokenResponseCacheManagerMocks.Get(
                        validTokenResponse
                        ? await TokenResponseObjects.GetValidTokenResponseAsync("access_token", 5)
                        : await TokenResponseObjects.GetInvalidTokenResponseAsync("invalid_client")
                    )
                )
            ).CreateHttpClientService();

            return httpClientService;
        }

        public static async Task<HttpClientService> GetNew(HttpStatusCode coreStatusCode, Stream coreContent, bool validTokenResponse)
        {
            var httpClientService = new HttpClientServiceFactory(
                GetConfigurationMock("{}"),
                new CoreHttpClient(
                    IHttpClientFactoryMocks.Get(coreStatusCode, coreContent).CreateClient()
                ),
                new HttpRequestMessageFactory(
                    IHttpContextAccessorMocks.Get(),
                    Options.Create(new HttpClientServiceOptions())
                ),
                new IdentityServerService(
                    new IdentityServerHttpClientSelector(
                        new List<IIdentityServerHttpClient> {
                            {
                                new ClientCredentialsHttpClient(
                                    IHttpClientFactoryMocks.Get(HttpStatusCode.OK).CreateClient()
                                )
                            },
                            {
                                new PasswordHttpClient(
                                    IHttpClientFactoryMocks.Get(HttpStatusCode.OK).CreateClient()
                                )
                            }
                        }
                    ),
                    ITokenResponseCacheManagerMocks.Get(
                        validTokenResponse
                        ? await TokenResponseObjects.GetValidTokenResponseAsync("access_token", 3600)
                        : await TokenResponseObjects.GetInvalidTokenResponseAsync("invalid_client")
                    )
                )
            ).CreateHttpClientService();

            return httpClientService;

        }

        private static IConfiguration GetConfigurationMock(string jsonConfiguration)
        {
            var byteArray = Encoding.UTF8.GetBytes(jsonConfiguration);
            var stream = new MemoryStream(byteArray);

            var conf = new ConfigurationBuilder();
            conf.AddJsonStream(stream);
            var confRoor = conf.Build();

            return confRoor; 
        }
    }
}
