using UnityEngine;

public class IceProjectile : Projectile
{
    public float slowMultiplier = 0.5f;
    public float slowDuration = 2f;

    protected override void HitTarget()
    {
        if (target != null)
        {
            EnemyHealth enemy =
                target.GetComponent<EnemyHealth>();

            if (enemy != null)
            {
                enemy.TakeDamage(damage);

                enemy.Freeze(
                    slowMultiplier,
                    slowDuration
                );
            }
        }

        Destroy(gameObject);
    }
}