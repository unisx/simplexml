/*
* Session.java February 2005
*
* Copyright (C) 2005, Niall Gallagher <niallg@users.sf.net>
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
*     http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
* implied. See the License for the specific language governing 
* permissions and limitations under the License.
*/
using System;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Session</code> object represents a session with name
	/// value pairs. The persister uses this to allow objects to add
	/// or remove name value pairs to an from an internal map. This is
	/// done so that the deserialized objects can set template values
	/// as well as share information. In particular this is useful for
	/// any <code>Strategy</code> implementation as it allows it so
	/// store persistence state during the persistence process.
	/// <p>
	/// Another important reason for the session map is that it is
	/// used to wrap the map that is handed to objects during callback
	/// methods. This opens the possibility for those objects to grab
	/// a reference to the map, which will cause problems for any of
	/// the strategy implementations that wanted to use the session
	/// reference for weakly storing persistence artifacts.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.strategy.Strategy">
	/// </seealso>
	sealed class Session : System.Collections.IDictionary
	{
		/// <summary> This returns the inner map used by the session object. The
		/// internal map is the <code>Map</code> instance that is used
		/// for persister callbacks, a reference to this map can be
		/// safely made by any object receiving a callback.
		/// 
		/// </summary>
		/// <returns> this returns the internal session map used
		/// </returns>
		public System.Collections.IDictionary Map
		{
			get
			{
				return map;
			}
			
		}
		/// <summary> This obviously enough provides the number of pairs that
		/// have been inserted into the internal map. This acts as
		/// a proxy method for the internal map <code>size</code>.
		/// 
		/// </summary>
		/// <returns> this returns the number of pairs are available
		/// </returns>
		public int Count
		{
			get
			{
				return map.Count;
			}
			
		}
		/// <summary> The <code>get</code> method is used to acquire the value for
		/// a named pair. So if a mapping for the specified name exists
		/// within the internal map the mapped entry value is returned.
		/// 
		/// </summary>
		/// <param name="name">this is a name used to search for the value
		/// 
		/// </param>
		/// <returns> this returns the value mapped to the given name     
		/// </returns>
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public System.Object this[System.Object name]
		{
			get
			{
				return map[name];
			}
			
			set
			{
			}
			
		}
		/// <summary> This method is used to acquire the value for all pairs that
		/// have currently been collected by this session. This is used
		/// to determine the values that are available in the session.
		/// 
		/// </summary>
		/// <returns> the list of values for all mappings in the session   
		/// </returns>
		public System.Collections.ICollection Values
		{
			get
			{
				return map.Values;
			}
			
		}
		
		/// <summary> This is the internal map that provides storage for pairs.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Collections.IDictionary map;
		
		/// <summary> Constructor for the <code>Session</code> object. This is 
		/// used to create a new session that makes use of a hash map
		/// to store key value pairs which are maintained throughout
		/// the duration of the persistence process this is used in.
		/// </summary>
		public Session()
		{
			//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
			this.map = new System.Collections.Hashtable();
		}
		
		/// <summary> This method is used to determine whether the session has 
		/// any pairs available. If the <code>size</code> is zero then
		/// the session is empty and this returns true. The is acts as 
		/// a proxy the the <code>isEmpty</code> of the internal map.
		/// 
		/// </summary>
		/// <returns> this is true if there are no available pairs
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Map.isEmpty' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public bool isEmpty()
		{
			return (map.Count == 0);
		}
		
		/// <summary> This is used to determine whether a value representing the
		/// name of a pair has been inserted into the internal map. The
		/// object passed into this method is typically a string which
		/// references a template variable but can be any object.
		/// 
		/// </summary>
		/// <param name="name">this is the name of a pair within the map
		/// 
		/// </param>
		/// <returns> this returns true if the pair of that name exists
		/// </returns>
		public bool Contains(System.Object name)
		{
			return map.Contains(name);
		}
		
		/// <summary> This method is used to determine whether any pair that has
		/// been inserted into the internal map had the presented value.
		/// If one or more pairs within the collected mappings contains
		/// the value provided then this method will return true.
		/// 
		/// </summary>
		/// <param name="value">this is the value that is to be searched for
		/// 
		/// </param>
		/// <returns> this returns true if any value is equal to this
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Map.containsValue' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public bool containsValue(System.Object value_Renamed)
		{
			return SupportClass.MapSupport.ContainsValue(map, value_Renamed);
		}
		
		/// <summary> The <code>put</code> method is used to insert the name and
		/// value provided into the internal session map. The inserted
		/// value will be available to all objects receiving callbacks.
		/// 
		/// </summary>
		/// <param name="name">this is the name the value is mapped under    
		/// </param>
		/// <param name="value">this is the value to mapped with the name
		/// 
		/// </param>
		/// <returns> this returns the previous value if there was any
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Map.put' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Object put(System.Object name, System.Object value_Renamed)
		{
			System.Object tempObject;
			tempObject = map[name];
			map[name] = value_Renamed;
			return tempObject;
		}
		
		/// <summary> The <code>remove</code> method is used to remove the named
		/// mapping from the internal session map. This ensures that
		/// the mapping is no longer available for persister callbacks.
		/// 
		/// </summary>
		/// <param name="name">this is a string used to search for the value
		/// 
		/// </param>
		/// <returns> this returns the value mapped to the given name   
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Map.remove' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Object remove(System.Object name)
		{
			System.Object tempObject;
			tempObject = map[name];
			map.Remove(name);
			return tempObject;
		}
		
		/// <summary> This method is used to insert a collection of mappings into 
		/// the session map. This is used when another source of pairs
		/// is required to populate the collection currently maintained
		/// within this sessions internal map. Any pairs that currently
		/// exist with similar names will be overwritten by this.
		/// 
		/// </summary>
		/// <param name="data">this is the collection of pairs to be added
		/// </param>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Map.putAll' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  putAll(System.Collections.IDictionary data)
		{
			SupportClass.MapSupport.PutAll(map, data);
		}
		
		/// <summary> This is used to acquire the names for all the pairs that 
		/// have currently been collected by this session. This is used
		/// to determine which mappings are available within the map.
		/// 
		/// </summary>
		/// <returns> the set of names for all mappings in the session    
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Map.keySet' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public SupportClass.SetSupport keySet()
		{
			//UPGRADE_TODO: Method 'java.util.Map.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilMapkeySet'"
			return new SupportClass.HashSetSupport(map.Keys);
		}
		
		/// <summary> This method is used to acquire the name and value pairs that
		/// have currently been collected by this session. This is used
		/// to determine which mappings are available within the session.
		/// 
		/// </summary>
		/// <returns> thie set of mappings that exist within the session   
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'java.util.Map.entrySet' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public SupportClass.SetSupport entrySet()
		{
			//UPGRADE_TODO: Method 'java.util.Map.entrySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilMapentrySet'"
			return new SupportClass.HashSetSupport(map);
		}
		
		/// <summary> The <code>clear</code> method is used to wipe out all the
		/// currently existing pairs from the collection. This is used
		/// when all mappings within the session should be erased.
		/// </summary>
		public void  Clear()
		{
			map.Clear();
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public void  Add(System.Object key, System.Object value)
		{
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
		{
			return null;
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public void  Remove(System.Object key)
		{
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
		public void  CopyTo(System.Array array, System.Int32 index)
		{
			System.Object[] keys = new System.Object[this.Count];
			System.Object[] values = new System.Object[this.Count];
			if (this.Keys != null)
				this.Keys.CopyTo(keys, index);
			if (this.Values != null)
				this.Values.CopyTo(values, index);
			for (int i = index; i < this.Count; i++)
				if (keys[i] != null || values[i] != null)
					array.SetValue(new System.Collections.DictionaryEntry(keys[i], values[i]), i);
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return null;
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public System.Collections.ICollection Keys
		{
			get
			{
				return null;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public System.Boolean IsReadOnly
		{
			get
			{
				return false;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public System.Boolean IsFixedSize
		{
			get
			{
				return false;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public System.Object SyncRoot
		{
			get
			{
				return null;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public System.Boolean IsSynchronized
		{
			get
			{
				return false;
			}
			
		}
	}
}