﻿using System.Net;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4.Contrib.HttpClientService.Tests.Helpers.HttpClientMocks;

namespace IdentityServer4.Contrib.HttpClientService.Tests.Helpers.CommonValues
{
    public static class TokenResponseObjects
    {
        public static string GetValidTokenResponseString(string accessToken, int expiresIn)
        {
            return @"{
                        ""access_token"": """ + accessToken + @""",
                        ""expires_in"": " + expiresIn + @",
                        ""token_type"": ""Bearer""
                    }";
        }
        public static string GetInvalidTokenResponseString(string error)
        {
            return "{\"error\":\"" + error + "\"}";
        }

        public static async Task<TokenResponse> GetValidTokenResponseAsync(string accessToken, int expiresIn)
        {
            var httpClient = IHttpClientFactoryMocks.Get(
                    HttpStatusCode.OK,
                    GetValidTokenResponseString(accessToken, expiresIn)
            ).CreateClient("TokenResponse");

            return await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost/"
            });
        }

        public static async Task<TokenResponse> GetInvalidTokenResponseAsync(string error)
        {
            var httpClient = IHttpClientFactoryMocks.Get(
                HttpStatusCode.OK,
                GetInvalidTokenResponseString(error)
            ).CreateClient("TokenResponse");

            return await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost/"
            });
        }
    }
}
