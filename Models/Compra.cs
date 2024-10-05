namespace GeneradorCompras.Models
{
    public class Compra
    {
       public int ID {  get; set; }
       public string? ProductName {  get; set; }
       public int Count {  get; set; }
       public int CreditCard_N {  get; set; }
       public bool IsSuccess { get; set; }
    }
}
