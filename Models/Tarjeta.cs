namespace GeneradorCompras.Models
{
    public class Tarjeta
    {
   public int ID {  get; set; }
   public int CardNumber {  get; set; }
   public int CVV {  get; set; } 
   public string? State {  get; set; }
   public double Funds {  get; set; }

    }
}
