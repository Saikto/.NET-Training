using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestsLibrary.Enums;
using TestsLibrary.Models;
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

        private void QueryStringOptions(QueryStringOptions QSA)
        {
            string qTitle = "";
            string qAuthor = "";
            string qAbstract = "";
            string qFullText = "";
            string qVolume = "";
            string qIssue = "";

            if (QSA.title != "") qTitle = $"Title:({QSA.title})";
            if (QSA.authors != "") qAuthor = $"Authors:({QSA.authors})";
            if (QSA.abstr != "") qAbstract = $"Abstract:({QSA.abstr})";
            if (QSA.fullText != "") qFullText = $"FullText:({QSA.fullText})";
            if (QSA.volume != "") qVolume = $"Volume:({QSA.volume})";
            if (QSA.issue != "") qIssue = $"Issue:({QSA.issue})";

            if (QueryString != null && QSA.qAllKeys != "") QueryString += " AND " + QSA.qAllKeys;
            else if (QueryString == null && QSA.qAllKeys != "") QueryString += QSA.qAllKeys;

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

        private void FilterQueriesOptions(FilterQueriesOptions FQO)
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

            if (filter != "" && FQO.articles) filter += " OR " + qArticles;
            else if (filter == "" && FQO.articles) filter += qArticles;

            if (filter != "" && FQO.image) filter += " OR " + qImages;
            else if (filter == "" && FQO.image) filter += qImages;

            if (filter != "" && FQO.blogposts) filter += " OR " + qBlogspots;
            else if (filter == "" && FQO.blogposts) filter += qBlogspots;

            if (filter != "" && FQO.other) filter += " OR " + qOther;
            else if (filter == "" && FQO.other) filter += qOther;

            if (filter != "" && FQO.podcast) filter += " OR " + qPodcast;
            else if (filter == "" && FQO.podcast) filter += qPodcast;

            if (filter != "" && FQO.video) filter += " OR " + qVideo;
            else if (filter == "" && FQO.video) filter += qVideo;

            string qAssetType = "";

            if (FQO.articles || FQO.image || FQO.blogposts || FQO.other || FQO.podcast || FQO.video) qAssetType += $"AssetType:({filter})";

            filterQuery += qAssetType;

            if (filterQuery != "" && FQO.cme) filterQuery += " AND " + qCME;
            else if (filterQuery == "" && FQO.cme) filterQuery += qCME;

            if (filterQuery != "" && FQO.openAccess) filterQuery += " AND " + qOpenAccess;
            else if (filterQuery == "" && FQO.openAccess) filterQuery += qOpenAccess;

            //TODO Other date ranges
            if (FQO.pDate == PublicationDateEnum.Last5Years)
            {
                string pattern = "yyyy-m-dtHH:mm:ssz";
                DateTime start = new DateTime(DateTime.Now.Year - 5, DateTime.Now.Month, DateTime.Now.Day);
                string qPublicationDateRange = $"PublicationDateRange:[{start:s}{TimeZone.CurrentTimeZone} TO {DateTime.Now:s}{TimeZone.CurrentTimeZone}]";

                if (filterQuery != "" && FQO.pDate != PublicationDateEnum.AllDates)
                    filterQuery += " AND " + qPublicationDateRange;
                else if (filterQuery == "" && FQO.pDate != PublicationDateEnum.AllDates)
                    filterQuery += qPublicationDateRange;
            }

            if (filterQuery != "")
            {
                FilterQueries.Add(filterQuery);
            }
        }

        private void ProductsOptions(params string[] prods)
        {
            Products = prods.ToList();
        }


        public static string GenerateRequest(QueryStringOptions QSA, FilterQueriesOptions FQO, params string[] prods)
        {
            SolrRequest jRequest = new SolrRequest();
            jRequest.QueryStringOptions(QSA);
            jRequest.FilterQueriesOptions(FQO);
            jRequest.ProductsOptions(prods);
            var request = JsonConvert.SerializeObject(jRequest);

            string sortingBlock;
            string resultSpec;

            string part1 = "\"QueryProcessingOptions\":{\"UseSynonyms\":true,\"BoostFields\":[{\"Name\":\"Title\",\"Value\":0.01}]},\"ResultSpec\":{\"Start\":0,\"CursorMark\":\"*\",\"";
            string part2 = $"Rows\":{FQO.rowsToGet},";
            string part3 = "\"SortFields\":[{";
            string part4;
            string part5 = "}],\"Highlighting\":{\"Fields\":[\"Abstract\"]},\"ReturnFields\":[\"AccessionNumber\",\"AssetType\",\"Title\",\"ImageTitle\",\"ImageWkmrid\",\"OtherIds\",\"EpisodeUrl\",\"ImageID\"],\"Debug\":false}";

            
            if (FQO.sorting == SortByOptionsEnum.Newest)
            {
                sortingBlock = "\"Name\":\"PublicationDate\",\"Order\":\"Descending\"";
                part4 = $"{sortingBlock}";
                resultSpec = part1 + part2 + part3 + part4 + part5;
                request = request.Replace("\"Hardcode\":\"Code\"", resultSpec);
                return request;
            }
            if (FQO.sorting == SortByOptionsEnum.Oldest)
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
