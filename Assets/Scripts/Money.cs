using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    public int startMoney = 100;
    public TextMeshProUGUI moneyText;

    private int currentMoney;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentMoney = startMoney;
        UpdateUI();
    }

    public bool SpendMoney(int amount)
    {
        if (currentMoney < amount)
            return false;

        currentMoney -= amount;
        UpdateUI();

        return true;
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (moneyText != null)
        {
            moneyText.text = currentMoney.ToString();
        }
    }

    public int GetMoney()
    {
        return currentMoney;
    }
}