using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dan.Main;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _nameList;
	[SerializeField] private List<TMP_Text> _scoreList;

    private string _publicLeaderboardKey = "1b0d7112951b1bd64bd66f0e84bb05a6f6d7dbd2f0ddb162d0fa88d2c28e5271";

	private void Start()
	{
		GameManager.Instance.submitScoreEvent += SetLeaderboardEntry;
	}

	public void GetLeaderboard()
    {
        LeaderboardCreator.GetLeaderboard(_publicLeaderboardKey, ((msg) =>
        {
            int loopLength = (msg.Length < _nameList.Count) ? msg.Length : _nameList.Count;
            for (int i = 0; i < loopLength; i++)
            {
                _nameList[i].text = msg[i].Username;
				_scoreList[i].text = msg[i].Score.ToString();
            }
        }));
    }

	public void SetLeaderboardEntry(string username, int score)
	{
        if(username == null) { return; }
		LeaderboardCreator.UploadNewEntry(_publicLeaderboardKey, username, score, ((msg) =>
        {
            //if (System.Array.IndexOf(badwords, name) != -1) return;
            GetLeaderboard();
        }));
	}

    //public void DeleteLeaderboardEntry(string username)
    //{
    //    LeaderboardCreator.DeleteEntry(_publicLeaderboardKey, username, ((msg =>
    //    {

    //    })));
    //}
}
