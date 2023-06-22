using Compori.Shopware.Factories;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Net;
using Xunit;

namespace Compori.Shopware.Factories
{
    public class RestClientFactoryTests : BaseTest
    {
        [Fact()]
        public void TestSetSecurityProtocol()
        {
            this.Setup();
            try
            {
                var sut = new RestClientFactory();
                var settings = new Settings
                {
                    EnableTls11 = true
                };
                sut.SetSecurityProtocol(settings, true);
                // Assert.Equal(SecurityProtocolType.Tls11, sut.SecurityProtocol & SecurityProtocolType.Tls11);
                settings.EnableTls12 = true;
                sut.SetSecurityProtocol(settings, true);
                // Assert.Equal(SecurityProtocolType.Tls12, sut.SecurityProtocol & SecurityProtocolType.Tls12);
                settings.EnableTls13 = true;
                sut.SetSecurityProtocol(settings, true);
                // Assert.Equal(SecurityProtocolType.Tls13, sut.SecurityProtocol & SecurityProtocolType.Tls13);

                settings.ForceTls11 = true;
                sut.SetSecurityProtocol(settings, true);
                // Assert.Equal(SecurityProtocolType.Tls11, sut.SecurityProtocol);
                settings.ForceTls12 = true;
                sut.SetSecurityProtocol(settings, true);
                // Assert.Equal(SecurityProtocolType.Tls12, sut.SecurityProtocol);
                settings.ForceTls13 = true;
                sut.SetSecurityProtocol(settings, true);
                // Assert.Equal(SecurityProtocolType.Tls13, sut.SecurityProtocol);
            }
            finally
            {
                this.Cleanup();
            }
        }

        [Fact()]
        public void TestCreate()
        {
            this.Setup();
            try
            {
                var sut = new RestClientFactory();
                Assert.Throws<ArgumentNullException>(() => sut.Create(null));
                var client = sut.Create(this.TestContext.GetSettings());
                Assert.NotNull(client);
                Assert.Null(client.Options.Authenticator);

                sut = new RestClientFactory();
                client = sut.Create(this.TestContext.GetSettings(), "TOKEN");
                Assert.NotNull(client);
                var authenticator = client.Options.Authenticator as OAuth2AuthorizationRequestHeaderAuthenticator;
                Assert.NotNull(authenticator);
            }
            finally
            {
                this.Cleanup();
            }
        }
    }
}