using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

internal class PTestAchievements : PTest
{
	public static int rnd;

	public static void List(Action done) {

		var section = "PTestAchievements.List";
		Debug.Log (section);

		var achievements = new List<PlayerAchievement>();
		achievements.Add(new PlayerAchievement(
			new Dictionary<string,object> {
				{"achievement", "Super Mega Achievement #1"},
				{"achievementkey", "secretkey"},
				{"playerid", "1"},
				{"playername", "ben"},
				{"fields", new Dictionary<string,object> { 
					{"rnd", rnd}
					}}
		}));
		achievements.Add(new PlayerAchievement(
			new Dictionary<string,object> {
			{"achievement", "Super Mega Achievement #1"},
			{"achievementkey", "secretkey"},
			{"playerid", "2"},
			{"playername", "michelle"},
			{"fields", new Dictionary<string,object> { 
				{"rnd", rnd}
				}}
		}));
		achievements.Add(new PlayerAchievement(
			new Dictionary<string,object> {
			{"achievement", "Super Mega Achievement #1"},
			{"achievementkey", "secretkey"},
			{"playerid", "3"},
			{"playername", "peter"},
			{"fields", new Dictionary<string,object> { 
				{"rnd", rnd}
				}}
		}));
		achievements.Add(new PlayerAchievement(
			new Dictionary<string,object> {
			{"achievement", "Super Mega Achievement #2"},
			{"achievementkey", "secretkey2"},
			{"playerid", "3"},
			{"playername", "peter"},
			{"fields", new Dictionary<string,object> { 
				{"rnd", rnd}
				}}
		}));
		achievements.Add(new PlayerAchievement(
			new Dictionary<string,object> {
			{"achievement", "Super Mega Achievement #2"},
			{"achievementkey", "secretkey2"},
			{"playerid", "2"},
			{"playername", "michelle"},
			{"fields", new Dictionary<string,object> { 
				{"rnd", rnd}
				}}
		}));

		ListLoop (section, achievements, () => {

			PAchievementOptions options = new PAchievementOptions {
			
				filters = new PDictionary{
					{"rnd",rnd}
				}
			};

			Playtomic.Achievements.List(options, (ach, r2) => {
				AssertTrue(section, "Request succeeded", r2.success);
				AssertEquals(section, "No errorcode", r2.errorcode, 0);
				AssertEquals(section, "Achievement 1 is correct", ach[0].achievement, "Super Mega Achievement #1");
				AssertEquals(section, "Achievement 2 is correct", ach[1].achievement, "Super Mega Achievement #2");
				AssertEquals(section, "Achievement 3 is correct", ach[2].achievement, "Super Mega Achievement #3");
				done();
			});
		});
	}

	private static void ListLoop(string section, List<PlayerAchievement> achievements, Action finished) {

		var item = achievements[0];
		achievements.RemoveAt(0);
		
		Playtomic.Achievements.Save(item, r => {
			AssertTrue(section, "Request succeeded (" + (5 - achievements.Count) + ")", r.success);
			AssertEquals(section, "No errorcode (" + (5 - achievements.Count) + ")", r.errorcode, 0);
			Thread.Sleep(2000);

			if(achievements.Count > 0) {
				ListLoop(section, achievements, finished);
				return;
			}

			finished();
		});

	}

	public static void ListWithFriends(Action done) {

		const string section = "PTestAchievements.ListWithFriends";
		Debug.Log (section);


		PAchievementOptions options = new PAchievementOptions {
		
			friendslist = new List<string>(new [] {"1", "2", "3"}),
	
			filters = new PDictionary{
				{"rnd",rnd}
			}
		
		
		};

		Playtomic.Achievements.List(options, (achievements, r) => {
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertEquals(section, "Achievement 1 is correct", achievements[0].achievement, "Super Mega Achievement #1");
			AssertEquals(section, "Achievement 2 is correct", achievements[1].achievement, "Super Mega Achievement #2");
			AssertEquals(section, "Achievement 3 is correct", achievements[2].achievement, "Super Mega Achievement #3");
			AssertTrue(section, "Achievement 1 has friends", achievements[0].friends != null);
			AssertTrue(section, "Achievement 2 has friends", achievements[1].friends != null);
			AssertTrue(section, "Achievement 3 has no friends", achievements[2].friends == null);
			AssertTrue(section, "Achievement 1 has 3 friends", achievements[0].friends.Count == 3);
			AssertTrue(section, "Achievement 1 friend 1", achievements[0].friends[0].playername == "ben");
			AssertTrue(section, "Achievement 1 friend 2", achievements[0].friends[1].playername == "michelle");
			AssertTrue(section, "Achievement 1 friend 3", achievements[0].friends[2].playername == "peter");
			AssertTrue(section, "Achievement 2 has 2 friend", achievements[1].friends.Count == 2);
			AssertTrue(section, "Achievement 2 friend 1", achievements[1].friends[0].playername == "michelle");
			AssertTrue(section, "Achievement 2 friend 2", achievements[1].friends[1].playername == "peter");
			done();
		});
	}

