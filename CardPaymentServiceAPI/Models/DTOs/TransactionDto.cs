namespace CardPaymentServiceAPI.Models.DTOs
{
    public class TransactionDto
    {
        public CardsDto CardDetails { get; set; }
        public PaymentDto Paymentdetails { get; set; }
    }

    
}
