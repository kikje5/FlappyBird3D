using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotationController : MonoBehaviour
{
    [SerializeField] private InputActionAsset _bird;
    public Global global;
    private InputAction _jump;
    private InputAction _move;
    private Rigidbody _rb;
    public Transform rotateMesh;

    public float verticalRotationStrength;

    public float verticalRotationSensitivity;
    
    public float horizontalRotationStrength;
    public float horizontalRotationSensitivity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _move = _bird["Move"];
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (global.isDead) return;
        float verticalSpeed = _rb.linearVelocity.y;
        float desiredXRotation =  -verticalSpeed * verticalRotationStrength;

        float currentXRotation = rotateMesh.rotation.eulerAngles.x;
        if (currentXRotation > 180)
        {
            currentXRotation -= 360;
        }
        
        float xRotation = Mathf.Lerp(currentXRotation, desiredXRotation, verticalRotationSensitivity * Time.deltaTime);
        
        float moveDirection = _move.ReadValue<float>();
        float desiredZRotation = -moveDirection * horizontalRotationStrength;
        
        float currentZRotation = rotateMesh.rotation.eulerAngles.z;
        if (currentZRotation > 180)
        {
            currentZRotation -= 360;
        }
        
        float zRotation = Mathf.Lerp(currentZRotation, desiredZRotation, horizontalRotationSensitivity * Time.deltaTime);
        

        rotateMesh.rotation = Quaternion.Euler(xRotation, 0, zRotation);
    }
}
