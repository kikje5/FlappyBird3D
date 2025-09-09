using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class BirdController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _bird;
    private InputAction _jump;
    private InputAction _move;
    private Rigidbody _rb;
    [SerializeField] private float jumpStength;
    [SerializeField] private float moveStrength;
    public Global global;
    public GameObject gameOverUI;
    private UIDocument _gameOverUIDocument;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
            _jump = _bird["Jump"];
            _move = _bird["Move"];
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = false;
            OnJumpEnable();
            global.score = 0;
            global.isPlaying = false;
            global.isDead = false;
            _gameOverUIDocument = gameOverUI.GetComponent<UIDocument>();
    }
    
    private void FixedUpdate()
    {
        if (global.isDead) return;
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
        if (!other.gameObject.CompareTag("Obstacle")) return;
        _gameOverUIDocument.enabled = true;
        global.isDead = true;
        global.isPlaying = false;
        OnJumpDisable();
    }
    public void Reset()
    {
        _rb.linearVelocity = Vector3.zero;
        transform.position = Vector3.zero;
        _rb.useGravity = false;
        global.score = 0;
        global.resetBird = false;
        OnJumpEnable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Score")) return;
        print("Scored");
        global.score++; 
    }
}
