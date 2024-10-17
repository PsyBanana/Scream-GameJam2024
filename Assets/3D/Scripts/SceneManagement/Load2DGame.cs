using UnityEngine;
using UnityEngine.SceneManagement;

public class Load2DGame : MonoBehaviour
{
    public string sceneName = "2D";

    void Start()
    {

        GameObject parentObject = new GameObject("2DSceneParent");
        parentObject.transform.position = new Vector3(0, 0, -50); // Set a position far from the player

        // Load the 2D game scene additively
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        Debug.Log($"{sceneName} loaded.");
    }
}