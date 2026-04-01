namespace Order.Infra;

public class EmailService : Order.Domain.IEmailService
{
    public Task SendEmailAsync(string to, string subject, string body)
    {
        Console.WriteLine($"Email to {to}: [{subject}] {body}");
        return Task.CompletedTask;
    }
}
