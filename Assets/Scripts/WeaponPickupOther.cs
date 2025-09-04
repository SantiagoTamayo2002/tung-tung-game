using UnityEngine;

public class WeaponPickupOther : MonoBehaviour, IInteractable
{
    public GameObject enemyPrefab;   // prefab del enemigo
    public float spawnDistance = 10f; // distancia desde el jugador donde aparecerá

    public void Interact()
    {
        Debug.Log("Arma recogida!");
        SpawnEnemy();
        Destroy(gameObject);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // Calculamos una posición alejada del jugador
        Vector3 spawnPos = transform.position + Random.onUnitSphere * spawnDistance;
        spawnPos.y = transform.position.y; // misma altura que el jugador

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }
}
