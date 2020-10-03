using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI totalPointsText;

    private void OnEnable()
    {
        int totalPoints = PointsManager.totalPoints;
        totalPointsText.text = totalPoints.ToString();
    }
}