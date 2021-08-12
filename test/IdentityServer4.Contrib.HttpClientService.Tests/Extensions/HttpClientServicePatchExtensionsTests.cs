using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4.Contrib.HttpClientService.Extensions;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.CommonValues;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientServiceMocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdentityServer4.Contrib.HttpClientService.Tests.Extensions
{
    [TestClass]
    public class HttpClientServicePatchExtensionsTests
    {

        [TestMethod]
        public async Task HttpClientServicePatch_StreamRequestStringResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            {
                writer.Write(ComplexTypes.ComplexTypeRequestString);
                writer.Flush();
                memoryStream.Position = 0;

                var result = await httpClientService.PatchAsync<string>(
                    "http://localhost",
                    new StreamContent(memoryStream)
                );

                //Status/HttpResponseMessage
                Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
                Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

                //HttpRequestMessage
                Assert.AreEqual(HttpMethod.Patch, result.HttpRequestMessge.Method);
                Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());

                //Body
                Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
                Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);

                result.HttpRequestMessge.Dispose();
            }
        }

        [TestMethod]
        public async Task HttpClientServicePatch_NoTypesDefined_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.PatchAsync(
                "http://localhost",
                ComplexTypes.ComplexTypeRequestString
            );

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Patch, result.HttpRequestMessge.Method);
            Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }

        [TestMethod]
        public async Task HttpClientServicePatch_StringRequestStringResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.PatchAsync<string>(
                "http://localhost",
                ComplexTypes.ComplexTypeRequestString
            );

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Patch, result.HttpRequestMessge.Method);
            Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }

        [TestMethod]
        public async Task HttpClientServicePatch_StringContentRequestTypedResponse_ShouldBeResponseType()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.PatchAsync<string>(
                "http://localhost",
                new StringContent(ComplexTypes.ComplexTypeRequestString, Encoding.UTF32, "fake/type")
            );

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Patch, result.HttpRequestMessge.Method);
            Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());
            Assert.IsNotNull("utf-32", result.HttpRequestMessge.Content.Headers.ContentType.CharSet);
            Assert.IsNotNull("fake/type", result.HttpRequestMessge.Content.Headers.ContentType.MediaType);

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }


        [TestMethod]
        public async Task HttpClientServicePatch_ComplexTypeRequestStringResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.PatchAsync<ComplexTypes.ComplexTypeRequest, string>(
                "http://localhost",
                ComplexTypes.ComplexTypeRequestInstance
            );

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Patch, result.HttpRequestMessge.Method);
            Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }
    }
}
