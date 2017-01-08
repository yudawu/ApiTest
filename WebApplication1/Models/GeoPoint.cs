using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class GeoPoint
    {
        [Range(-10, 10)]
        public double Lat { get; set; }
        [Range(-10, 10)]
        public double Lon { get; set; }
    }
}