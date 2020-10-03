using UnityEngine;
using TMPro;

public class SaveScorePanel : MonoBehaviour
{

    [SerializeField]
    private TMP_InputField playerNameInput;

    private void OnEnable()
    {
        playerNameInput.text = HighscoreManager.playerName;
    }

    public void SaveScore()
    {
        string playerName = playerNameInput.text;
        HighscoreManager.playerName = playerName;
        HighscoreManager.SubmitScore(PointsManager.totalPoints);
    }
}