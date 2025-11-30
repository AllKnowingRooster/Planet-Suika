using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerAction
{
    Hover,
    Click,
    Throw,
    Combine
}
public class GameManager : MonoBehaviour, ISubject
{
    public static GameManager instance { private set; get; }
    public List<IObserver> listObserver;
    private int score;
    public bool isGameOver;
    public bool isPaused;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        listObserver = new List<IObserver>();
        instance = this;
        isPaused = false;
        isGameOver = false;
        score = 0;
        DontDestroyOnLoad(instance);
        SceneManager.sceneLoaded += SceneLoadLogic;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneLoadLogic;
    }

    private void SceneLoadLogic(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Time.timeScale = 1.0f;
            score = 0;
            MainGameCanvasManager.instance.scoreValue.text = score.ToString();
            isGameOver = false;
            isPaused = false;
            MainGameCanvasManager.instance.
            StartCoroutine(Gameloop());
        }
    }

    public void UpdateScore(int value)
    {
        score += value;
        MainGameCanvasManager.instance.scoreValue.text = score.ToString();
    }

    private IEnumerator Gameloop()
    {
        yield return StartCoroutine(StartGame());
        yield return StartCoroutine(GamePlaying());
        yield return StartCoroutine(EndGame());
    }

    private IEnumerator StartGame()
    {
        SpawnManager.instance.GenerateNextPlanet();
        SpawnManager.instance.GetNextPlanet();
        yield return null;
    }

    private IEnumerator GamePlaying()
    {
        while (!isGameOver)
        {
            yield return null;
        }
    }

    private IEnumerator EndGame()
    {
        MainGameCanvasManager.instance.ShowGameOverScreen();
        yield return null;
    }

    public void NotifyObserver(PlayerAction action)
    {
        for (int i = 0; i < listObserver.Count; i++)
        {
            listObserver[i].OnNotify(action);
        }
    }

    public void AddObserver(IObserver observer)
    {
        listObserver.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        listObserver.Remove(observer);
    }
}
