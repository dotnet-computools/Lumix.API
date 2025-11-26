namespace Lumix.API.Contracts.Requests.Photo
{
	public class UploadRequest
	{
		public IFormFile? PhotoFile { get; set; }
		public string Title { get; set; } = string.Empty;
		public List<string> Tags { get; set; } = new();
	}
}
