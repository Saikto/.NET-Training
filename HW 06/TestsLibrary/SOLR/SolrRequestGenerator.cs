using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestsLibrary.Pages;

namespace TestsLibrary.SOLR
{
    [JsonObject(MemberSerialization.OptIn)]
    public class SolrRequest
    {
        [JsonProperty("QueryString", Order = 1)]
        private string QueryString { get; set; }

        [JsonProperty("Products", Order = 2)]
        private List<string> Products { get; set; }

        [JsonProperty("FilterQueries", Order = 3)]
        private List<string> FilterQueries { get; set; }

        [JsonProperty("Hardcode", Order = 4)]
        private string _hardcode = "Code";

        public SolrRequest()
        {
            Products = new List<string>();
            FilterQueries = new List<string>();
        }

        private void QueryStringOptions(string qAllKeys = "", string title = "", string authors = "", string abstr = "", string fullText = "",
            string volume = "", string issue = "")
        {
            string qTitle = "";
            string qAuthor = "";
            string qAbstract = "";
            string qFullText = "";
            string qVolume = "";
            string qIssue = "";

            if (title != "") qTitle = $"Title:({title})";
            if (authors != "") qAuthor = $"Authors:({authors})";
            if (abstr != "") qAbstract = $"Abstract:({abstr})";
            if (fullText != "") qFullText = $"FullText:({fullText})";
            if (volume != "") qVolume = $"Volume:({volume})";
            if (issue != "") qIssue = $"Issue:({issue})";

            if (QueryString != null && qAllKeys != "") QueryString += " AND " + qAllKeys;
            else if (QueryString == null && qAllKeys != "") QueryString += qAllKeys;

            if (QueryString != null && qTitle != "") QueryString += " AND " + qTitle;
            else if(QueryString == null && qTitle != "") QueryString += qTitle;

            if (QueryString != null && qAuthor != "") QueryString += " AND " + qAuthor;
            else if (QueryString == null && qAuthor != "") QueryString += qAuthor;

            if (QueryString != null && qAbstract != "") QueryString += " AND " + qAbstract;
            else if (QueryString == null && qAbstract != "") QueryString += qAbstract;

            if (QueryString != null && qFullText != "") QueryString += " AND " + qFullText;
            else if (QueryString == null && qFullText != "") QueryString += qFullText;

            if (QueryString != null && qVolume != "") QueryString += " AND " + qVolume;
            else if (QueryString == null && qVolume != "") QueryString += qVolume;

            if (QueryString != null && qIssue != "") QueryString += " AND " + qIssue;
            else if (QueryString == null && qIssue != "") QueryString += qIssue;
        }

        private void FilterQueriesOptions(bool articles = true, bool image = false, bool blogposts = false,
            bool other = false, bool podcast = false, bool video = false,
            bool cme = false, bool openAccess = false, int lastNYears = 0)
        {
            string filterQuery = "";

            string qArticles = "article";
            string qImages = "figure OR table OR math";
            string qBlogspots = "blogspots";
            string qOther = "other";
            string qPodcast = "podcast";
            string qVideo = "video";
            string qCME = "CME:*";
            string qOpenAccess = "OpenAccess:true";

            string filter = "";

            if (filter != "" && articles) filter += " OR " + qArticles;
            else if (filter == "" && articles) filter += qArticles;

            if (filter != "" && image) filter += " OR " + qImages;
            else if (filter == "" && image) filter += qImages;

            if (filter != "" && blogposts) filter += " OR " + qBlogspots;
            else if (filter == "" && blogposts) filter += qBlogspots;

            if (filter != "" && other) filter += " OR " + qOther;
            else if (filter == "" && other) filter += qOther;

            if (filter != "" && podcast) filter += " OR " + qPodcast;
            else if (filter == "" && podcast) filter += qPodcast;

            if (filter != "" && video) filter += " OR " + qVideo;
            else if (filter == "" && video) filter += qVideo;

            string qAssetType = "";

            if (articles || image || blogposts || other || podcast || video) qAssetType += $"AssetType:({filter})";

            filterQuery += qAssetType;

            if (filterQuery != "" && cme) filterQuery += " AND " + qCME;
            else if (filterQuery == "" && cme) filterQuery += qCME;

            if (filterQuery != "" && openAccess) filterQuery += " AND " + qOpenAccess;
            else if (filterQuery == "" && openAccess) filterQuery += qOpenAccess;

            DateTime start = new DateTime(DateTime.Now.Year - lastNYears, DateTime.Now.Month, DateTime.Now.Day);
            string qPublicationDateRange = $"PublicationDateRange:[{start} TO {DateTime.Now:O}]";

            if (filterQuery != "" && lastNYears != 0) filterQuery += " AND " + qPublicationDateRange;
            else if (filterQuery == "" && lastNYears != 0) filterQuery += qPublicationDateRange;

            if (filterQuery != "")
            {
                FilterQueries.Add(filterQuery);
            }
        }

