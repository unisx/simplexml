/*
* HyphenStyle.java July 2008
*
* Copyright (C) 2008, Niall Gallagher <niallg@users.sf.net>
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
namespace org.simpleframework.xml.stream
{
	
	/// <summary> The <code>HyphenStyle</code> is used to represent an XML style
	/// that can be applied to a serialized object. A style can be used to
	/// modify the element and attribute names for the generated document.
	/// This styles can be used to generate hyphenated XML.
	/// <pre>
	/// 
	/// &lt;example-element&gt;
	/// &lt;child-element example-attribute='example'&gt;
	/// &lt;inner-element&gt;example&lt;/inner-element&gt;
	/// &lt;/child-element&gt;
	/// &lt;/example-element&gt;
	/// 
	/// </pre>
	/// Above the hyphenated XML elements and attributes can be generated
	/// from a style implementation. Styles enable the same objects to be
	/// serialized in different ways, generating different styles of XML
	/// without having to modify the class schema for that object.    
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public class HyphenStyle : Style
	{
		
		/// <summary> This is used to perform the actual building of tokens.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'builder '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Builder builder;
		
		/// <summary> This is the strategy used to generate the style tokens.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Style style;
		
		/// <summary> Constructor for the <code>HyphenStyle</code> object. This is
		/// used to create a style that will hyphenate XML attributes
		/// and elements allowing a consistent format for generated XML.
		/// </summary>
		public HyphenStyle()
		{
			this.style = new HyphenBuilder();
			this.builder = new Builder(style);
		}
		
		/// <summary> This is used to generate the XML attribute representation of 
		/// the specified name. Attribute names should ensure to keep the
		/// uniqueness of the name such that two different names will
		/// be styled in to two different strings.
		/// 
		/// </summary>
		/// <param name="name">this is the attribute name that is to be styled
		/// 
		/// </param>
		/// <returns> this returns the styled name of the XML attribute
		/// </returns>
		public virtual System.String getAttribute(System.String name)
		{
			return builder.getAttribute(name);
		}
		
		/// <summary> This is used to set the attribute values within this builder.
		/// Overriding the attribute values ensures that the default
		/// algorithm does not need to determine each of the values. It
		/// allows special behaviour that the user may require for XML.
		/// 
		/// </summary>
		/// <param name="name">the name of the XML attribute to be overridden
		/// </param>
		/// <param name="value">the value that is to be used for that attribute
		/// </param>
		public virtual void  setAttribute(System.String name, System.String value_Renamed)
		{
			builder.setAttribute(name, value_Renamed);
		}
		
		/// <summary> This is used to generate the XML element representation of 
		/// the specified name. Element names should ensure to keep the
		/// uniqueness of the name such that two different names will
		/// be styled in to two different strings.
		/// 
		/// </summary>
		/// <param name="name">this is the element name that is to be styled
		/// 
		/// </param>
		/// <returns> this returns the styled name of the XML element
		/// </returns>
		public virtual System.String getElement(System.String name)
		{
			return builder.getElement(name);
		}
		
		/// <summary> This is used to set the element values within this builder.
		/// Overriding the element values ensures that the default
		/// algorithm does not need to determine each of the values. It
		/// allows special behaviour that the user may require for XML.
		/// 
		/// </summary>
		/// <param name="name">the name of the XML element to be overridden
		/// </param>
		/// <param name="value">the value that is to be used for that element
		/// </param>
		public virtual void  setElement(System.String name, System.String value_Renamed)
		{
			builder.setElement(name, value_Renamed);
		}
	}
}