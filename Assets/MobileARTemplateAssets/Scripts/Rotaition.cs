using UnityEngine;
public class The3DMoving : MonoBehaviour
{
    public float rotationSpeed = 3f;

    void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);
    }
}



