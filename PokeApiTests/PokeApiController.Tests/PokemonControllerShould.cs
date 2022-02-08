using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using PokeApi.Controllers;
using PokeApi.Models;
using PokeApi.PokemonManagement;
using PokeApi.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokeApiTests.PokeApiController.Tests
{
    [TestFixture]
    public class PokemonControllerShould
    {
        [Test]
        public void ReturnBadRequestIfGetAsyncIsCalledWithoutProvidingName()
        {
            Mock<IPokemonService> pokemonService = new Mock<IPokemonService>();
            Mock<IDescriptionProviderFactory> descriptionProviderFactory = new Mock<IDescriptionProviderFactory>();
            Mock<IMemoryCache> memoryCache = new Mock<IMemoryCache>();

            PokemonController controller = new PokemonController(pokemonService.Object,
                descriptionProviderFactory.Object, memoryCache.Object);

            var result = controller.GetAsync(null);

            Assert.That(result.Result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public async Task ShouldReturnCachedValueIfPresentAsync()
        {
            Mock<IPokemonService> pokemonService = new Mock<IPokemonService>();
            Mock<IDescriptionProviderFactory> descriptionProviderFactory = new Mock<IDescriptionProviderFactory>();
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            PokemonResponseModel model = new PokemonResponseModel { 
                Name = "test"
            };

            memoryCache.Set("test", model);

            PokemonController controller = new PokemonController(pokemonService.Object,
                descriptionProviderFactory.Object, memoryCache);

            var result = await controller.GetAsync("test");

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public async Task ReturnValidPokemonModelIfEverythingIsOkAsync()
        {
            Mock<IPokemonService> pokemonService = new Mock<IPokemonService>();
            Mock<IDescriptionProviderFactory> descriptionProviderFactory = new Mock<IDescriptionProviderFactory>();
            Mock<IDescriptionService> descriptionService = new Mock<IDescriptionService>();

            Habitat habitat = new Habitat { Name = "cave" };

            pokemonService.Setup(x => x.GetPokemonAsync(It.IsAny<string>()))
                .ReturnsAsync(() => new Pokemon { Name = "ditto", Habitat = habitat, IsLegendary = true });

            descriptionService.Setup(x => x.GetDescriptionAsync(It.IsAny<Pokemon>(), It.IsAny<bool>()))
                .ReturnsAsync(() => new DescriptionModel { Success = true, Description = "A test description" });

            descriptionProviderFactory.Setup(x => x.GetDescriptor(It.IsAny<Pokemon>()))
                .Returns(() => descriptionService.Object);

            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            PokemonController controller = new PokemonController(pokemonService.Object,
                descriptionProviderFactory.Object, memoryCache);

            var result = await controller.GetAsync("ditto");

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());

        }
    }
}
