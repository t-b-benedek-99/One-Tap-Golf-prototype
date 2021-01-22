using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    [SerializeField] GameObject gameOverPanel;

    [SerializeField] TextMeshProUGUI thisScoreText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    [SerializeField] Text scoreText;

    [SerializeField] Text hintText;

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

    void OnEnable()
    {
        GameManager.OnGameStateChanged += ReactToGameStateChange;
        GameManager.OnScoreChanged += UpdateMainScoreCounter;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= ReactToGameStateChange;
        GameManager.OnScoreChanged -= UpdateMainScoreCounter;
    }

    void UpdateMainScoreCounter()
    {
        scoreText.text = GameManager.Instance.Score.ToString();
    }

    void ReactToGameStateChange()
    {
        switch (GameManager.Instance.CurrentGameState)
        {
            case GameState.RUNNING:
                gameOverPanel.SetActive(false);
                break;
            case GameState.OVER:
                thisScoreText.text = "Score: " + GameManager.Instance.Score.ToString();
                bestScoreText.text = "Best Score: " + PlayerPrefs.GetInt("Best Score").ToString();
                gameOverPanel.SetActive(true);
                Time.timeScale = 0;
                break;
            default:
                break;
        }
    }

    public void TurnHintTextOff()
    {
        hintText.gameObject.SetActive(false);
    }
}