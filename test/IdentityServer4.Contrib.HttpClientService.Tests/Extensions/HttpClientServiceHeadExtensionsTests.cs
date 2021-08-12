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
    public class HttpClientServiceHeadExtensionsTests
    {

        [TestMethod]
        public async Task HttpClientServiceHead_NoTypedResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.HeadAsync("http://localhost");

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Head, result.HttpRequestMessge.Method);
            Assert.IsNull(result.HttpRequestMessge.Content);

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }

        [TestMethod]
        public async Task HttpClientServiceHead_TypedResponse_ShouldBeResponseType()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.HeadAsync<ComplexTypes.ComplexTypeResponse>("http://localhost");

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Head, result.HttpRequestMessge.Method);
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
