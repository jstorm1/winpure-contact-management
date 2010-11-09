using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinPure.DeduplicationModule.Interfaces
{
    interface IClustering
    {
        /// <summary>
        /// Determines if strings quite similar for comparison
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="targetString"></param>
        /// <returns></returns>
        bool IsNeedComparision(string sourceString,
                               string targetString);
    }
}
