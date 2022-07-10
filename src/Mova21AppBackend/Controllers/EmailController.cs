using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Mova21AppBackend.Controllers;

[Authorize(PolicyNames.Email)]
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
    public async Task SendMail([FromForm] string receivers, [FromForm] string subject, [FromForm] string body, IFormFile? attachment)
    {
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
    }
}