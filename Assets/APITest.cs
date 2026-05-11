using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class APITest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get("https://jawwabar-001-site1.ntempurl.com/api/Landmarks/1");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            Debug.Log("Response: " + request.downloadHandler.text);
        }
    }
}