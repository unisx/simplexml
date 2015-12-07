/*
* PrimitiveList.java July 2006
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
using Type = org.simpleframework.xml.strategy.Type;
using InputNode = org.simpleframework.xml.stream.InputNode;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>PrimitiveList</code> object is used to convert an element
	/// list to a collection of element entries. This in effect performs a 
	/// serialization and deserialization of primitive entry elements for 
	/// the collection object. On serialization each objects type must be 
	/// checked against the XML annotation entry so that it is serialized
	/// in a form that can be deserialized. 
	/// <pre>
	/// 
	/// &lt;list&gt;
	/// &lt;entry&gt;example one&lt;/entry&gt;
	/// &lt;entry&gt;example two&lt;/entry&gt;
	/// &lt;entry&gt;example three&lt;/entry&gt;
	/// &lt;entry&gt;example four&lt;/entry&gt;
	/// &lt;/list&gt;
	/// 
	/// </pre>
	/// For the above XML element list the element <code>entry</code> is
	/// used to wrap the primitive string value. This wrapping XML element 
	/// is configurable and defaults to the lower case string for the name
	/// of the class it represents. So, for example, if the primitive type
	/// is an <code>int</code> the enclosing element will be called int.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Primitive">
	/// </seealso>
	/// <seealso cref="org.simpleframework.xml.ElementList">
	/// </seealso>
	class PrimitiveList : Converter
	{
		
		/// <summary> This factory is used to create a suitable collection list.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private CollectionFactory factory;
		
		/// <summary> This performs the serialization of the primitive element.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Primitive root;
		
		/// <summary> This is the name that each array element is wrapped with.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String parent;
		
		/// <summary> This is the type of object that will be held within the list.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Type entry;
		
		/// <summary> Constructor for the <code>PrimitiveList</code> object. This is
		/// given the list type and entry type to be used. The list type is
		/// the <code>Collection</code> implementation that deserialized
		/// entry objects are inserted into. 
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// </param>
		/// <param name="type">this is the collection type for the list used
		/// </param>
		/// <param name="entry">the primitive type to be stored within the list
		/// </param>
		/// <param name="parent">this is the name to wrap the list element with 
		/// </param>
		public PrimitiveList(Context context, Type type, Type entry, System.String parent)
		{
			this.factory = new CollectionFactory(context, type);
			this.root = new Primitive(context, entry);
			this.parent = parent;
			this.entry = entry;
		}
		
		/// <summary> This <code>read</code> method will read the XML element list from
		/// the provided node and deserialize its children as entry types.
		/// This will deserialize each entry type as a primitive value. In
		/// order to do this the parent string provided forms the element.
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
			System.Object list = type.getInstance();
			
			if (!type.Reference)
			{
				return populate(node, list);
			}
			return list;
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
		
		/// <summary> This <code>populate</code> method wll read the XML element list 
		/// from the provided node and deserialize its children as entry types.
		/// This will deserialize each entry type as a primitive value. In
		/// order to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// </param>
		/// <param name="result">this is the collection that is to be populated
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		private System.Object populate(InputNode node, System.Object result)
		{
			System.Collections.ICollection list = (System.Collections.ICollection) result;
			
			while (true)
			{
				InputNode next = node.getNext();
				
				if (next == null)
				{
					return list;
				}
				SupportClass.ICollectionSupport.Add(list, root.read(next));
			}
		}
		
		/// <summary> This <code>validate</code> method wll validate the XML element list 
		/// from the provided node and validate its children as entry types.
		/// This will validate each entry type as a primitive value. In order 
		/// to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
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
				System.Type expect = value_Renamed.Type;
				
				return validate(node, expect);
			}
			return true;
		}
		
		/// <summary> This <code>validate</code> method will validate the XML element list 
		/// from the provided node and validate its children as entry types.
		/// This will validate each entry type as a primitive value. In order 
		/// to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// </param>
		/// <param name="type">this is the type to validate against the input node
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
				root.validate(next);
			}
		}
		
		/// <summary> This <code>write</code> method will write the specified object
		/// to the given XML element as as list entries. Each entry within
		/// the given list must be assignable to the given primitive type.
		/// This will deserialize each entry type as a primitive value. In
		/// order to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="source">this is the source object array to be serialized 
		/// </param>
		/// <param name="node">this is the XML element container to be populated
		/// </param>
		public virtual void  write(OutputNode node, System.Object source)
		{
			System.Collections.ICollection list = (System.Collections.ICollection) source;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Object item: list)
			{
				if (item != null)
				{
					OutputNode child = node.getChild(parent);
					
					if (!isOverridden(child, item))
					{
						root.write(child, item);
					}
				}
			}
		}
		
		/// <summary> This is used to determine whether the specified value has been
		/// overridden by the strategy. If the item has been overridden
		/// then no more serialization is require for that value, this is
		/// effectively telling the serialization process to stop writing.
		/// 
		/// </summary>
		/// <param name="node">the node that a potential override is written to
		/// </param>
		/// <param name="value">this is the object instance to be serialized
		/// 
		/// </param>
		/// <returns> returns true if the strategy overrides the object
		/// </returns>
		private bool isOverridden(OutputNode node, System.Object value_Renamed)
		{
			return factory.setOverride(entry, value_Renamed, node);
		}
	}
}