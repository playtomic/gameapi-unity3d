using System;
using System.Collections;
using System.Collections.Generic;

public class PNewsletter
{
	private const string SECTION = "newsletter";
	private const string SUBSCRIBE = "subscribe";

	/**
	 * Subscribes a person to your newsletter 
	 * @param	options	Dictionary<string,object>	The email and other information
	 * @param	callback	Action<PResponse>	Your callback function
	 */
	public void Subscribe(PNewsletterOptions options, Action<PResponse> callback)
	{
		Playtomic.API.StartCoroutine(SendRequest(SECTION, SUBSCRIBE, callback, options));
	}
			
	private IEnumerator SendRequest(string section, string action, Action<PResponse> callback, PNewsletterOptions options)
	{ 
		var www = PRequest.Prepare (section, action, options);
		yield return www;
		
		var response = PRequest.Process(www);
		callback(response);
	}
}