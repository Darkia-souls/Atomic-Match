using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderAM : MonoBehaviour
{
    public void ReloadCurrentScene()
    {
        Scene currentScene = gameObject.scene; // Get the scene where this GameObject exists

        // Check if the scene is valid before unloading
        if (currentScene.IsValid() && currentScene.isLoaded)
        {
            SceneManager.UnloadSceneAsync(currentScene.name);
            SceneManager.LoadSceneAsync(currentScene.name, LoadSceneMode.Additive);
        }
        else
        {
            Debug.LogError("Scene is not valid or not loaded: " + currentScene.name);
        }
    }
}

