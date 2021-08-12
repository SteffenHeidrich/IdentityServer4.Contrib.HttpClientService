using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Interfaces;
using IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer.Models;

namespace IdentityServer4.Contrib.HttpClientService.Infrastructure.IdentityServer
{

    /// <summary>
    /// Selects the correct <see cref="IIdentityServerHttpClient"/> based on <see cref="IIdentityServerOptions"/>.
    /// </summary>
    public class IdentityServerHttpClientSelector : IIdentityServerHttpClientSelector
    {
        private IEnumerable<IIdentityServerHttpClient> _httpClients;

        /// <summary>
        /// Constructor of the <see cref="IdentityServerHttpClientSelector"/>.
        /// </summary>
        public IdentityServerHttpClientSelector(IEnumerable<IIdentityServerHttpClient> httpClients)
        {
            _httpClients = httpClients;
        }

        /// <summary>
        /// Finds the appropriate implementation of <see cref="IIdentityServerHttpClient"/> based on the <paramref name="options"/>.
        /// </summary>
        /// <param name="options">The <paramref name="options"/> for retrieving an access token.</param>
        /// <returns>An <see cref="IIdentityServerHttpClient"/>.</returns>
        public IIdentityServerHttpClient Get(IIdentityServerOptions options)
        {
            if (!_httpClients.Any(x => x.HttpClientOptionsType.IsAssignableFrom(options.GetType())))
            {
                throw new InvalidOperationException("There is no assignable type for the options selected. " +
                    "Does your options inherit from either " + nameof(ClientCredentialsOptions) + " or " + nameof(PasswordOptions) + "?");
            }

            return _httpClients.First(x => x.HttpClientOptionsType.IsAssignableFrom(options.GetType()));
        }

    }
}