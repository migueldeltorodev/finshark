using FinShark.Server.Dtos.Comment;
using FinShark.Server.Models;

namespace FinShark.Server.Mappers
{
    public static class CommentMapper
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            };
        }
        
        public static Comment ToCommentFromCreateComment(this CreateCommentRequestDto createCommentRequest, int stockId)
        {
            return new Comment
            {
                Title = createCommentRequest.Title,
                Content = createCommentRequest.Content,
                StockId = stockId
            };
        }
    }
}
