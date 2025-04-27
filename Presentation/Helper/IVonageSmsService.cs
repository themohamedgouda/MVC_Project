namespace Presentation.Helper
{
    public interface IVonageSmsService
    {
        Task<bool> SendSmsAsync(string toPhoneNumber, string message);
    }
}
