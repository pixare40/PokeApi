using Moq;
using Moq.Protected;
using NUnit.Framework;
using PokeApi.Infrastructure;
using PokeApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokeApiTests.PokeApi.Infrastructure.Tests
{
    [TestFixture]
    public class HttpClientProviderShould
    {
        [OneTimeSetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void ThrowArgumentNullExceptionIfPokemonNameIsNull()
        {
            Mock<IHttpClientFactory> mockFactory = new Mock<IHttpClientFactory>();
            Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage());

            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            HttpClientProvider httpClientProvider = new HttpClientProvider(mockFactory.Object);
            Assert.ThrowsAsync<ArgumentNullException>(() =>  httpClientProvider.GetAsync<Pokemon>(null));
        }

        [Test]
        public void ThrowsExceptionIfInvalidUriIsProvided()
        {
            Mock<IHttpClientFactory> mockFactory = new Mock<IHttpClientFactory>();
            Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientProvider httpClientProvider = new HttpClientProvider(mockFactory.Object);

            Assert.ThrowsAsync<InvalidOperationException>(() => httpClientProvider.GetAsync<Pokemon>("string"));
        }

        [Test]
        public void ThrowsExceptionIfStatusCodeIsNot200()
        {
            Mock<IHttpClientFactory> mockFactory = new Mock<IHttpClientFactory>();
            Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.NotFound
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientProvider httpClientProvider = new HttpClientProvider(mockFactory.Object);

            Assert.ThrowsAsync<HttpRequestException>(() => httpClientProvider.GetAsync<Pokemon>("http://api.google.com"));
        }

        [Test]
        public void ReturnInstanceOfPokemonIfDataIsRecieved()
        {
            Mock<IHttpClientFactory> mockFactory = new Mock<IHttpClientFactory>();
            Mock<HttpMessageHandler> mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(@"{""name"":""ditto"", ""is_legendary"": true }")
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);

            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientProvider httpClientProvider = new HttpClientProvider(mockFactory.Object);

            var result = httpClientProvider.GetAsync<Pokemon>("http://api.google.com");
            
            Assert.That(result.Result, Is.InstanceOf<Pokemon>());
            Assert.That(result.Result.Name, Is.EqualTo("ditto"));
        }
    }
}
