using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class PlaytomicLegacyUtil {
	
#if !UNITY_WP8 && !NETFX_CORE
	
	public static Dictionary<K,V> HashtableToDictionary<K,V> (Hashtable table)
	{
	   return table.Cast<DictionaryEntry> ().ToDictionary (kvp => (K)kvp.Key, kvp => (V)kvp.Value);
	}
	
#endif
		
}
