using CardPaymentServiceAPI.Models.DTOs;
using CardPaymentServiceAPI.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NLog;
using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEncryptionService _encryptionService;
        private IConfiguration _config;
        private readonly IPaymentService _payService;
        public PaymentController(IEncryptionService encryptionService, IConfiguration config, IPaymentService payService)
        {
            _encryptionService = encryptionService;
            _config = config;
            _payService = payService;
        }

        [HttpPost]
        [Route("MakePayment")]
        public async Task<IActionResult> MakePayment(string transactionString)
        {
            var key = _config.GetValue<string>("Encryption:Key");
            var iv = _config.GetValue<string>("Encryption:IV");
            var decryptedTransactionDetails = _encryptionService.Decrypt(transactionString,key,iv);
            var transactiondetails = JsonConvert.DeserializeObject<TransactionDto>(decryptedTransactionDetails.Result);
            if (!ModelState.IsValid)
            {
                Logger.Info($"Error| Details not valid.\n details:  \n {JsonConvert.SerializeObject(transactiondetails)}");
                return StatusCode(404, "Error in one or more payment details");
            }
            var result = await _payService.MakePayment(transactiondetails);
            var resultString = JsonConvert.SerializeObject(result);
            var encryptedResult = _encryptionService.Encypt(resultString, key, iv);
            return StatusCode(result.StatusCode, encryptedResult);
        }

        [HttpPost]
        [Route("MakeReoccuringPayment")]
        public async Task<IActionResult> MakeReoccuringPayment(TransactionDto transaction)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(404, "Error in one or more payment details");
            }
            var result = await _payService.MakeReoccuringPayment(transaction);
            return StatusCode(result.StatusCode, result);
        }


    }
}
