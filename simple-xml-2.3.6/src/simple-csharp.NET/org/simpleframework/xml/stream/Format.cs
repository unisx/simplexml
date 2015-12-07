/*
* Format.java July 2006
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
namespace org.simpleframework.xml.stream
{
	
	/// <summary> The <code>Format</code> object is used to provide information on 
	/// how a generated XML document should be structured. The information
	/// provided tells the formatter whether an XML prolog is required and
	/// the number of spaces that should be used for indenting. The prolog
	/// specified will be written directly before the XML document.
	/// <p>
	/// Should a <code>Format</code> be created with an indent of zero or
	/// less then no indentation is done, and the generated XML will be on
	/// the same line. The prolog can contain any legal XML heading, which
	/// can domain a DTD declaration and XML comments if required.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public class Format
	{
		/// <summary> This method returns the size of the indent to use for the XML
		/// generated. The indent size represents the number of spaces that
		/// are used for the indent, and indent of zero means no indenting.
		/// 
		/// </summary>
		/// <returns> returns the number of spaces to used for indenting
		/// </returns>
		virtual public int Indent
		{
			get
			{
				return indent;
			}
			
		}
		/// <summary> This method returns the prolog that is to be used at the start
		/// of the generated XML document. This allows a DTD or a version
		/// to be specified at the start of a document. If this returns
		/// null then no prolog is written to the start of the XML document.
		/// 
		/// </summary>
		/// <returns> this returns the prolog for the start of the document    
		/// </returns>
		virtual public System.String Prolog
		{
			get
			{
				return prolog;
			}
			
		}
		/// <summary> This is used to acquire the <code>Style</code> for the format.
		/// If no style has been set a default style is used, which does 
		/// not modify the attributes and elements that are used to build
		/// the resulting XML document.
		/// 
		/// </summary>
		/// <returns> this returns the style used for this format object
		/// </returns>
		virtual public Style Style
		{
			get
			{
				return style;
			}
			
		}
		
		/// <summary> Represents the prolog that appears in the generated XML.</summary>
		private System.String prolog;
		
		/// <summary> This is the style that is used internally by the format.</summary>
		private Style style;
		
		/// <summary> Represents the indent size to use for the generated XML.</summary>
		private int indent;
		
		/// <summary> Constructor for the <code>Format</code> object. This creates an
		/// object that is used to describe how the formatter should create
		/// the XML document. This constructor uses an indent size of three.
		/// </summary>
		public Format():this(3)
		{
		}
		
		/// <summary> Constructor for the <code>Format</code> object. This creates an
		/// object that is used to describe how the formatter should create
		/// the XML document. This constructor uses the specified indent
		/// size and a null prolog, which means no prolog is generated.
		/// 
		/// </summary>
		/// <param name="indent">this is the number of spaces used in the indent
		/// </param>
		public Format(int indent):this(indent, null, null)
		{
		}
		
		/// <summary> Constructor for the <code>Format</code> object. This creates an
		/// object that is used to describe how the formatter should create
		/// the XML document. This constructor uses the specified prolog 
		/// that is to be inserted at the start of the XML document.
		/// 
		/// </summary>
		/// <param name="prolog">this is the prolog for the generated XML document
		/// </param>
		public Format(System.String prolog):this(3, prolog)
		{
		}
		
		/// <summary> Constructor for the <code>Format</code> object. This creates an
		/// object that is used to describe how the formatter should create
		/// the XML document. This constructor uses the specified indent
		/// size and the text to use in the generated prolog.
		/// 
		/// </summary>
		/// <param name="indent">this is the number of spaces used in the indent
		/// </param>
		/// <param name="prolog">this is the prolog for the generated XML document
		/// </param>
		public Format(int indent, System.String prolog):this(indent, prolog, null)
		{
		}
		
		/// <summary> Constructor for the <code>Format</code> object. This creates an
		/// object that is used to describe how the formatter should create
		/// the XML document. This constructor uses the specified style
		/// to style the attributes and elements of the XML document.
		/// 
		/// </summary>
		/// <param name="style">this is the style to apply to the format object
		/// </param>
		public Format(Style style):this(3, null, style)
		{
		}
		
		/// <summary> Constructor for the <code>Format</code> object. This creates an
		/// object that is used to describe how the formatter should create
		/// the XML document. This constructor uses the specified indent
		/// size and the style provided to style the XML document.
		/// 
		/// </summary>
		/// <param name="indent">this is the number of spaces used in the indent
		/// </param>
		/// <param name="style">this is the style to apply to the format object
		/// </param>
		public Format(int indent, Style style):this(indent, null, style)
		{
		}
		
		/// <summary> Constructor for the <code>Format</code> object. This creates an
		/// object that is used to describe how the formatter should create
		/// the XML document. This constructor uses the specified indent
		/// size and the text to use in the generated prolog.
		/// 
		/// </summary>
		/// <param name="indent">this is the number of spaces used in the indent
		/// </param>
		/// <param name="prolog">this is the prolog for the generated XML document
		/// </param>
		/// <param name="style">this is the style to apply to the format object
		/// </param>
		public Format(int indent, System.String prolog, Style style)
		{
			this.prolog = prolog;
			this.indent = indent;
			this.style = style;
		}
	}
}