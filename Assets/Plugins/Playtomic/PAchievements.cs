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
	public void List(Hashtable options, Action<List<PlayerAchievement>, PResponse> callback) {
		
		Playtomic.API.StartCoroutine(SendListRequest(SECTION, LIST, callback, options));
	}
	
	internal IEnumerator SendListRequest(string section, string action, Action<List<PlayerAchievement>, PResponse> callback, Hashtable postdata = null)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		var data = response.success ? response.json : null;
		var achievements = new List<PlayerAchievement>();
		if (response.success) 
		{
			var acharray = (ArrayList) data["achievements"];
			achievements.AddRange(from object t in acharray select new PlayerAchievement((Hashtable) t));
		}
		
		callback(achievements, response);
	}

	/**
	 * Shows a chronological stream of achievements 
	 * @param	options		The stream options
	 * @param	callback	Your callback Action<List<Achievement>, int, PResponse>
	 */ 
	public void Stream(Hashtable options, Action<List<PlayerAward>, int, PResponse> callback) {
		Playtomic.API.StartCoroutine(SendStreamRequest(SECTION, STREAM, callback, options));
	}
	
	internal IEnumerator SendStreamRequest(string section, string action, Action<List<PlayerAward>, int, PResponse> callback, Hashtable postdata = null)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		var data = response.success ? response.json : null;
		int numachievements = response.success ? (int)(double)data["numachievements"] : 0;
		var achievements = new List<PlayerAward>();

		if (response.success) 
		{
			var acharray = (ArrayList) data["achievements"];
			achievements.AddRange(from object t in acharray select new PlayerAward((Hashtable) t));
		}

		callback(achievements, numachievements, response);
	}

	/**
	 * Award an achievement to a player
	 * @param	achievement	The achievement
	 * @param	callback	Your callback Action<PResponse>
	 */
	public void Save(Hashtable achievement, Action<PResponse> callback) {
		Playtomic.API.StartCoroutine(SendSaveRequest(SECTION, SAVE, callback, achievement));
	}
	
	internal IEnumerator SendSaveRequest(string section, string action, Action<PResponse> callback, Hashtable postdata = null)
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		callback(response);
	}
}