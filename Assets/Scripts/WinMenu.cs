using UnityEngine;

public class WinMenu : MonoBehaviour
{
    public static WinMenu Instance;
    public GameObject winPanel; // Arrastra aquí tu panel en el Inspector

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // Asegurarse de que el panel esté oculto al inicio
        if (winPanel != null)
            winPanel.SetActive(false);
        else
            Debug.LogError("winPanel NO está asignado en WinMenu.");
    }

    public void ShowWinMenu()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f; // pausa el juego
        }
        else
        {
            Debug.LogError("winPanel NO está asignado en WinMenu.");
        }
    }
}
