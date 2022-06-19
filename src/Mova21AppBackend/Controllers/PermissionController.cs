using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PermissionController : Controller
{
    [HttpGet]
    public AvailablePermissions Get()
    {
        return new AvailablePermissions
        {
            CanEditActivities = HttpContext.User.FindFirst(Claims.ActivityEdit)?.Value == "true",
            CanEditBike = HttpContext.User.FindFirst(Claims.BikeEdit)?.Value == "true",
            CanEditWeather = HttpContext.User.FindFirst(Claims.WeatherEdit)?.Value == "true",
        };
    }
}