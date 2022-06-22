using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;

namespace Mova21AppBackend.Controllers;

[Authorize(PolicyNames.Activity)]
[ApiController]
[Route("api/[controller]")]
public class ActivityController : Controller
{
    private readonly IActivityRepository _activityRepository;

    public ActivityController(IActivityRepository activityRepository)
    {
        _activityRepository = activityRepository;
    }

    [HttpPost]
    public async Task<ActivityEntry> Create(ActivityEntry model)
    {
        return await _activityRepository.CreateActivityEntry(model);
    }
}