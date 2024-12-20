using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

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
				Tags = photo.Tags,
				CreatedAt = photo.CreatedAt,
				LikeCount = photo.LikeCount,
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

		public async Task<IEnumerable<PhotoDto>> GetAllByUserId(Guid userId)
		{
			var photoList = await _context.Photos
				.AsNoTracking()
				.Where(p => p.UserId == userId).ToListAsync() ?? throw new InvalidOperationException("Photo not found.");

			return _mapper.Map<IEnumerable<PhotoDto>>(photoList);
		}

		public async Task<PhotoDto> GetById(Guid id)
		{
			var photo = await _context.Photos
				.AsNoTracking()
				.FirstOrDefaultAsync(p => p.Id == id) ?? throw new InvalidOperationException("Photo not found.");

			return _mapper.Map<PhotoDto>(photo);
		}

		public async Task Update(PhotoDto photo)
		{
			var photoEntity = _mapper.Map<Photo>(photo);

			_context.Photos.Update(photoEntity);
			await _context.SaveChangesAsync();
		}
	}
}
