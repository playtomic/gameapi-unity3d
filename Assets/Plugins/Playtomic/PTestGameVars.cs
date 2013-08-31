using UnityEngine;
using System;
using System.Collections.Generic;

//namespace PlaytomicTest
//{
internal class PTestGameVars : PTest
{

	public static void All (Action done)
	{
		const string section = "PTestGameVars.All";
		Debug.Log (section);
		
		Playtomic.GameVars.Load ((gv, r) => {
			gv = gv ?? new Dictionary<string,GameVar> ();
			AssertTrue (section, "Request succeeded", r.success);
			AssertEquals (section, "No errorcode", r.errorcode, 0);
			AssertTrue (section, "Has known testvar1", gv.ContainsKey ("testvar1"));
			AssertTrue (section, "Has known testvar2", gv.ContainsKey ("testvar2"));
			AssertTrue (section, "Has known testvar3", gv.ContainsKey ("testvar3"));
			AssertEquals (section, "Has known testvar1 value", gv["testvar1"].value, "testvalue1");
			AssertEquals (section, "Has known testvar2 value", gv["testvar2"].value, "testvalue2");
			AssertEquals (section, "Has known testvar3 value", gv["testvar3"].value, "testvalue3 and the final gamevar");
			done ();
		});
	}

	public static void Single (Action done)
	{
		const string section = "PTestGameVars.LoadSingle";
		Debug.Log (section);
		
		Playtomic.GameVars.Load ("testvar1", (GameVar gv, PResponse r) => {
			gv = gv ?? new GameVar();
			AssertTrue (section, "Request succeeded", r.success);
			AssertEquals (section, "No errorcode", r.errorcode, 0);
			AssertTrue (section, "Has testvar1", gv.ContainsKey ("testvar1"));
			AssertEquals (section, "Has known testvar1 value", (string)gv ["testvar1"], "testvalue1");
			AssertFalse (section, "Does not have testvar2", gv.ContainsKey ("testvar2"));
			AssertFalse (section, "Does not have testvar3", gv.ContainsKey ("testvar3"));
			done ();
		});
	}
}
//}