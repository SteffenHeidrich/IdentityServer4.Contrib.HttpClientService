using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientServiceMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Contrib.HttpClientService.Tests
{
    [TestClass]
    public class HttpClientServiceTests_NoSuccessStatusCodes
    {

        [TestMethod]
        public async Task HttpClientServiceTests_ServerReturns400_ShouldHave400Error()
        {

            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.BadRequest, "body_of_response", true);


            var result = await httpClientService.SendAsync<string, object>(
                    new Uri("http://localhost"),
                    HttpMethod.Get,
                    null
                );


            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.IsTrue(result.HasError);
            Assert.AreEqual("Bad Request", result.Error);
        }

        [TestMethod]
        public async Task HttpClientServiceTests_ServerReturns404_ShouldHave404Error()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.NotFound, "body_of_response", true);


            var result = await httpClientService.SendAsync<string, object>(
                    new Uri("http://localhost"),
                    HttpMethod.Get,
                    null
                );


            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.IsTrue(result.HasError);
            Assert.AreEqual("Not Found", result.Error);
        }

        [TestMethod]
        public async Task HttpClientServiceTests_ServerReturns500_ShouldHave500Error()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.InternalServerError, "body_of_response", true);

            var result = await httpClientService.SendAsync<string, object>(
                    new Uri("http://localhost"),
                    HttpMethod.Get,
                    null
                );

            Assert.AreEqual(HttpStatusCode.InternalServerError, result.StatusCode);
            Assert.IsTrue(result.HasError);
            Assert.AreEqual("Internal Server Error", result.Error);
        }
    }
}
