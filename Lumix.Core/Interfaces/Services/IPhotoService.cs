﻿using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services
{
	public interface IPhotoService
	{
		Task Upload(string title, string tags, string url, Guid userId);
		Task<PhotoDto> GetById(Guid id);
		Task<IEnumerable<PhotoDto>> GetAll();
		Task<bool> IsPhotoBelongToUser(Guid userId, Guid photoId);
		Task<IEnumerable<PhotoDto>> GetAllUserPhotos(Guid userId);
		Task UpdateInfo(PhotoDto photoToUpdate, string newTitle, string newTags);
		Task Delete(Guid id);
	}
}
