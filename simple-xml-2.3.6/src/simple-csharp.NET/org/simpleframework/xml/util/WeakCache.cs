/*
* WeakCache.java July 2006
*
* Copyright (C) 2006, Niall Gallagher <niallg@users.sf.net>
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
namespace org.simpleframework.xml.util
{
	
	/// <summary> The <code>WeakCache</code> object is an implementation of a cache
	/// that holds on to cached items only if the key remains in memory.
	/// This is effectively like a concurrent hash map with weak keys, it
	/// ensures that multiple threads can concurrently access weak hash
	/// maps in a way that lowers contention for the locks used. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public class WeakCache
	{
		/// <summary> This is used to store a list of segments for the cache.</summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< K, V > implements Cache < K, V >
		private SegmentList list;
		
		/// <summary> Constructor for the <code>WeakCache</code> object. This is
		/// used to create a cache that stores values in such a way that
		/// when the key is garbage collected the value is removed from
		/// the map. This is similar to the concurrent hash map.
		/// </summary>
		public WeakCache():this(10)
		{
		}
		
		/// <summary> Constructor for the <code>WeakCache</code> object. This is
		/// used to create a cache that stores values in such a way that
		/// when the key is garbage collected the value is removed from
		/// the map. This is similar to the concurrent hash map.
		/// 
		/// </summary>
		/// <param name="size">this is the number of segments within the cache
		/// </param>
		public WeakCache(int size)
		{
			InitBlock();
			this.list = new SegmentList(this, size);
		}
		
		/// <summary> This method is used to insert a key value mapping in to the
		/// cache. The value can later be retrieved or removed from the
		/// cache if desired. If the value associated with the key is 
		/// null then nothing is stored within the cache.
		/// 
		/// </summary>
		/// <param name="key">this is the key to cache the provided value to
		/// </param>
		/// <param name="value">this is the value that is to be cached
		/// </param>
		public virtual void  cache(K key, V value_Renamed)
		{
			map(key).cache(key, value_Renamed);
		}
		
		/// <summary> This is used to exclusively take the value mapped to the 
		/// specified key from the cache. Invoking this is effectively
		/// removing the value from the cache.
		/// 
		/// </summary>
		/// <param name="key">this is the key to acquire the cache value with
		/// 
		/// </param>
		/// <returns> this returns the value mapped to the specified key 
		/// </returns>
		public virtual V take(K key)
		{
			return map(key).take(key);
		}
		
		/// <summary> This method is used to get the value from the cache that is
		/// mapped to the specified key. If there is no value mapped to
		/// the specified key then this method will return a null.
		/// 
		/// </summary>
		/// <param name="key">this is the key to acquire the cache value with
		/// 
		/// </param>
		/// <returns> this returns the value mapped to the specified key 
		/// </returns>
		public virtual V fetch(K key)
		{
			return map(key).fetch(key);
		}
		
		/// <summary> This is used to determine whether the specified key exists
		/// with in the cache. Typically this can be done using the 
		/// fetch method, which will acquire the object. 
		/// 
		/// </summary>
		/// <param name="key">this is the key to check within this segment
		/// 
		/// </param>
		/// <returns> true if the specified key is within the cache
		/// </returns>
		public virtual bool contains(K key)
		{
			return map(key).contains(key);
		}
		
		/// <summary> This method is used to acquire a <code>Segment</code> using
		/// the keys has code. This method effectively uses the hash to
		/// find a specific segment within the fixed list of segments.
		/// 
		/// </summary>
		/// <param name="key">this is the key used to acquire the segment
		/// 
		/// </param>
		/// <returns> this returns the segment used to get acquire value
		/// </returns>
		private Segment map(K key)
		{
			return list.get_Renamed(key);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SegmentList' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> This is used to maintain a list of segments. All segments that
		/// are stored by this object can be acquired using a given key.
		/// The keys hash is used to select the segment, this ensures that
		/// all read and write operations with the same key result in the
		/// same segment object within this list.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class SegmentList
		{
			private void  InitBlock(WeakCache enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private WeakCache enclosingInstance;
			public WeakCache Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			/// <summary> The list of segment objects maintained by this object.</summary>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < Segment > list;
			
			/// <summary> Represents the number of segments this object maintains.</summary>
			private int size;
			
			/// <summary> Constructor for the <code>SegmentList</code> object. This
			/// is used to create a list of weak hash maps that can be
			/// acquired using the hash code of a given key. 
			/// 
			/// </summary>
			/// <param name="size">this is the number of hash maps to maintain
			/// </param>
			public SegmentList(WeakCache enclosingInstance, int size)
			{
				InitBlock(enclosingInstance);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				this.list = new ArrayList < Segment >();
				this.size = size;
				this.create(size);
			}
			
			/// <summary> This is used to acquire the segment using the given key.
			/// The keys hash is used to determine the index within the 
			/// list to acquire the segment, which is a synchronized weak        
			/// hash map storing the key value pairs for a given hash.
			/// 
			/// </summary>
			/// <param name="key">this is the key used to determine the segment
			/// 
			/// </param>
			/// <returns> the segment that is stored at the resolved hash 
			/// </returns>
			public virtual Segment get_Renamed(K key)
			{
				int segment = segment(key);
				
				if (segment < size)
				{
					return Enclosing_Instance.list.get_Renamed(segment);
				}
				return null;
			}
			
			/// <summary> Upon initialization the segment list is populated in such
			/// a way that synchronization is not needed. Each segment is
			/// created and stored in an increasing index within the list.
			/// 
			/// </summary>
			/// <param name="size">this is the number of segments to be used
			/// </param>
			private void  create(int size)
			{
				int count = size;
				
				while (count-- > 0)
				{
					Enclosing_Instance.list.add(new Segment(enclosingInstance));
				}
			}
			
			/// <summary> This method performs the translation of the key hash code
			/// to the segment index within the list. Translation is done
			/// by acquiring the modulus of the hash and the list size.
			/// 
			/// </summary>
			/// <param name="key">this is the key used to resolve the index
			/// 
			/// </param>
			/// <returns> the index of the segment within the list 
			/// </returns>
			private int segment(K key)
			{
				return Math.abs(key.hashCode() % size);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Segment' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> The segment is effectively a synchronized weak hash map. If is
		/// used to store the key value pairs in such a way that they are
		/// kept only as long as the garbage collector does not collect 
		/// the key. This ensures the cache does not cause memory issues. 
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		//UPGRADE_TODO: Class 'java.util.WeakHashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilWeakHashMap'"
		public class Segment:System.Collections.Hashtable
		{
			public Segment(WeakCache enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			/// <summary> This method is used to insert a key value mapping in to the
			/// cache. The value can later be retrieved or removed from the
			/// cache if desired. If the value associated with the key is 
			/// null then nothing is stored within the cache.
			/// 
			/// </summary>
			/// <param name="key">this is the key to cache the provided value to
			/// </param>
			/// <param name="value">this is the value that is to be cached
			/// </param>
			private void  InitBlock(WeakCache enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				
			}
			private WeakCache enclosingInstance;
			public WeakCache Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< K, V >
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'cache'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual void  cache(K key, V value_Renamed)
			{
				lock (this)
				{
					System.Object tempObject;
					tempObject = this[key];
					this[key] = value_Renamed;
					System.Object generatedAux = tempObject;
				}
			}
			
			/// <summary> This method is used to get the value from the cache that is
			/// mapped to the specified key. If there is no value mapped to
			/// the specified key then this method will return a null.
			/// 
			/// </summary>
			/// <param name="key">this is the key to acquire the cache value with
			/// 
			/// </param>
			/// <returns> this returns the value mapped to the specified key 
			/// </returns>
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'fetch'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual V fetch(K key)
			{
				lock (this)
				{
					return this[key];
				}
			}
			
			/// <summary> This is used to exclusively take the value mapped to the 
			/// specified key from the cache. Invoking this is effectively
			/// removing the value from the cache.
			/// 
			/// </summary>
			/// <param name="key">this is the key to acquire the cache value with
			/// 
			/// </param>
			/// <returns> this returns the value mapped to the specified key 
			/// </returns>
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'take'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual V take(K key)
			{
				lock (this)
				{
					System.Object tempObject;
					tempObject = this[key];
					Remove(key);
					return tempObject;
				}
			}
			
			/// <summary> This is used to determine whether the specified key exists
			/// with in the cache. Typically this can be done using the 
			/// fetch method, which will acquire the object. 
			/// 
			/// </summary>
			/// <param name="key">this is the key to check within this segment
			/// 
			/// </param>
			/// <returns> true if the specified key is within the cache
			/// </returns>
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'contains'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual bool contains(K key)
			{
				lock (this)
				{
					return ContainsKey(key);
				}
			}
		}
	}
}