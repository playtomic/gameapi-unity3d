using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

internal class PTest : MonoBehaviour {

	protected static List<string> successes;
	protected static List<string> failures;
	protected static List<string> results;
	
	public static void Setup() {
		successes = new List<string>();
		failures = new List<string>();
		results = new List<string>();
	}
	
	public static bool AssertEquals(string section, string name, bool expected, bool received) {
		if (expected == received) {
			Record(true, section, name, expected, received);
			return true;
		}
		
		Record(false, section, name, expected, received);
		return false;
	}
	
	public static bool AssertEquals(string section, string name, int expected, int received) {
		if (expected == received) {
			Record(true, section, name, expected, received);
			return true;
		}
		
		Record(false, section, name, expected, received);
		return false;
	}
	
	public static bool AssertEquals(string section, string name, string expected, string received) {
		if (expected == received) {
			Record(true, section, name, expected, received);
			return true;
		}
		
		Record(false, section, name, expected, received);
		return false;
	}
	
	public static bool AssertTrue(string section, string name, bool value) {
		return AssertEquals(section, name, value, true);
	}
	
	public static bool AssertFalse(string section, string name, bool value) {
		return AssertEquals(section, name, value, false);
	}
	
	private static void Record(bool success, string section, string message, object expected, object received) {
		
		var m = "[" + section + "] " + message;
		
		if (success) {
			successes.Add (m);
		} else {
			m += " (" + expected + " vs " + received + ")";
			failures.Add (m);
		}
		
		results.Add (m);
	}
	
	public static void Render() {
		
		if(failures.Count > 0) {
			Debug.LogError ("[Playtomic.PTest] --------------------------------      errors      --------------------------------");
			
			foreach(var failure in failures) {
				Debug.LogError ("[Playtomic.PTest] " + failure);
			}
		}
		
		if(failures.Count == 0) {
			Debug.Log ("[Playtomic.PTest] " + successes.Count + " tests passed out of " + results.Count + " total");
		}
	}
}
