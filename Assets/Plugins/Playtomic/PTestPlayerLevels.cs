using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

internal class PTestPlayerLevels : PTest {

	public static int  rnd;
	private static string ratelevelid;
	
	public static void Create(Action done) {
		
		var level = new PlayerLevel {
			name = "create level" + rnd,
			playername = "ben" + rnd,
			playerid = "0",
			data = "this is the level data",
			fields = new Hashtable {
				{"rnd", rnd}
			}
		};
		
		Playtomic.PlayerLevels.Save (level, SaveComplete, done);
	}
	
	private static void SaveComplete(PlayerLevel l, PResponse r, Action done)
	{
		var section = "PTestPlayerLevels.Create";
		l = l ?? new PlayerLevel();
		
		var level = new PlayerLevel {
			name = "create level" + rnd,
			playername = "ben" + rnd,
			playerid = "0",
			data = "this is the level data",
			fields = new Hashtable {
				{"rnd", rnd}
			}
		};
				
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		AssertTrue(section, "Returned level is not null", l != null);
		AssertTrue(section, "Returned level has levelid", l.ContainsKey("levelid"));
		AssertEquals(section, "Level names match", level.name, l.name); 
		
		Playtomic.PlayerLevels.Save (level, SaveComplete2, done);
	}
	
	private static void SaveComplete2(PlayerLevel l, PResponse r, Action done)
	{
		var section = "PTestPlayerLevels.Create#2";
		
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "Duplicate level errorcode", r.errorcode, 405);
		done();
	}
	
	public static void List(Action done)
	{
		var listoptions = new Hashtable { 
			{"page", 1},
			{"perpage", 10},
			{"data", false},
			{"filters", new Hashtable {
					{"rnd", rnd}
				}
			}
		};
		
		Playtomic.PlayerLevels.List (listoptions, ListComplete, done);
	}
		
	private static void ListComplete(List<PlayerLevel> levels, int numlevels, PResponse r, Action done)
	{
		var section = "PTestPlayerLevels.List#1";
		levels = levels ?? new List<PlayerLevel>();
		
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		AssertTrue(section, "Received levels", levels.Count > 0);
		AssertTrue(section, "Received numlevels", numlevels >= levels.Count);

		if(levels.Count > 0) {
			AssertFalse(section, "First level has no data", levels[0].ContainsKey("data"));
		} else {
			AssertTrue(section, "First level has no data forced failure", false);
		}
			
		var listoptions = new Hashtable { 
			{"page", 1},
			{"perpage", 10},
			{"data", true},
			{"filters", new Hashtable 
				{
					{"rnd", rnd}
				}
			}
		};
			
		Playtomic.PlayerLevels.List (listoptions, ListComplete2, done);
	}
			
	private static void ListComplete2(List<PlayerLevel> levels, int numlevels, PResponse r, Action done)
	{
		var section = "PTestPlayerLevels.List#2";
		levels = levels ?? new List<PlayerLevel>();
				
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		AssertTrue(section, "Received levels", levels.Count > 0);
		AssertTrue(section, "Received numlevels", numlevels >= levels.Count);
		
		if(levels.Count > 0) {
			AssertTrue(section, "First level has data", levels[0].ContainsKey("data"));
		} else {
			AssertTrue(section, "First level has no data forced failure", false);
		}
				
		done();
	}
	
	public static void Rate(Action done)
	{
		var level = new PlayerLevel {
			name = "rate " + rnd,
			playername = "ben" + rnd,
			playerid = "0",
			data = "this is the level data",
			fields = new Hashtable {
				{"rnd", rnd}
			}
		};
		
		Playtomic.PlayerLevels.Save(level, RateSaveComplete, done);
	}
	
	private static void RateSaveComplete(PlayerLevel l, PResponse r, Action done)
	{
		var section = "TestPlayerLevels.Rate#1";
		
		l = l ?? new PlayerLevel();
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		AssertTrue(section, "Returned level is not null", l != null);
		AssertTrue(section, "Returned level has levelid", l.ContainsKey("levelid"));
		
		ratelevelid = l.levelid;
		Playtomic.PlayerLevels.Rate (ratelevelid, 70, RateComplete, done);
	}
	
	private static void RateComplete(PResponse r, Action done)
	{
		var section = "TestPlayerLevels.Rate#2";
		AssertFalse(section, "Request failed", r.success);
		AssertEquals(section, "Invalid rating errorcode", r.errorcode, 401);
		
		Playtomic.PlayerLevels.Rate (ratelevelid, 6, RateComplete2, done);
	}
	
	private static void RateComplete2(PResponse r, Action done)
	{
		var section = "TestPlayerLevels.Rate#3";
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		
		Playtomic.PlayerLevels.Rate (ratelevelid, 6, RateComplete3, done);
	}
	
	private static void RateComplete3(PResponse r, Action done)
	{
		var section = "TestPlayerLevels.Rate#4";
		AssertFalse(section, "Request failed", r.success);
		AssertEquals(section, "Already rated errorcode", r.errorcode, 402);
		done();
	}
	
	public static void Load(Action done)
	{
		var level = new PlayerLevel {
			name = "sample loading level " + rnd,
			playername = "ben" + rnd,
			playerid = rnd.ToString(),
			data = "this is the level data",
			fields = new Hashtable {
				{"rnd", rnd}
			}
		};
		
		Playtomic.PlayerLevels.Save (level, LoadSaveComplete, done);
	}
	
	private static void LoadSaveComplete(PlayerLevel l, PResponse r, Action done)
	{
		var section = "TestPlayerLevels.Load#1";
		
		l = l ?? new PlayerLevel();
		
		var level = new PlayerLevel {
			name = "sample loading level " + rnd,
			playername = "ben" + rnd,
			playerid = rnd.ToString(),
			data = "this is the level data",
			fields = new Hashtable {
				{"rnd", rnd}
			}
		};
		
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		AssertTrue(section, "Name is correct", l.ContainsKey("levelid"));
		AssertEquals(section, "Name is correct", level.name, l.name);
		AssertEquals(section, "Data is correct", level.data, l.data);
		
		Playtomic.PlayerLevels.Load (l.levelid, LoadComplete, done);
	}
	
	private static void LoadComplete(PlayerLevel l, PResponse r, Action done)
	{
		var section = "TestPlayerLevels.Load#2";
		
		l = l ?? new PlayerLevel();
		
		var level = new PlayerLevel {
			name = "sample loading level " + rnd,
			playername = "ben" + rnd,
			playerid = rnd.ToString(),
			data = "this is the level data",
			fields = new Hashtable {
				{"rnd", rnd}
			}
		};
		
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		AssertEquals(section, "Name is correct", level.name, l.name);
		AssertEquals(section, "Data is correct", level.data, l.data);

		done();
	}
}