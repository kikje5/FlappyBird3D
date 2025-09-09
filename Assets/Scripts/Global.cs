using UnityEngine;
using UnityEngine.UIElements;

public class Global : ScriptableObject
{
    public int score = 0;
    public bool isPlaying = false;
    private bool _isDead = false;
    public Visibility gameOverVisible = Visibility.Hidden;

    public bool isDead
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

    
}
