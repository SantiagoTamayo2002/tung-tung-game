using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Text objetosText, monedasText;
    public Image linternaIcon, armaIcon;
    public GameObject gameOverPanel, winPanel;

    void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void UpdateObjetos(int current, int total) => objetosText.text = $"Objetos: {current}/{total}";
    public void UpdateMonedas(int total) => monedasText.text = $"Monedas: {total}";
    public void SetLinterna(bool active) => linternaIcon.enabled = active;
    public void SetArma(bool active) => armaIcon.enabled = active;
    public void ShowGameOver() => gameOverPanel.SetActive(true);
    public void HideGameOver() => gameOverPanel.SetActive(false);
    public void ShowWin() => winPanel.SetActive(true);
    public void HideWin() => winPanel.SetActive(false);
}
