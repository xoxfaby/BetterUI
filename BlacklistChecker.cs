using Newtonsoft.Json.Linq;

namespace BetterUI
{
    public class BlacklistChecker : UnityEngine.MonoBehaviour
    {
        JArray blacklist;

        void Start()
        {
            StartCoroutine(CheckBlacklist());
        }

        void Update()
        {
            if (blacklist != null && Facepunch.Steamworks.Client.Instance != null)
            {
                foreach (var id in blacklist)
                {
                    if (id.Value<ulong>() == Facepunch.Steamworks.Client.Instance.SteamId)
                    {
                        Destroy(BetterUIPlugin.instance);
                        UnityEngine.Debug.Log("Disabling BetterUI. You have been blacklisted from using BetterUI");
                    }
                }
                Destroy(this);
            }
        }
        System.Collections.IEnumerator CheckBlacklist()
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get("https://faby.dev/mod_blacklist.json");
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                UnityEngine.Debug.Log(www.error);
            }
            else
            {
                blacklist = JArray.Parse(www.downloadHandler.text);

            }
        }
    }
}