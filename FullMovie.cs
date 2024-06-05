namespace omdb_backend;

public class FullMovie: ShortMovie
{
    public required string Rated { get; init; }
    public required string Released { get; init; }
    public required string Runtime { get; init; }
    public required string Genre { get; init; }
    public required string Director { get; init; }
    public required string Writer { get; init; }
    public required string Actors { get; init; }
    public required string Plot { get; init; }
    public required string Language { get; init; }
    public required string Country { get; init; }
    public required string Awards { get; init; }
    public required string Metascore { get; init; }
    public required string imdbRating { get; init; }
    public required string imdbVotes { get; init; }
    public required MovieRating[] Ratings { get; init; }
    public string? DVD { get; init; }
    public string? BoxOffice { get; init; }
    public string? Production { get; init; }
    public string? Website { get; init; }
    public string? totalSeasons { get; init; }
}

public record struct MovieRating
{
    public required string Source { get; init; }
    public required string Value { get; init; }
}