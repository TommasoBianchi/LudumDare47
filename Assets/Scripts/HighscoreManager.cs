using System.Collections.Generic;
using GlobalstatsIO;
using UnityEngine;
using System;

public class HighscoreManager : MonoBehaviour
{

    private const string GlobalstatsIOApiId = "NsJjtp79dKUlXooW9KYMDPbJLTDMTDXG5GZy57Hj";
    private const string GlobalstatsIOApiSecret = "CFP13LaOKXrA9bl3P8Ws6DxMC6SKNcCqRrrVdBJX";

    [HideInInspector]
    public static string playerName;
    private static GlobalstatsIOClient gs;
    private static HighscoreManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("More than one HighscoreManager");
            return;
        }

        gs = new GlobalstatsIOClient(GlobalstatsIOApiId, GlobalstatsIOApiSecret);
        playerName = "P" + UnityEngine.Random.state.GetHashCode();
    }

    public static void SubmitScore(int score)
    {
        instance.StartCoroutine(gs.Share(
            new Dictionary<string, string> { { "score", score.ToString() } },
            id: "",
            name: playerName,
            (success) => Debug.Log("Highscore loaded: " + success)
        ));
    }

    public static void LoadHighscores(Action<Leaderboard> callback, int amount = 20)
    {
        instance.StartCoroutine(gs.GetLeaderboard("score", amount, callback));
    }
}