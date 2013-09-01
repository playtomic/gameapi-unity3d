using UnityEngine;
using System;
using System.Collections.Generic;

internal class PTestNewsletter : PTest 
{
	public static void Subscribe(Action done) 
	{
		const string section = "PTestNewsletter.Subscribe";
		Debug.Log (section);

		PNewsletterOptions options = new PNewsletterOptions
		{
		
			email = "invalid @email.com"
			
		};

		Playtomic.Newsletter.Subscribe (options, r => {
			AssertFalse(section + "#1", "Request failed", r.success);
			AssertEquals(section + "#1", "Mailchimp API error", r.errorcode, 602);

			options["email"] = "valid@testuri.com";
			options["fields"] = new Dictionary<string,object> { 
				{"STRINGVALUE", "this is a string"}
			};

			Playtomic.Newsletter.Subscribe (options, r2 => {
				AssertTrue(section, "Request succeeded", r2.success);
				AssertEquals(section, "No errorcode", r2.errorcode, 0);
				done();
			});
		});
	}
}