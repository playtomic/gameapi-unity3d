using System;
using System.Collections;

public class PlayerAward : Hashtable
{
	public PlayerAward ()
	{
	}

	public PlayerAward(Hashtable data)
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
				awarded = new PlayerAchievement ((Hashtable) data[x]);
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

	public string achievementid
	{
		get { return GetString ("achievementid"); }
		set { SetProperty ("achievementid", value); }
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

	public Hashtable fields
	{
		get { return ContainsKey ("fields") ? (Hashtable)this["fields"] : new Hashtable();	}
		set { SetProperty ("fields", value); }
	}

	private long GetLong(string s) 
	{
		return ContainsKey (s) ? long.Parse(this[s].ToString ()) : 0L;
	}

	private string GetString(string s) 
	{	
		return ContainsKey (s) ? this[s].ToString () : null;
	}

	private void SetProperty(string key, object value) 
	{
		if(ContainsKey(key))
		{
			this[key] = value;
		} 
		else 
		{
			Add(key, value);
		}
	}
}