using System.ComponentModel.DataAnnotations;

namespace RentCar.ViewModel.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Nama lengkap wajib diisi")]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        [MaxLength(50)]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password wajib diisi")]
        [MinLength(6, ErrorMessage = "Password minimal 6 karakter")]
        [MaxLength(32)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Konfirmasi password wajib diisi")]
        [Compare("Password", ErrorMessage = "Password dan Konfirmasi Password tidak cocok")]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "Nomor HP wajib diisi")]
        [RegularExpression(@"^\d{1,13}$", ErrorMessage = "Nomor HP harus berupa angka dan maksimal 13 digit")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Alamat wajib diisi")]
        [MaxLength(50)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Nomor SIM wajib diisi")]
        [MaxLength(50)]
        public string DriverLicenseNumber { get; set; } = null!;
    }
}
