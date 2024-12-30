using Lumix.Core.DTOs;

namespace Lumix.Core.Interfaces.Services
{
	public interface ITagService
	{
		Task CheckAndAddNewTags(IEnumerable<string> tags);
		Task<IEnumerable<TagDto>> GetAllTagsFromStrings(IEnumerable<string> tags);
	}
}
