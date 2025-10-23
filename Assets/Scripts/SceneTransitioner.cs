using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public static SceneTransitioner Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void TransitionToScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void TransitionToSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void TransitionToLeaderBoard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void TransitionToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}