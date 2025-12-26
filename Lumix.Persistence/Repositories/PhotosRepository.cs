using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Lumix.Persistence.Repositories
{
	public class PhotosRepository : IPhotosRepository
	{
		private readonly LumixDbContext _context;
		private readonly IMapper _mapper;

		public PhotosRepository(LumixDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task Add(PhotoDto photo)
		{
			var photoEntity = new Photo()
			{
				Id = photo.Id,
				UserId = photo.UserId,
				Title = photo.Title,
				Url = photo.Url,
				CreatedAt = photo.CreatedAt,
				LikeCount = photo.LikeCount,
				IsAvatar = photo.IsAvatar
            };

			await _context.Photos.AddAsync(photoEntity);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteById(Guid id)
		{
			var photo = await _context.Photos
				.AsNoTracking()
				.FirstOrDefaultAsync(p => p.Id == id) ?? throw new InvalidOperationException("Photo not found.");

			_context.Photos.Remove(photo);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<PhotoDto>> GetAll()
		{
			var photoList = await _context.Photos
				.AsNoTracking()
				.OrderByDescending(p => p.CreatedAt)
				.ToListAsync();

			return _mapper.Map<IEnumerable<PhotoDto>>(photoList);
		}

		public async Task<IEnumerable<PhotoDto>> GetAllByUserId(Guid userId)
		{
			var photoList = await _context.Photos
				.AsNoTracking()
				.Where(p => p.UserId == userId)
				.Where(p => !p.IsAvatar)
				.OrderByDescending(p => p.CreatedAt)
				.ToListAsync() ?? throw new InvalidOperationException("Photo not found.");

			return _mapper.Map<IEnumerable<PhotoDto>>(photoList);
		}

		public async Task<PhotoDto> GetById(Guid id)
		{
            var photo = await _context.Photos
				.AsNoTracking()
				.Include(p => p.User)
				.Include(p => p.PhotoTags)
					.ThenInclude(pt => pt.Tag)
				.Include(p => p.Comments)
				.Include(p => p.Likes)
				.FirstOrDefaultAsync(p => p.Id == id);

            if (photo is null)
                throw new InvalidOperationException("Photo not found.");

            return _mapper.Map<PhotoDto>(photo);
        }

		public async Task<PhotoDto> GetByUserAndPhotoId(Guid userId, Guid photoId)
		{
			var photo = await _context.Photos
				.AsNoTracking()
				.FirstOrDefaultAsync(p => p.Id == photoId && p.UserId == userId) ?? throw new InvalidOperationException("Operation not available");

			return _mapper.Map<PhotoDto>(photo);
		}

		public async Task Update(PhotoDto photo)
		{
			var photoEntity = new Photo()
			{
				Id = photo.Id,
				UserId = photo.UserId,
				Title = photo.Title,
				Url = photo.Url,
				CreatedAt = photo.CreatedAt,
				LikeCount = photo.LikeCount,
				IsAvatar = photo.IsAvatar
            };

			_context.Photos.Update(photoEntity);
			await _context.SaveChangesAsync();
		}
	}
}
