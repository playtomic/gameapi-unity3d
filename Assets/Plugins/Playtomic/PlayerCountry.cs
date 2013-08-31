using System;
using System.Collections.Generic;

public class PlayerCountry
{
	public PlayerCountry() 
	{
	}
	
	public PlayerCountry(Dictionary<string,object> data)
	{
		if(data == null) {
			return;
		}
		
		name = data.ContainsKey ("name") ? (string)data["name"] : null;
		code = data.ContainsKey ("code") ? (string)data["code"] : null;
	}
	
	public string name { get; set; }
	public string code { get; set; }
}
