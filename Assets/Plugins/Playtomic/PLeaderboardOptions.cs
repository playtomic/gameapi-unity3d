using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PLeaderboardOptions : PDictionary
{

	public string table 
	{
		get { return GetString ("table"); }
		set { SetProperty("table", value); }
	}
	
	public bool highest 
	{
		get { return GetBool ("highest"); }
		set { SetProperty("highest", value); }
	}
	
	public bool lowest 
	{
		get { return GetBool ("lowest"); }
		set { SetProperty("lowest", value); }
	}
	
	public bool allowduplicates 
	{
		get { return GetBool ("allowduplicates"); }
		set { SetProperty("allowduplicates", value); }
	}
	
	public PDictionary filters 
	{
		get { return GetDictionary("filters"); }
		set { SetProperty("filters", value); }
	}
	
	public string source 
	{
		get { return GetString ("source"); }
		set { SetProperty("source", value); }
	}
	
	public List<string> friendslist 
	{
		get { return GetList<string>("friendslist"); }
		set { SetProperty("friendslist", value); }
	}
	
	public string playerid 
	{
		get { return GetString ("playerid"); }
		set { SetProperty("playerid", value); }
	}
	
	public bool excludeplayerid 
	{
		get { return GetBool ("excludeplayerid"); }
		set { SetProperty("excludeplayerid", value); }
	}
	
	public string mode 
	{
		get { return GetString ("mode"); }
		set { SetProperty("mode", value); }
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

}