using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TriggerScript : MonoBehaviour
{
    public GameManager gameManager;
    public LevelManager _levelManager;
    public string sceneName;

    void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<LevelManager>().LoadScene(sceneName);
        }

        if (other.gameObject.CompareTag("LoseTrigger"))
        {
            gameManager.GameOver();
            gameManager.gameState = GameManager.GameState.GameOver;
        }
    }
}
