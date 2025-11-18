using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerEdgeMovement : SingletonBehaviour<PlayerEdgeMovement>
{
    private Camera mainCamera;
    private float screenLeft;
    private float screenRight;
    private float screenTop;
    private float screenBottom;
    bool CanEdgeMovement;
    public float threshold = 0.5f;

    public void Init()
    {
        mainCamera = Camera.main;
        UpdateScreenBounds();
        CanEdgeMovement = true;
    }

    void UpdateScreenBounds()
    {
        screenLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        screenRight = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        screenTop = mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        screenBottom = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
    }
    void Update()
    {
        if (!CanEdgeMovement) return;
        Vector3 position = transform.position;

        // Wrap horizontally with threshold
        if (position.x < screenLeft - threshold)
        {
            position.x = screenRight + threshold;
        }
        else if (position.x > screenRight + threshold)
        {
            position.x = screenLeft - threshold;
        }

        // Wrap vertically with threshold
        if (position.y < screenBottom - threshold)
        {
            position.y = screenTop + threshold;
        }
        else if (position.y > screenTop + threshold)
        {
            position.y = screenBottom - threshold;
        }

        // Apply the new position
        transform.position = position;
    }
}


