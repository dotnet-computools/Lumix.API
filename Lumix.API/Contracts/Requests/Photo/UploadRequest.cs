namespace Lumix.API.Contracts.Requests.Photo
{
	public class UploadRequest
	{
		public IFormFile? PhotoFile { get; set; }
		public string? Title { get; set; }
		public List<string> Tags { get; set; } = new();
		public bool IsAvatar { get; set; }
    }
}
