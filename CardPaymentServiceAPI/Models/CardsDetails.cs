using System.ComponentModel.DataAnnotations;

namespace CardPaymentServiceAPI.Models
{
    public class CardsDetails
    {
        [Key]
        [Required]
        public int CardId { get; set; }

        [Required]
        [MaxLength(250)]
        public string EncryptedCardPan { get; set; }

        [Required]
        [MaxLength(250)]
        public string EncryptedCVV { get; set; }

        [Required]
        [MaxLength(250)]
        public string CardExpiry { get; set; }

        [Required]
        public bool  ReoccuringPaymentEnabled { get; set; }

        [Required]
        public string AccountLinkedToCard { get; set; }
    }
}
