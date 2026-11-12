using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform followObject;
    public float smoothTime;
    private Vector3 _vel;

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position,
            followObject.position, ref _vel, smoothTime);
    }
}
