using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    LayerMask groundlayer;

    public GameObject sword;
    private bool armed = true;

    private float horizontal;
    private float vertical;
    public float speed;
    private float normalSpeed;
    private float crouchSpeed = 0f;
    public float jumpPower = 16f;
    private bool isFacingRight = true;
    private bool diving = false;

    private bool dashStartUp = false;
    private bool dashing = false;
    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        Flip();

        Crouch();

        if (Input.GetButtonDown("Fire1") && armed == true)
        {
            armed = false;
            Instantiate(sword);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        if (!IsGrounded() && Input.GetButtonDown("Jump") && vertical == -1 && diving == false)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, -4);
            }
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 5f);
            diving = true;
            Debug.Log("diving");
        }

        if (IsGrounded())
        {
            diving = false;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundlayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void Crouch()
    {
        if (vertical <=-0.7 && IsGrounded())
        {
            speed = crouchSpeed;
        }
        
        if ((vertical >= -0.3 && IsGrounded()) || !IsGrounded())
        {
            speed = normalSpeed;
        }
    }
}
