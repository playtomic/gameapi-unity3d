using System;
using System.Collections;
using System.Collections.Generic;

public class PResponse
{
	public bool success;
	public int errorcode;
	public string overridemessage;
	internal Dictionary<string,object> json;
	
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
				
				if(errorcode == 0)
					return "Nothing went wrong!";
				
				switch(errorcode)
				{
				case 1:
					return "General error, this typically means the player is unable to connect to the server";
				case 2:
					return "Invalid game credentials. Make sure you use the right public and private keys";
				case 3:
					return "Request timed out";
				case 4:
					return "Invalid request";

				case 100:
					return "GeoIP API has been disabled for this game";
						
				case 200:
					return "Leaderboard API has been disabled for this game";
				case 201:
					return "The player's name wasn't provided";
				case 203:
					return "Player is banned from submitting scores in this game";
				case 204:
					return "Score was not saved because it was not the player's best, you can allow players to have " +
						"more than one score by specifying allowduplicates=true in your save options";
				case 207:
					return "The leaderboard table wasn't provided";
				
				case 300:
					return "GameVars API has been disabled for this game";
						
				case 400:
					return "Level sharing API has been disabled for this game";
				case 401:
					return "Invalid rating (must be 1 - 10)";
				case 402:
					return "Player has already rated that level";
				case 403:
					return "Missing level name";
				case 404:
					return "Missing level id";
				case 405:
					return "Level already exists";

				case 500:
					return "Achievements API has been disabled for this game";
				case 501:
					return "Missing playerid";
				case 502:
					return "Missing player name";
				case 503:
					return "Missing achievementid";
				case 504:
					return "Invalid achievementid or achievement key";
				case 505:
					return "Player already had the achievement, you can overwrite old achievements with overwrite=true or " +
						"save each time the player is awarded with allowduplicates=true";
				case 506:
					return "Player already had the achievement and it was overwritten or a duplicate was saved successfully";

				case 600:
					return "Newsletter API has been disabled for this game";
				case 601:
					return "MailChimp API Key has not been configured";
				case 602:
					return "MailChimp API returned an error";
				}

				return "Unknown error...";
		}
	}
}