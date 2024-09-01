using FinShark.Server.Data;
using FinShark.Server.Dtos.Stock;
using FinShark.Server.Interfaces;
using FinShark.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.Server.Repository
{
    public class StockRepository : InterfaceStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext applicationDbContext) 
        {
            _context = applicationDbContext;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);

            if(stockModel == null)
            {
                return null;
            }

            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel; 
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stock.Include(c => c.Comment).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            var stock = await _context.Stock.Include(c => c.Comment).FirstOrDefaultAsync(i => i.Id == id);
            return stock;
        }

        public async Task<bool> StockExists(int id)
        {
            return await _context.Stock.AnyAsync(x => x.Id == id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(s => s.Id == id);
            if (stockModel == null)
            {
                return null;
            }
            stockModel.Symbol = stockDto.Symbol;
            stockModel.CompanyName = stockDto.CompanyName;
            stockModel.Purchase = stockDto.Purchase;
            stockModel.MarketCap = stockDto.MarketCap;
            stockModel.Industry = stockDto.Industry;
            stockModel.LastDiv = stockDto.LastDiv;

            await _context.SaveChangesAsync();

            return stockModel;
        }
    }
}
