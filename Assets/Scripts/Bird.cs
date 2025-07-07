
using UnityEngine;
using UnityEngine.InputSystem;

public class Bird : MonoBehaviour
{
    public float jumpForce = 3f;
    public float rotateSpeed = 3f;
    private Rigidbody2D rd;
    private Animator animator;
    public bool isLive = true;
    private bool isDeadRotate = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLive)
        {
            if (isDeadRotate)
            {
                // 죽었을 때 아래로 회전
                transform.rotation = Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.Euler(0, 0, -90f),
                    Time.deltaTime * rotateSpeed
                );
            }
            return;
        }

        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            rd.linearVelocity = Vector2.up * jumpForce;
        }

        float vy = rd.linearVelocity.y;
        float targetAngle = vy > 0.1f ? 45f : (vy < -0.1f ? -45f : 0f);
        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            Quaternion.Euler(0, 0, targetAngle),
            Time.deltaTime * rotateSpeed
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isLive) return;

        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Pipe"))
        {
            isLive = false;
            isDeadRotate = true;
            Debug.Log("Game Over");
            animator.SetBool("Dead", true);
        }
    }
}
