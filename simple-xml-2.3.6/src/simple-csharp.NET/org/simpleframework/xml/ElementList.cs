/*
* ElementList.java July 2006
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
	
	/// <summary> The <code>ElementList</code> annotation represents a method or
	/// field that is a <code>Collection</code> for storing entries. The
	/// collection object deserialized is typically of the same type as
	/// the field. However, a <code>class</code> attribute can be used to
	/// override the field type, however the type must be assignable.
	/// <pre>
	/// 
	/// &lt;list class="java.util.ArrayList"&gt;
	/// &lt;entry name="one"/&gt;
	/// &lt;entry name="two"/&gt;
	/// &lt;entry name="three"/&gt;  
	/// &lt;/list&gt;
	/// 
	/// </pre>
	/// If a <code>class</code> attribute is not provided and the type or
	/// the field or method is abstract, a suitable match is searched for 
	/// from the collections available from the Java collections framework.
	/// This annotation can also compose an inline list of XML elements. 
	/// An inline list contains no parent or containing element.
	/// <pre>
	/// 
	/// &lt;entry name="one"/&gt;
	/// &lt;entry name="two"/&gt;
	/// &lt;entry name="three"/&gt;  
	/// 
	/// </pre>
	/// The above XML is an example of the output for an inline list of
	/// XML elements. In such a list the annotated field or method must
	/// not be given a name. Instead the name is acquired from the name of
	/// the entry type. For example if the <code>type</code> attribute of
	/// this was set to an object <code>example.Entry</code> then the name 
	/// of the entry list would be taken as the root name of the object
	/// as taken from the <code>Root</code> annotation for that object.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Retention(RetentionPolicy.RUNTIME)
	public interface ElementList
	{
		
		/// <summary> This represents the name of the XML element. Annotated fields
		/// can optionally provide the name of the element. If no name is
		/// provided then the name of the annotated field or method will
		/// be used in its place. The name is provided if the field or
		/// method name is not suitable as an XML element name. Also, if
		/// the list is inline then this must not be specified.
		/// 
		/// </summary>
		/// <returns> the name of the XML element this represents
		/// </returns>
		System.String name();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default ;
	
	/// <summary> This is used to provide a name of the XML element representing
	/// the entry within the list. An entry name is optional and is
	/// used when the name needs to be overridden. This also ensures
	/// that entry, regardless of type has the same root name.   
	/// 
	/// </summary>
	/// <returns> this returns the entry XML element for each value
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String entry()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default ;
	
	/// <summary> Represents the type of object the element list contains. This
	/// type is used to deserialize the XML elements from the list. 
	/// The object typically represents the deserialized type, but can
	/// represent a subclass of the type deserialized as determined
	/// by the <code>class</code> attribute value for the list. If 
	/// this is not specified then the type can be determined from the
	/// generic parameter of the annotated <code>Collection</code>.
	/// 
	/// </summary>
	/// <returns> the type of the element deserialized from the XML
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class type()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class;
	
	/// <summary> This is used to determine whether the element data is written
	/// in a CDATA block or not. If this is set to true then the text
	/// is written within a CDATA block, by default the text is output
	/// as escaped XML. Typically this is useful when this annotation
	/// is applied to an array of primitives, such as strings.
	/// 
	/// </summary>
	/// <returns> true if entries are to be wrapped in a CDATA block
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
	
	/// <summary> Determines whether the element list is inlined with respect
	/// to the parent XML element. An inlined element list does not
	/// contain an enclosing element. It is simple a sequence of 
	/// elements that appear one after another within an element.
	/// As such an inline element list must not have a name. 
	/// 
	/// </summary>
	/// <returns> this returns true if the element list is inline
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean inline()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default false;
	
	/// <summary> This is used to determine if an optional field or method can
	/// remain null if it does not exist. If this is false then the
	/// optional element is given an empty list. This is a convenience
	/// attribute which avoids having to check if the element is null
	/// before providing it with a suitable default instance.
	/// 
	/// </summary>
	/// <returns> false if an optional element is always instantiated
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean empty()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}