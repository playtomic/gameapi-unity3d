using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

internal class PTestLeaderboards : PTest 
{	
	public static int rnd;
	
	public static void FirstScore(Action done) 
	{		
		const string section = "TestLeaderboards.FirstScore";
		Debug.Log (section);

		PlayerScore score = new PlayerScore {
			table = "scores" + rnd,
			playername = "person1",
			points = 10000,
			highest =  true,
			fields = new PDictionary { 
				{"rnd", rnd}
			}
		};

		Playtomic.Leaderboards.Save (score, r => {
			AssertTrue(section + "#1", "Request succeeded", r.success);
			AssertEquals(section + "#1", "No errorcode", r.errorcode, 0);

			// duplicate score gets rejected
			score.points = 9000;
			Thread.Sleep (1000);

			Playtomic.Leaderboards.Save (score, r2 => {
				AssertTrue(section + "#2", "Request succeeded", r2.success);
				AssertEquals(section + "#2", "Rejected duplicate score", r2.errorcode, 209);

				// better score gets accepted
				score.points = 11000;

				Playtomic.Leaderboards.Save (score, r3 => {
					AssertTrue(section + "#3", "Request succeeded", r3.success);
					AssertEquals(section + "#3", "No errorcode", r3.errorcode, 0);

					// score gets accepted
					score.points = 9000;
					score.allowduplicates = true;

					Playtomic.Leaderboards.Save (score, r4 => {
						AssertTrue(section + "#4", "Request succeeded", r4.success);
						AssertEquals(section + "#4", "No errorcode", r4.errorcode, 0);
						done();
					});
				});
			});
		});
	}

	public static void SecondScore(Action done) 
	{		
		const string section = "TestLeaderboards.SecondScore";
		Debug.Log (section);

		var score = new PlayerScore {
			table = "scores" + rnd,
			playername = "person2",
			points = 20000,
			allowduplicates = true,
			highest =  true,
			fields = new PDictionary { 
				{"rnd", rnd}
			}
		};
		
		Thread.Sleep (1000);
		Playtomic.Leaderboards.Save (score, r => {
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			done();
		});
	}
	
	public static void HighScores(Action done)
	{
		const string section = "TestLeaderboards.Highscores";
		Debug.Log (section);

		PLeaderboardOptions options = new PLeaderboardOptions
		{
			
			table = "scores" + rnd,
			highest = true,
			filters = new PDictionary
			{
				{"rnd", rnd}
			}
		};
		
		Playtomic.Leaderboards.List (options, (scores, numscores, r) => {
			scores = scores ?? new List<PlayerScore>();

			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertTrue(section, "Received scores", scores.Count > 0);
			AssertTrue(section, "Received numscores", numscores > 0);

			if(scores.Count > 1) {
				AssertTrue(section, "First score is greater than second", scores[0].points > scores[1].points);
			} else {
				AssertTrue(section, "First score is greater than second forced failure", false);
			}

			done();
		});
	}
	
	public static void LowScores(Action done)
	{
		const string section = "TestLeaderboards.LowScores";
		Debug.Log (section);

		PLeaderboardOptions options = new PLeaderboardOptions
		{
			
			table = "scores" + rnd,
			lowest = true,
			perpage = 2,
			filters = new PDictionary
			{
				{"rnd", rnd}
			}
		};
		
		
		Playtomic.Leaderboards.List (options, (scores, numscores, r) => {
			scores = scores ?? new List<PlayerScore>();
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertTrue(section, "Received scores", scores.Count == 2);
			AssertTrue(section, "Received numscores", numscores > 0);

			if(scores.Count > 1) {
				AssertTrue(section, "First score is less than second", scores[0].points < scores[1].points);
			} else {
				AssertTrue(section, "First score is less than second forced failure", false);
			}

			done();
		});
	}
	
	public static void AllScores(Action done)
	{
		const string section = "TestLeaderboards.AllScores";
		Debug.Log (section);

		PLeaderboardOptions options = new PLeaderboardOptions
		{
			
			table = "scores" + rnd,
			mode = "newest",
			perpage = 2,
		};
		
		Playtomic.Leaderboards.List (options, (scores, numscores, r) => {
			scores = scores ?? new List<PlayerScore>();
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertTrue(section, "Received scores", scores.Count > 0);
			AssertTrue(section, "Received numscores", numscores > 0);

			if(scores.Count > 1) {
				AssertTrue(section, "First score is newer or equal to second", scores[0].date >= scores[1].date);
			} else {
				AssertTrue(section, "First score is newer or equal to second forced failure", false);
			}

			done();
		});
	}

