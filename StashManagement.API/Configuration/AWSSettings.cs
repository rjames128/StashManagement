namespace StashManagement.API.Configuration
{
    public class AWSSettings
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; }
        public string StashItemBucketName { get; set; }
        public bool IsLocal { get; set; }
        public string LocalUrl { get; set; }
    }
}
