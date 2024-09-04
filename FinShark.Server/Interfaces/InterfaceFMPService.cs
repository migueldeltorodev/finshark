using FinShark.Server.Models;

namespace FinShark.Server.Interfaces
{
    public interface InterfaceFMPService
    {
        Task<Stock> FindStockBySymbolAsync(string symbol);
    }
}
