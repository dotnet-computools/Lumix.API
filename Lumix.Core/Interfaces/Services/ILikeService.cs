namespace Lumix.Core.Interfaces.Services
{
	public interface ILikeService
	{
		Task Like(Guid userId, Guid photoId);
		Task Remove(Guid userId, Guid photoId);
		Task<bool> IsUserLikedPhoto(Guid userId, Guid photoId);
	}
}
