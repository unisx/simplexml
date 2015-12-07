/*
* GregorialCalendarTransform.java May 2007
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
	
	/// <summary> The <code>DateTransform</code> is used to transform calendar
	/// values to and from string representations, which will be inserted
	/// in the generated XML document as the value place holder. The
	/// value must be readable and writable in the same format. Fields
	/// and methods annotated with the XML attribute annotation will use
	/// this to persist and retrieve the value to and from the XML source.
	/// <pre>
	/// 
	/// &#64;Attribute
	/// private GregorianCalendar date;
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
	class GregorianCalendarTransform : Transform
	{
		/// <summary> This is the date transform used to parse and format dates.</summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< GregorianCalendar >
		//UPGRADE_NOTE: Final was removed from the declaration of 'transform '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private DateTransform transform;
		
		/// <summary> Constructor for the <code>GregorianCalendarTransform</code> 
		/// object. This is used to create a transform using a default 
		/// date format pattern. The format chosen for the default date 
		/// uses <code>2007-05-02 12:22:10.000 GMT</code> like dates.
		/// </summary>
		public GregorianCalendarTransform():this(typeof(System.DateTime))
		{
		}
		
		/// <summary> Constructor for the <code>GregorianCalendarTransform</code> 
		/// object. This is used to create a transform using a default 
		/// date format pattern. The format should typically contain 
		/// enough information to create the date using a different 
		/// locale or time zone between read and write operations.
		/// 
		/// </summary>
		/// <param name="type">this is the type of date to be transformed
		/// </param>
		public GregorianCalendarTransform(System.Type type)
		{
			InitBlock();
			this.transform = new DateTransform(type);
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="date">the string representation of the date value 
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		public virtual System.Globalization.GregorianCalendar read(System.String date)
		{
			return read(transform.read(date));
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="date">the string representation of the date value 
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private System.Globalization.GregorianCalendar read(ref System.DateTime date)
		{
			System.Globalization.GregorianCalendar calendar = new System.Globalization.GregorianCalendar();
			
			//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			if (date != null)
			{
				//UPGRADE_TODO: The differences in the format  of parameters for method 'java.util.Calendar.setTime'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				SupportClass.CalendarManager.manager.SetDateTime(calendar, date);
			}
			return calendar;
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="date">this is the value to be converted to a string
		/// 
		/// </param>
		/// <returns> this is the string representation of the given date
		/// </returns>
		public virtual System.String write(System.Globalization.GregorianCalendar date)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Calendar.getTime' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return transform.write(SupportClass.CalendarManager.manager.GetDateTime(date));
		}
	}
}