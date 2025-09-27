using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 5f;
    public float jumpHeight = 1.2f;
    public float gravity = -20f;           // Aumentada para rampas y escaleras

    [Header("Cámara")]
    public float mouseSensitivity = 2f;
    public Transform playerCamera;

    [Header("Ground Check")]
    public Transform groundCheck;           // objeto hijo en los pies
    public float groundDistance = 0.2f;     // radio para detectar suelo
    public LayerMask groundMask;            // capa del suelo

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    // Guardamos el índice del layer "Ignore"
    private int ignoreLayer;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        ignoreLayer = LayerMask.NameToLayer("Ignore");
    }

    void Update()
    {
        GroundCheck();
        Move();
        Look();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundDistance,
            groundMask
        );

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Mantener al player pegado al suelo
        }
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = move.normalized * speed;

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        Vector3 finalMove = move + new Vector3(0, velocity.y, 0);
        controller.Move(finalMove * Time.deltaTime);
    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Si el objeto está en el layer "Ignore", desactivamos la colisión
        if (hit.gameObject.layer == ignoreLayer)
        {
            Physics.IgnoreCollision(controller, hit.collider, true);
            Debug.Log($"⛔ Ignorando colisión con {hit.gameObject.name}");
        }
    }
}
