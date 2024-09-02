using FinShark.Server.Dtos.Comment;
using FinShark.Server.Models;

namespace FinShark.Server.Interfaces
{
    public interface InterfaceCommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> CreateAsync(Comment comment);
        Task<bool> CommentExists(int id);
        Task<Comment?> UpdateAsync(int id, UpdateCommentRequestDto comment);
        Task<Comment?> DeleteAsync(int id);
    }
}
