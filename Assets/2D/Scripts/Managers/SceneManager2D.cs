using UnityEngine.SceneManagement;
using StimpakEssentials;
using System.Collections;
using UnityEngine;

public class SceneManager2D : SingletonBehaviour<SceneManager2D>
{
    public void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            StartCoroutine(LoadSceneAtPosition(sceneName, new Vector3(0f, 250f, 0f)));
        }
    }

    public void UnloadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
        }
    }

    public void SwitchScene(string newSceneName, string currentStringName)
    {
        LoadScene(newSceneName);
        UnloadScene(currentStringName);
    }

    IEnumerator LoadSceneAtPosition(string sceneName, Vector3 positionOffset)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

        Scene loadedScene = SceneManager.GetSceneByName(sceneName);

        yield return loadedScene.isLoaded;

        Player2D player2D = FindAnyObjectByType<Player2D>();
        PlayerMovement2D playerMovement2D = FindAnyObjectByType<PlayerMovement2D>();

        if (loadedScene.isLoaded)
        {
            GameObject[] rootObjects = loadedScene.GetRootGameObjects();
            foreach (GameObject obj in rootObjects)
            {
                obj.transform.position += positionOffset;
            }

            if (player2D != null)
            {
                if (ES3.KeyExists(player2D.GetUniqueID() + "_Position"))
                {
                    player2D.LoadPlayerPosition();
                }
                else
                {
                    if (playerMovement2D != null)
                    {
                        playerMovement2D.GetRigidbody().MovePosition(positionOffset);
                    }
                }
            }
        }
    }
}