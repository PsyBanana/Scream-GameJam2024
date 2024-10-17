using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConectionPause : MonoBehaviour
{
    public bool isConnected = false;
    public PlayerMovement3D playerMovement3D; // Reference for the 3D player movement script
    public PlayerMovement2D playerMovement2D; // Reference for the 2D player movement script

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f); // Wait to ensure the 2D scene is loaded

        playerMovement3D = FindObjectOfType<PlayerMovement3D>();
        playerMovement2D = FindObjectOfType<PlayerMovement2D>(); // Find the 2D player movement script

        if (playerMovement2D == null)
        {
            Debug.LogWarning("PlayerMovement2D script not found!");
        }
    }

    private void Update()
    {
        // Ensure the references are not null before accessing them
        if (playerMovement2D != null && playerMovement3D != null)
        {
            // Enable or disable player movement based on connection status
            playerMovement3D.canMove = !isConnected;
            playerMovement2D.canMove = isConnected;
        }
    }

    public void SetConnectionStatus(bool status)
    {
        isConnected = status;
    }
}