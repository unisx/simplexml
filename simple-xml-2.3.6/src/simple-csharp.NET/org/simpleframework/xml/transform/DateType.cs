/*
* DateType.java May 2007
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
	
	/// <summary> The <code>DateType</code> enumeration provides a set of known date
	/// formats supported by the date transformer. This allows the XML
	/// representation of a date to come in several formats, from most 
	/// accurate to least. Enumerating the dates ensures that resolution
	/// of the format is fast by enabling inspection of the date string. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	enum DateType
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	
	/// <summary> This is the default date format used by the date transform.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	FULL(yyyy-MM-dd HH:mm:ss.S z),
	
	/// <summary> This is the date type without millisecond resolution.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	LONG(yyyy-MM-dd HH:mm:ss z),
	
	/// <summary> This date type enables only the specific date to be used.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	NORMAL(yyyy-MM-dd z),
	
	/// <summary> This is the shortest format that relies on the date locale.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SHORT(yyyy-MM-dd);
	
	/// <summary> This is the date formatter that is used to parse the date.</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private DateFormat format;
	
	/// <summary> Constructor for the <code>DateType</code> enumeration. This
	/// will accept a simple date format pattern, which is used to
	/// parse an input string and convert it to a usable date.
	/// 
	/// </summary>
	/// <param name="format">this is the format to use to parse the date
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private DateType(String format)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		this.format = new DateFormat(format);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Acquires the date format from the date type. This is then 
	/// used to parse the date string and convert it to a usable
	/// date. The format returned is synchronized for safety.
	/// 
	/// </summary>
	/// <returns> this returns the date format to be used
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private DateFormat getFormat()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return format;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> This is used to convert the date to a string value. The 
	/// string value can then be embedded in to the generated XML in
	/// such a way that it can be recovered as a <code>Date</code>
	/// when the value is transformed by the date transform.
	/// 
	/// </summary>
	/// <param name="date">this is the date that is converted to a string
	/// 
	/// </param>
	/// <returns> this returns the string to represent the date
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static String getText(Date date) throws Exception
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		DateFormat format = FULL.getFormat();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return format.getText(date);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> This is used to convert the string to a date value. The 
	/// date value can then be recovered from the generated XML by
	/// parsing the text with one of the known date formats. This
	/// allows bidirectional transformation of dates to strings.
	/// 
	/// </summary>
	/// <param name="text">this is the date that is converted to a date
	/// 
	/// </param>
	/// <returns> this returns the date parsed from the string value
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static Date getDate(String text) throws Exception
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		DateType type = getType(text);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	DateFormat format = type.getFormat();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return format.getDate(text);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> This is used to acquire a date type using the specified text
	/// as input. This will perform some checks on the raw string to
	/// match it to the appropriate date type. Resolving the date type
	/// in this way ensures that only one date type needs to be used.
	/// 
	/// </summary>
	/// <param name="text">this is the text to be matched with a date type
	/// 
	/// </param>
	/// <returns> the most appropriate date type for the given string
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static DateType getType(String text)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		int length = text.length();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(length > 23)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return FULL;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	if(length > 20)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return LONG;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	if(length > 11)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return NORMAL;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return SHORT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> The <code>DateFormat</code> provides a synchronized means for
	/// using the simple date format object. It ensures that should 
	/// there be many threads trying to gain access to the formatter 
	/// that they will not collide causing a race condition.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	private class DateFormat
	{
		
		/// <summary> This is the simple date format used to parse the string.</summary>
		//UPGRADE_ISSUE: Class 'java.text.SimpleDateFormat' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javatextSimpleDateFormat'"
		private SimpleDateFormat format;
		
		/// <summary> Constructor for the <code>DateFormat</code> object. This will
		/// wrap a simple date format, providing access to the conversion
		/// functions which allow date to string and string to date.
		/// 
		/// </summary>
		/// <param name="format">this is the pattern to use for the date type
		/// </param>
		public DateFormat(System.String format)
		{
			//UPGRADE_ISSUE: Constructor 'java.text.SimpleDateFormat.SimpleDateFormat' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javatextSimpleDateFormat'"
			this.format = new SimpleDateFormat(format);
		}
		
		/// <summary> This is used to provide a transformation from a date to a string.
		/// It ensures that there is a bidirectional transformation process
		/// which allows dates to be serialized and deserialized with XML.
		/// 
		/// </summary>
		/// <param name="date">this is the date to be converted to a string value
		/// 
		/// </param>
		/// <returns> returns the string that has be converted from a date
		/// </returns>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'getText'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual System.String getText(ref System.DateTime date)
		{
			lock (this)
			{
				return SupportClass.FormatDateTime(format, date);
			}
		}
		
		/// <summary> This is used to provide a transformation from a string to a date.
		/// It ensures that there is a bidirectional transformation process
		/// which allows dates to be serialized and deserialized with XML.
		/// 
		/// </summary>
		/// <param name="text">this is the string to be converted to a date value
		/// 
		/// </param>
		/// <returns> returns the date that has be converted from a string
		/// </returns>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'getDate'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual System.DateTime getDate(System.String text)
		{
			lock (this)
			{
				return System.DateTime.Parse(text, format);
			}
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}