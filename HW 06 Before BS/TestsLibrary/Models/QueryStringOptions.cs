using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsLibrary.Enums;

namespace TestsLibrary.Models
{
    public class QueryStringOptions
    {
        public string qAllKeys;
        public string title;
        public string authors;
        public string abstr;
        public string fullText;
        public string volume;
        public string issue;
        
        public string[] prod;

        public QueryStringOptions(string _qAllKeys = "", string _title = "", string _authors = "", string _abstr = "",
            string _fullText = "", string _volume = "", string _issue = "")
        {
            qAllKeys = _qAllKeys;
            title = _title;
            authors = _authors;
            abstr = _abstr;
            fullText = _fullText;
            volume = _volume;
            issue = _issue;
            
        }
    }
}
