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
    public class PokemonServiceTests
    {
        private PokemonService pokemonService;

        [SetUp]
        public void SetupPokemonService()
        {
            Mock<IHttpClientProvider> httpProviderMock = new Mock<IHttpClientProvider>();
            httpProviderMock
                .Setup(x => x.GetAsync<Pokemon>(It.IsAny<string>()))
                .Returns(()=> Task.FromResult(new Pokemon { Name = "Test Pokemon"}));
            Mock<PokemonApiConfigModel> optionConfigMock = new Mock<PokemonApiConfigModel>();
            Mock<IOptions<PokemonApiConfigModel>> opt = new Mock<IOptions<PokemonApiConfigModel>>();
            opt.SetupGet(x => x.Value).Returns(optionConfigMock.Object);

            pokemonService = new PokemonService(opt.Object, httpProviderMock.Object);
        }


        [Test]
        public void ReturnNullIfPokemonNameIsEmpty()
        {
            var result = pokemonService.GetPokemonAsync(null);

            Assert.AreEqual(null, result.Result);
        }

        [Test]
        public  void ReturnPokemonObjectIfValidationPasses()
        {
            var result = pokemonService.GetPokemonAsync("Test Pokemon");

            Assert.IsInstanceOf<Pokemon>(result.Result);
        }
    }
}
