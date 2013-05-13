using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PPlayerLevels
{	
	private const string SECTION = "playerlevels";
	private const string SAVE = "save";
	private const string LIST = "list";
	private const string LOAD = "load";
	private const string RATE = "rate";
	
	/**
	 * Saves a PlayerLevel
	 * @param	level	PlayerLevel	The level
	 * @param	callback	Action<PlayerLevel, PResponse>	Callback function
	 */
	public void Save(PlayerLevel level, Action<PlayerLevel, PResponse> callback)
	{
		Playtomic.API.StartCoroutine(SendSaveLoadRequest(SECTION, SAVE, (Hashtable) level, callback));
	}
	
	/**
	 * Loads a level
	 * @param	levelid	string 	The level id
	 * @param 	callback	Action<PlayerLevel, PResponse>	Callback function
	 */
	public void Load(string levelid, Action<PlayerLevel, PResponse> callback)
	{
		var postdata = new Hashtable
		{
			{"levelid", levelid }
		};
		
		Playtomic.API.StartCoroutine(SendSaveLoadRequest(SECTION, LOAD, postdata, callback));
	}

	private IEnumerator SendSaveLoadRequest(string section, string action, Hashtable postdata, Action<PlayerLevel, PResponse> callback)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		PlayerLevel level = null;
		
		if (response.success)
		{
			level = new PlayerLevel((Hashtable) response.json["level"]);
		}
		
		callback(level, response);
	}
	
	/**
	 * Lists levels
	 * @param	options	Hashtable	The listing options
	 * @param 	callback	Action<List<PlayerLevel>, int, PResponse>	Callback function
	 */
	public void List(Hashtable options, Action<List<PlayerLevel>, int, PResponse> callback)
	{
		Playtomic.API.StartCoroutine(SendListRequest(SECTION, LIST, options, callback));
	}
	
	private IEnumerator SendListRequest(string section, string action, Hashtable postdata, Action<List<PlayerLevel>, int, PResponse> callback) 
	{
		var www = PRequest.Prepare(SECTION, LIST, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		List<PlayerLevel> levels = null;
		int numlevels = 0;
	
		if (response.success)
		{
			var data = (Hashtable)response.json;
			levels = new List<PlayerLevel>();
			numlevels = (int)(double)data["numlevels"];
			
			var levelarr = (ArrayList)data["levels"];
			
			for(var i=0; i<levelarr.Count; i++)
			{
				levels.Add(new PlayerLevel((Hashtable) levelarr[i]));
			}
		}
		
		callback(levels, numlevels, response);
	}
	
	/**
	 * Rates a level
	 * @param	levelid	String	The level id
	 * @param	rating	Int		Rating from 1 - 10
	 * @param	callback	Action<PResponse> Your callback function
	 */
	public void Rate(string levelid, int rating, Action<PResponse> callback)
	{
		if(rating < 1 || rating > 10)
		{
			callback(PResponse.Error(401));
			return;
		}

		var postdata = new Hashtable
		{
			{"levelid", levelid},
			{"rating", rating}
		};
		
		Playtomic.API.StartCoroutine(SendRateRequest(SECTION, RATE, postdata, callback));
	}

	private IEnumerator SendRateRequest(string section, string action, Hashtable postdata, Action<PResponse> callback)
	{		
		var www = PRequest.Prepare(SECTION, RATE, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		callback(response);
	}
}