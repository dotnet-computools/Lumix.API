using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence.Repositories
{
	public class CommentsRepository : ICommentsRepository
	{
		private readonly LumixDbContext _context;
		private readonly IMapper _mapper;

		public CommentsRepository(LumixDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task Add(CommentDto comment)
		{
			var commentEntity = new Comment()
			{
				Id = comment.Id,
				UserId = comment.UserId,
				PhotoId = comment.PhotoId,
				Text = comment.Text,
				CreatedAt = comment.CreatedAt,
				ParentId = comment.ParentId
			};

			await _context.Comments.AddAsync(commentEntity);
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<CommentDto>?> GetByPhotoId(Guid photoId)
		{
			var comments = await _context.Comments
				.AsNoTracking()
				.Where(p => p.PhotoId == photoId)
				.Include(c => c.User)
				.OrderBy(c => c.CreatedAt)
                .ToListAsync();

			return _mapper.Map<IEnumerable<CommentDto>?>(comments);
		}
	}
}
