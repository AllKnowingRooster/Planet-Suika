using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour
{
    public static MainMenuCanvas instance { get; private set; }

    [Header("Play")]
    [SerializeField] private Button playButton;

    [Header("Exit")]
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => { Play(); });
        exitButton.onClick.RemoveAllListeners();
        exitButton.onClick.AddListener(() => { Exit(); });
    }

    private void Play()
    {
        SceneManager.LoadScene(1);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
