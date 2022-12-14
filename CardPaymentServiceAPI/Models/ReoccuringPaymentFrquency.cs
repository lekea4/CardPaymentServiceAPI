using System.ComponentModel.DataAnnotations;

namespace CardPaymentServiceAPI.Models
{
    public class ReoccuringPaymentFrquency
    {
        [Key]
        [Required]
        public int ReoccuringPaymentFrequencyId { get; set; }
        public string ReoccuringPaymentFrequencyType { get; set; }
        public string ReoccuringPaymentFrequencyCode { get; set; }
    }
}
