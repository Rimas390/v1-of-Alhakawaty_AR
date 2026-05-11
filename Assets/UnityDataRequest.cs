using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[System.Serializable]
public class LandmarkData
{
    public int landmark_id;
    public string model_3d;
    public string language_Name;
    public string script;
    public string voice_language;
}

public class UnityDataRequest : MonoBehaviour
{
    private string baseUrl = "https://jawwabar-001-site1.ntempurl.com/api/Landmarks/";

    public IEnumerator GetLandmarkInfo(int id, Action<LandmarkData> onSuccess, Action onFailure)
    {
        string finalUrl = baseUrl + id;
        Debug.Log("Final URL: " + finalUrl);

        using (UnityWebRequest webRequest = UnityWebRequest.Get(finalUrl))
        {
            // Authentication
            string user = "11307117";
            string pass = "60-dayfreetrial";
            string auth = Convert.ToBase64String(
                System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(user + ":" + pass)
            );

            webRequest.SetRequestHeader("Authorization", "Basic " + auth);
            webRequest.certificateHandler = new BypassCertificate();

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("RAW JSON: " + webRequest.downloadHandler.text);

                LandmarkData data = JsonUtility.FromJson<LandmarkData>(webRequest.downloadHandler.text);

                onSuccess?.Invoke(data);
            }
            else
            {
                Debug.LogError("API Error: " + webRequest.error);
                onFailure?.Invoke();
            }
        }
    }
}

// تجاوز SSL (للتجارب فقط)
public class BypassCertificate : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData) => true;
}