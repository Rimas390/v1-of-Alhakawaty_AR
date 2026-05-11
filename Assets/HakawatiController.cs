using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class HakawatiController : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioClip cachedClip; // مخزن للملف المحمل مسبقاً

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        // ابدأ التحميل المسبق فور تشغيل التطبيق
        string audioUrl = "https://jawwabgp26-001-site1.stempurl.com/audio/audio_Al-hakawati_ar_01.mp3";
        StartCoroutine(PreloadAudio(audioUrl));
    }

    IEnumerator PreloadAudio(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                cachedClip = DownloadHandlerAudioClip.GetContent(www);
                Debug.Log("تم التحميل المسبق للصوت بنجاح!");
            }
            else
            {
                Debug.LogError("فشل التحميل المسبق: " + www.error);
            }
        }
    }

    public void PlayVoice(string url) 
    {
        // إذا كان الملف محمل مسبقاً، شغله فوراً
        if (cachedClip != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = cachedClip;
                audioSource.Play();
            }
        }
        else
        {
            // إذا لم ينتهِ التحميل المسبق بعد، حاول تحميله بالطريقة العادية
            Debug.LogWarning("الصوت لم يجهز بعد، جاري التحميل السريع...");
            StartCoroutine(DownloadAndPlay(url));
        }
    }

    IEnumerator DownloadAndPlay(string url)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                cachedClip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = cachedClip;
                audioSource.Play();
            }
        }
    }
}