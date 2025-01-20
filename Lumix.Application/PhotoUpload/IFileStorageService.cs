using Microsoft.AspNetCore.Http;

namespace Lumix.Application.PhotoUpload
{
	public interface IFileStorageService
	{
		Task<string> UploadFileToStorage(IFormFile photoFile, Guid photoId, Guid userId);
		Task UploadThumbnailToStorage(IFormFile photoFile, Guid photoId, Guid userId);
		Task DeleteFileFromStorage(string s3Url, Guid userId);
		Task DeleteThumbnailFromStorage(string s3Url, Guid userId);
	}
}
