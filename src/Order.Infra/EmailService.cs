namespace Order.Infra;

public class EmailService : Order.Service.IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        Console.WriteLine($"Email to {to}: [{subject}] {body}");
        return Task.CompletedTask;
    }
}
