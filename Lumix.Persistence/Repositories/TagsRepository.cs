using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence.Repositories
{
	public class TagsRepository : ITagsRepository
	{
		private readonly LumixDbContext _context;
		private readonly IMapper _mapper;

		public TagsRepository(LumixDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task Add(TagDto tag)
		{
			var tagEntity = new Tag()
			{
				Id = tag.Id,
				Name = tag.Name,
				CreatedAt = tag.CreatedAt,
			};

			await _context.Tags.AddAsync(tagEntity);
			await _context.SaveChangesAsync();
		}

		public async Task AddRange(IEnumerable<TagDto> tags)
		{
			var tagEntitiesList = new List<Tag>();

			foreach (var tag in tags)
			{
				var tagEntity = new Tag()
				{
					Id = tag.Id,
					Name = tag.Name,
					CreatedAt = DateTime.UtcNow
				};

				tagEntitiesList.Add(tagEntity);
			}

			await _context.Tags.AddRangeAsync(tagEntitiesList);
			await _context.SaveChangesAsync();
		}

		public async Task<TagDto?> GetByName(string name)
		{
			var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
			return _mapper.Map<TagDto>(tag);
		}
	}
}
