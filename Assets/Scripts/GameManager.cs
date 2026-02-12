using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas; // Весь экран Game Over
    public TextMeshProUGUI gameOverText; // Текст "GAME OVER"
    public TextMeshProUGUI restartText; // Текст с инструкцией
    public float restartDelay = 5f; // Секунд до автоматического рестарта
    private bool isGameOver = false; // Флаг, чтобы не вызывать Game Over повторно
    // Синглтон для простого доступа из других скриптов
    public static GameManager Instance;
    public int currentLevel = 1;
    public string[] levelNames; // Массив названий уровней
    private void Awake()
    {
        // Настраиваем синглтон
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    
    private void Start()
    {
        // Убеждаемся, что экран Game Over выключен при старте
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
        
        // Разблокируем курсор на случай, если он был заблокирован
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    private void Update()
    {
        // Если игра окончена и нажата R - перезапускаем
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
    
    // Метод для вызова Game Over
    public void GameOver()
    {
        if (isGameOver) return; // Предотвращаем повторный вызов
        
        isGameOver = true;
        
        // Показываем UI Game Over
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
        }
        
        // Останавливаем время (замораживает игру)
        Time.timeScale = 0f;
        
        // Показываем курсор мыши
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        Debug.Log("GAME OVER!");
        
        // Запускаем таймер на рестарт
        StartCoroutine(RestartAfterDelay());
    }
    
    // Корутина для автоматического рестарта
    private IEnumerator RestartAfterDelay()
    {
        // Ждём указанное количество секунд (игнорируя остановленное время)
        yield return new WaitForSecondsRealtime(restartDelay);
        
        SceneManager.LoadScene("StartScene");
    }
    
    // Метод для загрузки главного меню
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartScene");
    }
    
    // Метод загрузки следующего уровня
    public void LoadNextLevel()
    {
        currentLevel++;
        
        // Проверяем, есть ли следующий уровень
        if (currentLevel <= levelNames.Length)
        {
            SceneManager.LoadScene(levelNames[currentLevel - 1]);
        }
        else
        {
            // Все уровни пройдены
            SceneManager.LoadScene("StartScene");
            Debug.Log("Поздравляем! Игра пройдена!");
        }
    }
    
    // Метод загрузки конкретного уровня
    public void LoadLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        SceneManager.LoadScene(levelNames[levelIndex - 1]);
    }
}