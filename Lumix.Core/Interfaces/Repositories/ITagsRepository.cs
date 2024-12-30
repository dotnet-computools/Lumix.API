using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Repositories
{
	public interface ITagsRepository
	{
		Task Add(TagDto tag);
		Task AddRange(IEnumerable<TagDto> tags);
		Task<TagDto?> GetByName(string name);
	}
}
