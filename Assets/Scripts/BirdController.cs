using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class BirdController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _bird;
    public AudioSource jumpAudio;
    public float jumpPitchRange;
    public AudioSource deathAudio;
    private InputAction _jump;
    private InputAction _move;
    private Rigidbody _rb;
    private ParticleSystem _particles;
    [SerializeField] private float jumpStength;
    [SerializeField] private float moveStrength;
    public Global global;
    public GameObject gameOverUI;
    private UIDocument _gameOverUIDocument;

    private void Awake()
    {
        global.highScore =  PlayerPrefs.GetInt("highScore", 0);
        global.coins = PlayerPrefs.GetInt("coins", 0);
    }

    private void Start()
    {
            _jump = _bird["Jump"];
            _move = _bird["Move"];
            _rb = GetComponent<Rigidbody>();
            _particles = GetComponent<ParticleSystem>();
            _rb.useGravity = false;
            OnJumpEnable();
            global.Score = 0;
            global.isPlaying = false;
            global.IsDead = false;
            _gameOverUIDocument = gameOverUI.GetComponent<UIDocument>();
    }
    
    private void FixedUpdate()
    {
        if (global.IsDead) return;
        if (global.resetBird) Reset();
        if (!global.isPlaying) return;
        Move();
    }

    private void AddJumpListener()
    {
        _jump.performed += Jump;
    }

    private void RemoveJumpListener()
    {
        _jump.performed -= Jump;
    }

    private void OnJumpEnable()
    {
        AddJumpListener();
    }

    private void OnJumpDisable()
    {
        RemoveJumpListener();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, jumpStength, 0);
        global.isPlaying = true;
        _rb.useGravity = true;
        jumpAudio.pitch = Random.Range(0.5f, 0.5f + jumpPitchRange);
        jumpAudio.Play();
        _particles.Play();
    }

    private void Move()
    {
        float moveDirection = _move.ReadValue<float>();
        _rb.AddForce(new Vector3(moveDirection * moveStrength, 0, 0));
    }

    private void OnApplicationQuit()
    {
        RemoveJumpListener();
    }

    private void OnCollisionEnter(Collision other)
    {
        CheckObstacleCollision(other);
    }

    private void CheckObstacleCollision(Collision other)
    {
        if (!other.gameObject.CompareTag("Obstacle")) return;
        _gameOverUIDocument.enabled = true;
        global.IsDead = true;
        global.isPlaying = false;
        KillBird();
    }
    

    private void KillBird()
    {
        deathAudio.Play();
        _rb.useGravity = false;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        OnJumpDisable();
        global.SaveHighScore();
        global.SaveCoins();
    }
    public void Reset()
    {
        transform.position = Vector3.zero;
        global.Score = 0;
        global.resetBird = false;
        OnJumpEnable();
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckScoreTrigger(other);
        CheckCoinTrigger(other);
    }

    private void CheckScoreTrigger(Collider other)
    {
        if (!other.gameObject.CompareTag("Score")) return;
        global.Score++; 
    }
    private void CheckCoinTrigger(Collider other)
    {
        if (!other.gameObject.CompareTag("Coin")) return;
        global.coins++;
        other.gameObject.SetActive(false);
    }
}
