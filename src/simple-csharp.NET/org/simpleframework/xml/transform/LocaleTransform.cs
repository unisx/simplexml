/*
* LocaleTransform.java May 2007
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
	
	/// <summary> The <code>LocaleTransform</code> is used to transform locale
	/// values to and from string representations, which will be inserted
	/// in the generated XML document as the value place holder. The
	/// value must be readable and writable in the same format. Fields
	/// and methods annotated with the XML attribute annotation will use
	/// this to persist and retrieve the value to and from the XML source.
	/// <pre>
	/// 
	/// &#64;Attribute
	/// private Locale locale;
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
	class LocaleTransform : Transform
	{
		/// <summary> This is the pattern used to split the parts of the locale.</summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Locale >
		//UPGRADE_NOTE: Final was removed from the declaration of 'pattern '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Pattern pattern;
		
		/// <summary> Constructor for the <code>LocaleTransform</code> object. This
		/// is used to create a transform that will convert locales to and
		/// from string representations. The representations use the Java
		/// locale representation of language, country, and varient.
		/// </summary>
		public LocaleTransform()
		{
			InitBlock();
			this.pattern = Pattern.compile("_");
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="locale">the string representation of the date value 
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		public virtual System.Globalization.CultureInfo read(System.String locale)
		{
			System.String[] list = pattern.split(locale);
			
			if (list.Length < 1)
			{
				throw new InvalidFormatException("Invalid locale %s", locale);
			}
			return read(list);
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="locale">the string representation of the date value 
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		private System.Globalization.CultureInfo read(System.String[] locale)
		{
			System.String[] list = new System.String[]{"", "", ""};
			
			for (int i = 0; i < list.Length; i++)
			{
				if (i < locale.Length)
				{
					list[i] = locale[i];
				}
			}
			return new System.Globalization.CultureInfo(list[0] + "-" + list[1]);
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="locale">this is the value to be converted to a string
		/// 
		/// </param>
		/// <returns> this is the string representation of the given date
		/// </returns>
		public virtual System.String write(System.Globalization.CultureInfo locale)
		{
			return locale.ToString();
		}
	}
}