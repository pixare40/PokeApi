using PokeApi.Models;
using System.Threading.Tasks;

namespace PokeApi.PokemonManagement
{
    public interface IPokemonService
    {
        public Task<Pokemon> GetPokemonAsync(string name);
    }
}
