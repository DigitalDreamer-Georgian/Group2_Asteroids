using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneLoader : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Method to load a scene additively
    public void LoadSceneAdditively(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        //Pause the current scene
        Time.timeScale = 0f;
    }
    // Example method to unload a scene
    public void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
        // Resume the current scene after unloading the additive scene
        Time.timeScale = 1f;
    }
}