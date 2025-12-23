using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    public class LtPayment
    {
        [Key]
        public string PaymentId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(100)]
        public string PaymentMethod { get; set; } = null!;

        // FK ke Rental
        [Required]
        public string RentalId { get; set; } = null!;

        [ForeignKey(nameof(RentalId))]
        public TrRental Rental { get; set; } = null!;
    }
}
