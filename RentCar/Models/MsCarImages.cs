using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentCar.Models
{
    public class MsCarImages
    {
        [Key]
        public string ImageCarId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string CarId { get; set; } = null!;

        [Required]
        [MaxLength(2000)]
        public string ImageLink { get; set; } = null!;

        [ForeignKey(nameof(CarId))]
        public MsCar Car { get; set; } = null!;
    }
}

