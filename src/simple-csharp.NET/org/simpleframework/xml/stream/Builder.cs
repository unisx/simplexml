/*
* Builder.java July 2008
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
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentHashMap = java.util.concurrent.ConcurrentHashMap;
namespace org.simpleframework.xml.stream
{
	
	/// <summary> The <code>Builder</code> class is used to represent an XML style
	/// that can be applied to a serialized object. A style can be used to
	/// modify the element and attribute names for the generated document.
	/// Styles can be used to generate hyphenated or camel case XML.
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
	class Builder : Style
	{
		
		/// <summary> This is the cache for the constructed attribute values.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'attributes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Cache attributes;
		
		/// <summary> This is the cache for the constructed element values. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'elements '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Cache elements;
		
		/// <summary> This is the style object used to create the values used.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Style style;
		
		/// <summary> Constructor for the <code>Builder</code> object. This will cache
		/// values constructed from the inner style object, which allows the
		/// results from the style to retrieved quickly the second time.
		/// 
		/// </summary>
		/// <param name="style">this is the internal style object to be used
		/// </param>
		public Builder(Style style)
		{
			this.attributes = new Cache(this);
			this.elements = new Cache(this);
			this.style = style;
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
			System.String value_Renamed = attributes.get_Renamed(name);
			
			if (value_Renamed != null)
			{
				return value_Renamed;
			}
			value_Renamed = style.getAttribute(name);
			
			if (value_Renamed != null)
			{
				attributes.put(name, value_Renamed);
			}
			return value_Renamed;
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
			System.String value_Renamed = elements.get_Renamed(name);
			
			if (value_Renamed != null)
			{
				return value_Renamed;
			}
			value_Renamed = style.getElement(name);
			
			if (value_Renamed != null)
			{
				elements.put(name, value_Renamed);
			}
			return value_Renamed;
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
			attributes.put(name, value_Renamed);
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
			elements.put(name, value_Renamed);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Cache' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> The <code>Cache</code> object is used to cache the values 
		/// used to represent the styled attributes and elements. This 
		/// is a concurrent hash map so that styles can be used by more
		/// than one thread simultaneously.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class Cache:ConcurrentHashMap
		{
			/// <summary> Constructor for the <code>Cache</code> object. This will
			/// create a concurrent cache that can translate between the
			/// XML attributes and elements and the styled values.
			/// </summary>
			private void  InitBlock(Builder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				
			}
			private Builder enclosingInstance;
			public Builder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< String, String >
			public Cache(Builder enclosingInstance):base()
			{
				InitBlock(enclosingInstance);
			}
		}
	}
}