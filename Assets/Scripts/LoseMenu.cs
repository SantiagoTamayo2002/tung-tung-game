using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseMenu : MonoBehaviour
{
    public static LoseMenu Instance;
    public GameObject losePanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (losePanel != null)
            losePanel.SetActive(false);
    }

    public void ShowLoseMenu()
    {
        if (losePanel != null)
            losePanel.SetActive(true);

        // pausa el juego
        Time.timeScale = 0f;

        // desbloqueamos el cursor para interactuar
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Botón: Reintentar
    public void Retry()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Botón: Menú Principal
    public void MainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None; // puedes desbloquear para menú
        Cursor.visible = true;
        SceneManager.LoadScene("MainMenu");
    }

    // Botón: Revivir
    public void Revive()
    {
        Time.timeScale = 1f;
        if (losePanel != null)
            losePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Hacemos que el enemigo desaparezca temporalmente
        EnemyAI enemy = FindObjectOfType<EnemyAI>();
        if (enemy != null)
        {
            enemy.gameObject.SetActive(false);
            enemy.Invoke(nameof(enemy.Reappear), 5f);
        }
    }
}
