using System.Collections;
using UnityEngine;
using StimpakEssentials;

public class ConnectionManager : SingletonBehaviour<ConnectionManager>
{
    public bool IsConnected = false;
    public PlayerMovement3D PlayerMovement3D; // Reference for the 3D player movement script
    public PlayerMovement2D PlayerMovement2D; // Reference for the 2D player movement script

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f); // Wait to ensure the 2D scene is loaded

        PlayerMovement3D = FindObjectOfType<PlayerMovement3D>();
        PlayerMovement2D = FindObjectOfType<PlayerMovement2D>(); // Find the 2D player movement script

        if (PlayerMovement2D == null)
        {
            Debug.LogWarning("PlayerMovement2D script not found!");
        }
    }

    private void Update()
    {
        // Ensure the references are not null before accessing them
        if (PlayerMovement2D != null && PlayerMovement3D != null)
        {
            // Enable or disable player movement based on connection status
            PlayerMovement3D.canMove = !IsConnected;
            PlayerMovement2D.canMove = IsConnected;
        }
    }

    public void SetConnectionStatus(bool status)
    {
        IsConnected = status;
    }
}
