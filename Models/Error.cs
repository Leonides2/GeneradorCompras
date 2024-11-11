namespace GeneradorCompras.Models
{
    public class Error
    {
        public int Id { get; set; }
        //public string? Description { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public Compra? ErrorObject {get; set;}
    }
}