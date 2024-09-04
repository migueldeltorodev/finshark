using FinShark.Server.Data;
using FinShark.Server.Extensions;
using FinShark.Server.Interfaces;
using FinShark.Server.Models;
using FinShark.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        //Refactorizar en el futuro
        private readonly UserManager<AppUser> _userManager;
        private readonly InterfaceStockRepository _stockRepo;
        private readonly InterfacePortfolioRepository _portfolioRepo;
        private readonly InterfaceFMPService _fmpService;
        public PortfolioController(UserManager<AppUser> userManager, InterfaceStockRepository stockRepo, InterfacePortfolioRepository interfacePortfolio, InterfaceFMPService fmpService) 
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = interfacePortfolio;
            _fmpService = fmpService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            //User de por si viene con el controller base, cada vez que creas un endpoind, de por si, 
            //un contexto http es creado y el User. te permite reunir los datos sobre el usuario actual, 
            //es decir: los claims
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            try
            {
                //Obtenemos el usuario actual
                var username = User.GetUsername();
                var appUser = await _userManager.FindByNameAsync(username);
                //Buscamos si existe o no el stock por su symbol
                var stock = await _stockRepo.GetBySymbolAsync(symbol);

                //Si el stock no existe
                if (stock == null)
                {
                    //Buscamos desde el httpClient el stock
                    stock = await _fmpService.FindStockBySymbolAsync(symbol);
                    //Si no existe dicho stock, envia un bad request, si existe, lo almacena en la db
                    if (stock == null)
                    {
                        return BadRequest("This stock doesn't exist");
                    }
                    else
                    {
                        await _stockRepo.CreateAsync(stock);
                    }
                }
                //Obtenemos el portfolio actual del usuario
                var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

                //Si ya contiene el stock, nos aseguramos de no añadirlo duplicado
                if (userPortfolio.Any(s => s.Symbol.ToLower() == symbol.ToLower()))
                    return BadRequest("Cannot add same stock");

                //De no existir, se crea o actualiza el portfolio con el nuevo stock (con la FK)
                var portfolioModel = new Portfolio
                {
                    StockId = stock.Id,
                    AppUserId = appUser.Id,
                };

                await _portfolioRepo.CreateAsync(portfolioModel);

                if (portfolioModel == null)
                    return StatusCode(500, "Could not create a portfolio");
                else
                    return Created();
            }catch(Exception ex)
            {
                return StatusCode(500, $"Failed to add portfolio: {ex.Message}");
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            var filteredStocks = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if(filteredStocks.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock not in your portfolio");
            }

            return Ok();
        }

    }
}
