namespace Lumix.API.Contracts.Requests.Photo
{
	public class UpdateRequest
	{
		public string Title { get; set; } = string.Empty;
		public IEnumerable<string>? Tags { get; set; }
	}
}