	public static void ListWithPlayer(Action done) {

		const string section = "PTestAchievements.ListWithPlayer";
		Debug.Log (section);


		PAchievementOptions options = new PAchievementOptions {
		
			playerid = "1",
			filters = new PDictionary{
				{"rnd",rnd}
			}
		
		
		};

		Playtomic.Achievements.List(options, (achievements, r) => {
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertEquals(section, "Achievement 1 is correct", achievements[0].achievement, "Super Mega Achievement #1");
			AssertEquals(section, "Achievement 2 is correct", achievements[1].achievement, "Super Mega Achievement #2");
			AssertEquals(section, "Achievement 3 is correct", achievements[2].achievement, "Super Mega Achievement #3");
			AssertTrue(section, "Achievement 1 has no friends", achievements[0].friends == null);
			AssertTrue(section, "Achievement 2 has no friends", achievements[1].friends == null);
			AssertTrue(section, "Achievement 3 has no friends", achievements[2].friends == null);
			AssertTrue(section, "Achievement 1 has does have player", achievements[0].player != null);
			AssertTrue(section, "Achievement 2 has no player", achievements[1].player == null);
			AssertTrue(section, "Achievement 3 has no player", achievements[2].player == null);
			AssertTrue(section, "Achievement 1 player is ben", achievements[0].player.playername == "ben");
			done();
		});
	}

	public static void ListWithPlayerAndFriends(Action done) {

		const string section = "PTestAchievements.ListWithPlayerAndFriends";
		Debug.Log (section);

		PAchievementOptions options = new PAchievementOptions {
		
			playerid = "1",
						
			filters = new PDictionary{
				{"rnd",rnd}
			},
			
			friendslist = new List<string>(new [] {"1", "2", "3"}),
	
		};
		

		Playtomic.Achievements.List(options, (achievements, r) => {
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertEquals(section, "Achievement 1 is correct", achievements[0].achievement, "Super Mega Achievement #1");
			AssertEquals(section, "Achievement 2 is correct", achievements[1].achievement, "Super Mega Achievement #2");
			AssertEquals(section, "Achievement 3 is correct", achievements[2].achievement, "Super Mega Achievement #3");
			AssertTrue(section, "Achievement 1 has player", achievements[0].player != null);			
			AssertTrue(section, "Achievement 1 has friends", achievements[0].friends != null);
			AssertTrue(section, "Achievement 2 has friends", achievements[1].friends != null);
			AssertTrue(section, "Achievement 2 has no player", achievements[1].player == null);
			AssertTrue(section, "Achievement 3 has no friends", achievements[2].friends == null);
			AssertTrue(section, "Achievement 3 has no player", achievements[2].player == null);
			AssertTrue(section, "Achievement 1 player", achievements[0].player.playername == "ben");
			AssertTrue(section, "Achievement 1 has 2 friend", achievements[0].friends.Count == 2);
			AssertTrue(section, "Achievement 1 friend 1", achievements[0].friends[0].playername == "michelle");
			AssertTrue(section, "Achievement 1 friend 2", achievements[0].friends[1].playername == "peter");
			AssertTrue(section, "Achievement 2 has 2 friend", achievements[1].friends.Count == 2);
			AssertTrue(section, "Achievement 2 friend 1", achievements[1].friends[0].playername == "michelle");
			AssertTrue(section, "Achievement 2 friend 2", achievements[1].friends[1].playername == "peter");
			done();
		});
	}

	public static void Stream(Action done) {

		const string section = "PTestAchievements.Stream";
		Debug.Log (section);

		PAchievementStreamOptions options = new PAchievementStreamOptions {
		
			filters = new PDictionary{
				{"rnd",rnd}
			}
		};

		Playtomic.Achievements.Stream(options, (achievements, numachievements, r) => {
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertTrue(section, "5 achievements returned", achievements.Count == 5);
			AssertTrue(section, "5 achievements in total", numachievements == 5);
			AssertTrue(section, "Achievement 1 person", achievements[0].playername == "michelle");
			AssertTrue(section, "Achievement 1 achievement", achievements[0].awarded.achievement == "Super Mega Achievement #2");
			AssertTrue(section, "Achievement 2 person", achievements[1].playername == "peter");
			AssertTrue(section, "Achievement 2 achievement", achievements[1].awarded.achievement == "Super Mega Achievement #2");
			AssertTrue(section, "Achievement 3 person", achievements[2].playername == "peter");
			AssertTrue(section, "Achievement 3 achievement", achievements[2].awarded.achievement == "Super Mega Achievement #1");
			AssertTrue(section, "Achievement 4 person", achievements[3].playername == "michelle");
			AssertTrue(section, "Achievement 4 achievement", achievements[3].awarded.achievement == "Super Mega Achievement #1");					
			AssertTrue(section, "Achievement 5 person", achievements[4].playername == "ben");
			AssertTrue(section, "Achievement 5 achievement", achievements[4].awarded.achievement == "Super Mega Achievement #1");

			done();
		});
	}

