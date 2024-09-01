using FinShark.Server.Models;

namespace FinShark.Server.Interfaces
{
    public interface InterfaceCommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment?> CreateAsync(Comment comment);
    }
}
