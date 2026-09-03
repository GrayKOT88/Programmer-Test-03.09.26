using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject winMenuUI; // Ваше меню победы с кнопками
    [SerializeField] private GameObject loseMenuUI; // (Опционально) меню поражения

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Убедитесь, что меню выключены в начале
        if (winMenuUI != null) winMenuUI.SetActive(false);
        if (loseMenuUI != null) loseMenuUI.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f; // На всякий случай, если игра на паузе
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void WinGame()
    {
        winMenuUI.SetActive(true);
        Time.timeScale = 0f; // Останавливаем игру
        Debug.Log("Победа!");
    }
    
    public void LoseGame()
    {
        loseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Поражение!");
    }

    public void OnRestartButton()
    {
        Time.timeScale = 1f;
        RestartLevel();
    }

    public void OnExitButton()
    {
        Debug.Log("Выход из игры");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Чтобы работало в редакторе
#endif
    }
}