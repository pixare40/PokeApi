using Microsoft.AspNetCore.Mvc;
using PokeApi.Models;
using PokeApi.PokemonManagement;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using PokeApi.ResponseModels;

namespace PokeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService pokemonService;
        private readonly IDescriptionProviderFactory descriptionProviderFactory;

        public PokemonController(IPokemonService pokemonService, IDescriptionProviderFactory descriptionProviderFactory)
        {
            this.pokemonService = pokemonService;
            this.descriptionProviderFactory = descriptionProviderFactory;
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
                IDescriptionService descService = descriptionProviderFactory.GetDescriptor(pokemon);

                PokemonResponseModel responseModel = new PokemonResponseModel
                {
                    Name = pokemon.Name,
                    Description = await descService.GetDescriptionAsync(pokemon),
                    Habitat = pokemon.Habitat.Name,
                    IsLegendary = pokemon.IsLegendary
                };

                return Ok(responseModel);
            }
            catch (Exception e)
            {
                return Problem();
            }
        }
        
        [HttpGet("/translated/{name}")]
        public async Task<ActionResult<Pokemon>> GetTranslatedAsync(string name)
        {
            Pokemon pokemon = new Pokemon();

            return Ok(pokemon);
        }
    }
}
