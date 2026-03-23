using Application.Common;

namespace Infrastructure;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        Console.WriteLine($"Email to {to}: [{subject}] {body}");
        return Task.CompletedTask;
    }
}
