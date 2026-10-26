using UnityEngine;

public class AxeProjectile : MonoBehaviour
{
    public float horitzontalForce;
    public float verticalForce;

    private Rigidbody2D rig;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        float direction = Random.value < 0.5f ? -1f : 1f;

        Vector2 force = new Vector2(horitzontalForce * direction, verticalForce);
        rig.AddForce(force, ForceMode2D.Impulse);
    }
     
}
