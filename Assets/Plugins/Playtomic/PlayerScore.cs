using System;
using System.Collections;

public class PlayerScore : Hashtable
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
	
	public string name 
	{
		get { return GetString ("name"); }
		set { SetProperty("name", value); }
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

	public Hashtable fields
	{
		get { return ContainsKey ("fields") ? (Hashtable)this["fields"] : new Hashtable();	}
		set { SetProperty ("fields", value); }
	}

	public bool highest
	{
		get { return ContainsKey ("highest") && (bool) this["highest"]; }
		set { SetProperty("highest", value); }
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