using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject enemyPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            SpawnEnemy(other.transform);
            gameObject.SetActive(false);
            UIManager.Instance.SetArma(true);
        }
    }

    void SpawnEnemy(Transform player)
    {
        Vector3 spawnPos = player.position + Random.insideUnitSphere * 10f;
        spawnPos.y = player.position.y;
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemy.GetComponent<EnemyAI>().player = player;
    }
}
