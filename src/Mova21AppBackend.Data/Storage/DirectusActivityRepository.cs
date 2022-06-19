using Microsoft.Extensions.Configuration;
using MoreLinq;
using MoreLinq.Experimental;
using Mova21AppBackend.Data.Interfaces;
using Mova21AppBackend.Data.Models;
using Mova21AppBackend.Data.RestModels;
using RestSharp;

namespace Mova21AppBackend.Data.Storage;

public class DirectusActivityRepository : BaseDirectusRepository, IActivityRepository
{
    const string ActivitiesUrl = "items/activites";

    public DirectusActivityRepository(IConfiguration configuration) : base(configuration)
    {
    }

    //public async Task<ActivityEntries> GetActivityEntriesByDateRange(DateTime startDate, DateTime endDate)
    //{
    //    var request = new RestRequest(ActivitiesUrl);
    //    request.Parameters.AddParameter(new QueryParameter("filter[date][_between]", $"[{startDate:yyyy-MM-dd},{endDate.AddDays(1):yyyy-MM-dd}]"));
    //    request.Parameters.AddParameter(new QueryParameter("sort", "-date"));
    //    var response = await Client.ExecuteGetAsync<WeatherEntriesResponse>(request);

    //    var existingEntries = (response.Data?.Data?.Select(x => x.ToWeatherEntry()) ?? Enumerable.Empty<WeatherEntry>())
    //        .ToList();

        //var newEntries = GetDateDayTimeCombinationsInRange(startDate, endDate)
        //    .Where(dateDayTimeCombination =>
        //        !existingEntries.Any(existingEntry => existingEntry.Date.Date == dateDayTimeCombination.Date 
        //                                              && existingEntry.DayTime == dateDayTimeCombination.DayTime))
        //    .Select(async dateDayTimeCombination => await CreateWeatherEntry(new WeatherEntry
        //    {
        //        Date = dateDayTimeCombination.Date,
        //        DayTime = dateDayTimeCombination.DayTime,
        //        Temperature = dateDayTimeCombination.DayTime switch
        //        {
        //            DayTime.Evening => 15,
        //            DayTime.Morning => 15,
        //            DayTime.Night => 8,
        //            DayTime.Midday => 23,
        //            _ => throw new NotImplementedException()
        //        },
        //        Weather = WeatherType.CloudSun
        //    }))
        //    .Await();
        
        //return null;
        //return new WeatherEntries
        //{
        //    Entries = existingEntries.Concat(newEntries).OrderBy(x => x.Date).ThenBy(x => x.DayTime)
        //};
    //}

    //public async Task UpdateActivityEntry(ActivityEntry model)
    //{
    //    var patchRequest = new RestRequest($"{ActivitiesUrl}/{model.Id}", Method.Patch)
    //        .AddJsonBody(ActivityEntryUpdateData.FromActivityEntry(model));
    //    var response = await Client.PatchAsync<WeatherEntryResponse>(patchRequest);
    //}

    //public async Task DeleteWeatherEntry(int id)
    //{
    //    var deleteRequest = new RestRequest($"{ActivitiesUrl}/{id}", Method.Delete);
    //    await Client.ExecuteAsync(deleteRequest);
    //}

    public async Task<WeatherEntry> CreateActivityEntry(ActivityEntry model)
    {
        var createRequest = new RestRequest($"{ActivitiesUrl}/", Method.Post)
            .AddJsonBody(new ActivityData
            {
                Category = model.Category,
                Date = model.Date,
                DescriptionDe = model.DescriptionDe,
                DescriptionFr = model.DescriptionFr,
                DescriptionIt = model.DescriptionIt,
                LocationDe = model.LocationDe,
                LocationFr = model.LocationFr,
                LocationIt = model.LocationIt,
                OpeningHoursDe = model.OpeningHoursDe,
                OpeningHoursFr = model.OpeningHoursFr,
                OpeningHoursIt = model.OpeningHoursIt,
                TitleDe = model.TitleDe,
                TitleFr = model.TitleFr,
                TitleIt = model.TitleIt,
                IsPermanent = model.IsPermanent,
            });
        var createResponse = await Client.ExecuteAsync<WeatherEntryResponse>(createRequest);
        if (createResponse.IsSuccessful)
        {
            return createResponse.Data?.Data?.ToWeatherEntry() ?? throw new ArgumentNullException();
        }

        throw new Exception("Failed to create activity entry in Directus: ", createResponse.ErrorException);
    }
}