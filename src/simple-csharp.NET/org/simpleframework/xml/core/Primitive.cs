/*
* Primitive.java July 2006
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
	
	/// <summary> The <code>Primitive</code> object is used to provide serialization
	/// for primitive objects. This can serialize and deserialize any
	/// primitive object and enumerations. Primitive values are converted
	/// to text using the <code>String.valueOf</code> method. Enumerated
	/// types are converted using the <code>Enum.valueOf</code> method.
	/// <p>
	/// Text within attributes and elements can contain template variables
	/// similar to those found in Apache <cite>Ant</cite>. This allows
	/// values such as system properties, environment variables, and user
	/// specified mappings to be inserted into the text in place of the
	/// template reference variables.
	/// <pre>
	/// 
	/// &lt;example attribute="${value}&gt;
	/// &lt;text&gt;Text with a ${variable}&lt;/text&gt;
	/// &lt;/example&gt;
	/// 
	/// </pre>
	/// In the above XML element the template variable references will be
	/// checked against the <code>Filter</code> object used by the context
	/// serialization object. If they corrospond to a filtered value then
	/// they are replaced, if not the text remains unchanged.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.filter.Filter">
	/// </seealso>
	class Primitive : Converter
	{
		
		/// <summary> This is used to convert the string values to primitives.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private PrimitiveFactory factory;
		
		/// <summary> The context object is used to perform text value filtering.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> This the value used to represent a null primitive value.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'empty '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String empty;
		
		/// <summary> This is the type that this primitive expects to represent.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type type;
		
		/// <summary> Constructor for the <code>Primitive</code> object. This is used
		/// to convert an XML node to a primitive object and vice versa. To
		/// perform deserialization the primitive object requires the context
		/// object used for the instance of serialization to performed.
		/// 
		/// </summary>
		/// <param name="context">the context object used for the serialization
		/// </param>
		/// <param name="type">this is the type of primitive this represents
		/// </param>
		public Primitive(Context context, Type type):this(context, type, null)
		{
		}
		
		/// <summary> Constructor for the <code>Primitive</code> object. This is used
		/// to convert an XML node to a primitive object and vice versa. To
		/// perform deserialization the primitive object requires the context
		/// object used for the instance of serialization to performed.
		/// 
		/// </summary>
		/// <param name="context">the context object used for the serialization
		/// </param>
		/// <param name="type">this is the type of primitive this represents
		/// </param>
		/// <param name="empty">this is the value used to represent a null value
		/// </param>
		public Primitive(Context context, Type type, System.String empty)
		{
			this.factory = new PrimitiveFactory(context, type);
			this.type = type.getType();
			this.context = context;
			this.empty = empty;
		}
		
		/// <summary> This <code>read</code> method will extract the text value from
		/// the node and replace any template variables before converting
		/// it to a primitive value. This uses the <code>Context</code>
		/// object used for this instance of serialization to replace all
		/// template variables with values from the context filter.
		/// 
		/// </summary>
		/// <param name="node">this is the node to be converted to a primitive
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been deserialized
		/// </returns>
		public virtual System.Object read(InputNode node)
		{
			if (node.Element)
			{
				return readElement(node);
			}
			return read(node, type);
		}
		
		/// <summary> This <code>read</code> method will extract the text value from
		/// the node and replace any template variables before converting
		/// it to a primitive value. This uses the <code>Context</code>
		/// object used for this instance of serialization to replace all
		/// template variables with values from the context filter.
		/// 
		/// </summary>
		/// <param name="node">this is the node to be converted to a primitive
		/// </param>
		/// <param name="value">this is the original primitive value used
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been deserialized
		/// 
		/// </returns>
		/// <throws>  Exception if value is not null an exception is thrown </throws>
		public virtual System.Object read(InputNode node, System.Object value_Renamed)
		{
			if (value_Renamed != null)
			{
				throw new PersistenceException("Can not read existing %s", type);
			}
			return read(node);
		}
		
		/// <summary> This <code>read</code> method will extract the text value from
		/// the node and replace any template variables before converting
		/// it to a primitive value. This uses the <code>Context</code>
		/// object used for this instance of serialization to replace all
		/// template variables with values from the context filter.
		/// 
		/// </summary>
		/// <param name="node">this is the node to be converted to a primitive
		/// </param>
		/// <param name="type">this is the type to read the primitive with
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been deserialized
		/// </returns>
		public virtual System.Object read(InputNode node, System.Type type)
		{
			System.String value_Renamed = node.getValue();
			
			if (value_Renamed == null)
			{
				return null;
			}
			if (empty != null && value_Renamed.Equals(empty))
			{
				return empty;
			}
			return readTemplate(value_Renamed, type);
		}
		
		/// <summary> This <code>read</code> method will extract the text value from
		/// the node and replace any template variables before converting
		/// it to a primitive value. This uses the <code>Context</code>
		/// object used for this instance of serialization to replace all
		/// template variables with values from the context filter.
		/// 
		/// </summary>
		/// <param name="node">this is the node to be converted to a primitive
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been deserialized
		/// </returns>
		private System.Object readElement(InputNode node)
		{
			Instance value_Renamed = factory.getInstance(node);
			
			if (!value_Renamed.Reference)
			{
				return readElement(node, value_Renamed);
			}
			return value_Renamed.getInstance();
		}
		
		/// <summary> This <code>read</code> method will extract the text value from
		/// the node and replace any template variables before converting
		/// it to a primitive value. This uses the <code>Context</code>
		/// object used for this instance of serialization to replace all
		/// template variables with values from the context filter.
		/// 
		/// </summary>
		/// <param name="node">this is the node to be converted to a primitive
		/// </param>
		/// <param name="value">this is the instance to set the result to
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been deserialized
		/// </returns>
		private System.Object readElement(InputNode node, Instance value_Renamed)
		{
			System.Object result = read(node, type);
			
			if (value_Renamed != null)
			{
				value_Renamed.setInstance(result);
			}
			return result;
		}
		
		/// <summary> This <code>read</code> method will extract the text value from
		/// the node and replace any template variables before converting
		/// it to a primitive value. This uses the <code>Context</code>
		/// object used for this instance of serialization to replace all
		/// template variables with values from the context filter.
		/// 
		/// </summary>
		/// <param name="value">this is the value to be processed as a template
		/// </param>
		/// <param name="type">this is the type that that the primitive is
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been deserialized
		/// </returns>
		private System.Object readTemplate(System.String value_Renamed, System.Type type)
		{
			System.String text = context.getProperty(value_Renamed);
			
			if (text != null)
			{
				return factory.getInstance(text, type);
			}
			return null;
		}
		
		/// <summary> This <code>validate</code> method will validate the primitive 
		/// by checking the node text. If the value is a reference then 
		/// this will not extract any value from the node. Transformation
		/// of the extracted value is not done as it can not account for
		/// template variables. Thus any text extracted is valid.
		/// 
		/// </summary>
		/// <param name="node">this is the node to be validated as a primitive
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been validated
		/// </returns>
		public virtual bool validate(InputNode node)
		{
			if (node.Element)
			{
				validateElement(node);
			}
			else
			{
				node.getValue();
			}
			return true;
		}
		
		/// <summary> This <code>validateElement</code> method validates a primitive 
		/// by checking the node text. If the value is a reference then 
		/// this will not extract any value from the node. Transformation
		/// of the extracted value is not done as it can not account for
		/// template variables. Thus any text extracted is valid.
		/// 
		/// </summary>
		/// <param name="node">this is the node to be validated as a primitive
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been validated
		/// </returns>
		private bool validateElement(InputNode node)
		{
			Instance type = factory.getInstance(node);
			
			if (!type.Reference)
			{
				type.setInstance((System.Object) null);
			}
			return true;
		}
		
		/// <summary> This <code>write</code> method will serialize the contents of
		/// the provided object to the given XML element. This will use
		/// the <code>String.valueOf</code> method to convert the object to
		/// a string if the object represents a primitive, if however the
		/// object represents an enumerated type then the text value is
		/// created using <code>Enum.name</code>.
		/// 
		/// </summary>
		/// <param name="source">this is the object to be serialized
		/// </param>
		/// <param name="node">this is the XML element to have its text set
		/// </param>
		public virtual void  write(OutputNode node, System.Object source)
		{
			System.String text = factory.getText(source);
			
			if (text != null)
			{
				node.setValue(text);
			}
		}
	}
}