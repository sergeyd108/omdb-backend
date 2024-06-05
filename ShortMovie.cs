namespace omdb_backend;

public class ShortMovie
{
    public required string imdbID { get; init; }
    public required string Type { get; init; }
    public required string Title { get; init; }
    public required string Year { get; init; }
    public required string Poster { get; init; }
}