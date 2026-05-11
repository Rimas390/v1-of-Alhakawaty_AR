using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;
using ArabicSupport;
using System.Collections;

public class ImageNameDisplay : MonoBehaviour
{
    [Header("AR Components")]
    public ARTrackedImageManager trackedImageManager;
    public TextMeshProUGUI nameText;

    [Header("Settings")]
    public GameObject modelInCorner;
    public GameObject hakawati; 

    void Awake()
    {
        if (trackedImageManager == null)
            trackedImageManager = GetComponent<ARTrackedImageManager>();
    }
   void Start()
    {
        if (modelInCorner != null) modelInCorner.SetActive(false);
        if (nameText != null) nameText.text = "";
    }

    void OnEnable() => trackedImageManager.trackablesChanged.AddListener(OnTrackablesChanged);
    void OnDisable() => trackedImageManager.trackablesChanged.RemoveListener(OnTrackablesChanged);

    void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
            UpdateImageStatus(trackedImage);

        foreach (var trackedImage in eventArgs.updated)
            UpdateImageStatus(trackedImage);

        foreach (var trackedImage in eventArgs.removed)
        {
            ClearDisplay();
        }
    }

    void UpdateImageStatus(ARTrackedImage trackedImage)
    {
        if (trackedImage.trackingState == TrackingState.Tracking)
        {
            if (modelInCorner != null) modelInCorner.SetActive(true);

            nameText.text = ArabicFixer.Fix(trackedImage.referenceImage.name, false, false);

            PlayLocalVoice();
        }
        else
        {
            ClearDisplay();
        }
    }

    void PlayLocalVoice()
    {
        if (hakawati != null)
        {
            var controller = hakawati.GetComponent<HakawatiController>();
            var audioSource = hakawati.GetComponent<AudioSource>();

            if (controller != null && audioSource != null)
            {
                // إذا كان الحكواتي لا يتحدث حالياً، ابدأ الحديث
                if (!audioSource.isPlaying)
                {
                    //controller.PlayVoice("audio_Al-hakawati_ar_01");
                    controller.PlayVoice("https://jawwabgp26-001-site1.stempurl.com/audio/audio_Al-hakawati_ar_01.mp3");

                    Debug.Log("بدأ الحكواتي بالكلام...");
                }
            }
        }
    }

    void ClearDisplay()
    {
        if (modelInCorner != null) modelInCorner.SetActive(false);
        if (nameText != null) nameText.text = ""; // مسح النص تماماً
        
        // اختياري: إيقاف الصوت فور اختفاء الصورة
        if (hakawati != null)
        {
            var audioSource = hakawati.GetComponent<AudioSource>();
            if (audioSource != null && audioSource.isPlaying) audioSource.Stop();
        }
    }
}