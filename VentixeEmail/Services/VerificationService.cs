using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Caching.Memory;
using VentixeEmail.Models;

namespace VentixeEmail.Services;

public class VerificationService(IConfiguration configuration, EmailClient client, IMemoryCache cache, Random random) : IVerificationService
{
    public async Task<bool> SendCodeAsync(string email)
    {
        try
        {
            if (email == null || string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var code = random.Next(100000, 999999).ToString();
            var subject = $"Your code is {code}";
            var plainText = @$"Your verification code is {code}";
            var html = @$"<html>
			                <body>
				                <h1>Your Ventixe verification code is {code}</h1>
			                </body>
		                 </html>";

            var emailMessage = new EmailMessage(
                senderAddress: configuration["SenderAddress"],
                recipients: new EmailRecipients([new(email)]),
                content: new EmailContent(subject)
                {
                    PlainText = plainText,
                    Html = html
                });

            await client.SendAsync(WaitUntil.Started, emailMessage);
            SaveVerificationCode(new VerificationCodeRequest
            {
                Email = email,
                Code = code,
                ValidFor = TimeSpan.FromMinutes(5)
            });

            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return false;
        }
    }

    public void SaveVerificationCode(VerificationCodeRequest request)
    {
        cache.Set(request.Email.ToLowerInvariant(), request.Code, request.ValidFor);
    }

    public bool Verify(VerifyCode request)
    {
        var key = request.Email.ToLowerInvariant();

        if (cache.TryGetValue(key, out string? storedCode))
        {
            if (storedCode == request.Code)
            {
                cache.Remove(key);
                return true;
            }
        }

        return false;
    }
}