using UnityEngine;
using System;

public class Playtomic : MonoBehaviour
{
	private PLeaderboards _leaderboards;
	private PPlayerLevels _playerlevels;
	private PGeoIP _geoip;
	private PGameVars _gamevars;
	private PAchievements _achievements;
	private PNewsletter _newsletter;
	
	private static Playtomic _instance = null;
	
	/// <summary>
	/// Initializes the API.  You must do this before anything else.
	/// </summary>
	/// <param name="publickey">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="privatekey">
	/// A <see cref="System.String"/>
	/// </param>
	/// <param name="apiurl">
	/// A <see cref="System.String"/>
	/// </param>
	public static void Initialize(string publickey, string privatekey, string apiurl)
	{
		if(_instance != null)
			return;
			
		var go = new GameObject("playtomic");
		GameObject.DontDestroyOnLoad(go);
			
		_instance = go.AddComponent("Playtomic") as Playtomic;
		_instance._leaderboards = new PLeaderboards();
		_instance._playerlevels = new PPlayerLevels();
		_instance._geoip = new PGeoIP();
		_instance._gamevars = new PGameVars();
		_instance._achievements = new PAchievements();
		_instance._newsletter = new PNewsletter();
		
		PRequest.Initialise(publickey, privatekey, apiurl);
				
	}
		
	internal static Playtomic API
	{
		get { return _instance; }
	}
	
	public static PLeaderboards Leaderboards
	{
		get  { return _instance._leaderboards; }
	}
	
	public static PPlayerLevels PlayerLevels
	{
		get { return _instance._playerlevels; }
	}
	
	public static PGeoIP GeoIP
	{
		get { return _instance._geoip; }
	}
	
	public static PGameVars GameVars
	{
		get { return _instance._gamevars; }
	}
	
	public static PAchievements Achievements
	{
		get { return _instance._achievements; }
	}
	
	public static PNewsletter Newsletter
	{
		get { return _instance._newsletter; }
	}
}