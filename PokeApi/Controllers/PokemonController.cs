using Microsoft.AspNetCore.Mvc;
using PokeApi.Models;
using PokeApi.PokemonManagement;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;

namespace PokeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService pokemonService;

        public PokemonController(IPokemonService pokemonService)
        {
            this.pokemonService = pokemonService;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Pokemon>> GetAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            try
            {
                Pokemon pokemon = await pokemonService.GetPokemonAsync(name);
                return Ok(pokemon);
            }
            catch (Exception e)
            {
                return Problem();
            }
        }
        
        [HttpGet("/translated/{name}")]
        public async Task<ActionResult<Pokemon>> GetTranslatedAsync(string name)
        {
            Pokemon pokemon = await pokemonService.GetTranslatedPokemon(name);

            return Ok(pokemon);
        }
    }
}
