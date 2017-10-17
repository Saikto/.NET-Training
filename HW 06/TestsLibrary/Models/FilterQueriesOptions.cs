using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsLibrary.Enums;

namespace TestsLibrary.Models
{
    public class FilterQueriesOptions
    {
        public bool articles;
        public bool image;
        public bool blogposts;
        public bool other;
        public bool podcast;
        public bool video;
        public bool cme;
        public bool openAccess;
        public PublicationDateEnum pDate;
        public int lastNYears;
        public SortByOptionsEnum sorting;
        public int rowsToGet;

        public FilterQueriesOptions(bool _articles = true,
                                    bool _image = false, bool _blogposts = false, bool _other = false, bool _podcast = false,
                                    bool _video = false, bool _cme = false, bool _openAccess = false, PublicationDateEnum _pDate = PublicationDateEnum.AllDates,
                                    int _lastNYears = 0, SortByOptionsEnum _sorting = SortByOptionsEnum.BestMatch,
                                    int _rowsToGet = 20)
        {
            articles = _articles;
            image = _image;
            blogposts = _blogposts;
            other = _other;
            podcast = _podcast;
            video = _video;
            cme = _cme;
            openAccess = _openAccess;
            pDate = _pDate; //TODO;
            lastNYears = _lastNYears; //TODO;
            sorting = _sorting;
            rowsToGet = _rowsToGet;
        }
    }
}
