using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelFinish : MonoBehaviour
{
    public string nextLevelName = "Level2"; // Название следующей сцены
    public bool showDebugMessages = true;
    
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что финиш пересек игрок
        if (other.CompareTag("Player"))
        {
            FinishLevel();
        }
    }
    
    private void FinishLevel()
    {
        if (showDebugMessages)
            Debug.Log($"Уровень пройден! Загрузка: {nextLevelName}");
        
        // Загружаем следующий уровень
        SceneManager.LoadScene(nextLevelName);
    }
}