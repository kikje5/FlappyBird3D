using UnityEngine;
using UnityEngine.UIElements;

public class Global : ScriptableObject
{
    public int uiScore = 0;
    public int Score
    {
        get => uiScore;
        set
        {
            uiScore = value;
            if (value > highScore)
            {
                highScore = value;
            }
        }
    }
    public int highScore = 0;
    public bool isPlaying = false;
    private bool _isDead = false;
    public Visibility gameOverVisible = Visibility.Hidden;

    public bool IsDead
    {
        get => _isDead;
        set
        {
            _isDead = value;
            gameOverVisible = value ?  Visibility.Visible : Visibility.Hidden;
        }
    }
    
    public bool resetObstacles = false;
    public bool resetBird = false;
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("highScore", highScore);
    }
}
