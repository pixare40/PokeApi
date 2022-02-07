using System.Text.Json;
using System.Text.Json.Serialization;

namespace PokeApi.Models
{
    public class Pokemon
    {
        [JsonPropertyName("base_happiness")]
        public object BaseHappiness { get; set; }

        [JsonPropertyName("capture_rate")]
        public object CaptureRate { get; set; }

        public object Color { get; set; }

        [JsonPropertyName("egg_groups")]
        public object[] EggGroups { get; set; }

        [JsonPropertyName("evolution_chain")]
        public object EvolutionChain { get; set; }

        [JsonPropertyName("evolves_from_species")]
        public object EvolvesFromSpecies { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public FlavourTextEntry[] FlavorTextEntries { get; set; }

        [JsonPropertyName("form_descriptions")]
        public object[] FormDescriptions { get; set; }

        [JsonPropertyName("forms_switchable")]
        public bool FormsSwitchable { get; set; }

        [JsonPropertyName("gender_rate")]
        public int GenderRate { get; set; }

        public object[] Genera { get; set; }

        public object Generation { get; set; }

        [JsonPropertyName("growth_rate")]
        public object GrowthRate { get; set; }

        public Habitat Habitat { get; set; }

        [JsonPropertyName("has_gender_differences")]
        public bool HasGenderDIfferences { get; set; }

        [JsonPropertyName("hatch_counter")]
        public int HatchCounter { get; set; }

        public double Id { get; set; }

        [JsonPropertyName("is_baby")]
        public bool IsBaby { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }

        [JsonPropertyName("is_mythical")]
        public bool IsMythical { get; set; }

        public string Name { get; set; }

        public object[] Names { get; set; }

        public int Order { get; set; }

        [JsonPropertyName("pal_park_encounters")]
        public object[] PalParkEncounters { get; set; }

        [JsonPropertyName("pokedex_numbers")]
        public object[] PokedexNumbers { get; set; }

        public object Shape { get; set; }

        public object[] Varieties { get; set; }
    }

    public class Habitat
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }

    public class FlavourTextEntry
    {
        [JsonPropertyName("flavor_text")]
        public string FlavourText { get; set; }

        public object Language { get; set; }

        public object Version { get; set; }
    }
}
