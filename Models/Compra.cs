namespace GeneradorCompras.Models
{
    public class Compra
    {
       public int ID {  get; set; }
       public required string CreditCard_N {  get; set; }
       public required List<Product> Details { get; set; }
       public required double Total { get; set; }

       public bool IsSuccess { get; set; }
    }
}
