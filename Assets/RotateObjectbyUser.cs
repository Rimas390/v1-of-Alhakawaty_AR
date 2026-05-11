using System.Collections.Generic;
using UnityEngine;

public class RotateObjectbyUser : MonoBehaviour
{
    [Header("Settings")]
    public float sensitivity = 0.5f; // سرعة الدوران
    
    private Vector2 lastTouchPosition;
    private bool isDragging = false;

    void Update()
    {
        // التحقق من وجود لمسة واحدة
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // سجلنا بداية اللمس
                lastTouchPosition = touch.position;
                isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // حساب حركة الإصبع الأفقية فقط
                float deltaX = touch.position.x - lastTouchPosition.x;

                // تدوير الحاوية (الأب) حول محور Y
                // هذا يضمن أن الجبل يدور في مكانه لأننا عدلنا Pivot سابقاً
                transform.Rotate(Vector3.up, -deltaX * sensitivity, Space.Self);

                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
            }
        }
    }
}