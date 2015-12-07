/*
* Text.java April 2007
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
//UPGRADE_TODO: The type 'java.lang.annotation.RetentionPolicy' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using RetentionPolicy = java.lang.annotation.RetentionPolicy;
//UPGRADE_TODO: The type 'java.lang.annotation.Retention' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Retention = java.lang.annotation.Retention;
namespace org.simpleframework.xml
{
	
	/// <summary> The <code>Text</code> annotation is used to represent a field or
	/// method that appears as text within an XML element. Methods and
	/// fields annotated with this must represent primitive values, which
	/// means that the type is converted to and from an XML representation
	/// using a <code>Transform</code> object. For example, the primitive 
	/// types typically annotated could be strings, integers, or dates.  
	/// <p>
	/// One restriction on this annotation is that it can only appear once 
	/// within a schema class, and it can not appear with the another XML 
	/// element annotations, such as the <code>Element</code> annotation. 
	/// It can however appear with any number of <code>Attribute</code> 
	/// annotations.
	/// <pre>
	/// 
	/// &lt;example one="value" two="value"&gt;
	/// Example text value       
	/// &lt;example&gt;
	/// 
	/// </pre>
	/// Text values are used when an element containing attributes is
	/// used to wrap a text value with no child elements. This can be
	/// used in place of an element annotation to represent a primitive
	/// which is wrapped in a surrounding XML element.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.Transformer">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Retention(RetentionPolicy.RUNTIME)
	public interface Text
	{
		
		/// <summary> This is used to provide a default value for the text data if
		/// the annotated field or method is null. This ensures the the
		/// serialization process writes the text data with a value even
		/// if the value is null, and allows deserialization to determine
		/// whether the value within the object was null or not.
		/// 
		/// </summary>
		/// <returns> this returns the default attribute value to use
		/// </returns>
		System.String empty();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default ;
	
	/// <summary> This is used to determine whether the text is written within 
	/// CDATA block or not. If this is set to true then the text is
	/// written within a CDATA block, by default the text is output
	/// as escaped XML. Typically this is used for large text values.
	/// 
	/// </summary>
	/// <returns> true if the data is to be wrapped in a CDATA block
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean data()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default false;
	
	/// <summary> Determines whether the text value is required within the XML
	/// document. Any field marked as not required may not have its
	/// value set when the object is deserialized. If an object is to
	/// be serialized only a null attribute will not appear in XML.
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