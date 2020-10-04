using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI totalPointsText;
    [SerializeField]
    private float fadeDuration = 1.0f;

    private float fadeTimeElapsed;
    private bool isFading;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        isFading = false;
        canvasGroup.alpha = 0.0f;
    }

    private void OnEnable()
    {
        fadeTimeElapsed = 0.0f;
        isFading = true;
        canvasGroup.alpha = 0.0f;

        int totalPoints = PointsManager.totalPoints;
        totalPointsText.text = totalPoints.ToString();
    }

    private void OnDisable()
    {
        isFading = false;
        canvasGroup.alpha = 0.0f;
    }

    private void Update()
    {
        if (!isFading) return;

        fadeTimeElapsed += Time.deltaTime;

        if (fadeTimeElapsed <= fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0.0f, 1.0f, fadeTimeElapsed / fadeDuration);
        }
        else
        {
            canvasGroup.alpha = 1.0f;
            isFading = false;
        }
    }
}