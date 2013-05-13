using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

public class PGeoIP
{
	private const string SECTION = "geoip";
	private const string LOOKUP = "lookup";
	
	public void Lookup(Action<PlayerCountry, PResponse> callback)
	{
		Playtomic.API.StartCoroutine(SendRequest(SECTION, LOOKUP, callback));
	}
	
	private IEnumerator SendRequest(string section, string action, Action<PlayerCountry, PResponse> callback)
	{ 
		var www = PRequest.Prepare (section, action);
		yield return www;
		
		var response = PRequest.Process(www);
		var data = response.success ? response.json : null;
		callback(new PlayerCountry(data), response);
	}
}