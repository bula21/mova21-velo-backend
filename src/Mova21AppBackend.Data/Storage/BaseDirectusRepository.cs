using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Mova21AppBackend.Data.RestModels;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.Json;

namespace Mova21AppBackend.Data.Storage
{
    public abstract class BaseDirectusRepository
    {
        public IConfiguration Configuration { get; }

        protected BaseDirectusRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            Client = new RestClient(Configuration["Directus:BaseUrl"]);
            Client.Authenticator = new JwtAuthenticator(Configuration["Directus:Token"]);
            Client.UseSystemTextJson(new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() }
            });
        }

        protected RestClient Client { get; }
    }
}
