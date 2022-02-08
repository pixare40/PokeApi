using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PokeApi.Infrastructure;
using PokeApi.Models;
using PokeApi.Models.ConfigModels;
using PokeApi.PokemonManagement.Descriptions.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeApiTests.PokeApi.PokemonManagement.Tests
{
    [TestFixture]
    public class FunTranslationsTranslator
    {
        [Test]
        public void ReturnEmptyDescriptionMethodIfFlavourEntiresIsNull()
        {
            Mock<IHttpClientWrapper> mockClientProvider = new Mock<IHttpClientWrapper>();
            Mock<PokemonApiConfigModel> optionConfigMock = new Mock<PokemonApiConfigModel>();
            Mock<IOptions<PokemonApiConfigModel>> opt = new Mock<IOptions<PokemonApiConfigModel>>();
            opt.SetupGet(x => x.Value).Returns(optionConfigMock.Object);

            YodaTranslator yodaTranslate = new YodaTranslator(mockClientProvider.Object, opt.Object);

            var result = yodaTranslate.GetDescriptionAsync(new Pokemon());

            Assert.That(result.Result.Description, Is.EqualTo(""));
        }

        [Test]
        public void ReturnDefaultDescriptionIfTranslationNotRequested()
        {
            Mock<IHttpClientWrapper> mockClientProvider = new Mock<IHttpClientWrapper>();
            Mock<PokemonApiConfigModel> optionConfigMock = new Mock<PokemonApiConfigModel>();
            Mock<IOptions<PokemonApiConfigModel>> opt = new Mock<IOptions<PokemonApiConfigModel>>();
            opt.SetupGet(x => x.Value).Returns(optionConfigMock.Object);

            YodaTranslator yodaTranslate = new YodaTranslator(mockClientProvider.Object, opt.Object);

            Pokemon pokemon = new Pokemon();
            pokemon.FlavorTextEntries = new FlavourTextEntry[2];
            pokemon.FlavorTextEntries[0] = new FlavourTextEntry { FlavourText = "test" };

            var result = yodaTranslate.GetDescriptionAsync(pokemon);

            Assert.That(result.Result.Description, Is.EqualTo("test"));
        }

        [Test]
        public void ReturnDescriptionModelWithSuccessAsFalseIfErrorOccursDuringTranslation()
        {
            Mock<IHttpClientWrapper> mockClientProvider = new Mock<IHttpClientWrapper>();
            Mock<PokemonApiConfigModel> optionConfigMock = new Mock<PokemonApiConfigModel>();
            Mock<IOptions<PokemonApiConfigModel>> opt = new Mock<IOptions<PokemonApiConfigModel>>();
            opt.SetupGet(x => x.Value).Returns(optionConfigMock.Object);

            YodaTranslator yodaTranslate = new YodaTranslator(mockClientProvider.Object, opt.Object);

            Pokemon pokemon = new Pokemon();
            pokemon.FlavorTextEntries = new FlavourTextEntry[2];
            pokemon.FlavorTextEntries[0] = new FlavourTextEntry { FlavourText = "test" };

            var result = yodaTranslate.GetDescriptionAsync(pokemon, true);

            Assert.That(result.Result.Success, Is.EqualTo(false));
        }

        [Test]
        public void ReturnTranslatedResponseModelIfTranslatedDataIsReceived()
        {
            Mock<IHttpClientWrapper> mockClientProvider = new Mock<IHttpClientWrapper>();
            mockClientProvider.Setup(x => x.GetAsync<TranslatedResponseModel>(It.IsAny<string>()))
                .ReturnsAsync(() => new TranslatedResponseModel()
                {
                    Contents = new Contents { Translated = "test translated" }
                });

            Mock<PokemonApiConfigModel> optionConfigMock = new Mock<PokemonApiConfigModel>();
            Mock<IOptions<PokemonApiConfigModel>> opt = new Mock<IOptions<PokemonApiConfigModel>>();
            opt.SetupGet(x => x.Value).Returns(optionConfigMock.Object);

            ShakespearTranslator yodaTranslate = new ShakespearTranslator(mockClientProvider.Object, opt.Object);

            Pokemon pokemon = new Pokemon();
            pokemon.FlavorTextEntries = new FlavourTextEntry[2];
            pokemon.FlavorTextEntries[0] = new FlavourTextEntry { FlavourText = "test" };

            var result = yodaTranslate.GetDescriptionAsync(pokemon, true);

            Assert.That(result.Result.Success, Is.EqualTo(true));
            Assert.That(result.Result.Description, Is.EqualTo("test translated"));
        }
    }
}
