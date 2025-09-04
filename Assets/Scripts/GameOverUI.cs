using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void ReturnToMenu() => SceneManager.LoadScene("MainMenu");

    public void Revive()
    {
        UIManager.Instance.HideGameOver();
        EnemyAI[] enemies = FindObjectsOfType<EnemyAI>();
        foreach(var enemy in enemies) enemy.DisableTemporarily(5f);
    }
}
