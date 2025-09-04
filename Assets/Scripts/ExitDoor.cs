using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private bool isUnlocked = false;
    private bool isOpening = false;

    public float openAngle = 90f;       // grados que rotará la puerta
    public float openSpeed = 2f;        // velocidad de rotación

    private Quaternion closedRotation;
    private Quaternion targetRotation;

    private void Awake()
    {
        closedRotation = transform.rotation;
        targetRotation = closedRotation;
        targetRotation = closedRotation; // inicio
    }

    public void UnlockDoor()
    {
        if (!isUnlocked)
        {
            isUnlocked = true;
            Debug.Log("La puerta se ha desbloqueado");
            OpenDoor();
            ShowWinPanel();
        }
    }

    private void OpenDoor()
    {
        // Calculamos la rotación final (abrir hacia Y positivo)
        targetRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, openAngle, 0));
        isOpening = true;
    }

    private void Update()
    {
        if (isOpening)
        {
            // Rotación suave hacia la posición objetivo
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);

            // Si estamos cerca del objetivo, dejamos de rotar
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isOpening = false;
            }
        }
    }

    private void ShowWinPanel()
    {
        if (WinMenu.Instance != null)
        {
            WinMenu.Instance.ShowWinMenu();
        }
        else
        {
            Debug.LogError("WinMenu.Instance es null. Revisa que WinMenuManager exista en la escena y tenga el panel asignado.");
        }
    }
}
