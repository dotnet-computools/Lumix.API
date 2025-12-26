using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Azure;
using Lumix.Application.PhotoUpload;
using Lumix.Infrastructure.Config;
using Lumix.Persistence.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net;

namespace Lumix.Infrastructure.PhotoUpload
{
	public class S3BucketService : IFileStorageService
	{
		private readonly IPhotoFileValidationService _photoFileValidationService;
		private readonly IPhotoResizeService _photoResizeService;
		private readonly IAmazonS3 _client;
		
		private const string BUCKET_NAME = "lumix";

		public S3BucketService(IPhotoFileValidationService photoFileValidationService, IPhotoResizeService photoResizeService, IOptions<AwsOptions> options)
		{
			var _aws = options.Value;
			_photoFileValidationService = photoFileValidationService;
			_photoResizeService = photoResizeService;
			_client = new AmazonS3Client(_aws.AccessKey, _aws.SecretKey, RegionEndpoint.GetBySystemName(_aws.Region));
		}

		public async Task<string> UploadFileToStorage(IFormFile photoFile, Guid photoId, Guid userId)
		{
			_photoFileValidationService.ValidateFile(photoFile);

			var request = new PutObjectRequest()
			{
				BucketName = BUCKET_NAME,
				Key = $"{userId}/{photoId}",
				InputStream = photoFile.OpenReadStream(),
				CannedACL = S3CannedACL.PublicRead
			};

			var responce = await _client.PutObjectAsync(request);
			if (responce.HttpStatusCode != HttpStatusCode.OK)
			{
				throw new InvalidOperationException("Failed to upload photo to storage");
			}

			var objectUrl = $"https://{BUCKET_NAME}.s3.{_client.Config.RegionEndpoint.SystemName}.amazonaws.com/{request.Key}";
			return objectUrl;
		}

		public async Task UploadThumbnailToStorage(IFormFile photoFile, Guid photoId, Guid userId)
		{
			_photoFileValidationService.ValidateFile(photoFile);

            var size = await _photoResizeService.GetSize(photoFile);

            var modifiedPhoto = _photoResizeService.IsNeedToCrop(size.Width, size.Height)
                ? await _photoResizeService.CropPhoto(photoFile)
                : await _photoResizeService.ResizePhoto(photoFile);

			var request = new PutObjectRequest()
			{
				BucketName = BUCKET_NAME,
				Key = $"{userId}/thumbnail_{photoId}",
				InputStream = modifiedPhoto.OpenReadStream(),
				CannedACL = S3CannedACL.PublicRead
            };

			var response = await _client.PutObjectAsync(request);
			if (response.HttpStatusCode != HttpStatusCode.OK)
			{
				throw new InvalidOperationException("Failed to upload thumbnail to storage");
			}
		}

		public async Task DeleteFileFromStorage(string s3Url, Guid userId)
		{
			var objectKey = GetObjectKeyFromUrl(s3Url);

			var deleteObjectRequest = new DeleteObjectRequest()
			{
				BucketName = BUCKET_NAME,
				Key = $"{userId}/{objectKey}"
			};

			var responce = await _client.DeleteObjectAsync(deleteObjectRequest);
			if (responce.HttpStatusCode != HttpStatusCode.NoContent)
			{
				throw new InvalidOperationException("Failed to delete photo from storage");
			}
		}

		public async Task DeleteThumbnailFromStorage(string s3Url, Guid userId)
		{
			var objectKey = GetObjectKeyFromUrl(s3Url, true);
			
			var deleteObjectRequest = new DeleteObjectRequest()
			{
				BucketName = BUCKET_NAME,
				Key = $"{userId}/{objectKey}"
			};

			var responce = await _client.DeleteObjectAsync(deleteObjectRequest);
			if (responce.HttpStatusCode != HttpStatusCode.NoContent)
			{
				throw new InvalidOperationException("Failed to delete thumbnail from storage");
			}
		}

		private string GetObjectKeyFromUrl(string s3Url, bool isThumbnail = false)
		{
			var objectKey = s3Url.Substring(79);

			if (!isThumbnail)
			{
				return objectKey;
			}
			return $"thumbnail_{objectKey}";
		}
	}
}
