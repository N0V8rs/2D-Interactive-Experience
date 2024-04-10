using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public GameObject player;

    public GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void LoadScene(string name)
    {
        SceneManager.sceneLoaded += OnSceneLoad;

        if (name == "Level1" || name == "Level2")
        {
            gameManager.gameState = GameManager.GameState.Gameplay;
        }
        else if (name == "MainMenu")
        {
            gameManager.gameState = GameManager.GameState.MainMenu;
        }
        else if (name == "WinningScene")
        {
            gameManager.gameState = GameManager.GameState.GameWin;
        }
        SceneManager.LoadScene(name);
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnPoint = GameObject.FindWithTag("SpawnPlayer");
        if (spawnPoint != null && player != null)
        {
            player.SetActive(true);
            player.transform.position = spawnPoint.transform.position;
            SceneManager.sceneLoaded -= OnSceneLoad;
        }
        else
        {
            Debug.LogError("Spawn point or player not found");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
