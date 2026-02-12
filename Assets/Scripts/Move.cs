using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f; // Скорость передвижения
    public float jumpForce = 8f; // Сила прыжка

    [SerializeField] private bool isJumping = false; // Флаг: в прыжке ли мы
    [SerializeField] private float jumpVelocity = 0f; // Текущая вертикальная скорость
    [SerializeField] private float gravity = -20f; // Гравитация

    private void Update() {
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical"); // W/S

        Vector3 direction = new Vector3(x, 0, z);

        // Горизонтальное движение
        transform.Translate(direction * speed * Time.deltaTime);

        // Прыжок на пробел
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            isJumping = true;
            jumpVelocity = jumpForce; // Задаем начальную скорость прыжка
        }

        // Мгновенный респавн
        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = new Vector3(0f, 0f, 0f);
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }

        // Если в прыжке - применяем физику
        if (isJumping) {
            jumpVelocity += gravity * Time.deltaTime; // Гравитация
            
            // Добавляем вертикальное движение
            Vector3 jumpDirection = new Vector3(0, jumpVelocity, 0);
            transform.Translate(jumpDirection * Time.deltaTime, Space.World);
            
            // Проверяем, не упали ли обратно на землю (Y <= 0)
            if (transform.position.y <= 0)
            {
                // Возвращаем на землю
                Vector3 pos = transform.position;
                pos.y = 0;
                transform.position = pos;
                
                // Сбрасываем прыжок
                isJumping = false;
                jumpVelocity = 0;
            }
        }
    }
}