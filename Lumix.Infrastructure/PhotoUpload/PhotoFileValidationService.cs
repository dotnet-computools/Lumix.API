using Lumix.Application.PhotoUpload;
using Microsoft.AspNetCore.Http;

namespace Lumix.Infrastructure.PhotoUpload
{
	public class PhotoFileValidationService : IPhotoFileValidationService
	{
		private readonly IReadOnlyCollection<string> allowedFileFormats = [".jpg", ".png"];
		private const int MAX_FILE_SIZE = 15 * 1024 * 1024;

		public void ValidateFile(IFormFile photoFile)
		{
			if (photoFile == null)
			{
				throw new ArgumentNullException("Photo file cannot be empty");
			}

			if (photoFile.Length > MAX_FILE_SIZE)
			{
				throw new ArgumentException("Photo file size cannot exceed 15 Mb");
			}

			var fileFormat = Path.GetExtension(photoFile.FileName).ToLower();
			if (!allowedFileFormats.Contains(fileFormat))
			{
				throw new ArgumentException("File type is not photo");
			}
		}
	}
}
