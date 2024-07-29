
using System.Net.Sockets;

string[] urls =
{
    "https://jsonplaceholder.typicode.com/posts",
    "https://jsonplaceholder.typicode.com/comments",
    "https://jsonplaceholder.typicode.com/albums"
};


Task<string>[] tasks = new Task<string>[urls.Length];

for (int i = 0; i < urls.Length; i++)
{
    tasks[i] = FetchDataAsync(urls[i]);
}

string[] results = await Task.WhenAll(tasks);

foreach (var result in results)
    Console.WriteLine(result.Substring(0, 100));

static async Task<string> FetchDataAsync(string url)
{
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}