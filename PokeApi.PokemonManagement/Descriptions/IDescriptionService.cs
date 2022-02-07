using PokeApi.Models;
using System.Threading.Tasks;

namespace PokeApi.PokemonManagement
{
    public interface IDescriptionService
    {
        public Task<DescriptionModel> GetDescriptionAsync(Pokemon pokemon, bool translated = false);
    }
}
