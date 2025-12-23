using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class MsCustomer
    {
        [Key]
        public string CustomerId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public  string Address { get; set; } = null!;
        [Required]
        [MaxLength(100)]
        public string DriverLicenseNumber { get; set; } = null!;

        public ICollection<TrRental> Rentals { get; set; } = new List<TrRental>();

    }
}
