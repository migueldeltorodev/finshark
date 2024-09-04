using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.Server.Models
{
    [Table("Comments")]
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        //Propiedades de Navegación: StockId y Stock
        //StockId vendria siendo la llave foránea de Comment.
        public int? StockId { get; set; }

        //A esto se le llama propiedad de navegación, es como para tener al padre instanciado
        //y tener acceso a sus propiedades, desde el hijo (similar a herencia)
        public Stock? Stock { get; set; }

        //Relation: One-to-One
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
