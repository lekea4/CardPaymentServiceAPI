using CardPaymentServiceAPI.Models;
using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Services.Interface
{
    public interface ICardsDetails
    {
        Task<bool> AddCard(CardsDetails cardsDetails);
        Task<bool> UpdateCard(CardsDetails cardsDetails);   
        Task<bool> DeleteCard(string cardId);
    }
}
