using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Lumix.Application.PhotoUpload;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Lumix.Infrastructure.PhotoUpload
{
	public class S3BucketService : IFileStorageService
	{
		private readonly IPhotoFileValidationService _photoFileValidationService;
		private readonly IAmazonS3 _client = new AmazonS3Client(RegionEndpoint.EUNorth1);
		private const string BUCKET_NAME = "lumix";

		public S3BucketService(IPhotoFileValidationService photoFileValidationService)
		{
			_photoFileValidationService = photoFileValidationService;
		}

		public async Task<string> UploadFileToStorage(IFormFile photoFile, Guid userId)
		{
			_photoFileValidationService.ValidateFile(photoFile);

			var request = new PutObjectRequest()
			{
				BucketName = BUCKET_NAME,
				Key = $"{userId}/{photoFile.FileName}",
				InputStream = photoFile.OpenReadStream()
			};

			var responce = await _client.PutObjectAsync(request);
			if (responce.HttpStatusCode != HttpStatusCode.OK)
			{
				throw new InvalidOperationException("Failed to upload photo to storage");
			}

			var objectUrl = $"https://{BUCKET_NAME}.s3.{_client.Config.RegionEndpoint.SystemName}.amazonaws.com/{request.Key}";
			return objectUrl;
		}
	}
}
