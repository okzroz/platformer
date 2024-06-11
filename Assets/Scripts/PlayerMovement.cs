using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpForce = 5f;
    public LayerMask groundLayer;
    public float groundCheckDistance = 0.1f;

    private Animator animator;
    private Rigidbody2D body;
    private bool onGround;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(body.velocity.magnitude));
        CheckIfGrounded();
        if (Input.GetAxis("Jump") > 0.0f && onGround)
        {
            body.velocity = new Vector2(body.velocity.x, jumpForce);
     
            onGround = false;
        }
    }
    void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider != null && Vector2.Angle(hit.normal, Vector2.up) < 45f)
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }
}
