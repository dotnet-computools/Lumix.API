using Lumix.Application.PhotoUpload;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Lumix.Infrastructure.PhotoUpload
{
	public class PhotoResizeService : IPhotoResizeService
	{
		private const int THUMBNAIL_WIDTH = 300;
		private const int THUMBNAIL_HEIGHT = 300;

		public async Task<IFormFile> ResizePhoto(IFormFile file)
		{
			using (var inputStream = file.OpenReadStream())
			using (var image = await Image.LoadAsync(inputStream))
			{
				image.Mutate(img => img.Resize(THUMBNAIL_WIDTH, THUMBNAIL_HEIGHT));

				var outputStream = new MemoryStream();
				await image.SaveAsJpegAsync(outputStream);
				
				outputStream.Position = 0;

				return new FormFile(outputStream, 0, outputStream.Length, file.Name, file.FileName)
				{
					Headers = file.Headers,
					ContentType = file.ContentType
				};
			}
		}
	}
}
