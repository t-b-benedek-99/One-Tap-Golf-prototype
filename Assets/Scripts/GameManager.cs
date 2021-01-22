using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance;  } }

    [SerializeField] int score;

    public int Score { get { return score; } }

    public float level;

    GameState currentGameState;

    public GameState CurrentGameState { get { return currentGameState;  } }

    public static event Action OnGameStateChanged;
    public static event Action OnScoreChanged;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }        
    }

    public void UpdateScore(int scoreToAdd)
    {
        GenerateNextLevel();
        score += scoreToAdd;
        OnScoreChanged();
    }

    public void GenerateNextLevel()
    {
        level++;
        SceneManager.LoadScene("Game");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
        score = 0;
        level = 1;
        OnScoreChanged();
        Time.timeScale = 1;
        ChangeState(GameState.RUNNING);
        TrajectoryController.Instance.velocity.x = 6.4f;
    }

    public void ChangeState(GameState newState)
    {
        currentGameState = newState;        
        if (newState == GameState.OVER)
        {
            GameOver();
        }
        OnGameStateChanged();
    }

    public void GameOver()
    {
        if(PlayerPrefs.GetInt("Best Score") < score)
        {
            PlayerPrefs.SetInt("Best Score", score);
        }
    } 
}

public enum GameState
{
    RUNNING,
    OVER
}