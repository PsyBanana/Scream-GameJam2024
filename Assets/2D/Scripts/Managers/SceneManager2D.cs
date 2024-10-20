using UnityEngine.SceneManagement;
using StimpakEssentials;

public class SceneManager2D : SingletonBehaviour<SceneManager2D>
{
    public void LoadScene(string sceneName)
    {
        if (!SceneManager.GetSceneByName(sceneName).isLoaded)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
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
}