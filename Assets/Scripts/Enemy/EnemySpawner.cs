using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemySpawnData spawnData;

    SpawnZone spawnZone;

    float timer;
    float currentCooldown;

    private void Start()
    {
        spawnZone = GetComponent<SpawnZone>();
        currentCooldown = spawnData.initialCooldown;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= currentCooldown)
        { 
            SpawnEnemies();
            timer = 0f;

            currentCooldown = Mathf.Max(spawnData.minCooldown, currentCooldown - spawnData.cooldownDecreaseRate);
        }

    }

    void SpawnEnemies ()
    {
        int count = Mathf.Min(spawnData.enemyPerSpawn, spawnData.maxEnemyPerSpawn);

        for (int i = 0; i < count; i++)
        {
            Vector3 pos = spawnZone.GetSpawnPosition();
            Instantiate(spawnData.enemyPrefab, pos, Quaternion.identity);
        }
    }

}
