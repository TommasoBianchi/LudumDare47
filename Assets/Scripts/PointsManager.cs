using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointsManager : MonoBehaviour
{

    [SerializeField]
    private int pointsMultiplier = 100;
    [SerializeField]
    private float circleTimeExponent = 1.0f;

    [SerializeField]
    private TextMeshProUGUI totalPointsText;
    [SerializeField]
    private TextMeshProUGUI pointsEffectPrefab;
    [SerializeField]
    private RectTransform pointsEffectSpawnPoint;

    private int totalPoints;

    private void Start()
    {
        totalPoints = 0;

        InputLoopCounter.onCircleCompleted += (circleTime) =>
        {
            int circlePoints = Mathf.FloorToInt(pointsMultiplier / Mathf.Pow(circleTime, circleTimeExponent));
            totalPoints += circlePoints;

            totalPointsText.text = totalPoints.ToString();

            TextMeshProUGUI pointsEffect = Instantiate(pointsEffectPrefab, pointsEffectSpawnPoint);
            pointsEffect.text = "+" + circlePoints;
        };
    }
}