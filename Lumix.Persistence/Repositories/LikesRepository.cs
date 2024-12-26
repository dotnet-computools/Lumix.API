using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence.Repositories
{
	public class LikesRepository : ILikesRepository
	{
		private readonly LumixDbContext _context;
		private readonly IMapper _mapper;

		public LikesRepository(LumixDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task Add(LikeDto like)
		{
			var likeEntity = new Like()
			{
				Id = like.Id,
				UserId = like.UserId,
				PhotoId = like.PhotoId,
				CreatedAt = DateTime.UtcNow
			};

			await _context.AddAsync(likeEntity);
			await _context.SaveChangesAsync();
		}

		public async Task<LikeDto?> GetByUserPhotoId(Guid userId, Guid photoId)
		{
			var like = await _context.Likes
				.AsNoTracking()
				.FirstOrDefaultAsync(l => l.UserId == userId && l.PhotoId == photoId);
			return _mapper.Map<LikeDto>(like);
		}

		public async Task DeleteByUserPhotoId(Guid userId, Guid photoId)
		{
			var like = await _context.Likes
				.AsNoTracking()
				.FirstOrDefaultAsync(l => l.UserId == userId && l.PhotoId == photoId) ?? throw new InvalidOperationException("Like not found");

			_context.Likes.Remove(like);
			await _context.SaveChangesAsync();
		}
	}
}
