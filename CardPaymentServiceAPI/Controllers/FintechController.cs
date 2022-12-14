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
    public class FintechController : ControllerBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IEncryptionService _encryptionService;
        private IConfiguration _config;
        private readonly IFintechService _fintechService;

        public FintechController(IEncryptionService encryptionService, IConfiguration config, IFintechService fintechService)
        {
            _encryptionService = encryptionService;
            _config = config;
            _fintechService = fintechService;
        }

        [HttpGet]
        [Route("GetFintechs")]
        public async Task<IActionResult> GetFintechs()
        {
            var key = _config.GetValue<string>("Encryption:Key");
            var iv = _config.GetValue<string>("Encryption:IV");
            var fintechs = await _fintechService.GetAllFintechs();
            var fintechString = JsonConvert.SerializeObject(fintechs);
            var encryptedData = _encryptionService.Encypt(fintechString, key, iv);
           return StatusCode(fintechs.StatusCode, encryptedData);
        }

        [HttpPost]
        [Route("AddFintech")]
        public async Task<IActionResult> AddFintech(string encryptedDetails)
        {
            var key = _config.GetValue<string>("Encryption:Key");
            var iv = _config.GetValue<string>("Encryption:IV");
            var decryptedFintech = _encryptionService.Decrypt(encryptedDetails, key, iv);
            var fintechs = JsonConvert.DeserializeObject<FintechsDto>(decryptedFintech.Result);
            if (!ModelState.IsValid)
            {
                Logger.Info($"Error| Details not valid.\n details : \n {JsonConvert.SerializeObject(fintechs)}");
                return StatusCode(404, "Error in one or more details");
            }
            var result = await _fintechService.AddFintech(fintechs);
            var resultString = JsonConvert.SerializeObject(result);
            var encryptResult = _encryptionService.Encypt(resultString,key,iv);
            return StatusCode(200, encryptResult);
        }
    }
}
