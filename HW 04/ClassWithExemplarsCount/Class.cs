using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassWithExemplarsCount
{
    public class Class
    {
        private static int ExemplarsCount = 0;

        public Class()
        {
            ExemplarsCount++;
        }
    }
}
