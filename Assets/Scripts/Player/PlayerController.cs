using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rig;

    private Vector2 movementDirection;

    public static PlayerController Instance;

    public void Awake()
    {
            Instance = this;
    }

    private void FixedUpdate()
    {
        rig.linearVelocity = movementDirection * speed;

    }

    void OnMove(InputValue value)
    {
        movementDirection = value.Get<Vector2>();
    }


}
