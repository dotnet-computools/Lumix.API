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

        public async Task<IFormFile> CropPhoto(IFormFile file)
        {
            using (var inputStream = file.OpenReadStream())
            using (var image = await Image.LoadAsync(inputStream))
            {
                var cropRectangle = new Rectangle(
                    (image.Width - THUMBNAIL_WIDTH) / 2,  
                    (image.Height - THUMBNAIL_HEIGHT) / 2, 
                    Math.Min(THUMBNAIL_WIDTH, image.Width),  
                    Math.Min(THUMBNAIL_HEIGHT, image.Height)
                );

                image.Mutate(img => img.Crop(cropRectangle));

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

        public async Task<(float Width, float Height)> GetSize(IFormFile file)
        {
            using (var inputStream = file.OpenReadStream())
            using (var image = await Image.LoadAsync(inputStream))
            {
                return (image.Width, image.Height);
            }
        }

        public bool IsNeedToCrop(float width, float height)
        {
            return width > height * 1.25 || height > width * 1.25;
        }
    }
}
