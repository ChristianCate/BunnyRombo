using UnityEngine;

public class Carrot : MonoBehaviour
{
    public float speed = 8f;

    private Transform target;
    private float damage;

    public void SetTarget(Transform newTarget, float projectileDamage)
    {
        target = newTarget;
        damage = projectileDamage;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            SnakeHealth health =
                target.GetComponentInParent<SnakeHealth>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
    }
}