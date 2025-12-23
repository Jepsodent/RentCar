using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class MsCar
    {
        [Key]
        public string CarId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Model { get; set; } = null!;
        
        [Required]
        public int Year { get; set; }
        [Required]
        [MaxLength(50)]
        public string LicensePlate { get; set; } = null!;

        [Required]
        public int NumberOfCarSeats { get; set; }

        [Required]
        [MaxLength(100)]
        public string Transmission { get; set; } = null!;

        [Required]
        public decimal PricePerDay { get; set; }

        [Required]
        public bool Status { get; set; }

       
        public ICollection<MsCarImages> CarImages { get; set; } = new List<MsCarImages>();  
        public ICollection<TrMaintenance> Maintenances { get; set; } = new List<TrMaintenance>();
        public ICollection<TrRental> Rentals { get; set; } = new List<TrRental>();

    }
}
