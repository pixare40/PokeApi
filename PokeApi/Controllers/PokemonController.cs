using Microsoft.AspNetCore.Mvc;
using PokeApi.Models;
using PokeApi.PokemonManagement;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace PokeApi.Controllers
{
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService pokemonService;

        PokemonController(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }

        [HttpGet("/{name}")]
        public async Task<ActionResult<Pokemon>> GetAsync(string name)
        {
            Pokemon pokemon = await pokemonService.GetPokemonAsync(name);

            return Ok(pokemon);
        }
        
        [HttpGet("/translated/{name}")]
        public async Task<ActionResult<Pokemon>> GetTranslatedAsync(string name)
        {
            Pokemon pokemon = await pokemonService.GetTranslatedPokemon(name);

            return Ok(pokemon);
        }
    }
}
