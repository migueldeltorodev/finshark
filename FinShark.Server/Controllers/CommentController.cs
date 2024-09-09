using FinShark.Server.Dtos.Comment;
using FinShark.Server.Extensions;
using FinShark.Server.Helpers;
using FinShark.Server.Interfaces;
using FinShark.Server.Mappers;
using FinShark.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinShark.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly InterfaceCommentRepository _commentRepo;
        private readonly InterfaceStockRepository _stockRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly InterfaceFMPService _fmpService;
        public CommentController(InterfaceCommentRepository interfaceCommentRepository, InterfaceStockRepository interfaceStock, UserManager<AppUser> userManager, InterfaceFMPService fmpService)
        {
            _commentRepo = interfaceCommentRepository;
            _stockRepo = interfaceStock;
            _userManager = userManager;
            _fmpService = fmpService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll([FromQuery]CommentQueryObject detailsQuery)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentsFromDatabase = await _commentRepo.GetAllAsync(detailsQuery);
            var commentsDto = commentsFromDatabase.Select(s => s.ToCommentDto());
            return Ok(commentsDto);
        }

        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.GetByIdAsync(id);

            if(comment == null)
                return NotFound();

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{symbol:alpha}")]
        public async Task<IActionResult> Create([FromRoute] string symbol, [FromBody] CreateCommentRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Obtenemos desde la ruta el stock por su symbol desde la base de datos
            var stock = await _stockRepo.GetBySymbolAsync(symbol); 

            //Si el stock no existe
            if(stock == null)
            {
                //Buscamos desde el httpClient el stock
                stock = await _fmpService.FindStockBySymbolAsync(symbol);
                //Si no existe dicho stock, envia un bad request, si existe, lo almacena en la db
                if(stock == null)
                {
                    return BadRequest("This stock doesn't exist");
                }
                await _stockRepo.CreateAsync(stock);
            }
            //Obtenemos el username del usuario actual
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            //Desde el comment dto que recibimos, lo mapeamos a comment
            var commentModel = requestDto.ToCommentFromCreateComment(stock.Id);
            commentModel.AppUserId = appUser.Id;
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] UpdateCommentRequestDto requestDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Verificamos con el repositorio, si el comentario existe, de acuerdo al id, si existe, con el requestDto
            //se actualiza reemplazando los datos anteriores y posteriormente SaveChangesAsync()
            var comment = await _commentRepo.UpdateAsync(id, requestDto);

            if(comment == null)
                return NotFound("Comment not found");

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = await _commentRepo.DeleteAsync(id);

            if(comment == null)
                return NotFound("Comment doesn't exist");

            return NoContent();
        }
        
    }
}
