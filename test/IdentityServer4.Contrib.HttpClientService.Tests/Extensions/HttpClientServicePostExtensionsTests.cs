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
    public class HttpClientServicePostExtensionsTests
    {

        [TestMethod]
        public async Task HttpClientServicePost_StreamRequestStringResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.Created, ComplexTypes.ComplexTypeResponseString, true);

            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream))
            {
                writer.Write(ComplexTypes.ComplexTypeRequestString);
                writer.Flush();
                memoryStream.Position = 0;

                var result = await httpClientService.PostAsync<string>(
                    "http://localhost",
                    new StreamContent(memoryStream)
                );

                //Status/HttpResponseMessage
                Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
                Assert.AreEqual(HttpStatusCode.Created, result.HttpResponseMessage.StatusCode);

                //HttpRequestMessage
                Assert.AreEqual(HttpMethod.Post, result.HttpRequestMessge.Method);
                Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());

                //Body
                Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
                Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
            }
        }

        [TestMethod]
        public async Task HttpClientServicePost_NoTypesDefined_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.OK, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.PostAsync(
                "http://localhost",
                ComplexTypes.ComplexTypeRequestString
            );

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.OK, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Post, result.HttpRequestMessge.Method);
            Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }

        [TestMethod]
        public async Task HttpClientServicePost_StringRequestStringResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.Created, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.PostAsync<string>(
                "http://localhost",
                ComplexTypes.ComplexTypeRequestString
            );

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.Created, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Post, result.HttpRequestMessge.Method);
            Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }

        [TestMethod]
        public async Task HttpClientServicePost_StringContentRequestStringResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.Created, ComplexTypes.ComplexTypeResponseString, true);


            var result = await httpClientService.PostAsync<string>(
                "http://localhost",
                new StringContent(ComplexTypes.ComplexTypeRequestString, Encoding.UTF32, "fake/type")
            );

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.Created, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Post, result.HttpRequestMessge.Method);
            Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());
            Assert.IsNotNull("utf-32", result.HttpRequestMessge.Content.Headers.ContentType.CharSet);
            Assert.IsNotNull("fake/type", result.HttpRequestMessge.Content.Headers.ContentType.MediaType);

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }


        [TestMethod]
        public async Task HttpClientServicePost_ComplexTypeRequestStringResponse_ShouldBeResponseString()
        {
            var httpClientService = await HttpClientServiceInstances.GetNew(HttpStatusCode.Created, ComplexTypes.ComplexTypeResponseString, true);

            var result = await httpClientService.PostAsync<ComplexTypes.ComplexTypeRequest, string>(
                "http://localhost",
                ComplexTypes.ComplexTypeRequestInstance
            );

            //Status/HttpResponseMessage
            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual(HttpStatusCode.Created, result.HttpResponseMessage.StatusCode);

            //HttpRequestMessage
            Assert.AreEqual(HttpMethod.Post, result.HttpRequestMessge.Method);
            Assert.IsNotNull(ComplexTypes.ComplexTypeRequestString, await result.HttpRequestMessge.Content.ReadAsStringAsync());

            //Body
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsString);
            Assert.AreEqual(ComplexTypes.ComplexTypeResponseString, result.BodyAsType);
        }
    }
}
