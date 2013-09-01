using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PAchievementOptions : PDictionary
{

	public string playerid 
	{
		get { return GetString ("playerid"); }
		set { SetProperty("playerid", value); }
	}
	
	public List<string> friendslist 
	{
		get { return GetList<string> ("friendslist"); }
		set { SetProperty("friendslist", value); }
	}
	
	public PDictionary filters 
	{
		get { return GetDictionary ("filters"); }
		set { SetProperty("filters", value); }
	}

}