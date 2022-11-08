using Newtonsoft.Json;

namespace Web_Scraping__Selenium_.Model
{
    public class CandidateModel
    {
        public class Root
        {
            [JsonProperty(Order = 1)]
            public string ParlimentName { get; set; }

            [JsonProperty(Order = 2)]
            public string ParlimentCode { get; set; }

            [JsonProperty(Order = 3)]
            public List<CandidateDetail> CandidateDetail = new List<CandidateDetail>();
        }

        public class CandidateDetail
        {
            public string CandidateName { get; set; }
            public string Party { get; set; }
        }
    }
}
