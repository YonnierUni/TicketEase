namespace TicketEase.Service.Movies.Events
{
    public class MovieAddedEvent
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string PosterImage { get; set; }
    }
    public class MovieUpdatedEvent
    {
        public Guid MovieId { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string PosterImage { get; set; }
    }
    public class MovieDeletedEvent
    {
        public Guid MovieId { get; set; }
    }
}
