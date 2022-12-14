using CardPaymentServiceAPI.DatabaseConnection.DBContext;
using CardPaymentServiceAPI.Models;
using CardPaymentServiceAPI.Services.Interface;
using NLog;
using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Services
{
    public class CardService : ICardsDetails
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly CardPaymentServiceDbContext _context;

        public CardService(CardPaymentServiceDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCard(CardsDetails cardsDetails)
        {
            bool success = false;
            try
            {
                var addCard = await _context.AddAsync(cardsDetails);
                await _context.SaveChangesAsync();
                success = true;
            }
            catch (System.Exception ex)
            {
                Logger.Error($"Error add card with details : \n{ex}\n");
            }
            return success;
        }

        public Task<bool> DeleteCard(string cardId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCard(CardsDetails cardsDetails)
        {
            throw new System.NotImplementedException();
        }
    }
}
