using System.ComponentModel.DataAnnotations;

namespace AndrewDigitalStore.Models.DTOs
{
    public class CheckoutFormDTO
    {
        [Required(ErrorMessage = "Customer Name is required.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer Name must be between 2 and 100 characters.")]
        public string CustomerName { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? CustomerEmail { get; set; }
    }
}