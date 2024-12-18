﻿namespace Lumix.Persistence.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public string Username { get; set; } = string.Empty;
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public string ProfilePictureUrl { get; set; } = string.Empty;
		public string Bio { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; }
	}
}