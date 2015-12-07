/*
* CompositeKey.java July 2007
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
using Position = org.simpleframework.xml.stream.Position;
using Style = org.simpleframework.xml.stream.Style;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>CompositeKey</code> object is used to convert an object
	/// to an from an XML element. This accepts only composite objects and
	/// will throw an exception if the <code>ElementMap</code> annotation
	/// is configured to have an attribute key. If a key name is given for
	/// the annotation then this will act as a parent element to the 
	/// resulting XML element for the composite object. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.ElementMap">
	/// </seealso>
	class CompositeKey : Converter
	{
		
		/// <summary> This is the context used to support the serialization process.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> This is the traverser used to read and write the composite key.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Traverser root;
		
		/// <summary> This is the style used to style the names used for the XML.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Style style;
		
		/// <summary> This is the entry object used to provide configuration details.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Entry entry;
		
		/// <summary> This represents the type of object the key is written as.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Type type;
		
		/// <summary> Constructor for the <code>CompositeKey</code> object. This will
		/// create an object capable of reading an writing composite keys
		/// from an XML element. This also allows a parent element to be
		/// created to wrap the key object if desired.
		/// 
		/// </summary>
		/// <param name="context">this is the root context for the serialization
		/// </param>
		/// <param name="entry">this is the entry object used for configuration
		/// </param>
		/// <param name="type">this is the type of object the key represents
		/// </param>
		public CompositeKey(Context context, Entry entry, Type type)
		{
			this.root = new Traverser(context);
			this.style = context.Style;
			this.context = context;
			this.entry = entry;
			this.type = type;
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then null is assumed and returned.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		public virtual System.Object read(InputNode node)
		{
			Position line = node.Position;
			System.Type expect = type.getType();
			System.String name = entry.getKey();
			
			if (name == null)
			{
				name = context.getName(expect);
			}
			if (entry.Attribute)
			{
				throw new ElementException("Can not have %s as an attribute at %s", expect, line);
			}
			return read(node, name);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then null is assumed and returned.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// </param>
		/// <param name="value">this is the value to deserialize in to
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// 
		/// </returns>
		/// <throws>  Exception if value is not null an exception is thrown </throws>
		public virtual System.Object read(InputNode node, System.Object value_Renamed)
		{
			System.Type expect = type.getType();
			
			if (value_Renamed != null)
			{
				throw new PersistenceException("Can not read key of %s", expect);
			}
			return read(node);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then null is assumed and returned.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// </param>
		/// <param name="key">this is the name of the key wrapper XML element
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		private System.Object read(InputNode node, System.String key)
		{
			System.String name = style.getElement(key);
			System.Type expect = type.getType();
			
			if (name != null)
			{
				node = node.getNext(name);
			}
			if (node == null)
			{
				return null;
			}
			if (node.isEmpty())
			{
				return null;
			}
			return root.read(node, expect);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then null is assumed and the node is valid.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		public virtual bool validate(InputNode node)
		{
			Position line = node.Position;
			System.Type expect = type.getType();
			System.String name = entry.getKey();
			
			if (name == null)
			{
				name = context.getName(expect);
			}
			if (entry.Attribute)
			{
				throw new ElementException("Can not have %s as an attribute at %s", expect, line);
			}
			return validate(node, name);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then null is assumed and the node is valid.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// </param>
		/// <param name="key">this is the name of the key wrapper XML element
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		private bool validate(InputNode node, System.String key)
		{
			System.String name = style.getElement(key);
			InputNode next = node.getNext(name);
			System.Type expect = type.getType();
			
			if (next == null)
			{
				return true;
			}
			if (next.isEmpty())
			{
				return true;
			}
			return root.validate(next, expect);
		}
		
		/// <summary> This method is used to write the value to the specified node.
		/// The value written to the node must be a composite object and if
		/// the element map annotation is configured to have a key attribute
		/// then this method will throw an exception.
		/// 
		/// </summary>
		/// <param name="node">this is the node that the value is written to
		/// </param>
		/// <param name="item">this is the item that is to be written
		/// </param>
		public virtual void  write(OutputNode node, System.Object item)
		{
			System.Type expect = type.getType();
			System.String key = entry.getKey();
			
			if (entry.Attribute)
			{
				throw new ElementException("Can not have %s as an attribute", expect);
			}
			if (key == null)
			{
				key = context.getName(expect);
			}
			System.String name = style.getElement(key);
			
			root.write(node, item, expect, name);
		}
	}
}