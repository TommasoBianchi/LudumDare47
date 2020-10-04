using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class LoadSceneAfterSeconds : MonoBehaviour
{

    [SerializeField]
    private string sceneName;
    [SerializeField]
    private float time;
    [SerializeField]
    private UnityEvent onSceneLoad;

    private void Start()
    {
        GameManager.AddFuture(time, () =>
        {
            SceneManager.LoadScene(sceneName);
            onSceneLoad.Invoke();
        });
    }
}