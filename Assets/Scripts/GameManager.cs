using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Coleccionables")]
    public int totalCollectibles = 5;
    private int collected = 0;

    [Header("Puerta de salida")]
    public ExitDoor exitDoor; // <-- referencia a la puerta

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
{
    Debug.Log("Has perdido");
    LoseMenu.Instance.ShowLoseMenu(); // asegura que LoseMenu está inicializado
}

    public void AddCollectible()
    {
        collected++;
        Debug.Log("Objetos recogidos: " + collected + "/" + totalCollectibles);

        if (collected >= totalCollectibles)
        {
            UnlockExit();
        }
    }

    void UnlockExit()
    {
        if (exitDoor != null)
        {
            exitDoor.UnlockDoor();
            Debug.Log("¡Puerta desbloqueada!");
        }
        else
        {
            Debug.LogError("ExitDoor NO está asignada en GameManager");
        }
    }
}
