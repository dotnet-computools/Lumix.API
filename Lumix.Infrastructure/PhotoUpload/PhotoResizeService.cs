using Lumix.Application.PhotoUpload;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Lumix.Infrastructure.PhotoUpload
{
	public class PhotoResizeService : IPhotoResizeService
	{
		public async Task<IFormFile> ResizePhoto(IFormFile file, int width, int height)
		{
			using (var inputStream = file.OpenReadStream())
			using (var image = await Image.LoadAsync(inputStream))
			{
				image.Mutate(img => img.Resize(width, height));

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