	public static void StreamWithFriends(Action done) {

		const string section = "PTestAchievements.StreamWithFriends";
		Debug.Log (section);

		PAchievementStreamOptions options = new PAchievementStreamOptions {
			group = true,
			friendslist = new List<string>(new [] {"2", "3"}),
			filters = new PDictionary{
				{"rnd",rnd}
			}
		};


		Playtomic.Achievements.Stream(options, (achievements, numachievements, r) => {
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertTrue(section, "2 achievements returned", achievements.Count == 2);
			AssertTrue(section, "2 achievements in total", numachievements == 2);
			AssertTrue(section, "Achievement 1 awards", achievements[0].awards == 2);
			AssertTrue(section, "Achievement 1 achievement", achievements[0].awarded.achievement == "Super Mega Achievement #2");
			AssertTrue(section, "Achievement 1 person", achievements[0].playername == "michelle");
			AssertTrue(section, "Achievement 2 awards", achievements[1].awards == 2);
			AssertTrue(section, "Achievement 2 achievement", achievements[1].awarded.achievement == "Super Mega Achievement #2");
			AssertTrue(section, "Achievement 2 person", achievements[1].playername == "peter");					
			done();
		});
	}

	public static void StreamWithPlayerAndFriends(Action done) {

		const string section = "PTestAchievements.StreamWithPlayerAndFriends";
		Debug.Log (section);

		PAchievementStreamOptions options = new PAchievementStreamOptions {
			group = true,
			friendslist = new List<string>(new [] {"2", "3"}),
			playerid = "1",
			filters = new PDictionary{
				{"rnd",rnd}
			}
		};

		Playtomic.Achievements.Stream(options, (achievements, numachievements, r) => {
			AssertTrue(section, "Request succeeded", r.success);
			AssertEquals(section, "No errorcode", r.errorcode, 0);
			AssertTrue(section, "3 achievements returned", achievements.Count == 3);
			AssertTrue(section, "3 achievements in total", numachievements == 3);
			AssertTrue(section, "Achievement 1 person", achievements[0].playername == "michelle");
			AssertTrue(section, "Achievement 1 awards", achievements[0].awards == 2);
			AssertTrue(section, "Achievement 1 achievement", achievements[0].awarded.achievement == "Super Mega Achievement #2");
			AssertTrue(section, "Achievement 2 person", achievements[1].playername == "peter");
			AssertTrue(section, "Achievement 2 awards", achievements[1].awards == 2);
			AssertTrue(section, "Achievement 2 achievement", achievements[1].awarded.achievement == "Super Mega Achievement #2");
			AssertTrue(section, "Achievement 3 person", achievements[2].playername == "ben");
			AssertTrue(section, "Achievement 3 awards", achievements[2].awards == 1);
			AssertTrue(section, "Achievement 3 achievement", achievements[2].awarded.achievement == "Super Mega Achievement #1");
			done();
		});
	}

	public static void Save(Action done) {

		const string section = "PTestAchievements.Save";
		Debug.Log (section);

		var achievement = new PlayerAchievement {
			{"achievement", "Super Mega Achievement #1"},
			{"achievementkey", "secretkey"},
			{"playerid", rnd.ToString()},
			{"playername", "a random name " + rnd},
			{"fields", new Dictionary<string,object> {
				{"rnd", rnd }
				}}
		};

		Playtomic.Achievements.Save(achievement, r => {
			AssertTrue(section + "#1", "Request succeeded", r.success);
			AssertEquals(section + "#1", "No errorcode", r.errorcode, 0);

			// second save gets rejected
			Playtomic.Achievements.Save(achievement, r2 => {
				AssertFalse(section + "#2", "Request failed", r2.success);
				AssertEquals(section + "#2", "Already had achievement errorcode", r2.errorcode, 505);

				// third save overwrites the first
				achievement.overwrite = true;

				Playtomic.Achievements.Save(achievement, r3 => {
					AssertTrue(section + "#3", "Request succeeded", r3.success);
					AssertEquals(section + "#3", "Already had achievement errorcode", r3.errorcode, 506);

					// fourth saves with allow duplicates
					achievement.allowduplicates = true;
					achievement.Remove("overwrite");

					Playtomic.Achievements.Save(achievement, r4 => {
						AssertTrue(section + "#4", "Request succeeded", r4.success);
						AssertEquals(section + "#4", "Already had achievement errorcode", r4.errorcode, 506);
						done();
					});
				});
			});
		});
	}
}