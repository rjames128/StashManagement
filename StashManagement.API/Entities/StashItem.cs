namespace StashManagement.API.Entities
{
    public record StashItem
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

    public record FabricItem : StashItem
    {
        public string Cut { get; set; }
        public decimal Amount { get; set; }
    }
}
