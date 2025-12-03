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
			var uniqueTags = tags.Distinct();
			var newTagsNames = await GetNewTagsNames(uniqueTags);
			if (newTagsNames.Any())
			{
				var newTags = ConvertToTags(newTagsNames);
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

		public async Task<IEnumerable<TagDto>> GetAllByPhotoTags(IEnumerable<PhotoTagDto> photoTags)
		{
			var tagList = new List<TagDto>();

			foreach (var tagItem in photoTags)
			{
				var tag = await _tagsRepository.GetById(tagItem.TagId);
				tagList.Add(tag);
			}

			return tagList;
		}

        public IEnumerable<TagDto> GetAllByPhotoTagsByInclude(IEnumerable<PhotoTagDto> photoTags)
        {
            var tags = photoTags
                .Select(x => x.Tag)
                .Select(tag => new TagDto { Id = tag.Id, Name = tag.Name, CreatedAt = tag.CreatedAt });

            return tags;
        }

        private async Task<IEnumerable<string>> GetNewTagsNames(IEnumerable<string> tags)
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
				var tag = new TagDto
				{
					Id = Guid.NewGuid(),
					Name = tagString,
					CreatedAt = DateTime.UtcNow
				};

                newTagsList.Add(tag);
			}

			return newTagsList;
		}
	}
}
