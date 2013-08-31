using System;
using System.Collections.Generic;

public class GameVar : Dictionary<string,object>
{
	public GameVar ()
	{
	}

	public GameVar(Dictionary<string,object> data)
	{
		foreach(string x in data.Keys)
		{			
			this[x] = data[x];
		}
	}

	public string name
	{
		get { return GetString ("name"); }
		set { SetProperty ("name", value); }
	}

	public string value
	{
		get { return GetString ("value"); }
		set { SetProperty ("value", value); }
	}

	private long GetLong(string s) 
	{
		return ContainsKey (s) ? long.Parse(this[s].ToString ()) : 0L;
	}
	
	private float GetFloat(string s) 
	{
		return ContainsKey (s) ? float.Parse(this[s].ToString ()) : 0f;
	}
	
	private float GetInt(string s) 
	{
		return ContainsKey (s) ? int.Parse(this[s].ToString ()) : 0;
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
	
	public override string ToString ()
	{
		return name;
	}
}