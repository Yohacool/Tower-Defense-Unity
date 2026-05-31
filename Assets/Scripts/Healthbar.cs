using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform fill;

    public float offsetY = 0.6f;

    private EnemyHealth health;
    private float maxScaleX;

    void Start()
    {
        health = GetComponentInParent<EnemyHealth>();

        if (fill != null)
            maxScaleX = fill.localScale.x;
    }

    void Update()
    {
        if (health == null || fill == null)
            return;

        transform.position = health.transform.position + Vector3.up * offsetY;

        float percent =
            (float)health.CurrentHealth / health.MaxHealth;

        Vector3 scale = fill.localScale;
        scale.x = maxScaleX * percent;
        fill.localScale = scale;
    }
}