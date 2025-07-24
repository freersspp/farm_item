using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterAPrefab;
    public GameObject monsterBPrefab;

    public Transform[] spawnPointsA; // 怪物A的出現點
    public Transform[] spawnPointsB; // 怪物B的出現點

    public int spawnCountA = 5;
    public int spawnCountB = 5;

    void Start()
    {
        SpawnMonsters(monsterAPrefab, spawnPointsA, spawnCountA);
        SpawnMonsters(monsterBPrefab, spawnPointsB, spawnCountB);
    }

    void SpawnMonsters(GameObject prefab, Transform[] points, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int pointIndex = i % points.Length; // 循環選取生成點
            Vector3 offset = new Vector3(Random.Range(-5f, 5f), 0F, 0f);
            Instantiate(prefab, points[pointIndex].position + offset, Quaternion.identity);
        }
    }
}
