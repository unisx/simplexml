/*
* Element.java July 2006
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
//UPGRADE_TODO: The type 'java.lang.annotation.RetentionPolicy' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using RetentionPolicy = java.lang.annotation.RetentionPolicy;
//UPGRADE_TODO: The type 'java.lang.annotation.Retention' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Retention = java.lang.annotation.Retention;
namespace org.simpleframework.xml
{
	
	/// <summary> The <code>Element</code> annotation is used to represent a field
	/// or method that appears as an XML element. Fields or methods that
	/// are annotated with this can be either primitive or compound, that
	/// is, represent an object that can be serialized and deserialized.
	/// Below is an example of the serialized format for a compound object.
	/// <p>
	/// If this annotates a type that contains no XML annotations then
	/// this will look for a suitable <code>Transform</code> for the type
	/// using the <code>Transformer</code>. For instance, all primitives 
	/// and primitive arrays that are annotated with this will make use 
	/// of a transform in order to convert its value to and from suitable 
	/// XML representations.
	/// <pre>
	/// 
	/// &lt;example class="demo.Example"&gt;
	/// &lt;data/&gt;
	/// &lt;example&gt;
	/// 
	/// </pre>
	/// Each element may have any number of attributes and sub-elements
	/// representing fields or methods of that compound object. Attribute
	/// and element names can be acquired from the annotation or, if the
	/// annotation does not explicitly declare a name, it is taken from
	/// the annotated field or method. There are exceptions in some cases,
	/// for example, the <code>class</code> attribute is reserved by the
	/// serialization framework to represent the serialized type. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Retention(RetentionPolicy.RUNTIME)
	public interface Element
	{
		
		/// <summary> This represents the name of the XML element. Annotated fields
		/// can optionally provide the name of the element. If no name is
		/// provided then the name of the annotated field or method will
		/// be used in its place. The name is provided if the field or
		/// method name is not suitable as an XML element name.
		/// 
		/// </summary>
		/// <returns> the name of the XML element this represents
		/// </returns>
		System.String name();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default ;
	
	/// <summary> This is used to determine whether the element data is written
	/// in a CDATA block or not. If this is set to true then the text
	/// is written within a CDATA block, by default the text is output
	/// as escaped XML. Typically this is useful for primitives only.
	/// 
	/// </summary>
	/// <returns> true if the data is to be wrapped in a CDATA block
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean data()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default false;
	
	/// <summary> Determines whether the element is required within the XML
	/// document. Any field marked as not required will not have its
	/// value set when the object is deserialized. If an object is to
	/// be serialized only a null attribute will not appear as XML.
	/// 
	/// </summary>
	/// <returns> true if the element is required, false otherwise
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean required()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}