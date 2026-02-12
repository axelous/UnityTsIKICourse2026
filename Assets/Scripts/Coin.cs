using UnityEngine;
public class Coin : MonoBehaviour
{
    public float rotationSpeed = 180f;
    public float floatSpeed = 2f;
    public float floatHeight = 0.2f;
    private Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
    }
    
    private void Update()
    {
        // Вращение
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        
        // Парение вверх-вниз
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddCoins(1);
                
                Destroy(gameObject);
            }
        }
    }
}