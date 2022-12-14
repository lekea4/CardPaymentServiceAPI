using System.Threading.Tasks;

namespace CardPaymentServiceAPI.Services.Interface
{
    public interface IEncryptionService
    {
        Task<string> Encypt(string plaintext, string secretkey, string iv);
       // Task<string> Encypt(Guid plaintext, string secretkey, string iv);
        Task<string> Decrypt(string ciphertext, string secretKey, string iv);
    }
}
