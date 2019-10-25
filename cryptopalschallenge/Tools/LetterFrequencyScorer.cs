using System.Collections.Generic;

namespace cryptopalschallenge.Tools
{
    /// <summary>
    /// Take the weighting of letters from wikipedia:  https://en.wikipedia.org/wiki/Letter_frequency#Relative_frequencies_of_letters_in_the_English_language
    /// </summary>
    public class LetterFrequencyScorer
    {
        private static Dictionary<char, double> scores = new Dictionary<char, double>()
        {
            { 'a', 8.1 },
            { 'b', 1.5 },
            { 'c', 2.8 },
            { 'd', 4.3 },
            { 'e', 12.7 },
            { 'f', 2.2 },
            { 'g', 2.0 },
            { 'h', 6.1 },
            { 'i', 7.0 },
            { 'j', 0.2 },
            { 'k', 0.8 },
            { 'l', 4.0 },
            { 'm', 2.4 },
            { 'n', 6.7 },
            { 'o', 7.5 },
            { 'p', 1.9 },
            { 'q', 0.1 },
            { 'r', 6.0 },
            { 's', 6.3 },
            { 't', 9.1 },
            { 'u', 2.8 },
            { 'v', 1.0 },
            { 'w', 2.4 },
            { 'x', 0.2 },
            { 'y', 2.0 },
            { 'z', 0.1 },
            { ' ', 10 }
        };

        /// <summary>
        /// I'm not sold this is the best algorithm.  It just adds up the letters based on frequency.  A 'bad' string of 
        /// all common letters (like 'e') would get chosen over a real sentence.
        /// 
        /// TODO:  We'll try this out for now, and if we run into issues we'll try to come up with a better analysis.
        /// 
        /// F/up:  If we aren't looking like sentences (bonus points for space), this definitely doesn't work.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static double ScoreStringBasic(string input)
        {
            //Work on a lower case version always
            string working = input.ToLower();

            double result = 0.0;
            foreach(char c in working)
            {
                if(scores.ContainsKey(c))
                {
                    result += scores[c];
                }
                else
                {
                    //No key, no score
                }
            }

            return result;
        }
    }
}
