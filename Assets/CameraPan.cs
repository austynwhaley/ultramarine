using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public Transform player;     
    public float panSpeed = 4.5f;   
    public float panThreshold = 0.39f;

    private Vector3 cameraInitialPosition;

    void Start()
    {
        cameraInitialPosition = transform.position;
    }

    void Update()
    {
        // Calculate the player's position relative to the camera's viewport
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(player.position);

        // If the player is outside the viewport on the left
        if (viewportPosition.x < 0 + panThreshold)
        {
            PanCamera(Vector3.left);
        }
        // If the player is outside the viewport on the right
        else if (viewportPosition.x > 1 - panThreshold)
        {
            PanCamera(Vector3.right);
        }
    }

    // Method to pan the camera in a specific direction
    private void PanCamera(Vector3 direction)
    {
        transform.position += direction * panSpeed * Time.deltaTime;
    }
}
