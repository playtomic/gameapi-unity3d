using System;
using System.Collections;

public class PlayerLevel : Hashtable
{
	public PlayerLevel()
	{
	}
	
	public PlayerLevel(Hashtable data)
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
	
	public string levelid 
	{
		get { return GetString ("levelid"); }
		set { SetProperty ("levelid", value); }
	}
	
	public string playersource
	{
		get { return GetString ("playersource"); }
		set { SetProperty ("playersource", value); }
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
		set { SetProperty ("votes", value); }
	}
	
	public long plays
	{
		get { return GetLong ("plays"); }
		set { SetProperty ("plays", value); }
	}
	
	public double rating
	{
		get { return score / votes; }
	}
	
	public long score 
	{
		get { return GetLong ("score"); }
		set { SetProperty ("score", value); }
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
		
	private long GetLong(string s) 
	{
		return this.ContainsKey (s) ? (Int64) this[s] : 0L;
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