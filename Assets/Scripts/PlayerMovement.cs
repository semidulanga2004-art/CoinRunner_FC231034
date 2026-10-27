using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
public float moveSpeed = 6f;
public float jumpForce = 12f;
public Transform groundCheck; // the small object under the feet
public float groundRadius = 0.2f; // size of the &quot;feeler&quot; circle
public LayerMask groundLayer; // which layer counts as ground
private Rigidbody2D rb;
private float moveInput;
private bool isGrounded;
private SpriteRenderer spriteRenderer;
void Start()
{
rb = GetComponent<Rigidbody2D>();
spriteRenderer = GetComponent<SpriteRenderer>();
}
void Update()
{
moveInput = Input.GetAxisRaw("Horizontal");
if (moveInput != 0)
{
    spriteRenderer.flipX = moveInput < 0;
}
// is there any Ground inside the small circle under our feet?
isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
// only jump when standing on the ground
if (Input.GetButtonDown("Jump") && isGrounded)
{
rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
}
if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
{
    rb.linearVelocity = new Vector2(
        rb.linearVelocity.x,
        rb.linearVelocity.y * 0.5f
    );
}
}
void FixedUpdate()
{
rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
}
// draws the ground-check circle in the Scene view (editor only)
void OnDrawGizmosSelected()
{
if (groundCheck == null) return;
Gizmos.color = Color.yellow;
Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
}
}