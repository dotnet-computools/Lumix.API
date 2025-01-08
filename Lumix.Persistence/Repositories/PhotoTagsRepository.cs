using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence.Repositories
{
	public class PhotoTagsRepository : IPhotoTagsRepository
	{
		private readonly LumixDbContext _context;
		private readonly IMapper _mapper;

		public PhotoTagsRepository(LumixDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task Add(PhotoTagDto photoTag)
		{
			var photoTagEntity = new PhotoTag()
			{
				Id = photoTag.Id,
				TagId = photoTag.TagId,
				PhotoId = photoTag.PhotoId,
				CreatedAt = photoTag.CreatedAt
			};

			await _context.PhotoTags.AddAsync(photoTagEntity);
			await _context.SaveChangesAsync();
		}

		public async Task AddRange(IEnumerable<PhotoTagDto> photoTags)
		{
			var photoTagsEntities = new List<PhotoTag>();

			foreach (var photoTag in photoTags)
			{
				var photoTagEntity = new PhotoTag()
				{
					Id = photoTag.Id,
					TagId = photoTag.TagId,
					PhotoId = photoTag.PhotoId,
					CreatedAt = photoTag.CreatedAt
				};

				photoTagsEntities.Add(photoTagEntity);
			}

			await _context.PhotoTags.AddRangeAsync(photoTagsEntities);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<PhotoTagDto>> GetByPhotoId(Guid photoId)
		{
			var photoTags = await _context.PhotoTags
				.AsNoTracking()
				.Where(pt => pt.PhotoId == photoId)
                .Include(x => x.Tag)
				.ToListAsync() ?? throw new InvalidOperationException("Tags not found.");

			return _mapper.Map<IEnumerable<PhotoTagDto>>(photoTags);
		}

		public async Task<IEnumerable<PhotoTagDto>> GetAll()
		{
			var photoTags = await _context.PhotoTags
				.AsNoTracking()
				.ToListAsync();

			return _mapper.Map<IEnumerable<PhotoTagDto>>(photoTags);
		}

		public async Task DeleteAllByPhotoId(Guid photoId)
		{
			var photoTagsToDelete = await _context.PhotoTags.Where(pt => pt.PhotoId == photoId).ToListAsync();

			_context.RemoveRange(photoTagsToDelete);
			await _context.SaveChangesAsync();
		}
	}
}
