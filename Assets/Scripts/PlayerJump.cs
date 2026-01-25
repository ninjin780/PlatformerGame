using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Header("Jump Parameters")]
    public float jumpHeight = 3.0f;              
    public float distanceToMaxHeight = 2.0f;     
    public float horizontalSpeed = 5.0f;         
    public float pressTimeToMaxJump = 0.25f; 

    [Header("Ground Detection")]
    public ContactFilter2D groundFilter;

    private Rigidbody2D rb;
    private float jumpStartTime;
    private float lastVelocityY;

    void Start()
    {
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isPeakReached())
        {
            tweakGravity();
        }
    }
    public void OnJumpStarted()
    {
        setGravity();
        Vector2 velocity = rb.linearVelocity;
        velocity.y = GetJumpForce();
        rb.linearVelocity = velocity;
        jumpStartTime = Time.time;
    }
    public void OnJumpFinished()
    {
        float fraction = 1f - Mathf.Clamp01((Time.time - jumpStartTime) / pressTimeToMaxJump);
        rb.gravityScale *= fraction;
    }
    private void setGravity()
    {
        float gravity = (2 * jumpHeight * horizontalSpeed * horizontalSpeed) / (distanceToMaxHeight * distanceToMaxHeight);
        rb.gravityScale = gravity / 9.81f;
    }
    private float GetJumpForce()
    {
        return 2f * jumpHeight * horizontalSpeed / distanceToMaxHeight;
    }
    private bool isPeakReached()
    {
        bool reached = lastVelocityY > 0 && rb.linearVelocity.y <= 0;
        lastVelocityY = rb.linearVelocity.y;
        return reached;
    }
    private void tweakGravity()
    {
        rb.gravityScale *= 1.2f;
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
        Physics2D.Raycast(transform.position, Vector2.down, groundFilter, hits, 10f);
        return hits[0].distance;
    }

}
