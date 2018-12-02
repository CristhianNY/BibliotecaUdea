using Newtonsoft.Json;
namespace BibliotecaUdeA.Business.Dtos
{
    public class ErrorResponse
    {
        [JsonProperty("error")]
        public ErrorContainer Error { get; set; }
    }
}