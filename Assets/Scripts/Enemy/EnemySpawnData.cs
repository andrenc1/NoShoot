using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnData", menuName = "Enemy/New EnemySpawnData")]
public class EnemySpawnData : ScriptableObject
{
    public GameObject enemyPrefab;

    public float initialCooldown;
    public float minCooldown;
    public float cooldownDecreaseRate;

    public int enemyPerSpawn;
    public int maxEnemyPerSpawn;
}
