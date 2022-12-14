using CardPaymentServiceAPI.Models;
using CardPaymentServiceAPI.Models.DTOs;
using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Services.Interface
{
    public interface IPaymentService
    {
        Task<Responses<PaymentResponseDto>> MakePayment(TransactionDto transaction);
        Task<Responses<PaymentDto>> MakeReoccuringPayment(TransactionDto transaction);
    }
}
