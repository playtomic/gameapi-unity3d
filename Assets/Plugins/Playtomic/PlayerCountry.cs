using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerCountry : PDictionary
{
	public PlayerCountry(): base() {}

	public PlayerCountry(IDictionary data)
	{
		foreach(string x in data.Keys)
		{			
			this[x] = data[x];
		}
	}
	
	public string name 
	{
		get { return GetString ("name"); }
		set { SetProperty("name", value); }
	}
	
	public string code
	{
		get { return GetString ("code"); }
		set { SetProperty ("code", value); }
	}	
	
}
