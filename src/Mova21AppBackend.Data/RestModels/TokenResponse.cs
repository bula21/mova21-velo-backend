using System.Text.Json.Serialization;

namespace Mova21AppBackend.Data.RestModels;

public class TokenResponse
{
    [JsonPropertyName("data")]
    public TokenResponseData? Data { get; set; }

    [JsonPropertyName("public")]
    public bool Public { get; set; }

    public class TokenResponseData
    {
        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("user")]
        public TokenResponseDataUser? User { get; set; }
        public class TokenResponseDataUser
        {
            [JsonPropertyName("id")]
            public string? Id { get; set; }

            [JsonPropertyName("status")]
            public string? Status { get; set; }

            [JsonPropertyName("role")]
            public string? Role { get; set; }

            [JsonPropertyName("first_name")]
            public string? FirstName { get; set; }

            [JsonPropertyName("last_name")]
            public string? LastName { get; set; }

            [JsonPropertyName("email")]
            public string? Email { get; set; }

            [JsonPropertyName("timezone")]
            public string? Timezone { get; set; }

            [JsonPropertyName("locale")]
            public string? Locale { get; set; }

            [JsonPropertyName("locale_options")]
            public string? LocaleOptions { get; set; }

            [JsonPropertyName("avatar")]
            public string? Avatar { get; set; }

            [JsonPropertyName("company")]
            public string? Company { get; set; }

            [JsonPropertyName("title")]
            public string? Title { get; set; }

            [JsonPropertyName("external_id")]
            public string? ExternalId { get; set; }

            [JsonPropertyName("theme")]
            public string? Theme { get; set; }

            [JsonPropertyName("2fa_secret")]
            public string? SecondFactorSecret { get; set; }

            [JsonPropertyName("password_reset_token")]
            public string? PasswordResetToken { get; set; }
        }
    }
}
