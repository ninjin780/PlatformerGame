using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D Rb;
    public float MoveSpeed;
    private Vector2 moveDirection;
    public InputActionReference Move;
    public ParticleSystem GrassParticles;

    private void Update()
    {
        moveDirection = Move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Rb.linearVelocity = new Vector2(x:moveDirection.x * MoveSpeed,y:moveDirection.y * MoveSpeed);
        GrassParticles.Play();
    }
}
