using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Die URL
        string apiUrl = "https://random-word-api.herokuapp.com/word";

        Console.WriteLine("Wie viele Wörter?");
        string time = Console.ReadLine();
        int wieOft = int.Parse(time);

        // Liste
        var tasks = Enumerable.Range(0, wieOft)
            .Select(i => Task.Run(() => GetAndPrintRandomWord(apiUrl)))
            .ToArray();

        // Wie KLange
        await Task.WhenAll(tasks);

        // Damit nicht zu
        Console.ReadLine();
    }

    static async Task GetAndPrintRandomWord(string apiUrl)
    {
        string randomWord = await GetRandomWord(apiUrl);
        Console.WriteLine(randomWord);
    }

    static async Task<string> GetRandomWord(string apiUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            try
            {
                // Die Request an sich
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                // Wen gut
                if (response.IsSuccessStatusCode)
                {
                    // Bro kommt an
                    string result = await response.Content.ReadAsStringAsync();
                    result = result.Trim('[', ']', '"');
                    return result;
                }
                else
                {
                    // Schaise passiert
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return null;
        }
    }
}
