using UnityEngine;
using TMPro;
public class PlayerInventory : MonoBehaviour
{
    public int totalCoins = 0; // Всего собранных монет
    public TextMeshProUGUI coinText; // Ссылка на текст с монетами
    private void Start()
    {
        UpdateCoinUI();
    }
    
    // Метод для добавления монет
    public void AddCoins(int amount)
    {
        totalCoins += amount;
        Debug.Log("Монет: " + totalCoins);
        UpdateCoinUI();
    }
    
     // Метод для траты монет
    public bool SpendCoins(int amount)
    {
        if (totalCoins >= amount)
        {
            totalCoins -= amount;
            UpdateCoinUI();
            Debug.Log($"Потрачено {amount} монет. Осталось: {totalCoins}");
            return true;
        }
        else
        {
            Debug.Log($"Недостаточно монет! Нужно {amount}, есть {totalCoins}");
            return false;
        }
    }
    
    // Обновление UI
    private void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "Coins: " + totalCoins;
        }
    }
}