/*
* CharacterArrayTransform.java May 2007
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
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>CharacterArrayTransform</code> is used to transform text
	/// values to and from string representations, which will be inserted
	/// in the generated XML document as the value place holder. The
	/// value must be readable and writable in the same format. Fields
	/// and methods annotated with the XML attribute annotation will use
	/// this to persist and retrieve the value to and from the XML source.
	/// <pre>
	/// 
	/// &#64;Attribute
	/// private char[] text;
	/// 
	/// </pre>
	/// As well as the XML attribute values using transforms, fields and
	/// methods annotated with the XML element annotation will use this.
	/// Aside from the obvious difference, the element annotation has an
	/// advantage over the attribute annotation in that it can maintain
	/// any references using the <code>CycleStrategy</code> object. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class CharacterArrayTransform : Transform
	{
		
		/// <summary> This is the entry type for the primitive array to be created.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type entry;
		
		/// <summary> Constructor for the <code>PrimitiveArrayTransform</code> object.
		/// This is used to create a transform that will create primitive
		/// arrays and populate the values of the array with values from a
		/// comma separated list of individual values for the entry type.
		/// 
		/// </summary>
		/// <param name="entry">this is the entry component type for the array
		/// </param>
		public CharacterArrayTransform(System.Type entry)
		{
			this.entry = entry;
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="value">this is the string representation of the value
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		public virtual System.Object read(System.String value_Renamed)
		{
			char[] list = value_Renamed.ToCharArray();
			int length = list.Length;
			
			if (entry == typeof(char))
			{
				return list;
			}
			return read(list, length);
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="list">this is the string representation of the value
		/// </param>
		/// <param name="length">this is the number of string values to use
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		private System.Object read(char[] list, int length)
		{
			System.Object array = System.Array.CreateInstance(entry, length);
			
			for (int i = 0; i < length; i++)
			{
				Array.set_Renamed(array, i, list[i]);
			}
			return array;
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="value">this is the value to be converted to a string
		/// 
		/// </param>
		/// <returns> this is the string representation of the given value
		/// </returns>
		public virtual System.String write(System.Object value_Renamed)
		{
			int length = ((System.Array) value_Renamed).Length;
			
			if (entry == typeof(char))
			{
				char[] array = (char[]) value_Renamed;
				return new System.String(array);
			}
			return write(value_Renamed, length);
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="value">this is the value to be converted to a string
		/// 
		/// </param>
		/// <returns> this is the string representation of the given value
		/// </returns>
		private System.String write(System.Object value_Renamed, int length)
		{
			StringBuilder text = new StringBuilder(length);
			
			for (int i = 0; i < length; i++)
			{
				System.Object entry = ((System.Array) value_Renamed).GetValue(i);
				
				if (entry != null)
				{
					text.append(entry);
				}
			}
			return text.toString();
		}
	}
}