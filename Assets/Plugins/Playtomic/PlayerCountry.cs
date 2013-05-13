using System;
using System.Collections;

public class PlayerCountry
{
	public PlayerCountry() 
	{
	}
	
	public PlayerCountry(Hashtable data)
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
