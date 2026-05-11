using UnityEngine;

public class SmartFollow : MonoBehaviour
{
    public Transform targetCamera; 

    void LateUpdate()
    {
        if (targetCamera != null)
        {
            
            Vector3 forwardPos = targetCamera.position + targetCamera.forward * 2.0f;
            
            
            forwardPos.y = 0.5f; 
            transform.position = forwardPos;

            
            Vector3 lookAtPos = targetCamera.position;
            lookAtPos.y = transform.position.y; 
            transform.LookAt(lookAtPos);
        }
    }
}