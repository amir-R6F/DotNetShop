using Newtonsoft.Json;

namespace Shop.Application.ZarinPal
{
    public class PaymentResponse
    {
        [JsonProperty("data")]
        public PaymentData Data { get; set; }

        [JsonProperty("errors")]
        public object Errors { get; set; }
    }
    
    
    public class PaymentData
    {
        [JsonProperty("authority")]
        public string Authority { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("fee")]
        public int Fee { get; set; }

        [JsonProperty("fee_type")]
        public string FeeType { get; set; }
    }
}