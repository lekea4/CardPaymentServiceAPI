using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardPaymentServiceAPI.Models.DTOs
{
    public class PaymentResponseDto
    {
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentReference { get; set; }

        [Required]
        public string PaymentResponseCode { get; set; }

        [Required]
        public string PaymentResponseMessage { get; set; }

        [Required]
        public DateTime PaymentTime { get; set; }

        [Required]
        public bool IsPaymentSuccessful { get; set; }

        [Required]
        public bool IsReocurringPayment { get; set; }

        [Required]
        public string FintechName { get; set; }

    }
}
