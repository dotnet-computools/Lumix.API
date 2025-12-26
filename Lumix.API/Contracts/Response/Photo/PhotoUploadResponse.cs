namespace Lumix.API.Contracts.Response.Photo
{
    public class PhotoUploadResponse
    {
        public Guid PhotoId { get; set; }
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
