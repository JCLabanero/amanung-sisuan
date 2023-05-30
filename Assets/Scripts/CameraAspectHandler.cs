using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAspectHandler : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f;
    public float orthographicSize = 5f;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();

        // Adjust camera size based on the target aspect ratio
        float currentAspectRatio = (float)Screen.width / Screen.height;
        float sizeMultiplier = currentAspectRatio / targetAspectRatio;
        mainCamera.orthographicSize = orthographicSize * sizeMultiplier;
    }
}

