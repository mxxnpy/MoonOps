using System.Text.Json.Serialization;

namespace MoonOps.Domain.Models.MottuModels.KeyCloak
{
    public class KeyCloakAuthResponse
    {
        #region Properties
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_expires_in")]
        public int RefreshExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }

        [JsonPropertyName("session_state")]
        public string? SessionState { get; set; }

        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
        #endregion
    }
}
