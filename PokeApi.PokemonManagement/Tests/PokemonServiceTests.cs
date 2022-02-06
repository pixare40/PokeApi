using Microsoft.Extensions.Options;
using NUnit.Framework;
using PokeApi.Models.ConfigModels;
using PokemonManagement;

namespace PokeApi.PokemonManagement.Tests
{
    [TestFixture]
    public class PokemonServiceTests
    {
        [Test]
        public void Send_Null_As_Pokemon_Name()
        {
            var options = Options.Create<PokemonApiConfigModel>(
                new PokemonApiConfigModel { PokemonApi = "" });

            var pokemonService = new PokemonService(options);
            Assert.IsNull(pokemonService.GetPokemonAsync(""));
        }
    }
}
