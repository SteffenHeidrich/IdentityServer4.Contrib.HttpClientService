using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityServer4.Contrib.HttpClientService.Extensions;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.CommonValues;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientServiceMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Contrib.HttpClientService.Tests.Extensions
{
    [TestClass]
    public class HttpClientServiceDeleteExtensionsTests
    {

        [TestMethod]
        public async Task HttpClientServiceDelete_NoTypedResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.NoContent, "", true);
            var result = await httpClientService.DeleteAsync("http://localhost");

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.NoContent, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Delete, result.HttpRequestMessge.Method);
            Assert.IsNull(result.HttpRequestMessge.Content);

            //Body
            Assert.IsTrue(String.IsNullOrEmpty(result.BodyAsString));
            Assert.IsTrue(String.IsNullOrEmpty(result.BodyAsType));
        }

        [TestMethod]
        public async Task HttpClientServiceDelete_TypedResponse_ShouldBeResponseType()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);
            var result = await httpClientService.DeleteAsync<ComplexTypes.ComplexTypeResponse>("http://localhost");

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Delete, result.HttpRequestMessge.Method);
            Assert.IsNull(result.HttpRequestMessge.Content);

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);

            Assert.IsInstanceOfType(result.BodyAsType.TestInt, typeof(int));
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseInstance.TestInt, result.BodyAsType.TestInt);

            Assert.IsInstanceOfType(result.BodyAsType.TestBool, typeof(bool));
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseInstance.TestBool, result.BodyAsType.TestBool);
        }
    }
}
