namespace Lumix.API.Contracts.Requests.Photo
{
	public class UploadRequest
	{
		public IFormFile PhotoFile { get; set; } = null!;
		public string Title { get; set; } = string.Empty;
		public string Tags { get; set; } = string.Empty;
	}
}
