namespace GeneradorCompras.Models
{
    public class ErrorDto
    {
        public required Error error {  get; set; }
        public string? Code { get; set; }
        public required string errorType { get; set; }
        public required string Message { get; set; }
        public bool IsRetriable { get; set; } = true;
    }
}
