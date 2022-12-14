using System;
using System.ComponentModel.DataAnnotations;

namespace CardPaymentServiceAPI.Models
{
    public class Fintechs
    {
        [Key]
        [Required]
        public int FintechID { get; set; }
        
        [Required]
        public string FintechName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        [Required]
        public string FintechEmail { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string CreationReference { get; set; }
    }
}
