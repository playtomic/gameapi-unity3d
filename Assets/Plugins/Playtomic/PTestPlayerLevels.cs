using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

//using Playtomic;

//namespace PlaytomicTest
//{
internal class PTestPlayerLevels : PTest
{
	public static int rnd;
		
	public static void Create (Action done)
	{
		const string section = "PTestPlaytomic.PlayerLevels.Create";
		Debug.Log(section);
		
		var level = new PlayerLevel {
				name = "create level" + rnd,
				playername = "ben" + rnd,
				playerid = "0",
				data = "this is the level data",
				fields = new Dictionary<string,object> {
					{"rnd", rnd}
				}
			};
			
		Playtomic.PlayerLevels.Save (level, (l, r) => {			
			l = l ?? new PlayerLevel ();
			AssertTrue (section + "#1", "Request succeeded", r.success);
			AssertEquals (section + "#1", "No errorcode", r.errorcode, 0);
			AssertTrue (section + "#1", "Returned level is not null", l.Keys.Count > 0);
			AssertTrue (section + "#1", "Returned level has levelid", l.ContainsKey ("levelid"));
			AssertEquals (section + "#1", "Level names match", level.name, l.name); 

			Playtomic.PlayerLevels.Save (level, (l2, r2) => {
				AssertTrue (section + "#2", "Request succeeded", r2.success);
				AssertEquals (section + "#2", "Duplicate level errorcode", r2.errorcode, 405);
				done ();
			});
		});
	}
		
	public static void List (Action done)
	{
		const string section = "PTestPlaytomic.PlayerLevels.List";
		Debug.Log(section);

		PPlayerLevelOptions listoptions = new PPlayerLevelOptions
		{
			
			page = 1,
			perpage = 10,
			data = false,
			filters = new PDictionary {
			
				{"rnd",rnd}
				
				
			}
			
			
		};
			
		Playtomic.PlayerLevels.List (listoptions, (levels, numlevels, r) => {			
			levels = levels ?? new List<PlayerLevel> ();
			AssertTrue (section + "#1", "Request succeeded", r.success);
			AssertEquals (section + "#1", "No errorcode", r.errorcode, 0);
			AssertTrue (section + "#1", "Received levels", levels.Count > 0);
			AssertTrue (section + "#1", "Received numlevels", numlevels >= levels.Count);

			if (levels.Count > 0) {
				AssertFalse (section + "#1", "First level has no data", levels [0].ContainsKey ("data"));
			} else {
				AssertTrue (section + "#1", "First level has no data forced failure", false);
			}

			// list with data
			listoptions ["data"] = true;

			Playtomic.PlayerLevels.List (listoptions, (levels2, numlevels2, r2) => {

				levels2 = levels2 ?? new List<PlayerLevel> ();

				AssertTrue (section + "#2", "Request succeeded", r2.success);
				AssertEquals (section + "#2", "No errorcode", r2.errorcode, 0);
				AssertTrue (section + "#2", "Received levels", levels2.Count > 0);
				AssertTrue (section + "#2", "Received numlevels", numlevels2 >= levels2.Count);

				if (levels2.Count > 0) {
					AssertTrue (section, "First level has data", levels2 [0].ContainsKey ("data"));
				} else {
					AssertTrue (section, "First level has no data forced failure", false);
				}

				done ();
			});

		});
	}
		
	public static void Rate (Action done)
	{
		const string section = "TestPlaytomic.PlayerLevels.Rate";
		Debug.Log(section);
		
		var level = new PlayerLevel {
				name = "rate " + rnd,
				playername = "ben" + rnd,
				playerid = "0",
				data = "this is the level data",
				fields = new Dictionary<string,object> {
					{"rnd", rnd}
				}
			};
			
		Playtomic.PlayerLevels.Save (level, (l, r) => {
			
			l = l ?? new PlayerLevel ();
			AssertTrue (section + "#1", "Request succeeded", r.success);
			AssertEquals (section + "#1", "No errorcode", r.errorcode, 0);
			AssertTrue (section + "#1", "Returned level is not null", l.Keys.Count > 0);
			AssertTrue (section + "#1", "Returned level has levelid", l.ContainsKey ("levelid"));

			// invalid rating
			Playtomic.PlayerLevels.Rate (l.levelid, 70, r2 => {
				AssertFalse (section + "#2", "Request failed", r2.success);
				AssertEquals (section + "#2", "Invalid rating errorcode", r2.errorcode, 401);

				// valid rating
				Playtomic.PlayerLevels.Rate (l.levelid, 7, r3 => {

					AssertTrue (section + "#3", "Request succeeded", r3.success);
					AssertEquals (section + "#3", "No errrorcode", r3.errorcode, 0);

					// duplicate rating
					Playtomic.PlayerLevels.Rate (l.levelid, 6, r4 => {
						AssertFalse (section + "#4", "Request failed", r4.success);
						AssertEquals (section + "#4", "Already rated errorcode", r4.errorcode, 402);
						done ();
					});
				});
			});
		});
	}
		
	public static void Load (Action done)
	{
		const string section = "TestPlaytomic.PlayerLevels.Load";
		Debug.Log(section);
		
		var level = new PlayerLevel {
				name = "sample loading level " + rnd,
				playername = "ben" + rnd,
				playerid = rnd.ToString (CultureInfo.InvariantCulture),
				data = "this is the level data",
				fields = new Dictionary<string,object> {
					{"rnd", rnd}
				}
			};
			
		Playtomic.PlayerLevels.Save (level, (l, r) => {
			AssertTrue (section + "#1", "Request succeeded", r.success);
			AssertEquals (section + "#1", "No errorcode", r.errorcode, 0);
			AssertTrue (section + "#1", "Name is correct", l.ContainsKey ("levelid"));
			AssertEquals (section + "#1", "Name is correct", level.name, l.name);
			AssertEquals (section + "#1", "Data is correct", level.data, l.data);

			Playtomic.PlayerLevels.Load (l.levelid, (l2, r2) => {
				AssertTrue (section, "Request succeeded", r2.success);
				AssertEquals (section, "No errorcode", r2.errorcode, 0);
				AssertEquals (section, "Name is correct", level.name, l2.name);
				AssertEquals (section, "Data is correct", level.data, l2.data);
				done ();
			});
		});
	}
}
//}