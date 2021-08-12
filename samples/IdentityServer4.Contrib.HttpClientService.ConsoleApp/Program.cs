using IdentityServer4.Contrib.HttpClientService.Extensions;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4.Contrib.HttpClientService.ConsoleApp
{
    public class Program
    {
        private static async Task Main()
        {
            var responseObject = await HttpClientServiceFactory
                .Instance
                .CreateHttpClientService()
                .SetIdentityServerOptions<ClientCredentialsOptions>(x =>
                {
                    x.Address = "https://demo.identityserver.io/connect/token";
                    x.ClientId = "m2m";
                    x.ClientSecret = "secret";
                    x.Scope = "api";
                })
                .GetAsync<IEnumerable<DemoProtectedResource>>("https://demo.identityserver.io/api/test");

            if (!responseObject.HasError)
            {
                var customers = responseObject.BodyAsType;
                //Do something with the customers
            }
            else
            {
                var httpStatusCode = responseObject.StatusCode;
                var errorMessage = responseObject.Error;
                //Do something with the error
            }

            foreach (var result in responseObject.BodyAsType)
            {
                Console.WriteLine(result.Type + ":" + result.Value);
            }
            Console.WriteLine(responseObject.BodyAsType.Count() + " results");
            Console.ReadKey();

        }

    }

    public class DemoProtectedResource
    {
        /// <summary>
        /// The type property of the result
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The value property of the result
        /// </summary>
        public string Value { get; set; }
    }

}
