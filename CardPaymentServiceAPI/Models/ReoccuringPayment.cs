using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardPaymentServiceAPI.Models
{
    public class ReoccuringPayment
    {
        [Key]
        [Required]
        public int ReoccuringPaymentId { get; set; }

        [Required]
        public bool ReoccuringPaymentStatus { get; set; }

        [Required]
        public ReoccuringPaymentFrquency ReoccuringPaymentFrquencyId { get; set; }

        [Required]
        public string ReoccuringPaymentFrquencyCode { get; set; }

        [Required]
        public DateTime CurrentPaymentDate { get; set; }

        [Required]
        public bool CurrentPaymentStatus { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal CurrentPaymentAmount { get; set; }

        [Required]
        public string CurrentPaymentResponseCode { get; set; }

        [Required]
        public string CurrentPayementResponseMessage { get; set; }

        [Required]
        public DateTime NextPayemntDate { get; set; }

        [Required]
        public CardsDetails Card { get; set; }

        [Required]
        [MaxLength(250)]
        public string EncryptedCardPan { get; set; }

        [Required]
        public Fintechs Fintechs { get; set; }
        //public int FintechId { get; set; }
    }
}
