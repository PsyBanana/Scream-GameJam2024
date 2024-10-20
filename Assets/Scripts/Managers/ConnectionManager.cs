using System.Collections;
using UnityEngine;
using StimpakEssentials;
using UnityEngine.SceneManagement;

public class ConnectionManager : SingletonBehaviour<ConnectionManager>
{
    public bool IsConnected = false;
    public Player3D Player3D; // Reference for the 3D player movement script
    public PlayerMovement2D PlayerMovement2D; // Reference for the 2D player movement script

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f); // Wait to ensure the 2D scene is loaded

        Player3D = FindAnyObjectByType<Player3D>();
        PlayerMovement2D = FindAnyObjectByType<PlayerMovement2D>(); // Find the 2D player movement script

        if (PlayerMovement2D == null)
        {
            Debug.LogWarning("PlayerMovement2D script not found at startup, waiting for scene load.");
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void Update()
    {
        // Ensure the references are not null before accessing them
        if (PlayerMovement2D != null && Player3D != null)
        {
            // Enable or disable player movement based on connection status
            Player3D.CanMove = !IsConnected;
            PlayerMovement2D.canMove = IsConnected;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayerMovement2D = FindAnyObjectByType<PlayerMovement2D>();
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (PlayerMovement2D != null && PlayerMovement2D.gameObject.scene == scene)
        {
            PlayerMovement2D = null;
        }
    }

    public void SetConnectionStatus(bool status)
    {
        IsConnected = status;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
