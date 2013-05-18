using System;
using System.Collections;

public class PlayerLevel : Hashtable
{
	public PlayerLevel()
	{
	}
	
	public PlayerLevel(IDictionary data)
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
	
	public string levelid 
	{
		get { return GetString ("levelid"); }
		set { SetProperty ("levelid", value); }
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
	
	public string name
	{
		get { return GetString ("name"); }
		set { SetProperty ("name", value); }
	}
	
	public string data
	{
		get { return GetString ("data"); }
		set { SetProperty ("data", value); }
	}
	
	public long votes 
	{
		get { return GetLong("votes"); }
	}

	public long score 
	{
		get { return GetLong ("score"); }
	}
	
	public double rating 
	{
		get { 
			var s = score;
			var v = votes;

			if (s == 0 || v == 0) {
				return 0;
			}

			return s / v;
		}
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