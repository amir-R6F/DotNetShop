using Newtonsoft.Json;

namespace Shop.Application.ZarinPal
{
    public class VerificationResponse
    {
        [JsonProperty("data")]
        public VerificationData Data { get; set; }

        [JsonProperty("errors")]
        public object Errors { get; set; }
    }

    public class VerificationData
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("ref_id")]
        public long RefId { get; set; }
    }

}