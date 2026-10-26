using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [Header("Render")]
    public SpriteRenderer spriteRenderer;
    public Material hitMaterial;
    public float flashTime;
    public GameObject CanvaHitPrefab;

    Material currentMaterial;

    private Rigidbody2D rig;

    [Header("Health")]
    public int maxLife;
    public GameObject dieEffect;

    int currentLife;

    Coroutine flashCoroutine;

    private void Awake()
    {
        currentLife = maxLife;

        currentMaterial = spriteRenderer.sharedMaterial;

        rig = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage) {
        currentLife -= damage;

        ApplyKnockback(PlayerController.Instance.transform.position, 8f, 0.1f);

        GameObject hit = Instantiate(CanvaHitPrefab, transform.position, Quaternion.identity);
        hit.transform.SetParent(transform);
        hit.GetComponent<HitCanva>().changeValue(damage);
        Destroy(hit, 2f);


        if (flashCoroutine != null){
            StopCoroutine(flashCoroutine);
        }



        flashCoroutine = StartCoroutine(FlashCoroutine());

        if (currentLife <= 0)
        {
            Die();
        }
    }

    
    IEnumerator FlashCoroutine()
    {
        spriteRenderer.sharedMaterial = hitMaterial;
        yield return new WaitForSeconds(flashTime);
        spriteRenderer.sharedMaterial = currentMaterial;

        flashCoroutine = null;
    }


    void Die()
        {
        GameManager.Instance.EnemyCount();

        Instantiate(GameManager.Instance.XPOrb, transform.position, Quaternion.identity);
        GameObject effect = Instantiate(dieEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        Debug.Log("Enemy died!");
            Destroy(gameObject);
        }

    #region Knockback

    public bool isKnockbacked;
    Coroutine knockbackRoutine;

    public void ApplyKnockback(Vector2 direction, float force, float duration)
    {
        Vector2 dir = ((Vector2)transform.position - direction).normalized;

        if (knockbackRoutine != null)
        {
            StopCoroutine(knockbackRoutine);
        }

        knockbackRoutine = StartCoroutine(KnockbackRoutine(dir, force, duration));
    }

    IEnumerator KnockbackRoutine(Vector2 direction, float force, float duration)
    {
        isKnockbacked = true;

        rig.linearVelocity = direction * force;
        yield return new WaitForSeconds(duration);
        rig.linearVelocity = Vector2.zero;
        isKnockbacked = false;

        knockbackRoutine = null;
    }

    #endregion
}
