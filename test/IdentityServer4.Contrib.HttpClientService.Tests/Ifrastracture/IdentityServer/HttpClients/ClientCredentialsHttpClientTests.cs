﻿using System;
using System.Net;
using System.Threading.Tasks;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.HttpClients;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Models;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.CommonValues;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Contrib.HttpClientService.Tests.Ifrastracture.IdentityServer.HttpClients
{
    [TestClass]
    public class ClientCredentialsHttpClientTests
    {
        [TestMethod]
        public async Task ClientCredentialsHttpClient_GetTokenResponseAsync_ShouldReturnAccessToken()
        {
            var httpClient = new ClientCredentialsHttpClient(
                IHttpClientFactoryMocks.Get(
                    HttpStatusCode.OK,
                    TokenResponseObjects.GetValidTokenResponseString("access_token", 10)
                ).CreateClient("test")
            );

            var clientCredentialOptions = new ClientCredentialsOptions
            {
                Address = "http://localhost/" + Guid.NewGuid(),
                ClientId = "ClientId",
                ClientSecret = "secret",
                Scope = "scope"
            };

            var tokenResponse = await httpClient.GetTokenResponseAsync(clientCredentialOptions);
            Assert.AreEqual(HttpStatusCode.OK, tokenResponse.HttpStatusCode);
            Assert.AreEqual("access_token", tokenResponse.AccessToken);
        }

        [TestMethod]
        public void ClientCredentialsHttpClient_GetCacheKey_ShouldReturnCorrectHash()
        {
            var httpClient = new ClientCredentialsHttpClient(
                IHttpClientFactoryMocks.Get(HttpStatusCode.OK).CreateClient("test")
            );

            var clientCredentialOptions = new ClientCredentialsOptions
            {
                Address = "http://localhost/" + Guid.NewGuid(),
                ClientId = "ClientId",
                ClientSecret = "secret",
                Scope = "scope"
            };

            var cacheKey = httpClient.GetCacheKey(clientCredentialOptions);
            var hash = (clientCredentialOptions.Address + clientCredentialOptions.ClientId + clientCredentialOptions.Scope).GetHashCode().ToString();

            Assert.AreEqual(hash, cacheKey);
        }

        [TestMethod]
        public void ClientCredentialsHttpClient_HttpClientOptionsType_ShouldBeCorrect()
        {
            var httpClient = new ClientCredentialsHttpClient(
                IHttpClientFactoryMocks.Get(
                    HttpStatusCode.OK,
                    TokenResponseObjects.GetValidTokenResponseString("access_token", 10)
                ).CreateClient("test")
            );

            Assert.AreEqual(typeof(ClientCredentialsOptions), httpClient.HttpClientOptionsType);
        }
    }
}
