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
    [SerializeField] private float dampenFactor;

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
        Jump();
    }

    private void Move()
    {
        Vector3 move = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized;
        Vector3 currentVelocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
        Vector3 idealVelocity = move * maxSpeed;
        Vector3 deltaVelocity = idealVelocity - currentVelocity;
        if(deltaVelocity.magnitude > moveSpeed) {
            deltaVelocity = Vector3.ClampMagnitude(deltaVelocity, moveSpeed);
        }
        Debug.Log($"Current Velocity: {currentVelocity.magnitude}    Ideal Velocity: {idealVelocity.magnitude}");
        rigidbody.AddForce(deltaVelocity);
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && IsGrounded()) {
            Debug.Log("aaa");
            rigidbody.AddForce(Vector3.up*jumpForce, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, sphereRadius, jumpMask);
    }
}
