using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public float duration;
    public DamageDealer damageDealer;

    private void Start()
    {
        Destroy(gameObject, duration);
    }
}
