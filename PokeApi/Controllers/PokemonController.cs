using Microsoft.AspNetCore.Mvc;
using PokeApi.Models;
using PokeApi.PokemonManagement;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using PokeApi.ResponseModels;
using Microsoft.Extensions.Caching.Memory;

namespace PokeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService pokemonService;
        private readonly IDescriptionProviderFactory descriptionProviderFactory;
        private readonly IMemoryCache memoryCache;

        public PokemonController(IPokemonService pokemonService,
            IDescriptionProviderFactory descriptionProviderFactory,
            IMemoryCache memoryCache)
        {
            this.pokemonService = pokemonService;
            this.descriptionProviderFactory = descriptionProviderFactory;
            this.memoryCache = memoryCache;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<PokemonResponseModel>> GetAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            if (memoryCache.TryGetValue(name, out PokemonResponseModel pokemonResponse))
            {
                return Ok(pokemonResponse);
            }

            try
            {
                PokemonResponseModel model = await GetPokemonResponseModel(name, name, false);

                return Ok(model);
            }
            catch (Exception e)
            {
                return Problem();
            }
        }

        [HttpGet("translated/{name}")]
        public async Task<ActionResult<PokemonResponseModel>> GetTranslatedAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            string cacheKey = string.Format("{0}{1}", name, "translated");

            if (memoryCache.TryGetValue(cacheKey, out PokemonResponseModel pokemonResponse))
            {
                return Ok(pokemonResponse);
            }

            try
            {
                PokemonResponseModel responseModel = await GetPokemonResponseModel(name, cacheKey, true);

                return Ok(responseModel);
            }
            catch (Exception e)
            {
                return Problem();
            }
        }

        private async Task<PokemonResponseModel> GetPokemonResponseModel(string name, string cacheKey, bool translate)
        {
            Pokemon pokemon = await pokemonService.GetPokemonAsync(name);
            IDescriptionService descService = descriptionProviderFactory.GetDescriptor(pokemon);

            DescriptionModel descModel = await descService.GetDescriptionAsync(pokemon, translate);

            PokemonResponseModel responseModel = GetResponseModel(pokemon, descModel);

            TryCacheResponseModel(cacheKey, descModel, responseModel);
            return responseModel;
        }

        private void TryCacheResponseModel(string name, DescriptionModel descModel, PokemonResponseModel responseModel)
        {
            if (descModel.Success)
            {
                MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
                memoryCache.Set(name, responseModel, cacheOptions);
            }
        }

        private static PokemonResponseModel GetResponseModel(Pokemon pokemon, DescriptionModel descModel)
        {
            return new PokemonResponseModel
            {
                Name = pokemon.Name,
                Description = descModel.Description,
                Habitat = pokemon.Habitat.Name,
                IsLegendary = pokemon.IsLegendary
            };
        }
    }
        
}
