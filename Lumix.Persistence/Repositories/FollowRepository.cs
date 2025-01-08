using AutoMapper;
using Lumix.Core.DTOs;
using Lumix.Core.Interfaces.Repositories;
using Lumix.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lumix.Persistence.Repositories;

public class FollowRepository : IFollowRepository
{
     private readonly LumixDbContext _context;
     private readonly IMapper _mapper;
     
     public FollowRepository(LumixDbContext context, IMapper mapper)
     {
         _context = context;
         _mapper = mapper;
     }
     
     public async Task FollowUser(Guid userId, Guid targetUserId)
     {
         var follow = new Follow
         {
             Id = Guid.NewGuid(),
             FollowerId = userId,
             FollowingId = targetUserId,
             CreatedAt = DateTime.UtcNow
         };
         
         _context.Follows.Add(follow);
         await _context.SaveChangesAsync();
     }
     
     public async Task<bool> IsFollowing(Guid userId, Guid targetUserId)
     {
         return await _context.Follows
             .AnyAsync(f => f.FollowerId == userId && f.FollowingId == targetUserId);
     }
     
     public async Task<List<Guid>> GetFollowing(Guid userId)
     {
         var followings = await _context.Follows
             .Where(f => f.FollowerId == userId)
             .Select(f => f.FollowingId) 
             .ToListAsync();

         return followings;
     }
}