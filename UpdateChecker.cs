using Newtonsoft.Json.Linq;

namespace BetterUI
{
    public class UpdateChecker : UnityEngine.MonoBehaviour
    {
        string latestVersion;
        void Start()
        {
            StartCoroutine(CheckForUpdate());
        }

        System.Collections.IEnumerator CheckForUpdate()
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get("https://faby.dev/api/v1/version/betterui");
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                UnityEngine.Debug.Log(www.error);
            }
            else
            {
                latestVersion = www.downloadHandler.text;

            }
        }
    }
}