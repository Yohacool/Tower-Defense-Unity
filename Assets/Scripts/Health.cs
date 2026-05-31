using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 5;
    public int reward = 10;
    private int currentHealth;
    public bool immuneToFreeze = false;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    private SpriteRenderer spriteRenderer;
    private Move moveScript;

    private Color normalColor = Color.white;
    private Color frozenColor = new Color(0.6f, 0.9f, 1f);
    private Color damageColor = Color.red;
    void Start()
    {
        currentHealth = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        moveScript = GetComponent<Move>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(DamageFlash());

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Freeze(float multiplier, float duration)
    {
        if (immuneToFreeze)
            return;

        StopCoroutine("FreezeRoutine");
        StartCoroutine(
            FreezeRoutine(multiplier, duration)
        );
    }

    IEnumerator FreezeRoutine(
        float multiplier,
        float duration)
    {
        moveScript.speedMultiplier = multiplier;

        spriteRenderer.color = frozenColor;

        yield return new WaitForSeconds(duration);

        moveScript.speedMultiplier = 1f;

        spriteRenderer.color = normalColor;
    }

    IEnumerator DamageFlash()
    {
        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(0.12f);

        if (moveScript.speedMultiplier < 1f)
            spriteRenderer.color = frozenColor;
        else
            spriteRenderer.color = normalColor;
    }

    void Die()
    {
        MoneyManager.Instance.AddMoney(reward);
        Destroy(gameObject);
    }
}