using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    public int damageAmount = 10; // Сколько урона наносит
    public float moveSpeed = 3f; // Скорость движения
    public float moveDistance = 5f; // Расстояние перемещения
    
    private Vector3 startPosition; // Стартовая позиция
    private int direction = 1; // Направление: 1 - вправо, -1 - влево
    
    private void Start()
    {
        // Запоминаем начальную позицию
        startPosition = transform.position;
    }
    
    private void Update()
    {
        // Двигаем препятствие
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
        
        // Проверяем, не ушло ли слишком далеко от стартовой позиции
        float distanceMoved = transform.position.x - startPosition.x;
        
        if (Mathf.Abs(distanceMoved) >= moveDistance)
        {
            // Меняем направление
            direction *= -1;
            
            // Корректируем позицию, чтобы не выходить за границы
            Vector3 pos = transform.position;
            pos.x = startPosition.x + (direction * -1 * moveDistance);
            transform.position = pos;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
                Debug.Log("Препятствие нанесло урон: " + damageAmount);
            }
        }
    }
}