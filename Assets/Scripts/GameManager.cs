using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private Transform gameOverPanel;

    private static GameManager instance;

    private void Start()
    {
        Time.timeScale = 1.0f;

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one GameManager");
        }
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
}