        private void ProductsOptions(params string[] prod)
        {
            Products = prod.ToList();
        }


        public static string GenerateRequest(string qAllKeys = "", string title = "", string authors = "", string abstr = "",
                                    string fullText = "", string volume = "", string issue = "", bool articles = true, 
                                    bool image = false, bool blogposts = false, bool other = false, bool podcast = false, 
                                    bool video = false, bool cme = false, bool openAccess = false, int lastNYears = 0, 
                                    SearchResultPage.SortByOptionsEnum sorting = SearchResultPage.SortByOptionsEnum.BestMatch,
                                    int rowsToGet = 20, params string[] prod)
        {
            SolrRequest jRequest = new SolrRequest();
            jRequest.QueryStringOptions(qAllKeys, title, authors, abstr, fullText, volume, issue);
            jRequest.FilterQueriesOptions(articles, image, blogposts, other, podcast, video, cme, openAccess, lastNYears);
            jRequest.ProductsOptions(prod);
            var request = JsonConvert.SerializeObject(jRequest);

            string sortingBlock;
            string resultSpec;

            string part1 = "\"QueryProcessingOptions\":{\"UseSynonyms\":true,\"BoostFields\":[{\"Name\":\"Title\",\"Value\":0.01}]},\"ResultSpec\":{\"Start\":0,\"CursorMark\":\"*\",\"";
            string part2 = $"Rows\":{rowsToGet},";
            string part3 = "\"SortFields\":[{";
            string part4;
            string part5 = "}],\"Highlighting\":{\"Fields\":[\"Abstract\"]},\"ReturnFields\":[\"AccessionNumber\",\"AssetType\",\"Title\",\"ImageTitle\",\"ImageWkmrid\",\"OtherIds\",\"EpisodeUrl\",\"ImageID\"],\"Debug\":false}";

            
            if (sorting == SearchResultPage.SortByOptionsEnum.Newest)
            {
                sortingBlock = "\"Name\":\"PublicationDate\",\"Order\":\"Descending\"";
                part4 = $"{sortingBlock}";
                resultSpec = part1 + part2 + part3 + part4 + part5;
                request = request.Replace("\"Hardcode\":\"Code\"", resultSpec);
                return request;
            }
            if (sorting == SearchResultPage.SortByOptionsEnum.Oldest)
            {
                sortingBlock = "\"Name\":\"PublicationDate\"";
                part4 = $"{sortingBlock}";
                resultSpec = part1 + part2 + part3 + part4 + part5;
                request = request.Replace("\"Hardcode\":\"Code\"", resultSpec);
                return request;
            }
            //IF BEST MATCH
            sortingBlock = "";
            part4 = $"{sortingBlock}";
            part3 = part3.Remove(part3.LastIndexOf('{'));
            part5 = part5.Remove(0, 1);
            resultSpec = part1 + part2 + part3 + part4 + part5;
            request = request.Replace("\"Hardcode\":\"Code\"", resultSpec);
            return request;
        }
    }
}
