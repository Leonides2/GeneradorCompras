using System.Text.Json.Serialization;

namespace GeneradorCompras.Models
{
    public class Compra
    {
       public int ID {  get; set; }
       public required List<Product> Details { get; set; }
       public required double Total { get; set; }
       public required User User { get; set; }
       public bool IsSuccess { get; set; }
       public DateTime Time { get; set; }

    }
}
