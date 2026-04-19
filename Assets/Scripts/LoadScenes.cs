using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneLoader : MonoBehaviour
{
    [SerializeField] private string[] mapScenes;

    private void Start()
    {
        foreach (string sceneName in mapScenes)
        {
            if (!SceneManager.GetSceneByName(sceneName).isLoaded)
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
        }
    }
}
