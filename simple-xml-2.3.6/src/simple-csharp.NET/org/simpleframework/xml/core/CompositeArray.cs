/*
* CompositeArray.java July 2006
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
using Position = org.simpleframework.xml.stream.Position;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>CompositeArray</code> object is used to convert a list of
	/// elements to an array of object entries. This in effect performs a 
	/// root serialization and deserialization of entry elements for the
	/// array object. On serialization each objects type must be checked 
	/// against the array component type so that it is serialized in a form 
	/// that can be deserialized dynamically. 
	/// <pre>
	/// 
	/// &lt;array length="2"&gt;
	/// &lt;entry&gt;
	/// &lt;text&gt;example text value&lt;/text&gt;
	/// &lt;/entry&gt;
	/// &lt;entry&gt;
	/// &lt;text&gt;some other example&lt;/text&gt;
	/// &lt;/entry&gt;
	/// &lt;/array&gt;
	/// 
	/// </pre>
	/// For the above XML element list the element <code>entry</code> is
	/// contained within the array. Each entry element is deserialized as 
	/// a root element and then inserted into the array. For serialization 
	/// the reverse is done, each element taken from the array is written
	/// as a root element to the parent element to create the list. Entry
	/// objects do not need to be of the same type.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Traverser">
	/// </seealso>
	/// <seealso cref="org.simpleframework.xml.ElementArray">
	/// </seealso>
	class CompositeArray : Converter
	{
		
		/// <summary> This factory is used to create an array for the contact.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ArrayFactory factory;
		
		/// <summary> This performs the traversal used for object serialization.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Traverser root;
		
		/// <summary> This is the name to wrap each entry that is represented.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String parent;
		
		/// <summary> This is the entry type for elements within the array.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Type entry;
		
		/// <summary> Constructor for the <code>CompositeArray</code> object. This is
		/// given the array type for the contact that is to be converted. An
		/// array of the specified type is used to hold the deserialized
		/// elements and will be the same length as the number of elements.
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// </param>
		/// <param name="type">this is the field type for the array being used
		/// </param>
		/// <param name="entry">this is the entry type for the array elements
		/// </param>
		/// <param name="parent">this is the name to wrap the array element with
		/// </param>
		public CompositeArray(Context context, Type type, Type entry, System.String parent)
		{
			this.factory = new ArrayFactory(context, type);
			this.root = new Traverser(context);
			this.parent = parent;
			this.entry = entry;
		}
		
		/// <summary> This <code>read</code> method will read the XML element list from
		/// the provided node and deserialize its children as entry types.
		/// This ensures each entry type is deserialized as a root type, that 
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
			Instance type = factory.getInstance(node);
			System.Object list = type.getInstance();
			
			if (!type.Reference)
			{
				return read(node, list);
			}
			return list;
		}
		
		/// <summary> This <code>read</code> method will read the XML element list from
		/// the provided node and deserialize its children as entry types.
		/// This ensures each entry type is deserialized as a root type, that 
		/// is, its <code>Root</code> annotation must be present and the
		/// name of the entry element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// </param>
		/// <param name="list">this is the array that is to be deserialized
		/// 
		/// </param>
		/// <returns> this returns the item to attach to the object contact
		/// </returns>
		public virtual System.Object read(InputNode node, System.Object list)
		{
			int length = ((System.Array) list).Length;
			
			for (int pos = 0; true; pos++)
			{
				Position line = node.Position;
				InputNode next = node.getNext();
				
				if (next == null)
				{
					return list;
				}
				if (pos >= length)
				{
					throw new ElementException("Array length missing or incorrect at %s", line);
				}
				read(next, list, pos);
			}
		}
		
		/// <summary> This is used to read the specified node from in to the list. If
		/// the node is null then this represents a null element value in
		/// the array. The node can be null only if there is a parent and
		/// that parent contains no child XML elements.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the array value from 
		/// </param>
		/// <param name="list">this is the list to add the array value in to
		/// </param>
		/// <param name="index">this is the offset to set the value in the array
		/// </param>
		private void  read(InputNode node, System.Object list, int index)
		{
			System.Type type = entry.getType();
			System.Object value_Renamed = null;
			
			if (!node.isEmpty())
			{
				value_Renamed = root.read(node, type);
			}
			((System.Array) list).SetValue(value_Renamed, index);
		}
		
		/// <summary> This <code>validate</code> method will validate the XML element 
		/// list against the provided node and validate its children as entry 
		/// types. This ensures each entry type is validated as a root type, 
		/// that is, its <code>Root</code> annotation must be present and the
		/// name of the entry element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be validated
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
		
		/// <summary> This <code>validate</code> method wll validate the XML element 
		/// list against the provided node and validate its children as entry 
		/// types. This ensures each entry type is validated as a root type, 
		/// that is, its <code>Root</code> annotation must be present and the
		/// name of the entry element must match that root element name.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be validated
		/// </param>
		/// <param name="type">this is the array type used to create the array
		/// 
		/// </param>
		/// <returns> true if the element matches the XML schema class given 
		/// </returns>
		private bool validate(InputNode node, System.Type type)
		{
			for (int i = 0; true; i++)
			{
				InputNode next = node.getNext();
				
				if (next == null)
				{
					return true;
				}
				if (!next.isEmpty())
				{
					root.validate(next, type);
				}
			}
		}
		
		/// <summary> This <code>write</code> method will write the specified object
		/// to the given XML element as as array entries. Each entry within
		/// the given array must be assignable to the array component type.
		/// Each array entry is serialized as a root element, that is, its
		/// <code>Root</code> annotation is used to extract the name. 
		/// 
		/// </summary>
		/// <param name="source">this is the source object array to be serialized 
		/// </param>
		/// <param name="node">this is the XML element container to be populated
		/// </param>
		public virtual void  write(OutputNode node, System.Object source)
		{
			int size = ((System.Array) source).Length;
			
			for (int i = 0; i < size; i++)
			{
				System.Object item = ((System.Array) source).GetValue(i);
				System.Type type = entry.getType();
				
				root.write(node, item, type, parent);
			}
			node.commit();
		}
	}
}