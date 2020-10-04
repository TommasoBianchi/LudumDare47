using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterSeconds : MonoBehaviour
{

    [SerializeField]
    private string sceneName;
    [SerializeField]
    private float time;

    private void Start()
    {
        GameManager.AddFuture(time, () => SceneManager.LoadScene(sceneName));
    }
}