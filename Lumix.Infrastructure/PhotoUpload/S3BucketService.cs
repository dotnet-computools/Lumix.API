using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Lumix.Application.PhotoUpload;
using Lumix.Persistence.Entities;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Lumix.Infrastructure.PhotoUpload
{
	public class S3BucketService : IFileStorageService
	{
		private readonly IPhotoFileValidationService _photoFileValidationService;
		private readonly IPhotoResizeService _photoResizeService;
		private readonly IAmazonS3 _client;
		
		private const string BUCKET_NAME = "lumix";
		private const int THUMBNAIL_WIDTH = 300;
		private const int THUMBNAIL_HEIGHT = 300;

		public S3BucketService(IPhotoFileValidationService photoFileValidationService, IPhotoResizeService photoResizeService)
		{
			_photoFileValidationService = photoFileValidationService;
			_photoResizeService = photoResizeService;
			_client = new AmazonS3Client(RegionEndpoint.EUNorth1);
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

		public async Task UploadThumbnailToStorage(IFormFile photoFile, Guid userId)
		{
			_photoFileValidationService.ValidateFile(photoFile);
			var resizedPhoto = await _photoResizeService.ResizePhoto(photoFile, THUMBNAIL_WIDTH, THUMBNAIL_HEIGHT);

			var request = new PutObjectRequest()
			{
				BucketName = BUCKET_NAME,
				Key = $"{userId}/thumbnail_{resizedPhoto.FileName}",
				InputStream = resizedPhoto.OpenReadStream()
			};

			var responce = await _client.PutObjectAsync(request);
			if (responce.HttpStatusCode != HttpStatusCode.OK)
			{
				throw new InvalidOperationException("Failed to upload photo to storage");
			}
		}
	}
}
