using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardPaymentServiceAPI.Models.DTOs
{
    public class PaymentDto
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string FintechName { get; set; }
    }
}
