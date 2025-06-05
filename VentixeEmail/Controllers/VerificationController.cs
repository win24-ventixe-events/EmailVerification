using Microsoft.AspNetCore.Mvc;
using VentixeEmail.Models;
using VentixeEmail.Services;

namespace VentixeEmail.Controllers;


[ApiController]
[Route("api/[controller]")]
public class VerificationController(IVerificationService verificationService) : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> Send([FromBody] CodeRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "Email is required" });
        
        var result = await verificationService.SendCodeAsync(request.Email);

        return result ? Ok("Code sent") : StatusCode(500, "Something went wrong while sending the code");
    }

    
    [HttpPost("verify")]
    public IActionResult Verify(VerifyCode request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = verificationService.Verify(request);

        if (result)
        {
            return Ok(new { Message = "Code verified successfully." });
        }
        else
        {
            return BadRequest(new { Error = "Invalid or expired verification code. Please try again." });
        }
    }
     
}