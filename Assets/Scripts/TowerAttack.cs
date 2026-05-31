using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float range = 3f;
    public float fireRate = 1f;

    private bool canAttack = false;

    public GameObject projectilePrefab;
    public Transform firePoint;

    private float fireCooldown;

    void Update()
    {
        if (!canAttack)
            return;

        fireCooldown -= Time.deltaTime;

        EnemyHealth target = FindTarget();

        if (target == null)
            return;

        if (fireCooldown <= 0f)
        {
            Shoot(target);
            fireCooldown = 1f / fireRate;
        }
    }

    EnemyHealth FindTarget()
    {
        EnemyHealth[] enemies =
            FindObjectsOfType<EnemyHealth>();

        EnemyHealth closestEnemy = null;

        float closestDistance = Mathf.Infinity;

        foreach (EnemyHealth enemy in enemies)
        {
            float distance =
                Vector2.Distance(
                    transform.position,
                    enemy.transform.position
                );

            if (distance <= range &&
                distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    void Shoot(EnemyHealth target)
    {
        GameObject projectile =
            Instantiate(
                projectilePrefab,
                firePoint.position,
                Quaternion.identity
            );

        Projectile projectileScript =
            projectile.GetComponent<Projectile>();

        projectileScript.SetTarget(target.transform);
    }
    public void Activate()
    {
        canAttack = true;
    }
}