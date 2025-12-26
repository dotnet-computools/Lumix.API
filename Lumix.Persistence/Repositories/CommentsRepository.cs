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

		public async Task<CommentDto> AddAsync(CommentDto comment)
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

			await _context.Entry(commentEntity)
				.Reference(c => c.User)
				.LoadAsync();

			return _mapper.Map<CommentDto>(commentEntity);
        }

		public async Task<CommentDto> GetById(Guid commentId)
		{
			var comment =  await _context.Comments
				.AsNoTracking()
				.Include(c => c.User)
				.FirstOrDefaultAsync(c => c.Id == commentId) 
				?? throw new InvalidOperationException("Коментар не знайдено");
			return _mapper.Map<CommentDto>(comment);
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

        public async Task DeleteById(Guid id)
        {
            var comment = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id == id) ?? throw new InvalidOperationException("Коментар не знайдено");

            if (comment.ParentId is null)
            {
                var children = await _context.Comments
                    .Where(c => c.ParentId == id)
                    .ToListAsync();
                _context.Comments.RemoveRange(children);
            }

            _context.Comments.Remove(comment);
			await _context.SaveChangesAsync();
        }
	}
}
