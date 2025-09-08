using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdController : MonoBehaviour
{
    public bool gameHasStarted = false;
    [SerializeField] private InputActionAsset _bird;
    private InputAction _jump;
    private InputAction _move;
    private Rigidbody _rb;
    [SerializeField] private float jumpStength;
    [SerializeField] private float moveStrength;
    public int score;
    public Score scoreObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        gameHasStarted = false;
            _jump = _bird["Jump"];
            _move = _bird["Move"];
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            OnJumpEnable();
            score = 0;
            UpdateScoreText();
    }
    

    private void FixedUpdate()
    {
        if (!gameHasStarted) return;
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
        gameHasStarted = true;
        _rb.useGravity = true;
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
        if (other.gameObject.CompareTag("Obstacle"))
        {
            print("GameOver");
            RemoveJumpListener();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Score")) return;
        print("Scored");
        score++;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreObject.score = score;
    }
}
