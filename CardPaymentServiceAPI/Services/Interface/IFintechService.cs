using CardPaymentServiceAPI.Models;
using CardPaymentServiceAPI.Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Services.Interface
{
    public interface IFintechService
    {
        Task<Response> AddFintech(FintechsDto fintechs);
        Task<Responses<IEnumerable<FintechsDto>>> GetAllFintechs();
        Task<Response> DeleteFintech(FintechsDto fintechs);
    }
}
