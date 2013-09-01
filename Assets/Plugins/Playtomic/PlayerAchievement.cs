using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerAchievement : PDictionary
{

	public PlayerAchievement(): base() {}
	
	public PlayerAchievement(Dictionary<string,object> data)
	{
		foreach(string x in data.Keys)
		{
			if (x == "player") {
				player = new PlayerAward ((Dictionary<string,object>)data["player"]);
				continue;
			}

			if( x == "friends") {
				var frarr = (List<object>)data [x];
				var fawards = new List<PlayerAward> ();
				fawards.AddRange(from object t in frarr select new PlayerAward((Dictionary<string,object>) t));
				friends = fawards;
				continue;
			}

			this[x] = data[x];
		}
	}

	public string achievement
	{
		get { return GetString ("achievement"); }
		set { SetProperty ("achievement", value); }
	}

	public string achievementkey
	{
		get { return GetString ("achievementkey"); }
		set { SetProperty ("achievementkey", value); }
	}

	public string source
	{
		get { return GetString ("source"); }
		set { SetProperty ("source", value); }
	}

	public string playerid
	{
		get { return GetString ("playerid"); }
		set { SetProperty ("playerid", value); }
	}

	public string playername
	{
		get { return GetString ("playername"); }
		set { SetProperty ("playername", value); }
	}

	public Dictionary<string,object> fields
	{
		get { return ContainsKey ("fields") ? (Dictionary<string,object>)this["fields"] : new Dictionary<string,object>();	}
		set { SetProperty ("fields", value); }
	}

	public PlayerAward player
	{
		get { return ContainsKey ("player") ? (PlayerAward) this["player"] : null; }
		private set { SetProperty("player", value); }
	}

	public List<PlayerAward> friends
	{
		get { return ContainsKey ("friends") ? (List<PlayerAward>) this["friends"] : null; }
		private set { SetProperty("friends", value); }
	}

	public bool allowduplicates
	{
		get { return ContainsKey ("allowduplicates") && (bool) this["allowduplicates"]; }
		set { SetProperty("allowduplicates", value); }
	}

	public bool overwrite
	{
		get { return ContainsKey ("overwrite") && (bool) this["overwrite"]; }
		set { SetProperty("overwrite", value); }
	}

}