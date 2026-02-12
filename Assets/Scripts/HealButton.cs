using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class HealButton : MonoBehaviour
{
    public int healAmount = 30; // Количество восстанавливаемого здоровья
    public int coinCost = 10; // Стоимость лечения в монетах
    public TextMeshProUGUI messageText; // Текст для сообщений
    public float messageDuration = 2f; // Сколько показывать сообщение
    public PlayerInventory playerInventory;
    public PlayerHealth playerHealth;
    private Button button;
    
    private void Start()
    {
        // Получаем компонент кнопки
        button = GetComponent<Button>();
        
        // Привязываем метод к нажатию кнопки
        button.onClick.AddListener(TryHeal);
        
        // Скрываем текст сообщения при старте
        if (messageText != null)
        {
            messageText.gameObject.SetActive(false);
        }
        
        // Если ссылки не заполнены - ищем компоненты автоматически
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
        }
        
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }
    }
    
    // Метод, который вызывается при нажатии на кнопку
    private void TryHeal()
    {
        // Проверяем, хватает ли монет
        if (playerInventory.totalCoins >= coinCost)
        {
            // Списываем монеты
            playerInventory.SpendCoins(coinCost);
            
            // Лечим игрока
            playerHealth.Heal(healAmount);
            
            Debug.Log($"Лечение: -{coinCost} монет, +{healAmount} HP");
        }
        else
        {
            // Недостаточно монет - показываем сообщение
            ShowMessage($"Недостаточно монет! Нужно {coinCost} монет");
            Debug.Log("Недостаточно монет для лечения");
        }
    }
    
    // Метод для показа временного сообщения
    private void ShowMessage(string message)
    {
        if (messageText != null)
        {
            // Останавливаем предыдущую корутину, если она запущена
            StopAllCoroutines();
            
            // Устанавливаем текст и показываем его
            messageText.text = message;
            messageText.gameObject.SetActive(true);
            
            // Запускаем корутину для скрытия текста
            StartCoroutine(HideMessageAfterDelay());
        }
    }
    
    // Корутина для скрытия сообщения через время
    private IEnumerator HideMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        messageText.gameObject.SetActive(false);
    }
}