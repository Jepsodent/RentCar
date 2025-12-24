using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    public class TrRental
    {
        [Key]
        public string RentalId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime RentalDate { get; set; }

        [Required]
        public DateTime ReturnDate { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        public bool PaymentStatus { get; set; }

        // FK ke Customer
        [Required]
        public string CustomerId { get; set; } = null!;

        [ForeignKey(nameof(CustomerId))]
        public MsCustomer Customer { get; set; } = null!;

        // FK ke Car
        [Required]
        public string CarId { get; set; } = null!;

        [ForeignKey(nameof(CarId))]
        public MsCar Car { get; set; } = null!;

        // Navigation (1 to 1)
        public LtPayment? Payment { get; set; }
    }
}
