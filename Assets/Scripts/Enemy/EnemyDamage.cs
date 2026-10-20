using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
