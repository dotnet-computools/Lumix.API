using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services
{
	public class LikeService : ILikeService
	{
		private readonly ILikesRepository _likesRepository;
		private readonly IPhotosRepository _photoRepository;

		public LikeService(ILikesRepository likesRepository, IPhotosRepository photosRepository)
		{
			_likesRepository = likesRepository;
			_photoRepository = photosRepository;
		}

		public async Task Like(Guid userId, Guid photoId)
		{
			var photo = await _photoRepository.GetById(photoId);
			var like = LikeDto.Create(
				Guid.NewGuid(),
				userId,
				photoId);

			await _likesRepository.Add(like);
			await _photoRepository.Update(photo);
		}

		public async Task<bool> IsUserLikedPhoto(Guid userId, Guid photoId)
		{
			var like = await _likesRepository.GetByUserPhotoId(userId, photoId);

			return like != null;
		}

		public async Task Remove(Guid userId, Guid photoId)
		{
			var photo = await _photoRepository.GetById(photoId);

			await _likesRepository.DeleteByUserPhotoId(userId, photoId);

			await _photoRepository.Update(photo);
		}
	}
}
