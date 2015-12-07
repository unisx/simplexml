/*
* Dictionary.java July 2006
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
	
	/// <summary> The <code>Dictionary</code> object represents a mapped set of entry
	/// objects that can be serialized and deserialized. This is used when
	/// there is a need to load a list of objects that can be mapped using 
	/// a name attribute. Using this object avoids the need to implement a
	/// commonly required pattern of building a map of XML element objects.
	/// <pre>
	/// 
	/// &lt;dictionary&gt;
	/// &lt;entry name="example"&gt;
	/// &lt;element&gt;example text&lt;/element&gt;
	/// &lt;/entry&gt;
	/// &lt;entry name="example"&gt;
	/// &lt;element&gt;example text&lt;/element&gt;
	/// &lt;/entry&gt;       
	/// &lt;/dictionary&gt;
	/// 
	/// </pre>
	/// This can contain implementations of the <code>Entry</code> object 
	/// which contains a required "name" attribute. Implementations of the
	/// entry object can add further XML attributes an elements. This must
	/// be annotated with the <code>ElementList</code> annotation in order
	/// to be serialized and deserialized as an object field.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.util.Entry">
	/// </seealso>
	public class Dictionary
	{
		/// <summary> Used to map the entries to their configured names.</summary>
		private void  InitBlock()
		{
			
			return map.Values.GetEnumerator();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< E extends Entry > extends AbstractSet < E >
		protected internal Table map;
		
		/// <summary> Constructor for the <code>Dictionary</code> object. This 
		/// is used to create a set that contains entry objects mapped 
		/// to an XML attribute name value. Entry objects added to this
		/// dictionary can be retrieved using its name value.
		/// </summary>
		public Dictionary()
		{
			InitBlock();
			this.map = new Table(this);
		}
		
		/// <summary> This method is used to add the provided entry to this set. If
		/// an entry of the same name already existed within the set then
		/// it is replaced with the specified <code>Entry</code> object.
		/// 
		/// </summary>
		/// <param name="item">this is the entry object that is to be inserted
		/// </param>
		public virtual bool add(E item)
		{
			System.Object tempObject;
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			tempObject = map[item.getName()];
			map[item.getName()] = item;
			return tempObject != null;
		}
		
		/// <summary> This returns the number of <code>Entry</code> objects within
		/// the dictionary. This will use the internal map to acquire the
		/// number of entry objects that have been inserted to the map.
		/// 
		/// </summary>
		/// <returns> this returns the number of entry objects in the set
		/// </returns>
		public virtual int size()
		{
			return map.Count;
		}
		
		/// <summary> Returns an iterator of <code>Entry</code> objects which can be
		/// used to remove items from this set. This will use the internal
		/// map object and return the iterator for the map values.
		/// 
		/// </summary>
		/// <returns> this returns an iterator for the entry objects
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < E > iterator()
		
		/// <summary> This is used to acquire an <code>Entry</code> from the set by
		/// its name. This uses the internal map to look for the entry, if
		/// the entry exists it is returned, if not this returns null.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the entry object to retrieve
		/// 
		/// </param>
		/// <returns> this returns the entry mapped to the specified name
		/// </returns>
		public virtual E get_Renamed(System.String name)
		{
			//UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapget_javalangObject'"
			return map[name];
		}
		
		/// <summary> This is used to remove an <code>Entry</code> from the set by
		/// its name. This uses the internal map to look for the entry, if
		/// the entry exists it is returned and removed from the map.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the entry object to remove
		/// 
		/// </param>
		/// <returns> this returns the entry mapped to the specified name
		/// </returns>
		public virtual E remove(System.String name)
		{
			System.Object tempObject;
			tempObject = map[name];
			map.Remove(name);
			return tempObject;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Table' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> The <code>Table</code> object is used to represent a map of
		/// entries mapped to a string name. Each implementation of the
		/// entry must contain a name attribute, which is used to insert
		/// the entry into the map. This acts as a typedef.
		/// 
		/// </summary>
		/// <seealso cref="org.simpleframework.xml.util.Entry">
		/// </seealso>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		[Serializable]
		protected internal class Table:System.Collections.Hashtable
		{
			/// <summary> Constructor for the <code>Table</code> object. This will
			/// create a map that is used to store the entry objects that
			/// are serialized and deserialized to and from an XML source.
			/// </summary>
			private void  InitBlock(Dictionary enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				
			}
			private Dictionary enclosingInstance;
			public Dictionary Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< String, E >
			//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
			public Table(Dictionary enclosingInstance):base()
			{
				InitBlock(enclosingInstance);
			}
		}
	}
}