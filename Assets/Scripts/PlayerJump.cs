using System;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerJumper : MonoBehaviour
{
    [Header("Jump Parameters")]
    public float JumpHeightPublic = 3.0f;              
    public float DistanceToMaxHeight = 2.0f;     
    public float SpeedHorizontal = 5.0f;         
    public float PressTimeToMaxJump = 0.25f;
    private float jumpHeight = 3.0f;

    [Header("Ground Detection")]
    public ContactFilter2D Filter;
    public float GroundCheckDistance = 0.6f;

    [Header("Jump Limits")]
    public int MaxJumps = 1;

    private Rigidbody2D rb;
    private float jumpStartTime;
    private float lastVelocityY;

    private float powerUpEndTime = 0.0f;
    private int jumpsUsed = 0;

    public Animator Animator;
    private bool grounded;
    public AudioSource JumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        Filter.useLayerMask = true;
        Filter.useTriggers = false;
    }

    void FixedUpdate()
    {
        grounded = IsGrounded();

        if (grounded)
        {
            jumpsUsed = 0;
        }
       
        else if (IsPeakReached())
        {
            TweakGravity();
        }
        if (Time.time > powerUpEndTime)
        {
            jumpHeight = JumpHeightPublic;
        }
        CheckFallState();
    }

    public void OnJump()
    {
        JumpSound.Play();
        if (jumpsUsed >= MaxJumps) return;

        SetGravity();
        Vector2 velocity = rb.linearVelocity;
        velocity.y = GetJumpForce();
        rb.linearVelocity = velocity;
        jumpStartTime = Time.time;
        jumpsUsed++;
        

    }

    public void OnJumpFinished()
    {
        float fraction = 1f - Mathf.Clamp01((Time.time - jumpStartTime) / PressTimeToMaxJump);
        rb.gravityScale *= fraction;

    }

    private void SetGravity()
    {
        float gravity = (2 * jumpHeight * SpeedHorizontal * SpeedHorizontal) / (DistanceToMaxHeight * DistanceToMaxHeight);
        rb.gravityScale = gravity / 9.81f;
    }

    private float GetJumpForce()
    {
        return 2f * jumpHeight * SpeedHorizontal / DistanceToMaxHeight;
    }

    private void CheckFallState()
    {
        if (rb.linearVelocity.y > 0.0f)
        {
            Animator.SetBool("isJumping", true);
        }
        else if (rb.linearVelocity.y < 0.0f)
        {
            Animator.SetBool("isJumping", false);
            Animator.SetBool("isFalling", true);
        }
        else if (grounded)
        {
            Animator.SetBool("isFalling", false);
        }
    }

    private bool IsPeakReached()
    {
            bool reached = lastVelocityY > 0 && rb.linearVelocity.y <= 0;
            lastVelocityY = rb.linearVelocity.y;
            return reached;
    }

    private void TweakGravity()
    {
        rb.gravityScale = rb.gravityScale * 1.2f;
    }

    private bool IsGrounded()
    {
            Vector2 origin = (Vector2)transform.position + Vector2.down * 0.5f;
            RaycastHit2D[] hits = new RaycastHit2D[2];

            int count = Physics2D.Raycast(origin, Vector2.down, Filter, hits, GroundCheckDistance);
            return count > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        float groundDistance = GetDistanceToGround();
        float h = groundDistance + jumpHeight;

        Vector3 start = transform.position + Vector3.left;
        Vector3 end = transform.position + Vector3.right;
        start.y = h;
        end.y = h;

        Gizmos.DrawLine(start, end);
    }

    private float GetDistanceToGround()
    {
        RaycastHit2D[] hits = new RaycastHit2D[3];
        int count = Physics2D.Raycast(transform.position, Vector2.down, Filter, hits, 10f);
        if (count == 0) return 0f;
        return hits[0].distance;
    }

    private void OnEnable()
    {
        PowerUp.OnPowerUpCollected += PowerUpBoost;
    }

    private void OnDisable()
    {
        PowerUp.OnPowerUpCollected -= PowerUpBoost;
    }

    private void PowerUpBoost(PowerUp collectedPower)
    {
        powerUpEndTime = Time.time + collectedPower.PowerUpTime;
        jumpHeight = collectedPower.PowerUpForce;
    }
}