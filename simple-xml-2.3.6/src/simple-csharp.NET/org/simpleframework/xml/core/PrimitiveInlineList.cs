/*
* PrimitiveInlineList.java July 2006
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
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.Mode' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Mode = org.simpleframework.xml.stream.Mode;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>PrimitiveInlineList</code> object is used to convert a
	/// group of elements in to a collection of element entries. This is
	/// used when a containing element for a list is not required. It 
	/// extracts the elements by matching elements to name of the type
	/// that the annotated field or method requires. This enables these
	/// element entries to exist as siblings to other objects within the
	/// object.  One restriction is that the <code>Root</code> annotation
	/// for each of the types within the list must be the same.
	/// <pre> 
	/// 
	/// &lt;entry&gt;example one&lt;/entry&gt;
	/// &lt;entry&gt;example two&lt;/entry&gt;
	/// &lt;entry&gt;example three&lt;/entry&gt;
	/// &lt;entry&gt;example four&lt;/entry&gt;      
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
	class PrimitiveInlineList : Repeater
	{
		
		/// <summary> This factory is used to create a suitable collection list.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private CollectionFactory factory;
		
		/// <summary> This performs the traversal used for object serialization.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Primitive root;
		
		/// <summary> This is the name that each list element is wrapped with.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String parent;
		
		/// <summary> This is the type of object that will be held in the list.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Type entry;
		
		/// <summary> Constructor for the <code>PrimitiveInlineList</code> object. 
		/// This is given the list type and entry type to be used. The list
		/// type is the <code>Collection</code> implementation that is used 
		/// to collect the deserialized entry objects from the XML source. 
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// </param>
		/// <param name="type">this is the collection type for the list used
		/// </param>
		/// <param name="entry">the entry type to be stored within the list
		/// </param>
		/// <param name="parent">this is the name to wrap the list element with 
		/// </param>
		public PrimitiveInlineList(Context context, Type type, Type entry, System.String parent)
		{
			this.factory = new CollectionFactory(context, type);
			this.root = new Primitive(context, entry);
			this.parent = parent;
			this.entry = entry;
		}
		
		/// <summary> This <code>read</code> method wll read the XML element list from
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
			System.Object value_Renamed = factory.getInstance();
			System.Collections.ICollection list = (System.Collections.ICollection) value_Renamed;
			
			if (list != null)
			{
				return read(node, list);
			}
			return null;
		}
		
		/// <summary> This <code>read</code> method wll read the XML element list from
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
		public virtual System.Object read(InputNode node, System.Object value_Renamed)
		{
			System.Collections.ICollection list = (System.Collections.ICollection) value_Renamed;
			
			if (list != null)
			{
				return read(node, list);
			}
			return read(node);
		}
		
		/// <summary> This <code>read</code> method wll read the XML element list from
		/// the provided node and deserialize its children as entry types.
		/// This will deserialize each entry type as a primitive value. In
		/// order to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// </param>
		/// <param name="list">this is the collection that is to be populated
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		private System.Object read(InputNode node, System.Collections.ICollection list)
		{
			InputNode from = node.getParent();
			System.String name = node.Name;
			
			while (node != null)
			{
				System.Object item = root.read(node);
				
				if (item != null)
				{
					SupportClass.ICollectionSupport.Add(list, item);
				}
				node = from.getNext(name);
			}
			return list;
		}
		
		/// <summary> This <code>read</code> method wll read the XML element list from
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
		public virtual bool validate(InputNode node)
		{
			InputNode from = node.getParent();
			System.String name = node.Name;
			
			while (node != null)
			{
				bool valid = root.validate(node);
				
				if (valid == false)
				{
					return false;
				}
				node = from.getNext(name);
			}
			return true;
		}
		
		/// <summary> This <code>write</code> method will write the specified object
		/// to the given XML element as as list entries. Each entry within
		/// the given list must be assignable to the given primitive type.
		/// This will deserialize each entry type as a primitive value. In
		/// order to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="source">this is the source collection to be serialized 
		/// </param>
		/// <param name="node">this is the XML element container to be populated
		/// </param>
		public virtual void  write(OutputNode node, System.Object source)
		{
			OutputNode parent = node.getParent();
			Mode mode = node.getMode();
			
			if (!node.isCommitted())
			{
				node.remove();
			}
			write(parent, source, mode);
		}
		
		/// <summary> This <code>write</code> method will write the specified object
		/// to the given XML element as as list entries. Each entry within
		/// the given list must be assignable to the given primitive type.
		/// This will deserialize each entry type as a primitive value. In
		/// order to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="node">this is the parent output node to write values to
		/// </param>
		/// <param name="source">this is the source collection to be serialized 
		/// </param>
		/// <param name="mode">this is used to determine whether to output CDATA    
		/// </param>
		private void  write(OutputNode node, System.Object source, Mode mode)
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
						child.setMode(mode);
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