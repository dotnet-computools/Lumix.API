using Microsoft.AspNetCore.Http;

namespace Lumix.Application.PhotoUpload
{
	public interface IPhotoResizeService
	{
		Task<IFormFile> ResizePhoto(IFormFile file);
		Task<IFormFile> CropPhoto(IFormFile file);

        Task<(float Width, float Height)> GetSize(IFormFile file);
        bool IsNeedToCrop(float Width, float Height);
    }
}
