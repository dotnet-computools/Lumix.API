namespace Lumix.Core.Entities
{
	public class User
	{
		private User(Guid id, string username, string email, string passwordHash, DateTime dateTimeNow, string? profilePictureUrl = null, string? bio = null)
		{
			Id = id;
			Username = username;
			Email = email;
			PasswordHash = passwordHash;
			ProfilePictureUrl = profilePictureUrl;
			Bio = bio;
			CreatedAt = dateTimeNow;
		}

		public Guid Id { get; }
		public string Username { get; private set; }
		public string Email { get; private set; }
		public string PasswordHash { get; private set; }
		public string? ProfilePictureUrl { get; private set; }
		public string? Bio { get; private set; }
		public DateTime CreatedAt { get; }

		public static User Create(Guid id, string username, string email, string passwordHash, DateTime dateTimeNow, string? profilePictureUrl = null, string? bio = null)
		{
			return new User(id, username, email, passwordHash, dateTimeNow, profilePictureUrl, bio);
		}
	}
}
