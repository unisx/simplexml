/*
* CompositeMap.java July 2007
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
	class CompositeMap : Converter
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
		public CompositeMap(Context context, Entry entry, Type type)
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
			Instance type = factory.getInstance(node);
			System.Object map = type.getInstance();
			
			if (!type.Reference)
			{
				return populate(node, map);
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
		/// </param>
		/// <param name="result">this is the map object that is to be populated
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		public virtual System.Object read(InputNode node, System.Object result)
		{
			Instance type = factory.getInstance(node);
			
			if (type.Reference)
			{
				return type.getInstance();
			}
			type.setInstance(result);
			
			if (result != null)
			{
				return populate(node, result);
			}
			return result;
		}
		
		/// <summary> This <code>populate</code> method will read the XML element map 
		/// from the provided node and deserialize its children as entry types.
		/// Each entry type must contain a key and value so that the entry 
		/// can be inserted in to the map as a pair. If either the key or 
		/// value is composite it is read as a root object, which means its
		/// <code>Root</code> annotation must be present and the name of the
		/// object element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// </param>
		/// <param name="result">this is the map object that is to be populated
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		private System.Object populate(InputNode node, System.Object result)
		{
			System.Collections.IDictionary map = (System.Collections.IDictionary) result;
			
			while (true)
			{
				InputNode next = node.getNext();
				
				if (next == null)
				{
					return map;
				}
				System.Object index = key.read(next);
				System.Object item = value_Renamed.read(next);
				
				map[index] = item;
			}
		}
		
		/// <summary> This <code>validate</code> method will validate the XML element 
		/// map from the provided node and validate its children as entry 
		/// types. Each entry type must contain a key and value so that the 
		/// entry can be inserted in to the map as a pair. If either the key 
		/// or value is composite it is read as a root object, which means its
		/// <code>Root</code> annotation must be present and the name of the
		/// object element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be validate
		/// 
		/// </param>
		/// <returns> true if the element matches the XML schema class given 
		/// </returns>
		public virtual bool validate(InputNode node)
		{
			Instance value_Renamed = factory.getInstance(node);
			
			if (!value_Renamed.Reference)
			{
				System.Object result = value_Renamed.setInstance((System.Object) null);
				System.Type type = value_Renamed.Type;
				
				return validate(node, type);
			}
			return true;
		}
		
		/// <summary> This <code>validate</code> method will validate the XML element 
		/// map from the provided node and validate its children as entry 
		/// types. Each entry type must contain a key and value so that the 
		/// entry can be inserted in to the map as a pair. If either the key 
		/// or value is composite it is read as a root object, which means its
		/// <code>Root</code> annotation must be present and the name of the
		/// object element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be validate
		/// </param>
		/// <param name="type">this is the type to validate the input node against
		/// 
		/// </param>
		/// <returns> true if the element matches the XML schema class given 
		/// </returns>
		private bool validate(InputNode node, System.Type type)
		{
			while (true)
			{
				InputNode next = node.getNext();
				
				if (next == null)
				{
					return true;
				}
				if (!key.validate(next))
				{
					return false;
				}
				if (!value_Renamed.validate(next))
				{
					return false;
				}
			}
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
			System.Collections.IDictionary map = (System.Collections.IDictionary) source;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Object index: map.keySet())
			{
				System.String root = entry.getEntry();
				System.String name = style.getElement(root);
				OutputNode next = node.getChild(name);
				System.Object item = map[index];
				
				key.write(next, index);
				value_Renamed.write(next, item);
			}
		}
	}
}