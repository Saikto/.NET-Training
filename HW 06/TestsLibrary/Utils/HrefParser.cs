using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsLibrary.Utils
{
    public static class HrefParser
    {
        public static string ParseArticleHrefToId(string href)
        {
            string[] splitByDot = href.Split('.');
            string articleNumberInIssue = splitByDot[splitByDot.Length - 2];
            while (articleNumberInIssue.Length != 5)
            {
                articleNumberInIssue = articleNumberInIssue.Insert(0, "0");
            }
            string[] splitBySlash = href.Split('/');
            string articleIssue = splitBySlash[splitBySlash.Length - 2];
            string articleYear = splitBySlash[splitBySlash.Length - 3];
            return articleYear + articleIssue + "-" + articleNumberInIssue;
        }

        public static string ParseImageHrefToId(string href)
        {
            string[] splitByAmp = href.Split('&');
            char[] articleNumberInIssue = splitByAmp[splitByAmp.Length - 2].ToCharArray().Reverse().Take(5).Reverse().ToArray();
            char[] articleIssue = splitByAmp[splitByAmp.Length - 3].ToCharArray().Reverse().Take(5).Reverse().ToArray();
            char[] articleYear = splitByAmp[splitByAmp.Length - 4].ToCharArray().Reverse().Take(4).Reverse().ToArray();

            return new string(articleYear) + new string(articleIssue) + "-" + new string(articleNumberInIssue);
        }
    }
}
