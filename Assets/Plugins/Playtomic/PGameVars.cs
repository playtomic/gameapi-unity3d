using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PGameVars
{
	
	private const string SECTION = "gamevars";
	private const string LOAD = "load";
	private const string LOADSINGLE = "single";

	public void Load(Action<Dictionary<string,GameVar>, PResponse> callback)
	{
		Load<GameVar>(callback);
	}
	
	public void Load(string name, Action<GameVar,PResponse> callback)
	{
		Load<GameVar>(name, callback);
	}
	
	public void Load<T>(Action<Dictionary<string,T>, PResponse> callback) where T : GameVar, new()
	{
		
		Playtomic.API.StartCoroutine(SendRequest<T>(SECTION, LOAD,callback));
		
	}
	
	public void Load<T>(string name, Action<T,PResponse> callback)  where T : GameVar, new()
	{
		
		var postdata = new Dictionary<string,object>
		{
			{"name", name}
		};
		
		Playtomic.API.StartCoroutine(SendRequest<T>(name, SECTION, LOADSINGLE, callback, postdata));
		
	}
	
	internal IEnumerator SendRequest<T>(string section, string action, Action<Dictionary<string,T>, PResponse> callback) where T : GameVar, new()
	{ 
		var www = PRequest.Prepare (section, action, null);
		
		yield return www;
		
		var response = PRequest.Process(www);

		var data = response.success ? response.json : null;
	
		Dictionary<string,T> gameVars = new Dictionary<string, T>();
		
		if (data != null)
		{
			
			if (data is IDictionary)
			{
				
				foreach(string key in data.Keys)
				{
					
					if (data[key] is IDictionary)
					{
						gameVars.Add(key, (T) Activator.CreateInstance(typeof(T), new object[] { data[key] }) );		
					}
					
				}
				
			}
		}
			
		callback(gameVars, response);
	
	}
	
	internal IEnumerator SendRequest<T>(string name, string section, string action, Action<T, PResponse> callback, Dictionary<string,object> postdata = null) where T : GameVar, new()
	{ 
		var www = PRequest.Prepare (section, action, postdata);
		
		yield return www;
		
		var response = PRequest.Process(www);

		var data = response.success ? response.json : null;
	
		T gameVar = new T();

		if (data != null)
		{
		
			if (data is IDictionary)
			{
			
				if (data.ContainsKey(name))
				{
					
					gameVar = (T) Activator.CreateInstance(typeof(T), new object[] { data[name] });
					
				}
			
			}
			
		}
			
		callback(gameVar, response);
	
	}
}