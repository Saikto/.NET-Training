using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestsLibrary.SOLR
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SearchResponse
    {
        [JsonProperty("TotalFound")]
        public int TotalFound { get; set; }

        [JsonProperty("QueryTime")]
        public int QueryTime { get; set; }

        [JsonProperty("NextCursorMark")]
        public string NextCursorMark { get; set; }

        [JsonProperty("Results")]
        public List<SearchApiResult> Results { get; set; }

        public List<string> GetTitlesApiWithoutPap()
        {
            return Results.Where(r => r.GetPublishDate() != 9000)
                            .Select(r => r.GetTitle())
                            .ToList();
        }

        public List<string> GetArticleIdsApiWithoutPap()
        {
            return Results.Where(r => r.GetPublishDate() != 9000)
                             .Select(r => r.GetComparableArticleId())
                             .ToList();
        }
    }

    public class SearchApiResult
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Score")]
        public double Score { get; set; }

        [JsonProperty("Fields")]
        public List<Fields> Fields { get; set; }

        [JsonProperty("HighlightFields")]
        public List<Fields> HighlightFields { get; set; }

        public int GetPublishDate()
        {
            var s = Fields[0].FieldValue.ToString().Split('-');
            var date = Int32.Parse(s[1].Substring(0, 4));
            return date;
        }

        public string GetTitle()
        {
            return Fields[2].FieldValue.ToString();
        }

        public string GetComparableArticleId()
        {
            string[] s = Fields[0].FieldValue.ToString().Split('-');
            return s[1] + "-" + s[2];
        }
    }

    public class Fields
    {
        [JsonProperty("FieldName")]
        public string FieldName { get; set; }

        [JsonProperty("FieldValue")]
        public object FieldValue { get; set; }

        [JsonProperty("FieldType")]
        public string FieldType { get; set; }
    }
}
