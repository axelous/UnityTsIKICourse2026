using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f; // Скорость передвижения
    public float jumpForce = 8f; // Сила прыжка

    [SerializeField] private bool isJumping = false; // Флаг: в прыжке ли мы
    [SerializeField] private float jumpVelocity = 0f; // Текущая вертикальная скорость
    [SerializeField] private float gravity = -20f; // Гравитация
    
    // Ссылки на компоненты
    private Animator animator;
    private Rigidbody rb;
    
    // Для более плавного переключения анимаций
    private float currentSpeed = 0f;
    private float speedSmoothVelocity = 0f;
    private float smoothTime = 0.1f; // Время сглаживания изменения скорости

    private void Start()
    {
        // Получаем компоненты при старте
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
        // Если есть Rigidbody, настраиваем его для наших нужд
        if (rb != null)
        {
            rb.freezeRotation = true; // Замораживаем вращение от физики
        }
    }

    private void Update() {
        // ПОЛУЧАЕМ ВВОД
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical"); // W/S

        Vector3 direction = new Vector3(x, 0, z);
        
        // Нормализуем направление, чтобы по диагонали не бежать быстрее
        if (direction.magnitude > 1f)
            direction.Normalize();

        // Прыжок на пробел
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping) {
            isJumping = true;
            jumpVelocity = jumpForce; // Задаем начальную скорость прыжка
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
        
        if (direction.magnitude > 0.1f)
        {
            // Плавный поворот в сторону движения
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        
        // --- ОТПРАВКА ПАРАМЕТРОВ В ANIMATOR ---
        if (animator != null)
        {
            // 1. Speed - для переключения Idle/Run
            // Плавно изменяем значение speed для аниматора
            float targetSpeed = direction.magnitude; // 0 если стоим, 1 если идем
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, smoothTime);
            animator.SetFloat("Speed", currentSpeed);
            
            // 2. Jump - триггер для анимации прыжка
            if (Input.GetKeyDown(KeyCode.C))
            {
                animator.SetTrigger("Jump");
            }
            
            // 3. isJumping - состояние в воздухе (если нужно)
            animator.SetBool("isJumping", isJumping);
        }

        // --- МГНОВЕННЫЙ РЕСПАВН (оставляем ваш) ---
        if (Input.GetKeyDown(KeyCode.R)) {
            transform.position = new Vector3(0f, 0f, 0f);
            transform.rotation = Quaternion.identity; // Более правильный способ сбросить вращение
        }
    }
    
}