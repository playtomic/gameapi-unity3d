using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PDictionary : Dictionary<string,object> {

	public PDictionary ()
	{
	}

	public PDictionary(IDictionary data)
	{
		if (data != null)
		{
			foreach(string x in data.Keys)
			{			
				this[x] = data[x];
			}
		}
	}
	
	protected long GetLong(string s) 
	{
		long r = 0L;
		
		if (ContainsKey(s))
		{
			long.TryParse(this[s].ToString(), out r);
		}
			
		return r;
	}
	
	protected float GetFloat(string s) 
	{
		float r = 0f;
		
		if (ContainsKey(s))
		{
			float.TryParse(this[s].ToString(), out r);
		}
			
		return r;
	}
	
	protected bool GetBool(string s) 
	{
		bool r = false;
		
		if (ContainsKey(s))
		{
			bool.TryParse(this[s].ToString(), out r);
		}
			
		return r;
	}
	
	protected int GetInt(string s) 
	{
		int r = 0;
		
		if (ContainsKey(s))
		{
			int.TryParse(this[s].ToString(), out r);
		}
			
		return r;
	}

	protected string GetString(string s) 
	{	
		return ContainsKey(s) ? this[s].ToString() : string.Empty;
	}
	
	protected PDictionary GetDictionary(string s) 
	{	
		PDictionary dictionary = new PDictionary();
		
		if (ContainsKey(s))
		{
			
			if (this[s] is IDictionary)
			{
				dictionary = (PDictionary) this[s];
			}
		}
		
		return dictionary;
	}
	
	protected List<T> GetList<T>(string s) 
	{	
		List<T> r = null;

		if (ContainsKey(s))
		{
			if (this[s] is IList)
			{
				
				r = new List<T>();
									
				foreach(var v in (IList) this[s])
				{
					if (v is T)
					{
						r.Add((T) v);
					}
				}
				
			}
			
		}
		
		return r;
	}

	protected void SetProperty(string key, object value) 
	{
		if (ContainsKey(key))
		{
			this[key] = value;
		} 
		else 
		{
			Add(key, value);
		}
	}
	
	public override string ToString ()
	{
		return ContainsKey("name") ? this["name"].ToString() : "No name field for type: " + this.GetType().ToString();
	}
	
}
