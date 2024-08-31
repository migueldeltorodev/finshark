using FinShark.Server.Data;
using FinShark.Server.Dtos.Stock;
using FinShark.Server.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;  
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            var stocks = _context.Stock.ToList().Select(s => s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute]int id)
        {
            var stock = _context.Stock.Find(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateStockDto();
            _context.Stock.Add(stockModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
        }
    }
}
