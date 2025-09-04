using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject collectiblePrefab;
    public int totalObjects = 5;
    public Vector3 areaMin;
    public Vector3 areaMax;

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        for(int i=0;i<totalObjects;i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(areaMin.x, areaMax.x),
                Random.Range(areaMin.y, areaMax.y),
                Random.Range(areaMin.z, areaMax.z)
            );
            Instantiate(collectiblePrefab,pos,Quaternion.identity);
        }
    }
}
