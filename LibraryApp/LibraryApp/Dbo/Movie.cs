namespace LibraryApp.Dbo
{
    public class Movie : IObjectWithId
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsWatched { get; set; }
        public string? PosterUrl { get; set; } // Nullable book photo URL
        public int? Year { get; set; } // Nullable year
        public string? Language { get; set; } // Nullable language
        public string? Genre { get; set; } // Nullable type
        public int? Grade { get; set; } // Nullable grade

        public int? Duration { get; set; } // Nullable duration
        public string? Description { get; set; } // Nullable description
    }
}
