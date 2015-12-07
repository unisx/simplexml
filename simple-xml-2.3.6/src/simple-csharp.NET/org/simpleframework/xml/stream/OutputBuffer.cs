/*
* OutputBuffer.java June 2007
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
namespace org.simpleframework.xml.stream
{
	
	/// <summary> This is primarily used to replace the <code>StringBuffer</code> 
	/// class, as a way for the <code>Formatter</code> to store the start
	/// tag for an XML element. This enables the start tag of the current
	/// element to be removed without disrupting any of the other nodes
	/// within the document. Once the contents of the output buffer have
	/// been filled its contents can be emitted to the writer object.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class OutputBuffer
	{
		
		/// <summary> The characters that this buffer has accumulated.</summary>
		private StringBuilder text;
		
		/// <summary> Constructor for <code>OutputBuffer</code>. The default 
		/// <code>OutputBuffer</code> stores 16 characters before a
		/// resize is needed to append extra characters. 
		/// </summary>
		public OutputBuffer()
		{
			this.text = new StringBuilder();
		}
		
		/// <summary> This will add a <code>char</code> to the end of the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code>
		/// method which will allow the buffer to dynamically grow in 
		/// size to accommodate more characters.
		/// 
		/// </summary>
		/// <param name="ch">the character to be appended to the buffer
		/// </param>
		public virtual void  append(char ch)
		{
			text.append(ch);
		}
		
		/// <summary> This will add a <code>String</code> to the end of the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code> 
		/// method which will allow the buffer to dynamically grow in 
		/// size to accommodate large string objects.
		/// 
		/// </summary>
		/// <param name="value">the string to be appended to this output buffer
		/// </param>
		public virtual void  append(System.String value_Renamed)
		{
			text.append(value_Renamed);
		}
		
		/// <summary> This will add a <code>char</code> array to the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code> 
		/// method which will allow the buffer to dynamically grow in 
		/// size to accommodate large character arrays.
		/// 
		/// </summary>
		/// <param name="value">the character array to be appended to this
		/// </param>
		public virtual void  append(char[] value_Renamed)
		{
			text.append(value_Renamed, 0, value_Renamed.Length);
		}
		
		/// <summary> This will add a <code>char</code> array to the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code> 
		/// method which will allow the buffer to dynamically grow in 
		/// size to accommodate large character arrays.
		/// 
		/// </summary>
		/// <param name="value">the character array to be appended to this
		/// </param>
		/// <param name="off">the read offset for the array to begin reading
		/// </param>
		/// <param name="len">the number of characters to append to this
		/// </param>
		public virtual void  append(char[] value_Renamed, int off, int len)
		{
			text.append(value_Renamed, off, len);
		}
		
		/// <summary> This will add a <code>String</code> to the end of the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code>
		/// method which will allow the buffer to dynamically grow in 
		/// size to accommodate large string objects.
		/// 
		/// </summary>
		/// <param name="value">the string to be appended to the output buffer
		/// </param>
		/// <param name="off">the offset to begin reading from the string
		/// </param>
		/// <param name="len">the number of characters to append to this
		/// </param>
		public virtual void  append(System.String value_Renamed, int off, int len)
		{
			text.append(value_Renamed, off, len);
		}
		
		/// <summary> This method is used to write the contents of the buffer to the
		/// specified <code>Writer</code> object. This is used when the
		/// XML element is to be committed to the resulting XML document.
		/// 
		/// </summary>
		/// <param name="out">this is the writer to write the buffered text to
		/// 
		/// </param>
		/// <throws>  IOException thrown if there is an I/O problem </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual void  write(System.IO.StreamWriter out_Renamed)
		{
			out_Renamed.append(text);
		}
		
		/// <summary> This will empty the <code>OutputBuffer</code> so that it does
		/// not contain any content. This is used to that when the buffer
		/// is written to a specified <code>Writer</code> object nothing
		/// is written out. This allows XML elements to be removed.
		/// </summary>
		public virtual void  clear()
		{
			text.setLength(0);
		}
	}
}