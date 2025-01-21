namespace TicketEase.Service.Movies.Entities
{
    public class Movie
    {
        public Guid MovieId { get; set; }  // GUID para MovieId
        public string Title { get; set; }
        public int Year { get; set; }
        public string Genre { get; set; }
        public string Director { get; set; }
        public string Cast { get; set; }
        public string PosterImage { get; set; }
    }
}
