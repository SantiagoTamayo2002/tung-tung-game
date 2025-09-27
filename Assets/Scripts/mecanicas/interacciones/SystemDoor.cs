using UnityEngine;

public class SystemDoor : MonoBehaviour
{

    [Header("Sonidos")]
    public AudioClip openSound;
    public AudioClip closeSound;


    [Header("Puerta")]
    public float doorOpenAngle = -90f;       // Ángulo de apertura
    public float doorOpenSpeed = 2f;         // Velocidad de apertura
    public float interactionDistance = 2f;   // Distancia máxima para interactuar
    public Vector3 rotationAxis = Vector3.up; // Eje de rotación local de la puerta


    [Header("Cámara")]
    public Camera cam;                        // Cámara principal (asignar en Inspector)

    private bool doorOpen = false;
    private Quaternion initialRotation;
    private Quaternion openRotation;

    void Start()
    {
        // Guardar rotación inicial
        initialRotation = transform.localRotation;

        // Calcular rotación abierta usando el eje definido por Inspector
        openRotation = initialRotation * Quaternion.AngleAxis(doorOpenAngle, rotationAxis);

        // Seguridad: si no se asignó la cámara, buscar MainCamera
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    void Update()
    {
        if (cam == null) return; // Evitar errores si no hay cámara

        // Detectar tecla E
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Raycast desde el centro de la pantalla
            Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                // Comprobar si este collider es la puerta
                if (hit.collider == this.GetComponent<Collider>())
                {
                    ToggleDoor();
                }
            }
        }

        // Animar rotación suavemente
        Quaternion targetRotation = doorOpen ? openRotation : initialRotation;
        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            Time.deltaTime * doorOpenSpeed
        );
    }

    // Función para abrir/cerrar la puerta
    public void ToggleDoor()
{
    doorOpen = !doorOpen;

    if (doorOpen)
    {
        AudioSource.PlayClipAtPoint(openSound, transform.position, 1f);
    }
    else
    {
        AudioSource.PlayClipAtPoint(closeSound, transform.position, 1f);
    }
}

}
