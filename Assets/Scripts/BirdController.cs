using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
            _jump = _bird["Jump"];
            _move = _bird["Move"];
            _rb = GetComponent<Rigidbody>();
            _particles = GetComponent<ParticleSystem>();
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
        if (!other.gameObject.CompareTag("Obstacle")) return;
        _gameOverUIDocument.enabled = true;
        global.isDead = true;
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
    }
    public void Reset()
    {
        transform.position = Vector3.zero;
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
