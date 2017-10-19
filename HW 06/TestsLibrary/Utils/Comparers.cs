using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsLibrary.SOLR;

namespace TestsLibrary.Utils
{
    public static class Comparers
    {
        public static bool CompareTitles(List<string> titlesUi, List<string> titlesApi)
        {
            if (titlesApi.Count != titlesUi.Count)
            {
                return false;
            }

            for(int i =0; i < titlesUi.Count; i++)
            {
                if (titlesUi[i] == "")
                {
                    continue;
                }
                if (!titlesUi[i].Contains(titlesApi[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool CompareIds(List<string> idsUi, List<string> idsApi)
        {
            if (idsUi.Count != idsApi.Count)
            {
                return false;
            }
            for (int i = 0; i < idsApi.Count; i++)
            {
                if (idsUi[i] != idsApi[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
