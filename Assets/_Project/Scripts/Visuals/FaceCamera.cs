using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }

    private void LateUpdate()
    {
        transform.forward = _camera.forward;
    }
}
