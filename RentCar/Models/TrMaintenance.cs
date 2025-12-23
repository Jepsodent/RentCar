using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    public class TrMaintenance
    {
        [Key]
        public string MaintenanceId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Description { get; set; } = null!;

        [Required]
        public decimal Cost { get; set; }

        // FK ke Car
        [Required]
        public string CarId { get; set; } = null!;

        [ForeignKey(nameof(CarId))]
        public MsCar Car { get; set; } = null!;

        // FK ke Employee
        [Required]
        public string EmployeeId { get; set; } = null!;

        [ForeignKey(nameof(EmployeeId))]
        public MsEmployee Employee { get; set; } = null!;
    }
}
