using FinShark.Server.Models;

namespace FinShark.Server.Interfaces
{
    public interface InterfaceStockRepository
    {
        Task<List<Stock>> GetAllAsync();
    }
}
