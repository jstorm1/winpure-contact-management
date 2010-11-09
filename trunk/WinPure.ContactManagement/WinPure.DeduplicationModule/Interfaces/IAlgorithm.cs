using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinPure.DeduplicationModule.Interfaces
{
    interface IAlgorithm
    {
        /// <summary>
        /// Compute Levenshtein distance
        /// </summary>
        /// <param name="sourceString">String 1</param>
        /// <param name="targetString">String 2</param>
        /// <returns>Distance between the two strings.
        /// The larger the number, the bigger the difference.
        /// </returns>
        int GetLevenshteinDistance(string sourceString, 
                                   string targetString);


        /// <summary>
        /// Calculate “rank” or “score” for the two strings compared
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="targetString"></param>
        /// <returns>Score between 0 and 100</returns>
        int GetSimilarityScore(string sourceString,
                               string targetString);


        /// <summary>
        /// Compares the strings on their identity
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="targetString"></param>
        /// <returns>True if strings are exactly same
        /// Otherwise False
        /// </returns>
        bool CompareStrings(string sourceString,
                            string targetString);


        /// <summary>
        /// Compares the strings on their identity using threshold parameter
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="targetString"></param>
        /// <param name="threshold">Threshold level</param>
        /// <returns>True if strings are matched
        /// Otherwise False
        /// </returns>
        bool IsFuzzyMatch(string sourceString,
                          string targetString,
                          int    threshold);


        /// <summary>
        /// Retrieves list of identical strings
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="targetStrings"></param>
        /// <returns>List of identical strings if there was at least one match
        /// Otherwise null
        /// </returns>
        List<string> GetIdenticalStrings(string sourceString,
                                         List<string> targetStrings);


        /// <summary>
        /// Retrieves list of identical strings by using threshold parameter
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="targetStrings"></param>
        /// <param name="threshold">Threshold level</param>
        /// <returns>List of identical strings if there was at least one match
        /// Otherwise null</returns>
        List<string> GetIdenticalStrings(string sourceString,
                                         List<string> targetStrings,
                                         int threshold);
    }
}
