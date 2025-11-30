using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainGameCanvasManager : MonoBehaviour
{
    public static MainGameCanvasManager instance { get; private set; }

    [Header("Main")]
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private Button pauseButton;
    public TextMeshProUGUI scoreValue;

    [Header("Pause")]
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseMainMenuButton;

    [Header("GameOver")]
    [SerializeField] private CanvasGroup GameOverCanvasGroup;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button gameOverMainMenuButton;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        pauseButton.onClick.RemoveAllListeners();
        pauseButton.onClick.AddListener(() => { Pause(); });
        resumeButton.onClick.RemoveAllListeners();
        resumeButton.onClick.AddListener(() => { Pause(); });
        pauseMainMenuButton.onClick.RemoveAllListeners();
        pauseMainMenuButton.onClick.AddListener(() => { GoToMainMenu(); });
        playAgainButton.onClick.RemoveAllListeners();
        playAgainButton.onClick.AddListener(() => { PlayAgain(); });
        gameOverMainMenuButton.onClick.RemoveAllListeners();
        gameOverMainMenuButton.onClick.AddListener(() => { GoToMainMenu(); });
    }

    public void ToggleCanvasGroup(CanvasGroup cg)
    {
        cg.alpha = cg.alpha == 1.0f ? 0.0f : 1.0f;
        cg.interactable = !cg.interactable;
        cg.blocksRaycasts = !cg.blocksRaycasts;
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowGameOverScreen()
    {
        ToggleCanvasGroup(GameOverCanvasGroup);

    }

    private void Pause()
    {
        ToggleCanvasGroup(mainCanvasGroup);
        ToggleCanvasGroup(pauseCanvasGroup);
        GameManager.instance.isPaused = !GameManager.instance.isPaused;
        Time.timeScale = GameManager.instance.isPaused ? 0.0f : 1.0f;
    }

}
