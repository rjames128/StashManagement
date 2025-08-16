namespace StashManagement.API.DAOs
{
    public class StashItemDAO
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SourceLocation { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string ImageSrc { get; set; }
    }
}
