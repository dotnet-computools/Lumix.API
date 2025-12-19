using Lumix.Application.PhotoUpload;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace Lumix.Application.Services
{
	public class PhotoService : IPhotoService
	{
		private readonly IPhotosRepository _photosRepository;
		private readonly IFileStorageService _storageService;
		private readonly ILogger<PhotoService> _logger;

        private const int PHOTOS_COUNT_ON_PAGE = 10;

		public PhotoService(IPhotosRepository photosRepository, IFileStorageService storageService, ILogger<PhotoService> logger)
		{
			_photosRepository = photosRepository;
			_storageService = storageService;
			_logger = logger;
        }

		public async Task<Guid> Upload(string title, string url, Guid photoId, Guid userId, bool isAvatar)
		{
			var photo = new PhotoDto
			{
				Id = photoId,
				UserId = userId,
				Title = title,
				Url = url,
				CreatedAt = DateTime.UtcNow,
				IsAvatar = isAvatar
            };
				

			await _photosRepository.Add(photo);
			return photo.Id;
		}

		public async Task<PhotoDto> GetById(Guid id)
		{
			return await _photosRepository.GetById(id);
		}

		public async Task<IEnumerable<PhotoDto>> GetByIds(IEnumerable<Guid> photosId)
		{
			var photos = new List<PhotoDto>();

			foreach (var photoIdItem in photosId)
			{
				var photo = await _photosRepository.GetById(photoIdItem);
				photos.Add(photo);
			}

			return photos.OrderByDescending(p => p.CreatedAt);
		}

		public async Task<IEnumerable<PhotoDto>> GetAll()
		{
			return await _photosRepository.GetAll();
		}

		public IEnumerable<PhotoDto> GetFromCollectionByPage(IEnumerable<PhotoDto> photos, int pageNumber)
		{
			var pagedPhotos = photos
				.Skip((pageNumber - 1) * PHOTOS_COUNT_ON_PAGE)
				.Take(PHOTOS_COUNT_ON_PAGE);

			return pagedPhotos;
		}

		public async Task<IEnumerable<PhotoDto>> GetAllUserPhotos(Guid userId)
		{
			return await _photosRepository.GetAllByUserId(userId);
		}

		public async Task<bool> IsPhotoBelongToUser(Guid userId, Guid photoId)
		{
			var photo = await _photosRepository.GetByUserAndPhotoId(userId, photoId);

			return photo != null;
		}

		public async Task UpdateInfo(PhotoDto photoToUpdate, string newTitle)
		{

			await _photosRepository.Update(photoToUpdate);
		}

		public async Task Delete(Guid photoId, Guid currentUserId)
        {
            var photo = await GetPhotoIfBelongsToUser(photoId, currentUserId);

			await _photosRepository.DeleteById(photo.Id);
        }

        public async Task FullDelete(Guid photoId, Guid currentUserId)
        {
            var photo = await GetPhotoIfBelongsToUser(photoId, currentUserId);

            await _photosRepository.DeleteById(photo.Id);

            try
            {
                await _storageService.DeleteFileFromStorage(photo.Url, currentUserId);
                await _storageService.DeleteThumbnailFromStorage(photo.Url, currentUserId);
            }catch(InvalidOperationException ex)
			{
                _logger.LogError(ex,
                    "Failed to delete photo from S3. PhotoId={PhotoId}, Url={Url}, UserId={UserId}",
                    photo.Id, photo.Url, currentUserId);
            }
        }

        private async Task<PhotoDto> GetPhotoIfBelongsToUser(Guid photoId, Guid userId)
        {
            PhotoDto photo;
            try
            {
                photo = await _photosRepository.GetById(photoId);
            }
            catch (InvalidOperationException)
            {
                throw new KeyNotFoundException("Фото не знайдено.");
            }

            if (photo.UserId != userId)
                throw new UnauthorizedAccessException("Ви не є власником фото.");

			return photo;
        }
	}
}
