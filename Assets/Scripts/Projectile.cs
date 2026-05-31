using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public int damage = 1;
    public float rotationOffset = 0f;

    protected Transform target;
    private Vector3 targetPosition;

    public void SetTarget(Transform enemyTarget)
    {
        target = enemyTarget;
        targetPosition = enemyTarget.position;
    }

    void Update()
    {
        Vector3 direction =
            targetPosition - transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x)
            * Mathf.Rad2Deg;

        angle += rotationOffset;

        transform.rotation =
            Quaternion.Euler(0, 0, angle);

        transform.position =
            Vector3.MoveTowards(
                transform.position,
                targetPosition,
                speed * Time.deltaTime
            );

        if (Vector2.Distance(
            transform.position,
            targetPosition) < 0.1f)
        {
            HitTarget();
        }
    }

    protected virtual void HitTarget()
    {
        if (target != null)
        {
            EnemyHealth enemyHealth =
                target.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}