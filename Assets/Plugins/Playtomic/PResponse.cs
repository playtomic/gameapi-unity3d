using System;
using System.Collections;
using System.Collections.Generic;

public class PResponse
{
	public bool success;
	public int errorcode;
	public string overridemessage;
	internal Hashtable json;
	
	public static PResponse GeneralError(string message)
	{
		return new PResponse { 
					success = false,
					errorcode = 1,
					overridemessage = message
				};
	}
	
	public static PResponse GeneralError(int nodataerror)
	{
		return new PResponse { 
					success = false,
					errorcode = -1
				};
	}
	
	public static PResponse Error(int errorcode)
	{
		return new PResponse { 
					success = false,
					errorcode = errorcode
				};
	}
	
	public string errormessage
	{
		get
		{
			if(!string.IsNullOrEmpty(overridemessage))
				return overridemessage;
			
			if(success || errorcode == 0)
				return "Nothing went wrong!";
			
			switch(errorcode)
			{
				case -1:
					return "No data was returned from the server.  We might be under heavy load or have broken stuff";
					
				case 1:
					return "General error, usually connectivity or a glitch on the servers";
				
				case 2:
					return "Invalid game credentials - make sure you get your details on the API settings page in the dashboard";
					
				case 3:
					return "Request timed out.";
					
				case 4:
					return "Invalid request.";

				case 100:
					return "GeoIP API has been disabled for your game.  This may happen in the first couple minutes till the tracker servers get updated that your game exists, or in very bad cases if your game is placing undue stress on our servers";
					
				case 200:
					return "Leaderboard API has been disabled for your game.  This may happen in the first couple minutes till the tracker servers get updated that your game exists, or in very bad cases if your game is placing undue stress on our servers";
					
				case 201:
					return "The source url or name weren't provided when saving the score.  Make sure the game is initialized, and the PPlayerScore has a Name";
					
				case 202:
					return "Invalid auth key.  This usually will only happen if someone messes with the URL the scores save to";
					
				case 203:
					return "No Facebook user id, on a Facebook score submission";
				
				case 204:
					return "Table name wasn't specified for creating a private leaderboard.";
	
				case 205:
					return "Permalink structure wasn't specified: http://website.com/game/whatever?leaderboard=";
					
				case 206:
					return "Leaderboard id wasn't provided loading a private leaderboard.";
					
				case 207:
					return "Invalid leaderboard id was provided for a private leaderboard.";
				
				case 208:
					return "Player is banned from submitting scores in your game.";
				
				case 209:
					return "Score was not the player's best score.  You can notify the player, or circumvent this by pecifying 'allowduplicates' to be true in your save options.";

				case 300:
					return "GameVars API has been disabled for your game.  This may happen in the first couple minutes till the tracker servers get updated that your game exists, or in very bad cases if your game is placing undue stress on our servers";
					
				case 400:
					return "Level sharing API has been disabled for your game.    This may happen in the first couple minutes till the tracker servers get updated that your game exists, or in very bad cases if your game is placing undue stress on our servers";
					
				case 401:
					return "Invalid rating.  Ratings must be integers between 1 and 10";
					
				case 402:
					return "Player already rated that level";
					
				case 403:
					return "The level name was not provided when saving a level";
				
				case 404:
					return "The level id was not provided";
					
				case 405:
					return "That level already exists.";
				
				default:
					return "Unknown error!";
			}
		}
	}
}