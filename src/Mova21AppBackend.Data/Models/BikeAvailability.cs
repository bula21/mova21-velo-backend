using System.ComponentModel.DataAnnotations;

namespace Mova21AppBackend.Data.Models
{
    public class BikeAvailability
    {
        public int Id { get; set; }
        public bool IsOpen { get; set; }
        public int RegularBikes { get; set; }
        public int CargoBikes { get; set; }
        public int BikeTrailers { get; set; }
    }
}
