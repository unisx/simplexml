/*
* EnumTransform.java May 2007
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
	
	/// <summary> The <code>EnumTransform</code> represents a transform that is 
	/// used to transform enumerations to strings and back again. This
	/// is used when enumerations are used in comma separated arrays.
	/// This may be created multiple times for different types.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class EnumTransform : Transform
	{
		/// <summary> This is the specific enumeration that this transforms.</summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Enum >
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type type;
		
		/// <summary> Constructor for the <code>EnumTransform</code> object. This
		/// is used to create enumerations from strings and convert them
		/// back again. This allows enumerations to be used in arrays.
		/// 
		/// </summary>
		/// <param name="type">this is the enumeration type to be transformed
		/// </param>
		public EnumTransform(System.Type type)
		{
			InitBlock();
			this.type = type;
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
		public virtual Enum read(System.String value_Renamed)
		{
			return Enum.valueOf(type, value_Renamed);
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
		public virtual System.String write(Enum value_Renamed)
		{
			return value_Renamed.name();
		}
	}
}