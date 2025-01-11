using Microsoft.AspNetCore.Http;

namespace Lumix.Application.PhotoUpload
{
	public interface IPhotoFileValidationService
	{
		void ValidateFile(IFormFile photoFile);
	}
}
