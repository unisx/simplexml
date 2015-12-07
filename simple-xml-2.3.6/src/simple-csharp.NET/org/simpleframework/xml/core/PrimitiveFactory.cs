/*
* PrimitiveFactory.java July 2006
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
using Value = org.simpleframework.xml.strategy.Value;
using InputNode = org.simpleframework.xml.stream.InputNode;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>PrimitiveFactory</code> object is used to create objects
	/// that are primitive types. This creates primitives and enumerated
	/// types when given a string value. The string value is parsed using
	/// a matched <code>Transform</code> implementation. The transform is
	/// then used to convert the object instance to an from a suitable XML
	/// representation. Only enumerated types are not transformed using 
	/// a transform, instead they use <code>Enum.name</code>. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.Transformer">
	/// </seealso>
	class PrimitiveFactory:Factory
	{
		
		/// <summary> Constructor for the <code>PrimitiveFactory</code> object. This
		/// is provided the field type that is to be instantiated. This
		/// must be a type that contains a <code>Transform</code> object,
		/// typically this is a <code>java.lang</code> primitive object
		/// or one of the primitive types such as <code>int</code>. Also
		/// this can be given a class for an enumerated type. 
		/// 
		/// </summary>
		/// <param name="context">this is the context used by this factory
		/// </param>
		/// <param name="type">this is the field type to be instantiated
		/// </param>
		public PrimitiveFactory(Context context, Type type):base(context, type)
		{
		}
		
		/// <summary> This method will instantiate an object of the field type, or if
		/// the <code>Strategy</code> object can resolve a class from the
		/// XML element then this is used instead. If the resulting type is
		/// abstract or an interface then this method throws an exception.
		/// 
		/// </summary>
		/// <param name="node">this is the node to check for the override
		/// 
		/// </param>
		/// <returns> this returns an instance of the resulting type
		/// </returns>
		public virtual Instance getInstance(InputNode node)
		{
			Value value_Renamed = getOverride(node);
			System.Type type = Type;
			
			if (value_Renamed == null)
			{
				return context.getInstance(type);
			}
			return new ObjectInstance(context, value_Renamed);
		}
		
		/// <summary> This will instantiate an object of the field type using the
		/// provided string. Typically this string is transformed in to the
		/// type using a <code>Transform</code> object. However, if the
		/// values is an enumeration then its value is created using the
		/// <code>Enum.valueOf</code> method. Also string values typically
		/// do not require conversion of any form and are just returned.
		/// 
		/// </summary>
		/// <param name="text">this is the value to be transformed to an object
		/// 
		/// </param>
		/// <returns> this returns an instance of the field type
		/// </returns>
		public virtual System.Object getInstance(System.String text)
		{
			System.Type type = Type;
			
			if (type == typeof(System.String))
			{
				return text;
			}
			return getInstance(text, type);
		}
		
		/// <summary> This will instantiate an object of the field type using the
		/// provided string. Typically this string is transformed in to the
		/// type using a <code>Transform</code> object. However, if the
		/// values is an enumeration then its value is created using the
		/// <code>Enum.valueOf</code> method. Also string values typically
		/// do not require conversion of any form and are just returned.
		/// 
		/// </summary>
		/// <param name="text">this is the value to be transformed to an object
		/// </param>
		/// <param name="type">this is the type of the primitive to instantiate
		/// 
		/// </param>
		/// <returns> this returns an instance of the field type
		/// </returns>
		public virtual System.Object getInstance(System.String text, System.Type type)
		{
			return support.read(text, type);
		}
		
		/// <summary> This is used to acquire a text value for the specified object.
		/// This will convert the object to a string using the transformer
		/// so that it can be deserialized from the generate XML document.
		/// However if the type is an <code>Enum</code> type then the text
		/// value is taken from <code>Enum.name</code> so it can later be
		/// deserialized easily using the enumeration class and name.
		/// 
		/// </summary>
		/// <param name="source">this is the object instance to get the value of
		/// 
		/// </param>
		/// <returns> this returns a string representation of the object
		/// 
		/// </returns>
		/// <throws>  Exception if the object could not be transformed </throws>
		public virtual System.String getText(System.Object source)
		{
			System.Type type = source.GetType();
			
			if (type.isEnum())
			{
				Enum value_Renamed = (Enum) source;
				return value_Renamed.name();
			}
			return support.write(source, type);
		}
	}
}