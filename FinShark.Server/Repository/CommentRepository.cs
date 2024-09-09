using FinShark.Server.Data;
using FinShark.Server.Dtos.Comment;
using FinShark.Server.Helpers;
using FinShark.Server.Interfaces;
using FinShark.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Server.Repository
{
    public class CommentRepository : InterfaceCommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<List<Comment>> GetAllAsync(CommentQueryObject detailsQuery)
        {
            var comments = _context.Comment.Include(a => a.AppUser).AsQueryable();

            if(!string.IsNullOrWhiteSpace(detailsQuery.Symbol))
                comments = comments.Where(s => s.Stock.Symbol == detailsQuery.Symbol);

            if(detailsQuery.IsDescending == true)
                comments = comments.OrderByDescending(c => c.CreatedOn);
            
            return await comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comment.Include(a => a.AppUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Comment?> CreateAsync(Comment comment)
        {
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comment.AnyAsync(x => x.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto updateModel)
        {
            var existingComment = await _context.Comment.FindAsync(id);

            if (existingComment == null)
                return null;

            existingComment.Title = updateModel.Title;
            existingComment.Content = updateModel.Content;

            await _context.SaveChangesAsync();
            return existingComment;

        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var commentModel = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);

            if (commentModel == null)
                return null; 
                
            _context.Comment.Remove(commentModel);
            await _context.SaveChangesAsync();

            return commentModel;
        }
    }
}
