namespace PokeApi.Models
{
    public class Pokemon
    {
        object[] Abilities { get; set; }

        object[] Forms { get; set; }

        object[] GameIndices { get; set; }

        double Height { get; set; }

        object[] HeldItems { get; set; }

        int Id { get; set; }

        bool IsDefault { get; set; }

        string LocationAreaEncounters { get; set; }

        object[] Moves { get; set; }

        string Name { get; set; }

        int Order { get; set; }

        object[] PastTypes { get; set; }

        object Species { get; set; }

        object Sprites { get; set; }

        object[] Stats { get; set; }

        object[] Types { get; set; }

        double Weight { get; set; }
    }
}
