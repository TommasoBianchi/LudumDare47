using UnityEngine;
using TMPro;

public class LeaderboardItem : MonoBehaviour
{

    [SerializeField]
    private int playerNameMaxLength = 20;

    [SerializeField]
    private TextMeshProUGUI playerNameText;
    [SerializeField]
    private TextMeshProUGUI playerScoreText;

    public void SetPlayerData(string name, string score)
    {
        if (name.Length > playerNameMaxLength)
        {
            name = name.Substring(0, playerNameMaxLength - 3) + "...";
        }

        playerNameText.text = name;
        playerScoreText.text = score;
    }
}