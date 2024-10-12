namespace GeneradorCompras.Models
{
    public class Product
    {
       public int ID {  get; set; }
       public required string Name {  get; set; }
       public double Price { get; set; }
       public int Count { get; set; }
    }
}
