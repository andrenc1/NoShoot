using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damage);
        }
    }

    public void GetDamage(int dmg)
    {
        damage = dmg;
    }
}