	public static void FriendsScores(Action done)
	{
		const string section = "TestLeaderboards.FriendsScores";
		Debug.Log (section);

		var playerids = new List<object>(){ "1", "2", "3", "4", "5", "6", "7", "8", "9", "10" };

		FriendsScoresLoop (playerids, 0, () => {

			PLeaderboardOptions list = new PLeaderboardOptions
			{
				
				table = "friends" + rnd,
				perpage = 3,
				friendslist = new List<string> { "1", "2", "3" }
			};
			
			Playtomic.Leaderboards.List(list, (scores, numscores, r2) => {
				scores = scores ?? new List<PlayerScore>();
				AssertTrue(section, "Request succeeded", r2.success);
				AssertEquals(section, "No errorcode", r2.errorcode, 0);
				AssertTrue(section, "Received 3 scores", scores.Count == 3);
				AssertTrue(section, "Received numscores 3", numscores == 3);
				AssertTrue(section, "Player id #1", scores[0].playerid == "3");
				AssertTrue(section, "Player id #2", scores[1].playerid == "2");
				AssertTrue(section, "Player id #3", scores[2].playerid == "1");
				done();
			});

		});
	}

	private static void FriendsScoresLoop(List<object> playerids, int points, Action finished)
	{
		Thread.Sleep (500);
		points += 1000;

		var playerid = playerids [0].ToString ();
		playerids.RemoveAt (0);

		var score =  new PlayerScore {
			playername = "playerid" + playerid,
			playerid = playerid,
			table = "friends" + rnd,
			points = points,
			highest = true,
			fields = {
				{"rnd", rnd}
			}
		};

		Playtomic.Leaderboards.Save (score, r => {

			if (playerids.Count > 0) {
				FriendsScoresLoop (playerids, points, finished);
				return;
			}
	
			finished();
		});
	}

	public static void OwnScores(Action done)
	{
		const string section = "TestLeaderboards.OwnScores";
		Debug.Log (section);

		OwnScoresLoop (0, 0, () => {

			var finalscore = new PlayerScore {
				playername = "test account",
				playerid = "test@testuri.com",
				table = "personal" + rnd,
				points = 2500,
				highest = true,
				allowduplicates = true,
				fields = {
					{"rnd", rnd}
				},
				perpage = 5
			};

			Playtomic.Leaderboards.SaveAndList (finalscore, (scores, numscores, r2) => {
				scores = scores ?? new List<PlayerScore> ();

				AssertTrue (section, "Request succeeded", r2.success);
				AssertEquals (section, "No errorcode", r2.errorcode, 0);
				AssertTrue (section, "Received 5 scores", scores.Count == 5);
				AssertTrue (section, "Received numscores 10", numscores == 10);
				AssertTrue (section, "Score 1 ranked 6", scores [0].rank == 6);
				AssertTrue (section, "Score 2 ranked 7", scores [1].rank == 7);
				AssertTrue (section, "Score 3 ranked 8", scores [2].rank == 8);
				AssertTrue (section, "Score 4 ranked 9", scores [3].rank == 9);
				AssertTrue (section, "Score 5 ranked 10", scores [4].rank == 10);
				AssertTrue (section, "Score 1 points", scores [0].points == 4000);
				AssertTrue (section, "Score 2 points", scores [1].points == 3000);
				AssertTrue (section, "Score 3 points", scores [2].points == 2500);
				AssertTrue (section, "Score 3 submitted", scores [2].submitted);
				AssertTrue (section, "Score 4 points", scores [3].points == 2000);
				AssertTrue (section, "Score 5 points", scores [4].points == 1000);
				done ();
			});
		});
	}

	private static void OwnScoresLoop(int count, int points, Action finished)
	{
		Thread.Sleep (500);
		points += 1000;
		count++;

		var score = new PlayerScore {
			playername = "test account",
			playerid = "test@testuri.com",
			table = "personal" + rnd,
			points = points,
			highest = true,
			allowduplicates = true,
			fields = {
				{"rnd", rnd}
			}
		};

		Playtomic.Leaderboards.Save (score, r => {

			if (count < 9) {
				OwnScoresLoop (count, points, finished);
				return;
			}

			finished ();
		});
	}
}