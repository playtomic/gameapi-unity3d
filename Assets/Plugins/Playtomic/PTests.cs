#define TEST_GAMEVARS
#define TEST_GEO
#define TEST_NEWSLETTER
#define TEST_LEADERBOARDS
#define TEST_PLAYERLEVELS
#define TEST_ACHIEVEMENTS
	
using UnityEngine;
using System;
using System.Collections.Generic;

//namespace PlaytomicTest
//{
public class PTests : MonoBehaviour
{

	public string testURL = "http://127.0.0.1:3000";
	
	private List<Action<Action>> _tests;
		
	public void Start ()
	{
		Playtomic.Initialize ("testpublickey", "testprivatekey", testURL);
		
		PTest.Setup ();	
		
		PTestLeaderboards.rnd = PTestPlayerLevels.rnd = PTestAchievements.rnd = RND ();
			
		_tests = new List<Action<Action>>
			    {
#if TEST_GAMEVARS
			        PTestGameVars.All,
			        PTestGameVars.Single,
#endif
#if TEST_GEO
			        PTestGeoIP.Lookup,
#endif
#if TEST_NEWSLETTER
					PTestNewsletter.Subscribe,
#endif
#if TEST_LEADERBOARDS
			        PTestLeaderboards.FirstScore,
			        PTestLeaderboards.SecondScore,
			        PTestLeaderboards.HighScores,
			        PTestLeaderboards.LowScores,
			        PTestLeaderboards.AllScores,
					PTestLeaderboards.FriendsScores,
					PTestLeaderboards.OwnScores,
#endif
#if TEST_PLAYERLEVELS
			        PTestPlayerLevels.Create,
			        PTestPlayerLevels.List,
			        PTestPlayerLevels.Load,
			        PTestPlayerLevels.Rate,
#endif
#if TEST_ACHIEVEMENTS
					PTestAchievements.List,
					PTestAchievements.ListWithFriends,
					PTestAchievements.ListWithPlayer,
					PTestAchievements.ListWithPlayerAndFriends,
					PTestAchievements.Stream,
					PTestAchievements.StreamWithFriends,
					PTestAchievements.StreamWithPlayerAndFriends,
					PTestAchievements.Save
#endif
			    };
		Next ();
	}
		
	void Next ()
	{
		if (_tests.Count == 0) {
			PTest.Render ();
			return;
		}
			
		var action = _tests [0];
		_tests.RemoveAt (0);
		action (Next);
	}
		
	private static int RND ()
	{
		var random = new System.Random ();
		return random.Next (int.MaxValue);
	}
}
//};