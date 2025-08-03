using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour, IPlayer, IDamageable
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float rotationSpeed = 0.5f;

    private Rigidbody rb;
    private bool isGrounded;
    private float yaw;

    private HealthSystem healthSystem;
    private ScoreSystem scoreSystem;

    public event Action OnDeath;
    public event Action<int> OnScoreChanged;
    public event Action<float> OnHealthChanged;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;

        healthSystem = new HealthSystem(30f);
        scoreSystem = new ScoreSystem();
    }

    private void Update()
    {
        Rotate();
        Jump();

        if (healthSystem.IsDead)
        {
            OnDeath?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void Move()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 direction = transform.forward * v + transform.right * h;
        rb.MovePosition(rb.position + direction.normalized * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Collectible"))
        {
            scoreSystem.AddScore(10);
            OnScoreChanged?.Invoke(scoreSystem.Score);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        healthSystem.TakeDamage(amount);
        OnHealthChanged?.Invoke(healthSystem.CurrentHealth);
    }

    public int GetScore() => scoreSystem.Score;
}
