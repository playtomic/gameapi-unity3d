using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PNewsletterOptions : PDictionary
{

	public string email 
	{
		get { return GetString ("email"); }
		set { SetProperty("email", value); }
	}
	
	public string firstname 
	{
		get { return GetString ("firstname"); }
		set { SetProperty("firstname", value); }
	}
	
	public string lastname 
	{
		get { return GetString ("lastname"); }
		set { SetProperty("lastname", value); }
	}
	
	public PDictionary fields 
	{
		get { return GetDictionary ("fields"); }
		set { SetProperty("fields", value); }
	}
	
}