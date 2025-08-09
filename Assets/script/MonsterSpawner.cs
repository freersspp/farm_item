using PPman;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public Transform spawnPoint;
    private float spawnTimer;
    private float spawnEachTime = 3.5f;
    [SerializeField, Header("場上怪物上限")] int currentEnemyCountMax = 4;
    private int currentEnemyCount = 0;
    private bool isRespawning = false;

    void Start()
    {
        // 一開始一次補滿
        FillEnemiesToMax();
    }

    void Update()
    {
        if (isRespawning)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnEachTime)
            {
                spawnTimer = 0f;
                SpawnEnemy();

                // 補滿後停止補怪，等待下一次全部被殺光
                if (currentEnemyCount >= currentEnemyCountMax)
                {
                    isRespawning = false;
                }
            }
        }
    }

    void FillEnemiesToMax()
    {
        for (int i = 0; i < currentEnemyCountMax; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemyPrefab.Length);
        GameObject currentEnemy = Instantiate(enemyPrefab[randomEnemy], spawnPoint.position, Quaternion.identity);
        currentEnemy.transform.localEulerAngles = new Vector3(0, Random.value >= 0.5f ? 180 : 0, 0);

        Enemy enemyScript = currentEnemy.GetComponent<Enemy>();
        enemyScript.onDie += OnEnemyDie;

        currentEnemyCount++;
    }

    private void OnEnemyDie()
    {
        currentEnemyCount--;

        // 當全部敵人被殺光時，開始補怪
        if (currentEnemyCount == 0)
        {
            isRespawning = true;
            spawnTimer = spawnEachTime; // 讓補怪馬上啟動
        }
    }
}
