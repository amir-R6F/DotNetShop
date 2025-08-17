using System.Text.Json.Serialization;

namespace Shop.Application.ZarinPal
{
    public class VerificationRequest
    {
        [JsonPropertyName("Amount")] public int Amount { get; set; }
        [JsonPropertyName("MerchantID")] public string MerchantID { get; set; }
        [JsonPropertyName("Authority")] public string Authority { get; set; }
    }
}