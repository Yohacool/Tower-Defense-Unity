using UnityEngine;
using TMPro;

public class CastleHealth : MonoBehaviour
{
    public int maxHealth = 20;

    private int currentHealth;

    public TextMeshProUGUI healthText;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        healthText.text = currentHealth.ToString();
    }

    void Die()
    {
        GameManager.Instance.LoseGame();
        Debug.Log("Castle destroyed");
    }
}