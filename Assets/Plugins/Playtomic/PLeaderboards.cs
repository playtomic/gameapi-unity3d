using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class PLeaderboards
{
	private const string SECTION = "leaderboards";
	private const string SAVEANDLIST = "saveandlist";
	private const string SAVE = "save";
	private const string LIST = "list";
	
	/**
	 * Saves a player's score
	 * @param	score	PlayerScore	The PlayerScore object 
	 * @param	callback	Action<PResponse> Your callback method
	 */
	public void Save(PlayerScore score, Action<PResponse> callback)
	{
		Playtomic.API.StartCoroutine(SendSaveRequest(SECTION, SAVE, score, callback));
	}
	
	private IEnumerator SendSaveRequest(string section, string action, Dictionary<string,object> postdata, Action<PResponse> callback)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		callback(response);
	}
	
	/**
	 * Saves a player's score and then returns the page of scores
	 * it is on
	 * @param	score	PlayerScore	The PlayerScore object 
	 * @param	callback	Action<List<PlayerScore>, int, PResponse> Your callback method
	 */
	public void SaveAndList(PlayerScore score, Action<List<PlayerScore>, int, PResponse> callback)
	{
		Playtomic.API.StartCoroutine(SendListRequest(SECTION, SAVEANDLIST, score, callback));
	}
	
	/**
	 * Lists scores
	 * @param	options	Dictionary<string,object>	The listing options
	 * @param	callback	Action<List<PlayerScore>, int, PResponse>	Your callback function
	 */
	public void List(Dictionary<string,object> options, Action<List<PlayerScore>, int, PResponse> callback)
	{	
		Playtomic.API.StartCoroutine(SendListRequest(SECTION, LIST, options, callback));
	}
	
	private IEnumerator SendListRequest(string section, string action, Dictionary<string,object> postdata, Action<List<PlayerScore>, int, PResponse> callback)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		var data = response.json;
		List<PlayerScore> scores;
		int numscores;
		ProcessScores (response, data, out scores, out numscores);
		
		callback(scores, numscores, response);
	}
	
	private void ProcessScores(PResponse response, Dictionary<string,object> data, out List<PlayerScore> scores, out int numitems)
	{
		scores = new List<PlayerScore>();
		numitems = 0;
		
		if (response.success)
		{
			numitems = (int)(double)data["numscores"];
			var scorearr = (List<object>) data["scores"];
			
			for(var i=0; i<scorearr.Count; i++)
			{
				scores.Add(new PlayerScore((Dictionary<string,object>) scorearr[i]));
			}
		}
	}
}