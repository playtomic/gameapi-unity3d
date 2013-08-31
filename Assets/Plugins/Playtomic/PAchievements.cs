using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PAchievements
{
	private static string SECTION = "achievements";
	private static string LIST = "list";
	private static string STREAM = "stream";
	private static string SAVE = "save";

	/**
	 * Lists all achievements
	 * @param	options		The list options
	 * @param	callback	Your callback Action<List<Achievement>, PResponse>
	 */
	public void List(Dictionary<string,object> options, Action<List<PlayerAchievement>, PResponse> callback) {
		
		Playtomic.API.StartCoroutine(SendListRequest(SECTION, LIST, callback, options));
	}
	
	internal IEnumerator SendListRequest(string section, string action, Action<List<PlayerAchievement>, PResponse> callback, Dictionary<string,object> postdata = null)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		var data = response.success ? response.json : null;
		
		UnityEngine.Debug.Log(data);
		var achievements = new List<PlayerAchievement>();
		if (response.success) 
		{
			var acharray = (List<PlayerAchievement>) data["achievements"];
			achievements.AddRange(from object t in acharray select new PlayerAchievement((Dictionary<string,object>) t));
		}
		
		callback(achievements, response);
	}

	/**
	 * Shows a chronological stream of achievements 
	 * @param	options		The stream options
	 * @param	callback	Your callback Action<List<Achievement>, int, PResponse>
	 */ 
	public void Stream(Dictionary<string,object> options, Action<List<PlayerAward>, int, PResponse> callback) {
		Playtomic.API.StartCoroutine(SendStreamRequest(SECTION, STREAM, callback, options));
	}
	
	internal IEnumerator SendStreamRequest(string section, string action, Action<List<PlayerAward>, int, PResponse> callback, Dictionary<string,object> postdata = null)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		var data = response.success ? response.json : null;
		
		int numachievements = 0;
				
		int.TryParse( data["numachievements"].ToString(), out numachievements);
		
		var achievements = new List<PlayerAward>();

		if (response.success) 
		{
			var acharray = (ArrayList) data["achievements"];
			achievements.AddRange(from object t in acharray select new PlayerAward((Dictionary<string,object>) t));
		}

		callback(achievements, numachievements, response);
	}

	/**
	 * Award an achievement to a player
	 * @param	achievement	The achievement
	 * @param	callback	Your callback Action<PResponse>
	 */
	public void Save(Dictionary<string,object> achievement, Action<PResponse> callback) {
		Playtomic.API.StartCoroutine(SendSaveRequest(SECTION, SAVE, callback, achievement));
	}
	
	internal IEnumerator SendSaveRequest(string section, string action, Action<PResponse> callback, Dictionary<string,object> postdata = null)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		callback(response);
	}
}