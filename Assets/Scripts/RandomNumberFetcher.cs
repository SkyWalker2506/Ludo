using System.Net;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class RandomNumberFetcher
{
    private static int min = 1;
    private static int max = 6;
    private const string API_KEY = "dvp8PhxWTp60tA3HAqrW3YbjfeH60OggkuvYTmPhN/dM0IwB9Tw1Dp73p63j3BHC20GisQ9NeQvuI4Ub8S5Agw==";
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
        }

        return randomNumber;
    }
}