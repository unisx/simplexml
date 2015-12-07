/*
* ConvertException.java January 2010
*
* Copyright (C) 2010, Niall Gallagher <niallg@users.sf.net>
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
namespace org.simpleframework.xml.convert
{
	
	/// <summary> The <code>ConvertException</code> is thrown when there is a
	/// problem converting an object. Such an exception can occur if an
	/// annotation is use incorrectly, or if a <code>Converter</code>
	/// can not be instantiated. Messages provided to this exception are
	/// formatted similar to the <code>PrintStream.printf</code> method.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	[Serializable]
	public class ConvertException:System.Exception
	{
		public ConvertException()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			base(String.format(text, list));
		}
		
		/// <summary> Constructor for the <code>ConvertException</code> object. 
		/// This constructor takes a format string an a variable number of 
		/// object arguments, which can be inserted into the format string. 
		/// 
		/// </summary>
		/// <param name="text">a format string used to present the error message
		/// </param>
		/// <param name="list">a list of arguments to insert into the string
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public ConvertException(String text, Object...list)
	}
}