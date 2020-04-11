using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D;
    public Animator animator;
    BasicAttack basicAttack;

    [SerializeField] LayerMask ground = default;
    [SerializeField] Transform groundCheck = default;

    public float moveSpeed = 16f;
    public float sprintSpeed = 24f;
    float xMove = 0f;

    public float jumpVelocity = 7f;
    public float fallMultiplier = 2f;
    public float lowJumpMultiplier = 2f;
    Vector3 velocity = Vector3.zero;

    public bool isGrounded;
    bool facingRight = true;


    public UnityEvent OnLandEvent;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        basicAttack = GetComponentInChildren<BasicAttack>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if (!basicAttack.attacking || !isGrounded)
        {
            // Set horizontal movement
            if (Input.GetButton("Shift"))
                xMove = Input.GetAxisRaw("Horizontal") * sprintSpeed;
            else
                xMove = Input.GetAxisRaw("Horizontal") * moveSpeed;

            animator.SetFloat("Speed", Mathf.Abs(xMove));

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb2D.velocity = Vector2.up * jumpVelocity;
                animator.SetBool("isJumping", true);
            }
        }
        else
            xMove = 0;

        // Flip the player
        if ((xMove > 0 && !facingRight) || (xMove < 0 && facingRight))
            Flip();
    }

    void FixedUpdate()
    {
        // Physics to control jump height depending on how long the Jump button is pressed
        if (rb2D.velocity.y < 0)
            rb2D.velocity += Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        else if (rb2D.velocity.y > 0 && !Input.GetButton("Jump"))
            rb2D.velocity += Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        animator.SetFloat("yVelocity", rb2D.velocity.y);

        // Checking if player is grounded or not
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, .1f, ground);
        for (int i = 0; i < colliders.Length; i++)
        {
            // Ignore one-way platforms if currently jumping up (not falling)
            if (colliders[i].CompareTag("One-way"))
                // Using > 0 instead of > 0.2 causes some issues with jumping even when standing still on platform
                if (rb2D.velocity.y > 0.2)
                    continue;
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
            }
        }

        Vector3 targetVelocity = new Vector2(xMove * 25f * Time.fixedDeltaTime, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, 0.05f);
    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        animator.SetFloat("yVelocity", 0);
    }

    // Flips the sprite
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void EnableHitbox()
    {
        basicAttack.EnableHitbox();
    }

    void DisableHitbox()
    {
        basicAttack.DisableHitbox();
    }

    // Used for moving forward on warrior bassic attack
    void MoveOnAttack()
    {
        float xMove;
        if (facingRight)
            xMove = 500;
        else
            xMove = -500;

        if (isGrounded)
        {
            Vector3 targetVelocity = new Vector2(xMove * 25f * Time.fixedDeltaTime, 0);
            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, 0.05f);
        }
    }
}
