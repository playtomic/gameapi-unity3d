using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PGameVars
{
	private const string SECTION = "gamevars";
	private const string LOAD = "load";
	private const string LOADSINGLE = "single";
		
	/**
	 * Loads all GameVars
	 */
	public void Load(Action<Dictionary<string,object>, PResponse> callback)
	{
		Playtomic.API.StartCoroutine(SendRequest(SECTION, LOAD, callback));
	}
	
	/**
	 * Loads a single GameVar
	 * @param	name	string	The variable name to load
	 */
	public void LoadSingle(string name, Action<Dictionary<string,object>, PResponse> callback)
	{
		var postdata = new Dictionary<string,object>
		{
			{"name", name}
		};
		
		Playtomic.API.StartCoroutine(SendRequest(SECTION, LOADSINGLE, callback, postdata));
	}
	
	internal IEnumerator SendRequest(string section, string action, Action<Dictionary<string,object>, PResponse> callback, Dictionary<string,object> postdata = null)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		var data = response.success ? response.json : null;
		callback(data, response);
	}
}