﻿using IdentityServer4.Contrib.HttpClientService.Models;
using Microsoft.AspNetCore.Http;
using Moq;

namespace IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientServiceMocks
{
    public static class IHttpContextAccessorMocks
    {
        public static IHttpContextAccessor Get()
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

            return mockHttpContextAccessor.Object;
        }

        public static IHttpContextAccessor Get(string xHeader)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            context.Request.Headers[new HttpClientServiceOptions().HeaderCollerationName] = xHeader;
            mockHttpContextAccessor.Setup(_ => _.HttpContext).Returns(context);

            return mockHttpContextAccessor.Object;
        }
    }
}
