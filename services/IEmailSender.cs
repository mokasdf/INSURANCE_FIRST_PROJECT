namespace INSURANCE_FIRST_PROJECT.services
{
    public interface IEmailSender
    {
        void SendEmailAsync(string email, string subject, string message, string invoice,  string username, string price, string subtype, bool bene);

    }
}
