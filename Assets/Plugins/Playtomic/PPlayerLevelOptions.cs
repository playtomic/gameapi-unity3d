using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PPlayerLevelOptions : PDictionary
{

	public string mode 
	{
		get { return GetString ("mode"); }
		set { SetProperty("mode", value); }
	}
	
	public string datemin 
	{
		get { return GetString ("datemin"); }
		set { SetProperty("datemin", value); }
	}
	
	public string datemax 
	{
		get { return GetString ("datemax"); }
		set { SetProperty("datemax", value); }
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
	
		
	public bool data 
	{
		get { return GetBool ("data"); }
		set { SetProperty("data", value); }
	}
	
		
	public string source 
	{
		get { return GetString ("source"); }
		set { SetProperty("source", value); }
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

}