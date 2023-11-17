using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;

    ScoreKeeper scoreKeeper;

    void Start()
    {
        scoreKeeper = ScoreKeeper.Instance;

        if (scoreKeeper == null)
        {
            Debug.Log("ScoreKeeper.Instance; is not found.");
        }
    }

    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene(GameConstants.GameSceneName);
    }

    public void ExitGame()
    {
        Debug.Log("Quitting Game.");
        Application.Quit();
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(GameConstants.MainMenuSceneName);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoadScene(GameConstants.GameOverSceneName, sceneLoadDelay));
    }

    IEnumerator WaitAndLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
