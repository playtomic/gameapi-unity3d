using System;
using System.Collections.Generic;

public class PlayerAward : PDictionary
{
	
	public PlayerAward(): base() {}

	public PlayerAward(Dictionary<string,object> data)
	{
		foreach(string x in data.Keys)
		{			
			if(x == "date") 
			{
				var d = new DateTime(1970, 1, 1, 0, 0, 0);
				date = d.AddSeconds ((double)data[x]);
				continue;
			} 

			if(x == "awarded") {
				awarded = new PlayerAchievement ((Dictionary<string,object>) data[x]);
				continue;
			}

			this[x] = data[x];
		}
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
	
	public string source
	{
		get { return GetString ("source"); }
		set { SetProperty ("source", value); }
	}

	public PlayerAchievement awarded
	{
		get { return ContainsKey ("awarded") ? (PlayerAchievement) this["awarded"] : null; }
		private set { SetProperty ("awarded", value); }
	}

	public long awards 
	{
		get { return GetLong ("awards"); }
		set { SetProperty ("awards", value); }
	}

	public DateTime date
	{
		get { return ContainsKey ("date") ? (DateTime) this["date"] : DateTime.Now; }
		private set { SetProperty ("date", value); }
	}

	public string rdate 
	{
		get { return GetString ("rdate"); }
	}

	public Dictionary<string,object> fields
	{
		get { return ContainsKey ("fields") ? (Dictionary<string,object>)this["fields"] : new Dictionary<string,object>();	}
		set { SetProperty ("fields", value); }
	}

}