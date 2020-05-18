using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        lost,
        won,
        playing
    }
    public GameState gameState = GameState.playing;

    private static GameManager instance;
    public static GameManager Get()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartGameplay()
    {
        gameState = GameState.playing;
        SceneManager.LoadScene(1);
    }
    public void GameOver(int win)
    {
        gameState = (GameState)win;
        SceneManager.LoadScene(2);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
        gameState = GameState.playing;
    }

}

