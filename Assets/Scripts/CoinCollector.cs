using UnityEngine;
public class CoinCollector : MonoBehaviour
{
    public int coinValue = 1; // Сколько монет дает
    
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что монету собрал игрок
        if (other.CompareTag("Player"))
        {
            // Получаем компонент инвентаря игрока
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            
            if (inventory != null)
            {
                inventory.AddCoins(coinValue);
                
                Debug.Log("Монетка собрана! +" + coinValue);
                
                // Уничтожаем монетку
                Destroy(gameObject);
            }
        }
    }
}