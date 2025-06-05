using Microsoft.AspNetCore.Mvc;
using VentixeEmail.Models;
using VentixeEmail.Services;

namespace VentixeEmail.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VerificationController(IVerificationService verificationService) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> Send(string email)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "Email is required" });
        var result = await verificationService.SendCodeAsync(email);

        return result ? Ok("Code sent") : StatusCode(500, "Something went wrong while sending the code");
    }

    
    [HttpPost("verify")]
    public IActionResult Verify(VerifyCode request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "Expired or invalid code, try again." });

        var result = verificationService.Verify(request);
        
        return result ? Ok("Code verified") : StatusCode(500, "Something went wrong while verifying");
    }
}