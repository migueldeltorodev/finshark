using System.ComponentModel.DataAnnotations.Schema;

namespace FinShark.Server.Models
{
    public class Stock
    {
        public int Id { get; set; }
        //Se pone = string.Empty; para que no de un null refence error
        public string Symbol { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal LastDiv { get; set; }
        public string Industry { get; set; } = string.Empty;
        public long MarketCap { get; set; }
        //Relación one-to-many porque en 1 stock puedes tener x cantidad de comentarios
        //Cuando tenemos una relación asi, tendremos una primary key (Id) y las foreign key
        public List<Comment> Comment { get; set; } = new List<Comment>();

        public Stock() { }
    }

}
