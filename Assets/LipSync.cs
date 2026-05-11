using UnityEngine;

public class HeadBobSync : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform headBone; 
    public float sensitivity = 5.0f; 
    public float speed = 20.0f;    
    
    private Vector3 originalPosition;

    void Start()
    {
        if (headBone != null)
            originalPosition = headBone.localPosition;
    }

    // نستخدم LateUpdate لنتخطى تحكم الـ Animator
    void LateUpdate()
    {
        if (audioSource != null && audioSource.isPlaying && headBone != null)
        {
            float[] data = new float[256];
            audioSource.GetOutputData(data, 0);
            
            float volume = 0;
            foreach (var sample in data) volume += Mathf.Abs(sample);
            volume /= 256;

            // حساب حركة اهتزاز بسيطة للأعلى والأسفل بناءً على علو الصوت
            float displacement = volume * sensitivity;
            headBone.localPosition = originalPosition + new Vector3(0, displacement, 0);
        }
        else if (headBone != null)
        {
            // إعادة الرأس لمكانه الطبيعي عند التوقف
            headBone.localPosition = originalPosition;
        }
    }
}