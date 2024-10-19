namespace GeneradorCompras.Models
{
    public class Product
    {
       public int ID {  get; set; }
       public required string Name {  get; set; }
       public double Price { get; set; }
       public int Count { get; set; }
       public string Category { get; set; }
       public string CommerceName { get; set; }
       public int CommerceID { get; set; }
    }
}
