using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PTests : MonoBehaviour 
{
	private List<Action<Action>> tests;
	
	void Start() 
	{
		Playtomic.Initialize("testpublickey", "testprivatekey", "http://127.0.0.1:3000");
		PTest.Setup ();	
		PTestLeaderboards.rnd = PTestPlayerLevels.rnd = RND();
		
		tests = new List<Action<Action>>();
		tests.Add (PTestGameVars.All);
		tests.Add (PTestGameVars.Single);
		tests.Add (PTestGeoIP.Lookup);
		tests.Add (PTestLeaderboards.FirstScore);
		tests.Add (PTestLeaderboards.SecondScore);
		tests.Add (PTestLeaderboards.HighScores);
		tests.Add (PTestLeaderboards.LowScores);
		tests.Add (PTestLeaderboards.AllScores);
		tests.Add (PTestPlayerLevels.Create);
		tests.Add (PTestPlayerLevels.List);
		tests.Add (PTestPlayerLevels.Load);
		tests.Add (PTestPlayerLevels.Rate);
		Next ();
	}
	
	void Next()
	{
		if(tests.Count == 0) {
			PTest.Render ();
			return;
		}
		
		var action = tests[0];
		action(Next);
		
		tests.RemoveAt(0);
	}
	
	private int RND()
	{
		var random = new System.Random();
		return random.Next (int.MaxValue);
	}
}
