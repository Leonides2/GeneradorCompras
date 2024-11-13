namespace GeneradorCompras.Models
{
    public class ErrorDto
    {
        public Compra? purchase { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public string? Code { get; set; }
        public required string errorType { get; set; }
        public required string message { get; set; }
        public bool isRetriable { get; set; } = true;
    }
}
