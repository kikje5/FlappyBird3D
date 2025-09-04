using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BirdController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _bird;
    private InputAction _jump;
    private InputAction _move;
    private Rigidbody _rb;
    private Material mat;
    [SerializeField] private float _jumpStength;
    [SerializeField] private float _moveStrength;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
            _jump = _bird["Jump"];
            _move = _bird["Move"];
            _rb = GetComponent<Rigidbody>();
            OnJumpEnable();
            mat = GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    private void Update()
    {
        mat.SetVector("_ObjectVelocity", _rb.linearVelocity);
    }

    private void FixedUpdate()
    {
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
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _jumpStength, 0);
    }

    private void Move()
    {
        float moveDirection = _move.ReadValue<float>();
        _rb.AddForce(new Vector3(moveDirection * _moveStrength, 0, 0));
        
    }

    private void OnApplicationQuit()
    {
        RemoveJumpListener();
    }
}
