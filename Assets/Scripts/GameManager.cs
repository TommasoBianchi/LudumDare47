using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Transform gameOverPanel;

    private static GameManager instance;

    private static List<Tuple<float, Action>> futures;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one GameManager");
            return;
        }

        futures = new List<Tuple<float, Action>>();
        Time.timeScale = 1.0f;
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
        Time.timeScale = 0.0f;
        instance.gameOverPanel.gameObject.SetActive(true);
    }

    public static void Retry()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
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