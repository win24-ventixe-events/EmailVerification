using VentixeEmail.Models;

namespace VentixeEmail.Services;

public interface IVerificationService
{
    Task<bool> SendCodeAsync(string email);
    public void SaveVerificationCode(VerificationCodeRequest request);
    public bool Verify(VerifyCode code);
}