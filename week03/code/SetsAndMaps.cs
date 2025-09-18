using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // TODO Problem 1 - ADD YOUR CODE HERE
        HashSet<string> wordSet = new HashSet<string>(words);
        HashSet<string> pairsFound = new HashSet<string>();
        List<string> result = new List<string>();

        foreach (string i in words)
        {
            // Skip words with same letter (example "aa")
            if (i[0] == i[1])
                continue;

            string reversed = new string(new char[] { i[1], i[0] });

            // Check if reversed word exists in the set
            if (wordSet.Contains(reversed) && !pairsFound.Contains(i) && !pairsFound.Contains(reversed))
            {
                // Adding to results
                result.Add($"{reversed} & {i}");
                pairsFound.Add(i);
                pairsFound.Add(reversed);
            }
        }

        return result.ToArray();
    }


    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename) // filename = "census.txt"
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // TODO Problem 2 - ADD YOUR CODE HERE

            // Checking if the line has at least 4 columns (JUST IN CASE)
            if (fields.Length >= 4)
            {
                string degree = fields[3].Trim(); // Searching in the 4th column (USING the index #3)

                // Counting degrees
                if (degrees.ContainsKey(degree))
                {
                    degrees[degree]++;
                }
                else
                {
                    degrees[degree] = 1;
                }
            }
        }

        return degrees;
    }
    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // TODO Problem 3 - ADD YOUR CODE HERE
        // Remove spaces and convert to lowercase both WORDS
        // HANDLE both assumptions in line 112 and 113
        // Assumption #1: When determining if two words are anagrams, you should ignore any spaces.
        // Assumption #2: You should ignore letter case. For example, 'Ab' and 'bA' should be considered anagrams.
        string cleanWord1 = word1.Replace(" ", "").ToLower();
        string cleanWord2 = word2.Replace(" ", "").ToLower();

        // If lengths are different after removing spaces, they CANNOT be anagrams
        if (cleanWord1.Length != cleanWord2.Length)
            return false; //using false as word is NOT an anagram

        // Creating a Dictionary to counting letters
        Dictionary<char, int> charCount = new Dictionary<char, int>();

        // Counting letters from first word
        foreach (char l in cleanWord1)
        {
            if (charCount.ContainsKey(l))
                charCount[l]++;
            else
                charCount[l] = 1;
        }

        // Subtract counts using second word
        foreach (char l in cleanWord2)
        {
            if (!charCount.ContainsKey(l))
                return false; // Because the Letter is not found in first word

            charCount[l]--;

            // If count becomes negative, second word has more of this letter
            if (charCount[l] < 0)
                return false;
        }

        // Checking if all counts are zero (all letters balanced)
        foreach (int count in charCount.Values)
        {
            if (count != 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        if (featureCollection?.Features == null)
            return Array.Empty<string>();

        var summaries = new List<string>();
        foreach (var feature in featureCollection.Features)
        {
            if (feature?.Properties != null)
            {
                string place = feature.Properties.Place ?? "Unknown location";
                decimal magnitude = feature.Properties.Mag;
                summaries.Add($"{place} - Mag {magnitude}");
            }
        }

        return summaries.ToArray();
    }
}