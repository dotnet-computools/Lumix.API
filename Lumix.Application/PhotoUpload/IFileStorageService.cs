using Microsoft.AspNetCore.Http;

namespace Lumix.Application.PhotoUpload
{
	public interface IFileStorageService
	{
		Task<string> UploadFileToStorage(IFormFile photoFile, Guid userId);
	}
}
