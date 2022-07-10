using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Controllers;

[Authorize(PolicyNames.Activity)]
[ApiController]
[Route("api/[controller]")]
public class ActivityController : Controller
{
    private readonly IActivityRepository _activityRepository;
    private readonly IConfiguration _configuration;

    public ActivityController(IActivityRepository activityRepository, IConfiguration configuration)
    {
        _activityRepository = activityRepository;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<ActivityEntry> Create(ActivityEntry model)
    {
        var activity = await _activityRepository.CreateActivityEntry(model);
        await SendActivityCreatedNotificationEmail(activity.Id, activity.TitleDe);
        return activity;
    }


    private async Task SendActivityCreatedNotificationEmail(int activityId, string titleDe)
    {
        using var client = new SmtpClient(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]));
        client.EnableSsl = true;
        client.Credentials = new NetworkCredential(_configuration["Smtp:Address"], _configuration["Smtp:Password"]);
        var createUser = HttpContext.User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value ?? "unbekannter Benutzer";
        var mailMessage = new MailMessage(_configuration["Smtp:Address"], _configuration["ActivityApprovers"],
            "Neue Aktivität erfasst",
            $"Es wurde eine neue Aktivität mit dem Titel {titleDe} erfasst.\n" +
            $"Sie kann unter https://app-backend.mova.ch/admin/content/activities/{activityId} freigegeben werden.\n" +
            $"Die Aktivität wurde von '{createUser}' erfasst.");
        
        await client.SendMailAsync(mailMessage);
    }
}