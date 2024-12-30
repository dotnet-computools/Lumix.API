using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;

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
	}
}
