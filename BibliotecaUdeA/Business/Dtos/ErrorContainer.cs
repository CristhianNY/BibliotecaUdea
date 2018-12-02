using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace BibliotecaUdeA.Business.Dtos
{
    public class ErrorContainer
    {
        [JsonProperty("errors")]
        public List<Error> Errors { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
