using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    private static List<Tuple<float, Action>> futures;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("More than one GameManager");
            return;
        }

        futures = new List<Tuple<float, Action>>();
    }

    private void Update()
    {
        for (int i = 0; i < futures.Count; ++i)
        {
            var (futureTime, callback) = futures[i];
            if (Time.time > futureTime)
            {
                callback();
                futures.RemoveAt(i);
            }
        }
    }

    public static void AddFuture(float futureTime, Action callback)
    {
        futures.Add(new Tuple<float, Action>(Time.time + futureTime, callback));
    }

    public static void GameOver()
    {
        GameOverPanel gameOverPanel = FindObjectOfType<GameOverPanel>(includeInactive: true);
        gameOverPanel.gameObject.SetActive(true);
        SoundManager.PlayMenuMusic();
    }

    public static void Play()
    {
        SceneManager.LoadScene("Game");
        SoundManager.PlayGameMusic();
    }

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SoundManager.PlayMenuMusic();
    }

    public static void Exit()
    {
        if (Application.isEditor)
        {
            //EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}