using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Models;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientServiceMocks;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Contrib.HttpClientService.Tests
{
    [TestClass]
    public class HttpClientServiceTests_SetIdentityOptions_ClientCredentialsOptions
    {

        [TestMethod]
        public async Task HttpClientServiceTests_HttpClientServiceTests_SetIdentityOptionsIOptions_ClientCredentials_ShouldRequestWitnAuth()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, "body_of_response", true);

            httpClientService.SetIdentityServerOptions(
                Options.Create(
                    new ClientCredentialsOptions
                    {
                        Address = "http://localhost",
                        ClientId = "ClientId",
                        ClientSecret = "ClientSecret",
                        Scope = "Scope"
                    }
                )
            );

            var result = await httpClientService.SendAsync<object, string>(
                new Uri("http://localhost"),
                HttpMethod.Get,
                null
            );

            result.HttpRequestMessge.Dispose();

            Assert.IsTrue(result.HttpRequestMessge.Headers.Contains("Authorization"));
            Assert.AreEqual("Bearer", result.HttpRequestMessge.Headers.Authorization.Scheme);
            Assert.AreEqual("access_token", result.HttpRequestMessge.Headers.Authorization.Parameter);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("body_of_response", result.BodyAsType);
        }

        [TestMethod]
        public async Task HttpClientServiceTests_HttpClientServiceTests_SetIdentityOptionsObject_ClientCredentials_ShouldRequestWitnAuth()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, "body_of_response", true);

            httpClientService.SetIdentityServerOptions(
                new ClientCredentialsOptions
                {
                    Address = "http://localhost",
                    ClientId = "ClientId",
                    ClientSecret = "ClientSecret",
                    Scope = "Scope"
                }
            );

            var result = await httpClientService.SendAsync<object, string>(
                new Uri("http://localhost"),
                HttpMethod.Get,
                null
            );

            result.HttpRequestMessge.Dispose();

            Assert.IsTrue(result.HttpRequestMessge.Headers.Contains("Authorization"));
            Assert.AreEqual("Bearer", result.HttpRequestMessge.Headers.Authorization.Scheme);
            Assert.AreEqual("access_token", result.HttpRequestMessge.Headers.Authorization.Parameter);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("body_of_response", result.BodyAsType);
        }

        [TestMethod]
        public async Task HttpClientServiceTests_HttpClientServiceTests_SetIdentityOptionsDelegate_ClientCredentials_ShouldRequestWitnAuth()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, "body_of_response", true);

            httpClientService.SetIdentityServerOptions<ClientCredentialsOptions>( x => 
                {
                    x.Address = "http://localhost";
                    x.ClientId = "ClientId";
                    x.ClientSecret = "ClientSecret";
                    x.Scope = "Scope";
                }
            );

            var result = await httpClientService.SendAsync<object, string>(
                new Uri("http://localhost"),
                HttpMethod.Get,
                null
            );

            result.HttpRequestMessge.Dispose();

            Assert.IsTrue(result.HttpRequestMessge.Headers.Contains("Authorization"));
            Assert.AreEqual("Bearer", result.HttpRequestMessge.Headers.Authorization.Scheme);
            Assert.AreEqual("access_token", result.HttpRequestMessge.Headers.Authorization.Parameter);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("body_of_response", result.BodyAsType);
        }

        [TestMethod]
        public async Task HttpClientServiceTests_HttpClientServiceTests_SetIdentityOptionString_ClientCredentials_ShouldRequestWitnAuth()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(
                HttpStatusCode.OK, 
                "body_of_response", 
                true,
                @"{""SomeClientCredentialsOptions"":{
                    ""Address"": ""https://demo.identityserver.io/connect/token"",
                        ""ClientId"": ""m2m.short"",
                        ""ClientSecret"": ""secret"",
                        ""Scope"": ""api""
                      }}"
                );

            httpClientService.SetIdentityServerOptions("SomeClientCredentialsOptions");

            var result = await httpClientService.SendAsync<object, string>(
                new Uri("http://localhost"),
                HttpMethod.Get,
                null
            );

            result.HttpRequestMessge.Dispose();

            Assert.IsTrue(result.HttpRequestMessge.Headers.Contains("Authorization"));
            Assert.AreEqual("Bearer", result.HttpRequestMessge.Headers.Authorization.Scheme);
            Assert.AreEqual("access_token", result.HttpRequestMessge.Headers.Authorization.Parameter);

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual("body_of_response", result.BodyAsType);
        }


    }
}
