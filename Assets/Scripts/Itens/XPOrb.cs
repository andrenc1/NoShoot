using UnityEngine;

public class XPOrb : MonoBehaviour
{
    public int xpValue = 1;
    public float speed;
    public float detectRadius;

    private Transform target;

    void Start()
    {
        target = PlayerController.Instance.transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerXP>().AddXP(xpValue);
            Destroy(gameObject);
        
        }
    }

    private void Update()
    {
        if (target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance <= detectRadius)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
