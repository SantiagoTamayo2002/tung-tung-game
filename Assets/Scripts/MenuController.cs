using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ajustesPanel;
    public GameObject tiendaPanel;
    public GameObject idiomaPanel;

    void Start()
    {
        ajustesPanel.SetActive(false);
        tiendaPanel.SetActive(false);
        idiomaPanel.SetActive(false);
    }

    public void PlayGame() => SceneManager.LoadScene("GameScene");
    public void OpenAjustes() => ajustesPanel.SetActive(true);
    public void CloseAjustes() => ajustesPanel.SetActive(false);
    public void OpenTienda() 
    {
        tiendaPanel.SetActive(true);
        tiendaPanel.GetComponent<ShopManager>().UpdateMonedasUI();
    }
    public void CloseTienda() => tiendaPanel.SetActive(false);
    public void OpenIdioma() => idiomaPanel.SetActive(true);
    public void CloseIdioma() => idiomaPanel.SetActive(false);
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }
}
