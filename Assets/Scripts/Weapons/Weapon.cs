using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/New Weapon")]
public class Weapon : ScriptableObject
{
    public GameObject attackPrefab;
    public float cooldown;
    public int damage;
}
