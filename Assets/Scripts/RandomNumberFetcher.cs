using System.Net;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomNumberFetcher
{
    private static int min = 1;
    private static int max = 6;
    private const string API_KEY = "8cc74b05-20ec-4089-b051-e3a6309b7e9f";
    private static string RANDOM_ORG_API_URL = $"https://www.random.org/integers/?num=1&min={min}&max={max}&col=1&base=10&format=plain&rnd=new";
    public static async UniTask<int> FetchRandomNumber()
    {
        int randomNumber;

        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            await UniTask.WaitForSeconds(2);
            randomNumber = Random.Range(min, max + 1);
            Debug.Log($"Local value {randomNumber}");
        }
        else
        {
            string url = RANDOM_ORG_API_URL + "&apiKey=" + API_KEY;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseText = reader.ReadToEnd();
            randomNumber = int.Parse(responseText);

            Debug.Log($"Online value {randomNumber}");
        }

        return randomNumber;
    }
}