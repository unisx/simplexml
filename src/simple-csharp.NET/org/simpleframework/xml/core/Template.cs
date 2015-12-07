/*
* Template.java February 2001
*
* Copyright (C) 2001, Niall Gallagher <niallg@users.sf.net>
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
namespace org.simpleframework.xml.core
{
	
	/// <summary> This is primarily used to replace the <code>StringBuffer</code> 
	/// class, as a way for the <code>TemplateEngine</code> to store the 
	/// data for a specific region within the parse data that constitutes 
	/// a desired value. The methods are not synchronized so it enables 
	/// the characters to be taken quicker than the string buffer class.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Template
	{
		
		/// <summary> This is used to quicken <code>toString</code>.</summary>
		protected internal System.String cache;
		
		/// <summary> The characters that this buffer has accumulated.</summary>
		protected internal char[] buf;
		
		/// <summary> This is the number of characters this has stored.</summary>
		protected internal int count;
		
		/// <summary> Constructor for <code>Template</code>. The default 
		/// <code>Template</code> stores 16 characters before a
		/// <code>resize</code> is needed to append extra characters. 
		/// </summary>
		public Template():this(16)
		{
		}
		
		/// <summary> This creates a <code>Template</code> with a specific 
		/// default size. The buffer will be created the with the 
		/// length specified. The <code>Template</code> can grow 
		/// to accomodate a collection of characters larger the the 
		/// size spacified.
		/// 
		/// </summary>
		/// <param name="size">initial size of this <code>Template</code>
		/// </param>
		public Template(int size)
		{
			this.buf = new char[size];
		}
		
		/// <summary> This will add a <code>char</code> to the end of the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code>
		/// method which will allow the buffer to dynamically grow in 
		/// size to accomodate more characters.
		/// 
		/// </summary>
		/// <param name="c">the <code>char</code> to be appended
		/// </param>
		public virtual void  append(char c)
		{
			ensureCapacity(count + 1);
			buf[count++] = c;
		}
		
		/// <summary> This will add a <code>String</code> to the end of the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code> 
		/// method which will allow the buffer to dynamically grow in 
		/// size to accomodate large <code>String</code> objects.
		/// 
		/// </summary>
		/// <param name="str">the <code>String</code> to be appended to this
		/// </param>
		public virtual void  append(System.String str)
		{
			ensureCapacity(count + str.Length);
			SupportClass.GetCharsFromString(str, 0, str.Length, buf, count);
			count += str.Length;
		}
		
		/// <summary> This will add a <code>Template</code> to the end of this.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code> 
		/// method which will allow the buffer to dynamically grow in 
		/// size to accomodate large <code>Template</code> objects.
		/// 
		/// </summary>
		/// <param name="text">the <code>Template</code> to be appended 
		/// </param>
		public virtual void  append(Template text)
		{
			append(text.buf, 0, text.count);
		}
		
		/// <summary> This will add a <code>char</code> to the end of the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code> 
		/// method which will allow the buffer to dynamically grow in 
		/// size to accomodate large <code>char</code> arrays.
		/// 
		/// </summary>
		/// <param name="c">the <code>char</code> array to be appended to this
		/// </param>
		/// <param name="off">the read offset for the array    
		/// </param>
		/// <param name="len">the number of characters to append to this
		/// </param>
		public virtual void  append(char[] c, int off, int len)
		{
			ensureCapacity(count + len);
			Array.Copy(c, off, buf, count, len);
			count += len;
		}
		
		/// <summary> This will add a <code>String</code> to the end of the buffer.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code>
		/// method which will allow the buffer to dynamically grow in 
		/// size to accomodate large <code>String</code> objects.
		/// 
		/// </summary>
		/// <param name="str">the <code>String</code> to be appended to this
		/// </param>
		/// <param name="off">the read offset for the <code>String</code>
		/// </param>
		/// <param name="len">the number of characters to append to this
		/// </param>
		public virtual void  append(System.String str, int off, int len)
		{
			ensureCapacity(count + len);
			SupportClass.GetCharsFromString(str, off, len, buf, count);
			count += len;
		}
		
		
		/// <summary> This will add a <code>Template</code> to the end of this.
		/// The buffer will not overflow with repeated uses of the 
		/// <code>append</code>, it uses an <code>ensureCapacity</code> 
		/// method which will allow the buffer to dynamically grow in 
		/// size to accomodate large <code>Template</code> objects.
		/// 
		/// </summary>
		/// <param name="text">the <code>Template</code> to be appended 
		/// </param>
		/// <param name="off">the read offset for the <code>Template</code>
		/// </param>
		/// <param name="len">the number of characters to append to this
		/// </param>
		public virtual void  append(Template text, int off, int len)
		{
			append(text.buf, off, len);
		}
		
		/// <summary> This ensure that there is enough space in the buffer to allow
		/// for more characters to be added. If the buffer is already 
		/// larger than min then the buffer will not be expanded at all.
		/// 
		/// </summary>
		/// <param name="min">the minimum size needed for this buffer
		/// </param>
		protected internal virtual void  ensureCapacity(int min)
		{
			if (buf.Length < min)
			{
				int size = buf.Length * 2;
				int max = System.Math.Max(min, size);
				char[] temp = new char[max];
				Array.Copy(buf, 0, temp, 0, count);
				buf = temp;
			}
		}
		
		/// <summary> This will empty the <code>Template</code> so that the
		/// <code>toString</code> paramater will return <code>null</code>. 
		/// This is used so that the same <code>Template</code> can be 
		/// recycled for different tokens.
		/// </summary>
		public virtual void  clear()
		{
			cache = null;
			count = 0;
		}
		
		/// <summary> This will return the number of bytes that have been appended 
		/// to the <code>Template</code>. This will return zero after 
		/// the clear method has been invoked.
		/// 
		/// </summary>
		/// <returns> the number of characters within this buffer object
		/// </returns>
		public virtual int length()
		{
			return count;
		}
		
		/// <summary> This will return the characters that have been appended to the 
		/// <code>Template</code> as a <code>String</code> object.
		/// If the <code>String</code> object has been created before then
		/// a cached <code>String</code> object will be returned. This
		/// method will return <code>null</code> after clear is invoked.
		/// 
		/// </summary>
		/// <returns> the characters appended as a <code>String</code>
		/// </returns>
		public override System.String ToString()
		{
			return new System.String(buf, 0, count);
		}
	}
}