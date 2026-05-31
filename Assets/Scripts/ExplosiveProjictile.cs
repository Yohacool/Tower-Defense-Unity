using UnityEngine;

public class ExplosiveProjectile : Projectile
{
    public float explosionRadius = 1.5f;

    protected override void HitTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 explosionCenter = target.position;

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                explosionCenter,
                explosionRadius
            );

        foreach (Collider2D hit in hits)
        {
            EnemyHealth enemy =
                hit.GetComponentInParent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (target != null)
        {
            Gizmos.DrawWireSphere(
                target.position,
                explosionRadius
            );
        }
    }
}