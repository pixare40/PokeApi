using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PokeApi.Infrastructure;
using PokeApi.Models;
using PokeApi.Models.ConfigModels;
using PokeApi.PokemonManagement;
using PokemonManagement;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeApiTests.PokeApi.PokemonManagement.Tests
{
    [TestFixture]
    public class PokemonServiceShould
    {
        private PokemonService pokemonService;
        private Mock<IHttpClientProvider> httpProviderMock;

        [OneTimeSetUp]
        public void SetupPokemonService()
        {
            httpProviderMock = new Mock<IHttpClientProvider>();
            httpProviderMock
                .Setup(x => x.GetAsync<Pokemon>("ditto"))
                .Returns(()=> Task.FromResult(new Pokemon { Name = "ditto"}));
            Mock<PokemonApiConfigModel> optionConfigMock = new Mock<PokemonApiConfigModel>();
            Mock<IOptions<PokemonApiConfigModel>> opt = new Mock<IOptions<PokemonApiConfigModel>>();
            opt.SetupGet(x => x.Value).Returns(optionConfigMock.Object);

            pokemonService = new PokemonService(opt.Object, httpProviderMock.Object);
        }


        [Test]
        public void ReturnNullIfPokemonNameIsEmpty()
        {
            var result = pokemonService.GetPokemonAsync(null);

            Assert.That(result.Result, Is.EqualTo(null));
        }

        [Test]
        public  void ReturnPokemonObjectIfValidationPasses()
        {
            var result = pokemonService.GetPokemonAsync("ditto");

            Assert.That(result.Result, Is.InstanceOf<Pokemon>());
        }

        [Test]
        public void CallsHttpProviderGetAsyncMethodOnce()
        {
            var result = pokemonService.GetPokemonAsync("mewtwo");

            httpProviderMock.Verify(x => x.GetAsync<Pokemon>(string.Format("{0}{1}", null, "mewtwo")), Times.Once);
        }
    }
}
