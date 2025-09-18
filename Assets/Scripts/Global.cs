using UnityEngine;
using UnityEngine.Serialization;
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
    public int coins = 0;
    public bool isPlaying = false;
    private bool _isDead = false;
    public Visibility gameOverVisible = Visibility.Hidden;
    public DisplayStyle shieldIconVisible = DisplayStyle.None;
    public DisplayStyle doubleIconVisible = DisplayStyle.None;
    public DisplayStyle shrinkIconVisible = DisplayStyle.None;

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
    private bool _shield = false;
    private bool _shrunk = false;
    private bool _double = false;

    public bool ShieldIsActive
    {
        get => _shield;
        set
        {
            _shield = value;
            shieldIconVisible = value ?  DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    public bool ShrinkIsActive
    {
        get => _shrunk;
        set
        {
            _shrunk = value;
            shrinkIconVisible = value ?  DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    public bool DoubleIsActive
    {
        get => _double;
        set
        {
            _double = value;
            doubleIconVisible = value ?  DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    public float shrinkTimer;
    public float doubleTimer;
    
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("highScore", highScore);
    }

    public void SaveCoins()
    {
        PlayerPrefs.SetInt("coins", coins);
    }

    public void AddPlayTime()
    {
        int currentPlayTime =  PlayerPrefs.GetInt("playTimes", 0);
        PlayerPrefs.SetInt("playTimes", currentPlayTime + 1);
    }
}
