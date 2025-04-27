
using Vonage;
using Vonage.Messaging;

namespace Presentation.Helper
{
    public class VonageSmsService : IVonageSmsService
    {
        private readonly VonageClient _vonageClient;

        public VonageSmsService(VonageClient vonageClient)
        {
            _vonageClient = vonageClient;
        }
        public async Task<bool> SendSmsAsync(string toPhoneNumber, string message)
        {
            var smsRequest = new SendSmsRequest
            {
                To = toPhoneNumber,
                From = "VonageAPI", // أو ممكن تيجي من config برضو لو عايز تزود
                Text = message          
            };

            var response = await _vonageClient.SmsClient.SendAnSmsAsync(smsRequest);
            return response.Messages[0].Status == "0"; // true لو تم الإرسال
        }
    }
}
