using Microsoft.Extensions.Configuration;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;
using RestSharp;

namespace Mova21AppBackend.Data.Storage;

public class DirectusActivityRepository : BaseDirectusRepository, IActivityRepository
{
    const string ActivitiesUrl = "items/activities";

    public DirectusActivityRepository(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<ActivityEntry> CreateActivityEntry(ActivityEntry model)
    {
        var createRequest = new RestRequest($"{ActivitiesUrl}", Method.Post)
            .AddJsonBody(new
            {
                status = "draft",
                category = model.Category switch
                {
                    ActivityCategory.Both => "all",
                    ActivityCategory.Rover => "rover",
                    ActivityCategory.WalkIn => "walk-in",
                    _ => throw new NotSupportedException()
                },
                date = model.Date.Value.ToString("yyyy-MM-dd"),
                description_de = model.DescriptionDe,
                description_fr = model.DescriptionFr,
                description_it = model.DescriptionIt,
                location_de = model.LocationDe,
                location_fr = model.LocationFr,
                location_it = model.LocationIt,
                opening_hours_de = model.OpeningHoursDe,
                opening_hours_fr = model.OpeningHoursFr,
                opening_hours_it = model.OpeningHoursIt,
                title_de = model.TitleDe,
                title_fr = model.TitleFr,
                title_it = model.TitleIt,
                is_permanent = model.IsPermanent,
            });

        var createResponse = await Client.ExecuteAsync<ActivityData>(createRequest);
        if (createResponse.IsSuccessful)
        {
            return createResponse.Data?.ToActivityEntry() ?? throw new ArgumentNullException();
        }

        throw new Exception("Failed to create activity entry in Directus: ", createResponse.ErrorException);
    }
}