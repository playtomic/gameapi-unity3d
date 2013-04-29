using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

internal class PTestGeoIP : PTest {

	public static void Lookup(Action next) {
		Playtomic.GeoIP.Lookup (LookupComplete, next);
	}
	
	private static void LookupComplete(PlayerCountry geo, PResponse r, Action done)
	{
		geo = geo ?? new PlayerCountry();
		
		var section = "PTestGeoIP.Lookup";
		AssertTrue(section, "Request succeeded", r.success);
		AssertEquals(section, "No errorcode", r.errorcode, 0);
		AssertFalse(section, "Has country name", string.IsNullOrEmpty (geo.name));
		AssertFalse(section, "Has country code", string.IsNullOrEmpty (geo.code));
		done();
	}
}
