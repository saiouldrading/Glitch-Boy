using UnityEngine;
using UnityEngine.Video;

public class ParallexBackground : MonoBehaviour
{
    public Transform Camera;
    public float parallexEffect = 0.5f;
    private Vector3 LastCameraUpdate;
    void Start()
    {
        LastCameraUpdate = Camera.position;
    }
    void LateUpdate()
    {
        Vector3 CameraMovement = Camera.position - LastCameraUpdate;
        transform.position += new Vector3(CameraMovement.x * parallexEffect, CameraMovement.y * parallexEffect, 0);
        LastCameraUpdate = Camera.position;
    }
}
