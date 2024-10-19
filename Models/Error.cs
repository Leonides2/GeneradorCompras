namespace GeneradorCompras.Models
{
    public class Error
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public string? Code { get; set; }
    }
}