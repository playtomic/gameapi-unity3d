using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerScore : Dictionary<string,object>
{
	public PlayerScore()
	{
	}

	public PlayerScore(IDictionary data)
	{
		foreach(string x in data.Keys)
		{			
			if(x == "date") 
			{
				var d = new DateTime(1970, 1, 1, 0, 0, 0);
				date = d.AddSeconds ((double)data[x]);
			} 
			else 
			{
				this[x] = data[x];
			}
		}
	}
	
	public string playername 
	{
		get { return GetString ("playername"); }
		set { SetProperty("playername", value); }
	}
	
	public string playerid
	{
		get { return GetString ("playerid"); }
		set { SetProperty ("playerid", value); }
	}	
	
	public long points
	{
		get { return GetLong ("points"); }
		set { SetProperty ("points", value); }
	}
	
	public string source
	{
		get { return GetString ("source"); }
		set { SetProperty ("source", value); }
	}
	
	public long rank
	{
		get { return GetLong ("rank"); }
		set { SetProperty ("rank", value); }
	}

	public string table 
	{
		get { return GetString ("table"); }
		set { SetProperty ("table", value); }
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

	public Dictionary<string,object> filters
	{
		get { return ContainsKey ("filters") ? (Dictionary<string,object>)this["filters"] : new Dictionary<string,object>();	}
		set { SetProperty ("filters", value); }
	}
	
	public bool highest
	{
		get { return ContainsKey ("highest") && (bool) this["highest"]; }
		set { SetProperty("highest", value); }
	}
	
	public bool submitted
	{
		get { return ContainsKey ("submitted") && (bool) this["submitted"]; }
		set { SetProperty("submitted", value); }
	}

	public bool lowest
	{
		get { return ContainsKey ("lowest") && (bool) this["lowest"]; }
		set { SetProperty("lowest", value); }
	}
	
	public bool allowduplicates
	{
		get { return ContainsKey ("allowduplicates") && (bool) this["allowduplicates"]; }
		set { SetProperty("allowduplicates", value); }
	}

	public long perpage
	{
		get { return GetLong ("perpage"); }
		set { SetProperty ("perpage", value); }
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