/*
* PrimitiveKey.java July 2007
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
	
	/// <summary> The <code>PrimitiveKey</code> is used to serialize a primitive key 
	/// to and from a node. If a key name is provided in the annotation 
	/// then this will serialize and deserialize that key with the given
	/// name, if the key is an attribute, then it is written using the 
	/// provided name. 
	/// <pre>
	/// 
	/// &lt;entry key="one"&gt;example one&lt;/entry&gt;
	/// &lt;entry key="two"&gt;example two&lt;/entry&gt;
	/// &lt;entry key="three"&gt;example three&lt;/entry&gt;    
	/// 
	/// </pre>
	/// Allowing the key to be written as either an XML attribute or an
	/// element enables a more flexible means for representing the key.
	/// Composite elements can not be used as attribute values as they do 
	/// not serialize to a string. Primitive keys as elements can be
	/// maintained as references using the cycle strategy.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.CompositeMap">
	/// </seealso>
	class PrimitiveKey : Converter
	{
		
		/// <summary> The primitive factory used to resolve the primitive to a string.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private PrimitiveFactory factory;
		
		/// <summary> This is the context used to support the serialization process.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> The primitive converter used to read the key from the node.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Primitive root;
		
		/// <summary> This is the style used to style the XML elements for the key.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Style style;
		
		/// <summary> The entry object contains the details on how to write the key.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Entry entry;
		
		/// <summary> Represents the primitive type the key is serialized to and from.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Type type;
		
		/// <summary> Constructor for the <code>PrimitiveKey</code> object. This is 
		/// used to create the key object which converts the map key to an 
		/// instance of the key type. This can also resolve references. 
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// </param>
		/// <param name="entry">this is the entry object that describes entries
		/// </param>
		/// <param name="type">this is the type that this converter deals with
		/// </param>
		public PrimitiveKey(Context context, Entry entry, Type type)
		{
			this.factory = new PrimitiveFactory(context, type);
			this.root = new Primitive(context, type);
			this.style = context.Style;
			this.context = context;
			this.entry = entry;
			this.type = type;
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then an exception is thrown.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		public virtual System.Object read(InputNode node)
		{
			System.Type expect = type.getType();
			System.String name = entry.getKey();
			
			if (name == null)
			{
				name = context.getName(expect);
			}
			if (!entry.Attribute)
			{
				return readElement(node, name);
			}
			return readAttribute(node, name);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then an exception is thrown.
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
		/// attributes then an null is assumed and returned.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// </param>
		/// <param name="key">this is the name of the attribute used by the key 
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		private System.Object readAttribute(InputNode node, System.String key)
		{
			System.String name = style.getAttribute(key);
			InputNode child = node.getAttribute(name);
			
			if (child == null)
			{
				return null;
			}
			return root.read(child);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then null is assumed and returned.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// </param>
		/// <param name="key">this is the name of the element used by the key 
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		private System.Object readElement(InputNode node, System.String key)
		{
			System.String name = style.getElement(key);
			InputNode child = node.getNext(name);
			
			if (child == null)
			{
				return null;
			}
			return root.read(child);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then the node is considered as null and is valid.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		public virtual bool validate(InputNode node)
		{
			System.Type expect = type.getType();
			System.String name = entry.getKey();
			
			if (name == null)
			{
				name = context.getName(expect);
			}
			if (!entry.Attribute)
			{
				return validateElement(node, name);
			}
			return validateAttribute(node, name);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then the node is considered as null and is valid.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// </param>
		/// <param name="key">this is the name of the attribute used by the key 
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		private bool validateAttribute(InputNode node, System.String key)
		{
			System.String name = style.getElement(key);
			InputNode child = node.getAttribute(name);
			
			if (child == null)
			{
				return true;
			}
			return root.validate(child);
		}
		
		/// <summary> This method is used to read the key value from the node. The 
		/// value read from the node is resolved using the template filter.
		/// If the key value can not be found according to the annotation
		/// attributes then the node is considered as null and is valid.
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the key value from
		/// </param>
		/// <param name="key">this is the name of the element used by the key 
		/// 
		/// </param>
		/// <returns> this returns the value deserialized from the node
		/// </returns>
		private bool validateElement(InputNode node, System.String key)
		{
			System.String name = style.getElement(key);
			InputNode child = node.getNext(name);
			
			if (child == null)
			{
				return true;
			}
			return root.validate(child);
		}
		
		/// <summary> This method is used to write the value to the specified node.
		/// The value written to the node can be an attribute or an element
		/// depending on the annotation attribute values. This method will
		/// maintain references for serialized elements.
		/// 
		/// </summary>
		/// <param name="node">this is the node that the value is written to
		/// </param>
		/// <param name="item">this is the item that is to be written
		/// </param>
		public virtual void  write(OutputNode node, System.Object item)
		{
			if (!entry.Attribute)
			{
				writeElement(node, item);
			}
			else if (item != null)
			{
				writeAttribute(node, item);
			}
		}
		
		/// <summary> This method is used to write the value to the specified node.
		/// This will write the item as an element to the provided node,
		/// also this enables references to be used during serialization.
		/// 
		/// </summary>
		/// <param name="node">this is the node that the value is written to
		/// </param>
		/// <param name="item">this is the item that is to be written
		/// </param>
		private void  writeElement(OutputNode node, System.Object item)
		{
			System.Type expect = type.getType();
			System.String key = entry.getKey();
			
			if (key == null)
			{
				key = context.getName(expect);
			}
			System.String name = style.getElement(key);
			OutputNode child = node.getChild(name);
			
			if (item != null)
			{
				if (!isOverridden(child, item))
				{
					root.write(child, item);
				}
			}
		}
		
		/// <summary> This method is used to write the value to the specified node.
		/// This will write the item as an attribute to the provided node,
		/// the name of the attribute is taken from the annotation.
		/// 
		/// </summary>
		/// <param name="node">this is the node that the value is written to
		/// </param>
		/// <param name="item">this is the item that is to be written
		/// </param>
		private void  writeAttribute(OutputNode node, System.Object item)
		{
			System.Type expect = type.getType();
			System.String text = factory.getText(item);
			System.String key = entry.getKey();
			
			if (key == null)
			{
				key = context.getName(expect);
			}
			System.String name = style.getAttribute(key);
			
			if (text != null)
			{
				node.setAttribute(name, text);
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
			return factory.setOverride(type, value_Renamed, node);
		}
	}
}