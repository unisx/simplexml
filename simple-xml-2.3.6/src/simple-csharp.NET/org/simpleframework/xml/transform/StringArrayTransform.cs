/*
* StringArrayTransform.java May 2007
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
//UPGRADE_TODO: The type 'java.util.regex.Pattern' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Pattern = java.util.regex.Pattern;
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>StringArrayTransform</code>  is used to transform string
	/// arrays to and from string representations, which will be inserted
	/// in the generated XML document as the value place holder. The
	/// value must be readable and writable in the same format. Fields
	/// and methods annotated with the XML attribute annotation will use
	/// this to persist and retrieve the value to and from the XML source.
	/// <pre>
	/// 
	/// &#64;Attribute
	/// private String[] array;
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
	class StringArrayTransform : Transform
	{
		/// <summary> Represents the pattern used to split the string values.</summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< String [] >
		//UPGRADE_NOTE: Final was removed from the declaration of 'pattern '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Pattern pattern;
		
		/// <summary> This is the token used to split the string into an array.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'token '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String token;
		
		/// <summary> Constructor for the <code>StringArrayTransform</code> object.
		/// This will create a transform that will split an array using a
		/// comma as the delimeter. In order to perform the split in a
		/// reasonably performant manner the pattern used is compiled.
		/// </summary>
		public StringArrayTransform():this(",")
		{
		}
		
		/// <summary> Constructor for the <code>StringArrayTransform</code> object.
		/// This will create a transform that will split an array using a
		/// specified regular expression pattern. To keep the performance
		/// of the transform reasonable the pattern used is compiled.
		/// 
		/// </summary>
		/// <param name="token">the pattern used to split the string values
		/// </param>
		public StringArrayTransform(System.String token)
		{
			InitBlock();
			this.pattern = Pattern.compile(token);
			this.token = token;
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
		public virtual System.String[] read(System.String value_Renamed)
		{
			return read(value_Renamed, token);
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="value">this is the string representation of the value
		/// </param>
		/// <param name="token">this is the token used to split the string
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		private System.String[] read(System.String value_Renamed, System.String token)
		{
			System.String[] list = pattern.split(value_Renamed);
			
			for (int i = 0; i < list.Length; i++)
			{
				System.String text = list[i];
				
				if (text != null)
				{
					list[i] = text.Trim();
				}
			}
			return list;
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="list">this is the value to be converted to a string
		/// 
		/// </param>
		/// <returns> this is the string representation of the given value
		/// </returns>
		public virtual System.String write(System.String[] list)
		{
			return write(list, token);
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="list">this is the value to be converted to a string
		/// </param>
		/// <param name="token">this is the token used to join the strings
		/// 
		/// </param>
		/// <returns> this is the string representation of the given value
		/// </returns>
		private System.String write(System.String[] list, System.String token)
		{
			StringBuilder text = new StringBuilder();
			
			for (int i = 0; i < list.Length; i++)
			{
				System.String item = list[i];
				
				if (item != null)
				{
					if (text.length() > 0)
					{
						text.append(token);
						text.append(' ');
					}
					text.append(item);
				}
			}
			return text.toString();
		}
	}
}