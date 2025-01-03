using System.Text.RegularExpressions;

namespace Lumix.Core.DTOs
{
	public class TagDto
	{
		private const string PATTERN = @"^#[a-zA-Z0-9]{0,29}$";
		private readonly List<PhotoTagDto> _photoTags = new();

		private TagDto(
			Guid id,
			string name,
			DateTime createdAt)
		{
			Id = id;
			Name = name;
			CreatedAt = createdAt;
		}

		public Guid Id { get; }
		public string Name { get; } = string.Empty;
		public DateTime CreatedAt { get; }

		public IReadOnlyList<PhotoTagDto> PhotoTags => _photoTags;

		public static TagDto Create(
			Guid id,
			string name,
			DateTime createdAt)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("Tag name cannot be empty");
			if (name.Length > 30) throw new ArgumentException("Tag name cannot be longer than 30 characters");
			if (!Regex.IsMatch(name, PATTERN)) 
				throw new ArgumentException("Make sure that the tag begins with the \"#\" symbol and does not contain special characters or spaces");

			return new TagDto(id, name.ToLower(), createdAt);
		}
	}
}
