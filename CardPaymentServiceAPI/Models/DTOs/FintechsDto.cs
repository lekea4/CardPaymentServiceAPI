using System.ComponentModel.DataAnnotations;

namespace CardPaymentServiceAPI.Models.DTOs
{
    public class FintechsDto
    {
        [Required]
        public string FintechName { get; set; }

        [Required]
        public string FintechEmail { get; set; }
    }
}
