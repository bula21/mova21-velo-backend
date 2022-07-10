using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Mova21AppBackend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class EmailController : Controller
{
    private readonly IConfiguration _configuration;

    public EmailController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Sends an email to the specified receivers with the given attachment in html format.
    /// </summary>
    /// <param name="receivers">One or more email addresses separated by comma ','.</param>
    /// <param name="subject">The subject of the email.</param>
    /// <param name="body">The html body of the email.</param>
    /// <param name="attachment">The attachment to be sent, can be null.</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> SendMail([FromForm]string logDbJwtToken, [FromForm] string receivers, [FromForm] string subject, [FromForm] string body, IFormFile? attachment)
    {
        if (!IsTokenValid(logDbJwtToken))
        {
            return Forbid();
        }
        
        using var smtp = new SmtpClient
        {
            Host = _configuration["Smtp:Host"],
            Port = Convert.ToInt32(_configuration["Smtp:Port"]),
            EnableSsl = true,
            Credentials = new NetworkCredential(_configuration["Smtp:Address"], _configuration["Smtp:Password"])
        };
        using var message = new MailMessage(_configuration["Smtp:Address"], receivers)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
            
        };

        if (attachment != null)
        {
            message.Attachments.Add(new Attachment(attachment.OpenReadStream(), attachment.FileName));
        }
        await smtp.SendMailAsync(message);
        return Ok();
    }

    private bool IsTokenValid(string logDbJwtToken)
    {
        using var client = new HttpClient();
        var message = new HttpRequestMessage(HttpMethod.Get, _configuration["JwtLogDbValidationUrl"]);
        message.Headers.Add("Authorization", $"Bearer {logDbJwtToken}");
        var response = client.Send(message);

        return response.IsSuccessStatusCode;
    }
}