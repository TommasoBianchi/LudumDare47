using UnityEngine;
using GlobalstatsIO;

public class LeaderboardPanel : MonoBehaviour
{

    [SerializeField]
    private LeaderboardItem leaderboardItemPrefab;
    [SerializeField]
    private Transform leaderboardItemsContainer;

    private void OnEnable()
    {
        HighscoreManager.LoadHighscores((leaderboard) =>
        {
            foreach (LeaderboardValue value in leaderboard.data)
            {
                LeaderboardItem item = Instantiate(leaderboardItemPrefab, leaderboardItemsContainer);
                item.SetPlayerData(value.name, value.value);
            }
        }, 50);
    }

    private void OnDisable()
    {
        for (int i = leaderboardItemsContainer.childCount - 1; i >= 0; --i)
        {
            Destroy(leaderboardItemsContainer.GetChild(i).gameObject);
        }
    }
}