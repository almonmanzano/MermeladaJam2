using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class Leaderboard : MonoBehaviour
{
    private const string LeaderboardId = "mermelada-jam-2-leaderboard";

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
        catch (Exception)
        {
            return;
        }
    }

    public async Task<bool> AddScore(int score, string name)
    {
        name = name.Replace(" ", ""); // Remove blank spaces
        await AuthenticationService.Instance.UpdatePlayerNameAsync(name);
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, score);
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        return true;
    }

    public async Task<int> GetPlayerScore()
    {
        try
        {
            var scoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId);
            int score = (int)scoreResponse.Score;
            return score;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    public async Task<Dictionary<string, int>> GetScores()
    {
        Dictionary<string, int> scores = new Dictionary<string, int>();
        var scoreResponse = await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { Limit = 5 });
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
        foreach (var result in scoreResponse.Results)
        {
            string playerName = result.PlayerName;
            int score = (int)result.Score;
            scores[playerName] = score;
        }
        return scores;
    }
}
