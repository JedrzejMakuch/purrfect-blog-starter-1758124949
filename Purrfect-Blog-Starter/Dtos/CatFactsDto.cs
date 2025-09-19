using Newtonsoft.Json;
using System.Collections.Generic;

namespace Purrfect_Blog_Starter.Dtos
{
    public class CatFactsDto
    {
        [JsonProperty("data")]
        public List<CatFactDto> Data { get; set; }
    }

    public class CatFactDto
    {
        [JsonProperty("fact")] 
        public string Fact { get; set; }

        [JsonProperty("length")]
        public int Length { get; set; }
    }
}