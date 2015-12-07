/*
* IntegerTransform.java May 2007
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
	
	/// <summary> The <code>IntegerTransform</code> is used to transform integer
	/// values to and from string representations, which will be inserted
	/// in the generated XML document as the value place holder. The
	/// value must be readable and writable in the same format. Fields
	/// and methods annotated with the XML attribute annotation will use
	/// this to persist and retrieve the value to and from the XML source.
	/// <pre>
	/// 
	/// &#64;Attribute
	/// private Integer number;
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
	class IntegerTransform : Transform
	{
		public IntegerTransform()
		{
			InitBlock();
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
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Integer >
		public virtual System.Int32 read(System.String value_Renamed)
		{
			return System.Int32.Parse(value_Renamed);
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
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual System.String write(ref System.Int32 value_Renamed)
		{
			return value_Renamed.ToString();
		}
	}
}