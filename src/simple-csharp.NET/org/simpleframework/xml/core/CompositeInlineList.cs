/*
* CompositeInlineList.java July 2006
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
	
	/// <summary> The <code>CompositeInlineList</code> object is used to convert an 
	/// group of elements in to a collection of element entries. This is
	/// used when a containing element for a list is not required. It 
	/// extracts the elements by matching elements to name of the type
	/// that the annotated field or method requires. This enables these
	/// element entries to exist as siblings to other objects within the
	/// object.  One restriction is that the <code>Root</code> annotation
	/// for each of the types within the list must be the same.
	/// <pre> 
	/// 
	/// &lt;entry attribute="one"&gt;
	/// &lt;text&gt;example text value&lt;/text&gt;
	/// &lt;/entry&gt;
	/// &lt;entry attribute="two"&gt;
	/// &lt;text&gt;some other example&lt;/text&gt;
	/// &lt;/entry&gt;  
	/// &lt;entry attribute="three"&gt;
	/// &lt;text&gt;yet another example&lt;/text&gt;
	/// &lt;/entry&gt;      
	/// 
	/// </pre>
	/// For the above XML element list the element <code>entry</code> is
	/// contained within the list. Each entry element is thus deserialized
	/// as a root element and then inserted into the list. This enables
	/// lists to be composed from XML documents. For serialization the
	/// reverse is done, each element taken from the collection is written
	/// as a root element to the owning element to create the list. 
	/// Entry objects do not need to be of the same type.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Traverser">
	/// </seealso>
	/// <seealso cref="org.simpleframework.xml.ElementList">
	/// </seealso>
	class CompositeInlineList : Repeater
	{
		
		/// <summary> This factory is used to create a suitable collection list.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private CollectionFactory factory;
		
		/// <summary> This performs the traversal used for object serialization.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Traverser root;
		
		/// <summary> This represents the name of the entry elements to write.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String name;
		
		/// <summary> This is the entry type for elements within the list.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Type entry;
		
		/// <summary> Constructor for the <code>CompositeInlineList</code> object. 
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
		/// <param name="name">this is the name of the entries used for this list
		/// </param>
		public CompositeInlineList(Context context, Type type, Type entry, System.String name)
		{
			this.factory = new CollectionFactory(context, type);
			this.root = new Traverser(context);
			this.entry = entry;
			this.name = name;
		}
		
		/// <summary> This <code>read</code> method wll read the XML element list from
		/// the provided node and deserialize its children as entry types.
		/// This will each entry type is deserialized as a root type, that 
		/// is, its <code>Root</code> annotation must be present and the
		/// name of the entry element must match that root element name.
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
		
		/// <summary> This <code>read</code> method will read the XML element list from
		/// the provided node and deserialize its children as entry types.
		/// This will each entry type is deserialized as a root type, that 
		/// is, its <code>Root</code> annotation must be present and the
		/// name of the entry element must match that root element name.
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
		/// This will each entry type is deserialized as a root type, that 
		/// is, its <code>Root</code> annotation must be present and the
		/// name of the entry element must match that root element name.
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
				System.Type type = entry.getType();
				System.Object item = read(node, type);
				
				if (item != null)
				{
					SupportClass.ICollectionSupport.Add(list, item);
				}
				node = from.getNext(name);
			}
			return list;
		}
		
		/// <summary> This <code>read</code> method will read the XML element from the     
		/// provided node. This checks to ensure that the deserialized type
		/// is the same as the entry type provided. If the types are not 
		/// the same then an exception is thrown. This is done to ensure
		/// each node in the collection contain the same root annotation. 
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// </param>
		/// <param name="expect">this is the type expected of the deserialized type
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		private System.Object read(InputNode node, System.Type expect)
		{
			System.Object item = root.read(node, expect);
			System.Type result = item.GetType();
			System.Type type = entry.getType();
			
			if (!type.IsAssignableFrom(result))
			{
				throw new PersistenceException("Entry %s does not match %s", result, entry);
			}
			return item;
		}
		
		/// <summary> This <code>read</code> method wll read the XML element list from
		/// the provided node and deserialize its children as entry types.
		/// This will each entry type is deserialized as a root type, that 
		/// is, its <code>Root</code> annotation must be present and the
		/// name of the entry element must match that root element name.
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
			System.Type type = entry.getType();
			System.String name = node.Name;
			
			while (node != null)
			{
				bool valid = root.validate(node, type);
				
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
		/// the given collection must be assignable from the annotated 
		/// type specified within the <code>ElementList</code> annotation.
		/// Each entry is serialized as a root element, that is, its
		/// <code>Root</code> annotation is used to extract the name. 
		/// 
		/// </summary>
		/// <param name="source">this is the source collection to be serialized 
		/// </param>
		/// <param name="node">this is the XML element container to be populated
		/// </param>
		public virtual void  write(OutputNode node, System.Object source)
		{
			System.Collections.ICollection list = (System.Collections.ICollection) source;
			OutputNode parent = node.getParent();
			
			if (!node.isCommitted())
			{
				node.remove();
			}
			write(parent, list);
		}
		
		/// <summary> This <code>write</code> method will write the specified object
		/// to the given XML element as as list entries. Each entry within
		/// the given collection must be assignable from the annotated 
		/// type specified within the <code>ElementList</code> annotation.
		/// Each entry is serialized as a root element, that is, its
		/// <code>Root</code> annotation is used to extract the name. 
		/// 
		/// </summary>
		/// <param name="list">this is the source collection to be serialized 
		/// </param>
		/// <param name="node">this is the XML element container to be populated
		/// </param>
		public virtual void  write(OutputNode node, System.Collections.ICollection list)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Object item: list)
			{
				if (item != null)
				{
					System.Type type = entry.getType();
					System.Type result = item.getClass();
					
					if (!type.IsAssignableFrom(result))
					{
						throw new PersistenceException("Entry %s does not match %s", result, type);
					}
					root.write(node, item, type, name);
				}
			}
		}
	}
}