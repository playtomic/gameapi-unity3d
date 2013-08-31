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
		
		Playtomic.API.StartCoroutine(SendRequest(SECTION, LOAD,
			
			delegate(Dictionary<string,object> results, PResponse response)
			{
				
				Dictionary<string,T> gameVars = new Dictionary<string,T>();
				
				if (response.success)
				{
					foreach(string key in results.Keys)
					{
					
						if (results[key] is Dictionary<string,object>)
						{
						
							gameVars.Add( key , (T) Activator.CreateInstance(typeof(T), new object[] { results[key] }) );
						
						}
				
					}
				
				}
			
				callback(gameVars, response);
				
			}));
		
	}
	
	public void Load<T>(string name, Action<T,PResponse> callback)  where T : GameVar, new()
	{
		
		var postdata = new Dictionary<string,object>
		{
			{"name", name}
		};
		
		Playtomic.API.StartCoroutine(SendRequest(SECTION, LOADSINGLE,
			
			delegate(Dictionary<string,object> result, PResponse response)
			{
			
				T gameVar = null;
			
				if (result != null)
				{
			
					if (result.ContainsKey(name))
					{
					
						gameVar = (T) Activator.CreateInstance(typeof(T), new object[] { result[name] });
					
					}
				
				}
			
				if (gameVar == null)
				{
					gameVar = new T();
					gameVar["name"] = gameVar["value"] = name + " not stored in GameVars";
				}
					
				callback(gameVar, response);
			
			}, postdata));
		
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