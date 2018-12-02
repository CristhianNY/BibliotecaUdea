using Newtonsoft.Json;

namespace BibliotecaUdeA.Business.Dtos
{
    public class Error
    {
        [JsonProperty("domain")]
        public string Domain { get; set; }
        [JsonProperty("reason")]
        public string Reason { get; set; }
        [JsonProperty("location")]
        public string Location { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}