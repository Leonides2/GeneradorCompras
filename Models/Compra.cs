using System.Text.Json.Serialization;

namespace GeneradorCompras.Models
{
    public class Compra : IDisposable
    {
       public int? ID {  get; set; }
       public List<Product>? Details { get; set; }
       public double? Total { get; set; }
       public User? User { get; set; }
       public bool? IsSuccess { get; set; } = false;
       public DateTime? Time { get; set; } = DateTime.Now;

        public void Dispose()
        {
            if(this.Details != null)
            {
                this.Details = null;
            }
            if (this.User != null) {
                this.User = null;
            }
            if (this.ID != null) { 
                this.ID = null;
            }
            if (this.IsSuccess != null) { 
                this.IsSuccess = null;
            }
            if (this.Total != null) { this.Total = null; }
            if (this.Time != null) { this.Time = null; }
        }
    }
}
