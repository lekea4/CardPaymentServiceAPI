using AutoMapper;
using CardPaymentServiceAPI.DatabaseConnection.DBContext;
using CardPaymentServiceAPI.Models;
using CardPaymentServiceAPI.Models.DTOs;
using CardPaymentServiceAPI.Services.Interface;
using CardPaymentServiceAPI.Utilitiy;
using NLog;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly CardPaymentServiceDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICardsDetails _cardsDetails;

        public PaymentService(CardPaymentServiceDbContext context, IMapper mapper, ICardsDetails cardsDetails)
        {
            _context = context;
            _mapper = mapper;
            _cardsDetails = cardsDetails;
        }

        public async Task<Responses<PaymentResponseDto>> MakePayment(TransactionDto transaction)
        {
            try
            {
                var accountNumber = Helper.AccountNumber();
                var cards = _mapper.Map<CardsDetails>(transaction.CardDetails);
                cards.AccountLinkedToCard = accountNumber;
                
               
                //Corebanking payment endpoint here 
                //var payment = _mapper.Map<Payment>(transaction.Paymentdetails);
                var fintech = _context.Fintechs.FirstOrDefault(x => x.FintechName == transaction.Paymentdetails.FintechName);
                var reoccuringPayment = _context.ReoccuringPayment.FirstOrDefault(r => r.ReoccuringPaymentFrquencyCode == transaction.CardDetails.ReoccurringFrequency);
                var paymentCardDetails = _mapper.Map<Payment>(transaction.CardDetails);
                paymentCardDetails.Amount = transaction.Paymentdetails.Amount;
                paymentCardDetails.PaymentReference = Guid.NewGuid().ToString();
                paymentCardDetails.PaymentResponseCode = "00";
                paymentCardDetails.PaymentResponseMessage = "Success";
                paymentCardDetails.PaymentTime = DateTime.Now;
                paymentCardDetails.IsPaymentSuccessful = true;
                paymentCardDetails.IsReocurringPayment = transaction.CardDetails.ReoccuringPaymentEnabled;
                paymentCardDetails.FintechName = transaction.Paymentdetails.FintechName;
                paymentCardDetails.Fintech = fintech;
                var addCardDetailsToPayment = await _context.AddAsync<Payment>(paymentCardDetails);
                await _context.SaveChangesAsync();
                var response = _mapper.Map<PaymentResponseDto>(paymentCardDetails);

                if (transaction.CardDetails.ReoccuringPaymentEnabled)
                {
                    try
                    {
                        
                        var addCard = await _context.AddAsync<CardsDetails>(cards);
                        await _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {

                        Logger.Info($"Adding Reoccuring payment Failed with error: \n {ex}\n");
                    }
                 

                 
                }
                return Responses<PaymentResponseDto>.Success("Success", "00", 200);

            }
            catch (System.Exception ex)
            {

                Logger.Error($"Payment Failed with error: \n {ex}\n");
                return Responses<PaymentResponseDto>.Fail("Failed", "02", 404);
            }
        }

        public Task<Responses<PaymentDto>> MakeReoccuringPayment(TransactionDto transaction)
        {
            throw new System.NotImplementedException();
        }
    }
}
