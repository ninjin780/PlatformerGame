using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    [SerializeField]
    private LayerMask WhatIsGround;
    [SerializeField]
    private LayerMask WhatIsPlatform;

    [SerializeField]
    private Transform GroundCheckPoint;
    [SerializeField]
    private Transform FrontCheckPoint;
    [SerializeField]
    private Transform RoofCheckPoint;

    public Transform CurrentPlatform;
    [SerializeField]
    public Animator Animator;

    private float checkRadius = 0.15f;
    private bool wasGrounded;

    [SerializeField]
    private bool isGrounded;
    public bool IsGrounded { get { return isGrounded || isPlatformGround; } }

    [SerializeField]
    private bool isTouchingFront;
    public bool IsTouchingFront { get { return isTouchingFront; } }

    [SerializeField]
    private bool isPlatformGround;
    public bool IsPlatForm { get { return isPlatformGround; } }

    [SerializeField]
    private bool isTouchingRoof;
    public bool IsTouchingRoof { get { return isTouchingRoof; } }

    [SerializeField]
    private float distanceToGround;
    public float DistanceToGround { get { return distanceToGround; } }

    [SerializeField]
    private float groundAngle;
    public float GroundAngle { get { return groundAngle; } }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(GroundCheckPoint.position, checkRadius);
        Gizmos.DrawWireSphere(FrontCheckPoint.position, checkRadius);
        Gizmos.color = Color.white;
    }

    void FixedUpdate()
    {
        // NOTE: Physics are recommended to be updated at fixed time steps
        // so logic is added to FixedUpdate() method

        CheckCollisions();
        CheckDistanceToGround();
    }

    private void CheckCollisions()
    {
        CheckGrounded();
        CheckPlatformed();
        CheckFront();
        //CheckRoof();          // Enable check once a RoofCheckPoint has been set!
    }

    private void CheckFront()
    {
        var colliders = Physics2D.OverlapCircleAll(FrontCheckPoint.position, checkRadius, WhatIsGround);

        isTouchingFront = (colliders.Length > 0);
    }

    private void CheckRoof()
    {
        var colliders = Physics2D.OverlapCircleAll(RoofCheckPoint.position, checkRadius, WhatIsGround);

        isTouchingRoof = (colliders.Length > 0);
    }

    private void CheckGrounded()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position, checkRadius, WhatIsGround);

        isGrounded =  (colliders.Length > 0);
        if (isGrounded) { Animator.SetBool("isFalling", false); }

        //if (!wasGrounded && isGrounded) SendMessage("OnLanding");
        //wasGrounded = isGrounded;
    }

    private void CheckPlatformed()
    {
        var colliders = Physics2D.OverlapCircleAll(GroundCheckPoint.position, checkRadius, WhatIsPlatform);

        isPlatformGround = (colliders.Length > 0);
        if (isPlatformGround) CurrentPlatform = colliders[0].transform;

        //if (!wasGrounded && isGrounded) SendMessage("OnLanding");
        //wasGrounded = isGrounded;
    }

    private void CheckDistanceToGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(GroundCheckPoint.position, Vector2.down, 100, WhatIsGround);

        distanceToGround = hit.distance;
        groundAngle = Vector2.Angle(hit.normal,new Vector2(1,0));
    }
}
