using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace HistogramCodingChallenge
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StreamReader inputFile = null;
            StreamWriter outputFile = null;
            Dictionary<string, int> wordOcurrences;
            string line;

            try
            {
                inputFile = new StreamReader(@"input.txt");
                wordOcurrences = new Dictionary<string, int>();

                // Read lines until end of file.
                while ((line = inputFile.ReadLine()) != null)
                {
                    // Format data to remove all non alphanumeric characters, then extra spaces.
                    line = Regex.Replace(line.ToLower(), @"[^a-zA-Z0-9]", " ");
                    line = Regex.Replace(line, @"\s+", " ").Trim();

                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] wordList = line.Split(' ');
                        foreach (string word in wordList)
                        {
                            // Add new words to dictionary, otherwise update word count.
                            if (wordOcurrences.ContainsKey(word))
                            {
                                wordOcurrences[word]++;
                            }
                            else
                            {
                                wordOcurrences.Add(word, 1);
                            }
                        }
                    }
                }

                // Show sorted histogram with desired format.
                outputFile = new StreamWriter("output.txt");
                foreach (KeyValuePair<string, int> word in wordOcurrences.OrderBy(i => i.Key).OrderByDescending(i => i.Value))
                {
                    outputFile.WriteLine("{0,8} | {1} ({2})", word.Key, new string('=', word.Value), word.Value);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An exception occurred. {e.StackTrace}");
            }
            finally
            {
                inputFile?.Close();
                outputFile?.Close();
            }
        }
    }
}
