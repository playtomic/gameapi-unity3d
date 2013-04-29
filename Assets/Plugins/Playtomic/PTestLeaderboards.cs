using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

internal class PTestLeaderboards : PTest {
	
	public static int rnd;
	
	public static void FirstScore(Action done) 
	{		
		var score = new PlayerScore {
			table = "scores" + rnd,
			name = "person1",
			points = 10000,
			highest =  true,
			fields = new Hashtable { 
				{"rnd", rnd}
			}
		};
		
		Playtomic.Leaderboards.Save (score, FirstScoreCompletePart1, done);
	}
	
	private static void FirstScoreCompletePart1(PResponse r, Action done)
	{
		var section = "TestLeaderboards.FirstScore";
		AssertTrue(section + "#1", "Request succeeded", r.success);
		AssertEquals(section + "#1", "No errorcode", r.errorcode, 0);
		
		// duplicate score gets rejected
		var score = new PlayerScore {
			table = "scores" + rnd,
			name = "person1",
			points = 9000,
			highest =  true,
			fields = new Hashtable { 
				{"rnd", rnd}
			}
		};
		
		new WaitForSeconds(1);
		Playtomic.Leaderboards.Save (score, FirstScoreCompletePart2, done);
	}
	
	private static void FirstScoreCompletePart2(PResponse r, Action done)
	{
		var section = "TestLeaderboards.FirstScore#2";
		AssertTrue(section + "#2", "Request succeeded", r.success);
		AssertEquals(section + "#2", "No errorcode", r.errorcode, 209);
		
		// duplicate score gets rejected
		var score = new PlayerScore {
			table = "scores" + rnd,
			name = "person1",
			points = 9000,
			allowduplicates = true,
			highest =  true,
			fields = new Hashtable { 
				{"rnd", rnd}
			}
		};
		
		new WaitForSeconds(1);
		Playtomic.Leaderboards.Save (score, FirstScoreCompletePart3, done);
	}
	
	private static void FirstScoreCompletePart3(PResponse r, Action done)
	{
		var section = "TestLeaderboards.FirstScore#3";
		AssertTrue(section + "#3", "Request succeeded", r.success);
		AssertEquals(section + "#3", "No errorcode", r.errorcode, 0);
		done();
	}
	
	public static void SecondScore(Action done) 
	{		
		var score = new PlayerScore {
			table = "scores" + rnd,
			name = "person2",
			points = 20000,
			allowduplicates = true,
			highest =  true,
			fields = new Hashtable { 
				{"rnd", rnd}
			}
		};
		
		new WaitForSeconds(1);
		Playtomic.Leaderboards.Save (score, SecondScoreComplete, done);
	}
	
	private static void SecondScoreComplete(PResponse r, Action done)
	{
		var section = "TestLeaderboards.SecondScore";
		AssertTrue(section + "#3", "Request succeeded", r.success);
		AssertEquals(section + "#3", "No errorcode", r.errorcode, 0);
		done();
	}
	
	public static void HighScores(Action done)
	{
		var options = new Hashtable
		{
			{"table", "scores" + rnd},
			{"highest", true},
			{"filters", new Hashtable
				{
					{"rnd", rnd}
				}
			}
		};
		
		Playtomic.Leaderboards.List (options, HighScoresComplete, done);
	}
	
	private static void HighScoresComplete(List<PlayerScore> scores, int numscores, PResponse r, Action done)
	{
		var section = "TestLeaderboards.Highscores";
		scores = scores ?? new List<PlayerScore>();
		
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		AssertTrue(section, "Received scores", scores.Count > 0);
		AssertTrue(section, "Received numscores", numscores > 0);
		
		if(scores.Count > 1) {
			AssertTrue(section, "First score is less than second", scores[0].points > scores[1].points);
		} else {
			AssertTrue(section, "First score is less than second forced failure", false);
		}
		
		done();
	}
	
	public static void LowScores(Action done)
	{
		var options = new Hashtable
		{
			{"table", "scores" + rnd},
			{"lowest", true},
			{"perpage", 2},
			{"filters", new Hashtable
				{
					{"rnd", rnd}
				}
			}
		};
		
		Playtomic.Leaderboards.List (options, LowScoresComplete, done);
	}
	
	private static void LowScoresComplete(List<PlayerScore> scores, int numscores, PResponse r, Action done)
	{
		var section = "TestLeaderboards.LowScores";
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
	}
	
	public static void AllScores(Action done)
	{
		var options = new Hashtable
		{
			{"table", "scores" + rnd},
			{"mode", "newest"},
			{"perpage", 2}
		};
		
		Playtomic.Leaderboards.List (options, AllScoresComplete, done);
	}
	
	private static void AllScoresComplete(List<PlayerScore> scores, int numscores, PResponse r, Action done)
	{
		var section = "TestLeaderboards.AllScores";
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
	}
}
