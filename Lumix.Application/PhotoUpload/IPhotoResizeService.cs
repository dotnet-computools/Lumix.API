using Microsoft.AspNetCore.Http;

namespace Lumix.Application.PhotoUpload
{
	public interface IPhotoResizeService
	{
		Task<IFormFile> ResizePhoto(IFormFile file, int width, int height);
	}
}
