using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PAchievementStreamOptions : PDictionary
{
	
	public bool group 
	{
		get { return GetBool ("group"); }
		set { SetProperty("group", value); }
	}
	
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
	
	public int page 
	{
		get { return GetInt ("page"); }
		set { SetProperty("page", value); }
	}
	
	public int perpage 
	{
		get { return GetInt ("perpage"); }
		set { SetProperty("perpage", value); }
	}
	
	public string mode 
	{
		get { return GetString ("mode"); }
		set { SetProperty("mode", value); }
	}
	
	public string source 
	{
		get { return GetString ("source"); }
		set { SetProperty("source", value); }
	}

}