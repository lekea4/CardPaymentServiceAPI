using System.ComponentModel.DataAnnotations;

namespace CardPaymentServiceAPI.Models.DTOs
{
    public class CardsDto
    {
        [Required]
        public string EncryptedCardPan { get; set; }

        [Required]
        public string EncryptedCVV { get; set; }

        [Required]
        public string CardExpiry { get; set; }

        [Required]
        public bool ReoccuringPaymentEnabled { get; set; }

        public string ReoccurringFrequency { get; set; }

        //[Required]
        //public string AccountLinkedToCard { get; set; }
    }
}
