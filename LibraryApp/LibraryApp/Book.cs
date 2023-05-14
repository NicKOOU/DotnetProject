namespace LibraryApp
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsRead { get; set; }
        public string? PhotoUrl { get; set; } // Nullable book photo URL
        public int? Year { get; set; } // Nullable year
        public string? Language { get; set; } // Nullable language
        public string? Type { get; set; } // Nullable type
        public int? Grade { get; set; } // Nullable grade

        public string? Description { get; set; } // Nullable description
    }
}
