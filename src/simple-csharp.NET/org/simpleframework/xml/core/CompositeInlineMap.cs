/*
* CompositeInlineMap.java July 2007
*
* Copyright (C) 2007, Niall Gallagher <niallg@users.sf.net>
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
using Type = org.simpleframework.xml.strategy.Type;
using InputNode = org.simpleframework.xml.stream.InputNode;
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.Mode' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Mode = org.simpleframework.xml.stream.Mode;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
using Style = org.simpleframework.xml.stream.Style;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>CompositeMap</code> is used to serialize and deserialize
	/// maps to and from a source XML document. The structure of the map in
	/// the XML format is determined by the annotation. Keys can be either
	/// attributes or elements, and values can be inline. This can perform
	/// serialization and deserialization of the key and value objects 
	/// whether the object types are primitive or composite.
	/// <pre>
	/// 
	/// &lt;map&gt;
	/// &lt;entry key='1'&gt;           
	/// &lt;value&gt;one&lt;/value&gt;
	/// &lt;/entry&gt;
	/// &lt;entry key='2'&gt;
	/// &lt;value&gt;two&lt;/value&gt;
	/// &lt;/entry&gt;      
	/// &lt;/map&gt;
	/// 
	/// </pre>
	/// For the above XML element map the element <code>entry</code> is 
	/// used to wrap the key and value such that they can be grouped. This
	/// element does not represent any real object. The names of each of
	/// the XML elements serialized and deserialized can be configured.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Entry">
	/// </seealso>
	class CompositeInlineMap : Repeater
	{
		
		/// <summary> The factory used to create suitable map object instances.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private MapFactory factory;
		
		/// <summary> This is the type that the value objects are instances of. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Converter value_Renamed;
		
		/// <summary> This is the name of the entry wrapping the key and value.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'key '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Converter key;
		
		/// <summary> This is the style used to style the names used for the XML.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Style style;
		
		/// <summary> The entry object contains the details on how to write the map.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Entry entry;
		
		/// <summary> Constructor for the <code>CompositeMap</code> object. This will
		/// create a converter that is capable of writing map objects to 
		/// and from XML. The resulting XML is configured by an annotation
		/// such that key values can attributes and values can be inline. 
		/// 
		/// </summary>
		/// <param name="context">this is the root context for the serialization
		/// </param>
		/// <param name="entry">this provides configuration for the resulting XML
		/// </param>
		/// <param name="type">this is the map type that is to be converted
		/// </param>
		public CompositeInlineMap(Context context, Entry entry, Type type)
		{
			this.factory = new MapFactory(context, type);
			this.value_Renamed = entry.getValue(context);
			this.key = entry.getKey(context);
			this.style = context.Style;
			this.entry = entry;
		}
		
		/// <summary> This <code>read</code> method will read the XML element map from
		/// the provided node and deserialize its children as entry types.
		/// Each entry type must contain a key and value so that the entry 
		/// can be inserted in to the map as a pair. If either the key or 
		/// value is composite it is read as a root object, which means its
		/// <code>Root</code> annotation must be present and the name of the
		/// object element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		public virtual System.Object read(InputNode node)
		{
			System.Object value_Renamed = factory.getInstance();
			System.Collections.IDictionary table = (System.Collections.IDictionary) value_Renamed;
			
			if (table != null)
			{
				return read(node, table);
			}
			return null;
		}
		
		/// <summary> This <code>read</code> method will read the XML element map from
		/// the provided node and deserialize its children as entry types.
		/// Each entry type must contain a key and value so that the entry 
		/// can be inserted in to the map as a pair. If either the key or 
		/// value is composite it is read as a root object, which means its
		/// <code>Root</code> annotation must be present and the name of the
		/// object element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		public virtual System.Object read(InputNode node, System.Object value_Renamed)
		{
			System.Collections.IDictionary map = (System.Collections.IDictionary) value_Renamed;
			
			if (map != null)
			{
				return read(node, map);
			}
			return read(node);
		}
		
		/// <summary> This <code>read</code> method will read the XML element map from
		/// the provided node and deserialize its children as entry types.
		/// Each entry type must contain a key and value so that the entry 
		/// can be inserted in to the map as a pair. If either the key or 
		/// value is composite it is read as a root object, which means its
		/// <code>Root</code> annotation must be present and the name of the
		/// object element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// </param>
		/// <param name="map">this is the map object that is to be populated
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		private System.Object read(InputNode node, System.Collections.IDictionary map)
		{
			InputNode from = node.getParent();
			System.String name = node.Name;
			
			while (node != null)
			{
				System.Object index = key.read(node);
				System.Object item = value_Renamed.read(node);
				
				if (map != null)
				{
					map[index] = item;
				}
				node = from.getNext(name);
			}
			return map;
		}
		
		/// <summary> This <code>read</code> method will read the XML element map from
		/// the provided node and deserialize its children as entry types.
		/// Each entry type must contain a key and value so that the entry 
		/// can be inserted in to the map as a pair. If either the key or 
		/// value is composite it is read as a root object, which means its
		/// <code>Root</code> annotation must be present and the name of the
		/// object element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		public virtual bool validate(InputNode node)
		{
			InputNode from = node.getParent();
			System.String name = node.Name;
			
			while (node != null)
			{
				if (!key.validate(node))
				{
					return false;
				}
				if (!value_Renamed.validate(node))
				{
					return false;
				}
				node = from.getNext(name);
			}
			return true;
		}
		
		/// <summary> This <code>write</code> method will write the key value pairs
		/// within the provided map to the specified XML node. This will 
		/// write each entry type must contain a key and value so that
		/// the entry can be deserialized in to the map as a pair. If the
		/// key or value object is composite it is read as a root object 
		/// so its <code>Root</code> annotation must be present.
		/// 
		/// </summary>
		/// <param name="node">this is the node the map is to be written to
		/// </param>
		/// <param name="source">this is the source map that is to be written 
		/// </param>
		public virtual void  write(OutputNode node, System.Object source)
		{
			OutputNode parent = node.getParent();
			Mode mode = node.getMode();
			System.Collections.IDictionary map = (System.Collections.IDictionary) source;
			
			if (!node.isCommitted())
			{
				node.remove();
			}
			write(parent, map, mode);
		}
		
		/// <summary> This <code>write</code> method will write the key value pairs
		/// within the provided map to the specified XML node. This will 
		/// write each entry type must contain a key and value so that
		/// the entry can be deserialized in to the map as a pair. If the
		/// key or value object is composite it is read as a root object 
		/// so its <code>Root</code> annotation must be present.
		/// 
		/// </summary>
		/// <param name="node">this is the node the map is to be written to
		/// </param>
		/// <param name="map">this is the source map that is to be written 
		/// </param>
		/// <param name="mode">this is the mode that has been inherited
		/// </param>
		private void  write(OutputNode node, System.Collections.IDictionary map, Mode mode)
		{
			System.String root = entry.getEntry();
			System.String name = style.getElement(root);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Object index: map.keySet())
			{
				OutputNode next = node.getChild(name);
				System.Object item = map[index];
				
				next.setMode(mode);
				key.write(next, index);
				value_Renamed.write(next, item);
			}
		}
	}
}