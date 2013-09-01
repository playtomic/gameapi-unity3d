using System;
using System.Collections.Generic;

public class GameVar : PDictionary
{
	
	public GameVar(): base() {}
	
	public GameVar(Dictionary<string,object> data): base(data) {}
	
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

}