using System.ComponentModel.DataAnnotations;

namespace RentCar.Models
{
    public class MsEmployee
    {
        [Key]
        public string EmployeeId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Position { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string PhoneNumber { get; set; } = null!;

        public ICollection<TrMaintenance> Maintenances { get; set; } = new List<TrMaintenance>();
    }
}
