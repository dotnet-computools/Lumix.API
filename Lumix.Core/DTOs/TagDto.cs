namespace Lumix.Core.DTOs
{
	public class TagDto
	{
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
			if (name.Length > 30) throw new ArgumentException("Tag name can't be longer than 30 characters");

			return new TagDto(id, name, createdAt);
		}
	}
}
