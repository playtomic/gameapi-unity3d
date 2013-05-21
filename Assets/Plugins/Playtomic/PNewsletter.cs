using System;
using System.Collections;

public class PNewsletter
{
	private const string SECTION = "newsletter";
	private const string SUBSCRIBE = "subscribe";

	/**
	 * Subscribes a person to your newsletter 
	 * @param	options	Hashtable	The email and other information
	 * @param	callback	Action<PResponse>	Your callback function
	 */
	public void Subscribe(Hashtable options, Action<PResponse> callback)
	{
		Playtomic.API.StartCoroutine(SendRequest(SECTION, SUBSCRIBE, callback, options));
	}
			
	private IEnumerator SendRequest(string section, string action, Action<PResponse> callback, Hashtable options)
	{ 
		var www = PRequest.Prepare (section, action, options);
		yield return www;
		
		var response = PRequest.Process(www);
		callback(response);
	}
}