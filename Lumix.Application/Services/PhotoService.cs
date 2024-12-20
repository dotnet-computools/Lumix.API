﻿using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services
{
	public class PhotoService : IPhotoService
	{
		private readonly IPhotosRepository _photosRepository;

		public PhotoService(IPhotosRepository photosRepository)
		{
			_photosRepository = photosRepository;
		}

		public async Task Upload(string title, string tags, string url, Guid userId)
		{
			var photo = PhotoDto.Create(
				Guid.NewGuid(), 
				userId, 
				title, 
				url, 
				tags);

			await _photosRepository.Add(photo);
		}

		public async Task<PhotoDto> GetById(Guid id)
		{
			return await _photosRepository.GetById(id);
		}

		public async Task<IEnumerable<PhotoDto>> GetAll()
		{
			return await _photosRepository.GetAll();
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

		public async Task UpdateInfo(PhotoDto photoToUpdate, string newTitle, string newTags)
		{
			photoToUpdate.Update(newTitle, newTags);

			await _photosRepository.Update(photoToUpdate);
		}

		public async Task Delete(Guid id)
		{
			await _photosRepository.DeleteById(id);
		}
	}
}