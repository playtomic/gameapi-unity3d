using UnityEngine;
using System;

//using Playtomic;

//namespace PlaytomicTest
//{
internal class PTestGeoIP : PTest
{
	public static void Lookup (Action done)
	{
		const string section = "PTestGeoIP.Lookup";
		Debug.Log (section);
		
		Playtomic.GeoIP.Lookup ((geo, r) => {
			geo = geo ?? new PlayerCountry ();
			AssertTrue (section, "Request succeeded", r.success);
			AssertEquals (section, "No errorcode", r.errorcode, 0);
			AssertFalse (section, "Has country name", string.IsNullOrEmpty (geo.name));
			AssertFalse (section, "Has country code", string.IsNullOrEmpty (geo.code));
			done ();
		});
	}
}
//}
