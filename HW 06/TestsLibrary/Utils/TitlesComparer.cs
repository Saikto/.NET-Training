using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsLibrary.SOLR;

namespace TestsLibrary.Utils
{
    public static class TitlesComparer
    {
        public static bool AreTitlesEqual(List<string> titlesUi, List<SearchApiResult> resultsApi)
        {
            if (resultsApi.Count != titlesUi.Count)
            {
                return false;
            }
            for(int i =0; i < titlesUi.Count; i++)
            {
                if (titlesUi[i] != resultsApi[i].GetTitle())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
