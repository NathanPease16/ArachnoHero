using UnityEngine;

public class Movement : MonoBehaviour
{
    // References
    private new Rigidbody rigidbody;
    private Transform mainCamera;
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
        mainCamera = Camera.main.transform;
        groundCheck = transform.Find("Ground Check");
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 move = (transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal")).normalized;
        Vector3 currentVelocity = rigidbody.velocity;
        Vector3 idealVelocity = move * maxSpeed;
        Vector3 deltaVelocity = idealVelocity - currentVelocity;
        if(deltaVelocity.magnitude > moveSpeed) {deltaVelocity = Vector3.ClampMagnitude(deltaVelocity, moveSpeed);}
        rigidbody.AddForce(deltaVelocity);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, sphereRadius, jumpMask);
    }
}
