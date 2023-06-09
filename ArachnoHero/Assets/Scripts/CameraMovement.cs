using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // References
    private Transform player;
    private Transform mainCamera;

    [Header("Attributes")]
    [SerializeField] private float sensitivity;
    [SerializeField] private float maxRotation;
    [SerializeField] private float tiltRotation;
    [SerializeField] private float tiltSpeed;

    // Rotation
    private float xRotation;
    private float yRotation;
    private float tilt;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        mainCamera = Camera.main.transform;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        xRotation += Input.GetAxis("Mouse X") * sensitivity;
        yRotation -= Input.GetAxis("Mouse Y") * sensitivity;

        yRotation = Mathf.Clamp(yRotation, -maxRotation, maxRotation);


        if (Input.GetKey(KeyCode.A))
            tilt = Mathf.Lerp(tilt, tiltRotation, tiltSpeed * Time.deltaTime);
        else if (Input.GetKey(KeyCode.D))
            tilt = Mathf.Lerp(tilt, -tiltRotation, tiltSpeed * Time.deltaTime);
        else
            tilt = Mathf.Lerp(tilt, 0, tiltSpeed * Time.deltaTime);


        player.rotation = Quaternion.Euler(player.rotation.x, xRotation, player.rotation.z);
        mainCamera.localRotation = Quaternion.Euler(yRotation, mainCamera.rotation.y, tilt);
    }
}
