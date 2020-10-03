using UnityEngine;
using TMPro;

public class LeaderboardItem : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI playerNameText;
    [SerializeField]
    private TextMeshProUGUI playerScoreText;

    public void SetPlayerData(string name, string score)
    {
        if (name.Length > 15)
        {
            name = name.Substring(0, 12) + "...";
        }

        playerNameText.text = name;
        playerScoreText.text = score;
    }
}