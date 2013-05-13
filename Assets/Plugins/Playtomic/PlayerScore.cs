using UnityEngine;
using System;
using System.Collections;

public class PlayerScore : Hashtable
{
	public PlayerScore()
	{
	}
	
	public PlayerScore(Hashtable data)
	{
		foreach(string x in data.Keys)
		{			
			if(x == "date") 
			{
				var d = new DateTime(1970, 1, 1, 0, 0, 0);
				this.date = d.AddSeconds ((double)data[x]);
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
	
	public string sdate 
	{
		get { return GetString ("sdate"); }
		set { SetProperty ("sdate", value); }
	}
	
	public DateTime date
	{
		get { return this.ContainsKey ("date") ? (DateTime) this["date"] : DateTime.Now; }
		set { SetProperty ("date", value); }
	}
	
	public Hashtable fields
	{
		get { return this.ContainsKey ("fields") ? (Hashtable)this["fields"] : new Hashtable();	}
		set { SetProperty ("fields", value); }
	}
	
	public bool submittedorbest
	{
		get { return this.ContainsKey ("submittedorbest"); }
		set { SetProperty("submittedorbest", value); }
	}

	public bool highest
	{
		get { return this.ContainsKey ("highest"); }
		set { SetProperty("highest", value); }
	}

	public bool lowest
	{
		get { return this.ContainsKey ("lowest"); }
		set { SetProperty("lowest", value); }
	}
	
	public bool allowduplicates
	{
		get { return this.ContainsKey ("allowduplicates"); }
		set { SetProperty("allowduplicates", value); }
	}	
	
	public long perpage
	{
		get { return GetLong ("perpage"); }
		set { SetProperty ("perpage", value); }
	}

	private long GetLong(string s) 
	{
		return this.ContainsKey (s) ? long.Parse(this[s].ToString ()) : 0L;
	}
	
	private string GetString(string s) 
	{	
		return this.ContainsKey (s) ? this[s].ToString () : null;
	}
	
	private void SetProperty(string key, object value) 
	{
		if(this.ContainsKey(key))
		{
			this[key] = value;
		} 
		else 
		{
			this.Add(key, value);
		}
	}
}
