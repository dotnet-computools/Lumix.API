using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Core.Interfaces.Services;

namespace Lumix.Application.Services
{
	public class TagService : ITagService
	{
		private readonly ITagsRepository _tagsRepository;

		public TagService(ITagsRepository tagsRepository)
		{
			_tagsRepository = tagsRepository;
		}
		public async Task CheckAndAddNewTags(IEnumerable<string> tags)
		{
			var newTagStrings = await GetNewTagStrings(tags);
			if (newTagStrings != null)
			{
				var newTags = ConvertToTags(newTagStrings);
				await _tagsRepository.AddRange(newTags);
			}
		}

		public async Task<IEnumerable<TagDto>> GetAllTagsFromStrings(IEnumerable<string> tags)
		{
			var tagList = new List<TagDto>();

			foreach (var tagItem in tags)
			{
				var tag = await _tagsRepository.GetByName(tagItem);
				if (tag == null)
				{
					throw new Exception("Tag wasn't found");
				}

				tagList.Add(tag);
			}
			return tagList;
		}

		private async Task<IEnumerable<string>> GetNewTagStrings(IEnumerable<string> tags)
		{
			var newTags = new List<string>();
			foreach (var tagItem in tags)
			{
				var tag = await _tagsRepository.GetByName(tagItem);

				if (tag != null)
				{
					continue;
				}
				newTags.Add(tagItem);
			}

			return newTags;
		}

		private IEnumerable<TagDto> ConvertToTags(IEnumerable<string> newTagStrings)
		{
			var newTagsList = new List<TagDto>();

			foreach (var tagString in newTagStrings)
			{
				var tag = TagDto.Create(
					Guid.NewGuid(),
					tagString,
					DateTime.UtcNow);

				newTagsList.Add(tag);
			}

			return newTagsList;
		}
	}
}
