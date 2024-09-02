using FinShark.Server.Data;
using FinShark.Server.Dtos.Comment;
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

        public async Task<List<Comment>> GetAllAsync()
        {
            return await _context.Comment.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            return await _context.Comment.FindAsync(id);
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await _context.Comment.AddAsync(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<bool> CommentExists(int id)
        {
            return await _context.Comment.AnyAsync(x => x.Id == id);
        }

        public async Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto commentModel)
        {
            var existingComment = await _context.Comment.FindAsync(id);
            if (existingComment == null)
            {
                return null;
            }

            existingComment.Title = commentModel.Title;
            existingComment.Content = commentModel.Content;

            await _context.SaveChangesAsync();
            return existingComment;

        }

        public async Task<Comment?> DeleteAsync(int id)
        {
            var comment = await _context.Comment.FirstOrDefaultAsync(x => x.Id == id);
            if (comment == null)
            {
                return null; 
            }
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return comment;
        }
    }
}
