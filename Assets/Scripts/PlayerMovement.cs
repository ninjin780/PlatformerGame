using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float speed = 5.0f;
    private bool facingRight = false;

    public ParticleSystem WalkParticles;
    public Animator Animator;


    Rigidbody2D rb;
    private float horizontalDir; // Horizontal move direction value [-1, 1]
     
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    void FixedUpdate()
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.x = horizontalDir * speed;
        rb.linearVelocity = velocity;
        Animator.SetFloat("isWalking", Mathf.Abs(rb.linearVelocity.x));

        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f)
        {
           if (!WalkParticles.isPlaying)
                WalkParticles.Play();
        
        }
        else
        {
            if (WalkParticles.isPlaying)
                WalkParticles.Stop();
        }



        if (horizontalDir > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalDir < 0 && facingRight)
        {
            Flip();
        }

    }
    

    void OnMove(InputValue value)
    {
        
        var inputVal = value.Get<Vector2>();
        horizontalDir = inputVal.x;
        
    }
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }


}