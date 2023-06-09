using UnityEngine;

public class Movement : MonoBehaviour
{
    // References
    private new Rigidbody rigidbody;
    private Transform groundCheck;

    // Attributes
    [Header("Moving")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float sphereRadius;
    [SerializeField] private LayerMask jumpMask;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        groundCheck = transform.Find("Ground Check");
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float forward = 0;
        float right = 0;
        float up = rigidbody.velocity.y;

        if (Input.GetKey(KeyCode.W))
            forward += moveSpeed;
            
        if (Input.GetKey(KeyCode.S))
            forward -= moveSpeed;

        if (Input.GetKey(KeyCode.A))
            right -= moveSpeed;

        if (Input.GetKey(KeyCode.D))
            right += moveSpeed;

        if (Input.GetKey(KeyCode.Space) && IsGrounded())
            up = jumpForce;

        Vector3 moveDirection = transform.forward * forward + transform.right * right;
        moveDirection = new Vector3(moveDirection.x, up, moveDirection.z);
        rigidbody.velocity = moveDirection;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, sphereRadius, jumpMask);
    }
}
