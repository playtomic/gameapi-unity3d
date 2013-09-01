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
	public void List(PAchievementOptions options, Action<List<PlayerAchievement>, PResponse> callback) 
	{
		
		List<PlayerAchievement>(options,callback);
	}
	
	public void List<T>(PAchievementOptions options, Action<List<T>, PResponse> callback) where T : PlayerAchievement, new()
	{
		
		Playtomic.API.StartCoroutine(SendListRequest(SECTION, LIST, callback, options));
	}
	
	internal IEnumerator SendListRequest<T>(string section, string action, Action<List<T>, PResponse> callback, Dictionary<string,object> postdata = null) where T : PlayerAchievement
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		yield return www;
		
		var response = PRequest.Process(www);
		var data = response.success ? response.json : null;
		
		List<T> achievements = new List<T>();
		
		if (response.success) 
		{

			foreach(IDictionary achievment in (IList) data["achievements"])
			{
				achievements.Add((T) Activator.CreateInstance(typeof(T), new object[] { achievment }));
			}
			
		}
		
		callback(achievements, response);
	}

	/**
	 * Shows a chronological stream of achievements 
	 * @param	options		The stream options
	 * @param	callback	Your callback Action<List<Achievement>, int, PResponse>
	 */ 
	public void Stream(PAchievementStreamOptions options, Action<List<PlayerAward>, int, PResponse> callback) {
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
			var acharray = (List<object>) data["achievements"];
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