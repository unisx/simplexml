/*
* PrimitiveArray.java July 2006
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
	
	/// <summary> The <code>PrimitiveArray</code> object is used to convert a list of
	/// elements to an array of object entries. This in effect performs a 
	/// serialization and deserialization of primitive elements for the
	/// array object. On serialization each primitive type must be checked 
	/// against the array component type so that it is serialized in a form 
	/// that can be deserialized dynamically. 
	/// <pre>
	/// 
	/// &lt;array&gt;
	/// &lt;entry&gt;example text one&lt;/entry&gt;
	/// &lt;entry&gt;example text two&lt;/entry&gt;
	/// &lt;entry&gt;example text three&lt;/entry&gt;
	/// &lt;/array&gt;
	/// 
	/// </pre>
	/// For the above XML element list the element <code>entry</code> is
	/// contained within the array. Each entry element is deserialized as 
	/// a from a parent XML element, which is specified in the annotation.
	/// For serialization the reverse is done, each element taken from the 
	/// array is written into an element created from the parent element.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Primitive">
	/// </seealso>
	/// <seealso cref="org.simpleframework.xml.ElementArray">
	/// </seealso>
	class PrimitiveArray : Converter
	{
		
		/// <summary> This factory is used to create an array for the contact.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ArrayFactory factory;
		
		/// <summary> This performs the serialization of the primitive element.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Primitive root;
		
		/// <summary> This is the name that each array element is wrapped with.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String parent;
		
		/// <summary> This is the type of object that will be held in the list.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Type entry;
		
		/// <summary> Constructor for the <code>PrimitiveArray</code> object. This is
		/// given the array type for the contact that is to be converted. An
		/// array of the specified type is used to hold the deserialized
		/// elements and will be the same length as the number of elements.
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// </param>
		/// <param name="type">this is the actual field type from the schema
		/// </param>
		/// <param name="entry">the entry type to be stored within the array
		/// </param>
		/// <param name="parent">this is the name to wrap the array element with     
		/// </param>
		public PrimitiveArray(Context context, Type type, Type entry, System.String parent)
		{
			this.factory = new ArrayFactory(context, type);
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
				return read(node, list);
			}
			return list;
		}
		
		/// <summary> This <code>read</code> method will read the XML element list from
		/// the provided node and deserialize its children as entry types.
		/// This will deserialize each entry type as a primitive value. In
		/// order to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element that is to be deserialized
		/// </param>
		/// <param name="list">this is the array to read the array values in to
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
				((System.Array) list).SetValue(root.read(next), pos);
			}
		}
		
		/// <summary> This <code>validate</code> method will validate the XML element list 
		/// from the provided node and validate its children as entry types.
		/// This will validate each entry type as a primitive value. In order 
		/// to do this the parent string provided forms the element.
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
				System.Type expect = value_Renamed.Type;
				
				return validate(node, expect);
			}
			return true;
		}
		
		/// <summary> This <code>validate</code> method wll validate the XML element list 
		/// from the provided node and validate its children as entry types.
		/// This will validate each entry type as a primitive value. In order 
		/// to do this the parent string provided forms the element.
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
				root.validate(next);
			}
		}
		
		/// <summary> This <code>write</code> method will write the specified object
		/// to the given XML element as as array entries. Each entry within
		/// the given array must be assignable to the array component type.
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
			int size = ((System.Array) source).Length;
			
			for (int i = 0; i < size; i++)
			{
				OutputNode child = node.getChild(parent);
				
				if (child == null)
				{
					break;
				}
				write(child, source, i);
			}
		}
		
		/// <summary> This <code>write</code> method will write the specified object
		/// to the given XML element as as array entries. Each entry within
		/// the given array must be assignable to the array component type.
		/// This will deserialize each entry type as a primitive value. In
		/// order to do this the parent string provided forms the element.
		/// 
		/// </summary>
		/// <param name="source">this is the source object array to be serialized 
		/// </param>
		/// <param name="node">this is the XML element container to be populated
		/// </param>
		/// <param name="index">this is the position in the array to set the item
		/// </param>
		private void  write(OutputNode node, System.Object source, int index)
		{
			System.Object item = ((System.Array) source).GetValue(index);
			
			if (item != null)
			{
				if (!isOverridden(node, item))
				{
					root.write(node, item);
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