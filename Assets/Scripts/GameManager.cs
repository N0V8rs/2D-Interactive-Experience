using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager _uiManager;
    public LevelManager _levelManager;

    public GameObject player;
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject winScreen;
    public GameObject loseScreen;

    public enum GameState { MainMenu, Gameplay, Paused, Options, GameWin, GameOver }
    public GameState gameState;

    public void Awake()
    {
        Cursor.visible = true;
        gameState = GameState.MainMenu;

        player.SetActive(true);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);

        _levelManager = FindObjectOfType<LevelManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Update()
    {
        // Pause game 
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (gameState == GameState.Gameplay)
            {
                gameState = GameState.Paused;
                Paused();
            }
            else if (gameState == GameState.Paused)
            {
                ResumeGame();
            }
        }

        switch (gameState)
        {
            case GameState.MainMenu:
                MainMenu();
                break;
            case GameState.Gameplay:
                Gameplay();
                break;
            case GameState.Paused:
                Paused();
                break;
            case GameState.Options:
                Options();
                break;
            case GameState.GameWin:
                GameWin();
                break;
            case GameState.GameOver:
                GameOver();
                break;

        }
    }

    public void MainMenu()
    {
        gameState = GameState.MainMenu;
        _uiManager.UIMainMenu();
        pauseMenu.SetActive(false);
        mainMenu.SetActive(true);
        player.SetActive(false);


    }

    public void Gameplay()
    {
        Time.timeScale = 1f;
        _uiManager.UIGameplay();
        Cursor.visible = false;
        player.SetActive(true);


    }

    public void Paused()
    {
        gameState = GameState.Paused;
        _uiManager.UIpause();
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);

        Time.timeScale = 0f;
        Cursor.visible = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Gameplay();
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        gameState = GameState.Gameplay;
    }

    public void Options()
    {
        gameState = GameState.Options;
        optionsMenu.SetActive(true);
        _uiManager.UIoptions();

    }

    public void GameWin()
    {
        Time.timeScale = 0f;
        gameState = GameState.GameWin;
        _uiManager.UIGameWin();
        winScreen.SetActive(true);
        Cursor.visible = true;

    }
    public void GameOver()
    {
        Time.timeScale = 0f;
        gameState = GameState.GameOver;
        _uiManager.UIGameOver();
        Cursor.visible = true;
        loseScreen.SetActive(true);
    }

}
