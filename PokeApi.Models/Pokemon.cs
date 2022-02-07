using Newtonsoft.Json;

namespace PokeApi.Models
{
    public class Pokemon
    {
        [JsonProperty("abilities")]
        public object Abilities { get; set; }

        public object BaseExperience { get; set; }

        [JsonProperty("forms")]
        public object Forms { get; set; }

        public object GameIndices { get; set; }

        public double Height { get; set; }

        public object[] HeldItems { get; set; }

        public int Id { get; set; }

        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }

        public string LocationAreaEncounters { get; set; }

        public object[] Moves { get; set; }

        public string Name { get; set; }

        public int Order { get; set; }

        public object[] PastTypes { get; set; }

        public object Species { get; set; }

        public object Sprites { get; set; }

        public object[] Stats { get; set; }

        public object[] Types { get; set; }

        public double Weight { get; set; }
    }
}
