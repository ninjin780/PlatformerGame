using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float speed = 5.0f;

    public Animator animator;
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


        animator.SetFloat("isWalking", rb.linearVelocity.magnitude);

    }
    void OnMove(InputValue value)
    {
        
        var inputVal = value.Get<Vector2>();
        horizontalDir = inputVal.x;

        
    }
    
}