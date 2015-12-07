/*
* TransformException.java May 2007
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
using PersistenceException = org.simpleframework.xml.core.PersistenceException;
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>TransformException</code> is thrown if a problem occurs
	/// during the transformation of an object. This can be thrown either
	/// because a transform could not be found for a specific type or
	/// because the format of the text value had an invalid structure.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	[Serializable]
	public class TransformException:PersistenceException
	{
		public TransformException()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			base(String.format(text, list));
			base(String.format(text, list), cause);
		}
		
		/// <summary> Constructor for the <code>TransformException</code> object. 
		/// This constructor takes a format string an a variable number of 
		/// object arguments, which can be inserted into the format string. 
		/// 
		/// </summary>
		/// <param name="text">a format string used to present the error message
		/// </param>
		/// <param name="list">a list of arguments to insert into the string
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public TransformException(String text, Object...list)
		
		/// <summary> Constructor for the <code>TransformException</code> object. 
		/// This constructor takes a format string an a variable number of 
		/// object arguments, which can be inserted into the format string. 
		/// 
		/// </summary>
		/// <param name="cause">the source exception this is used to represent
		/// </param>
		/// <param name="text">a format string used to present the error message
		/// </param>
		/// <param name="list">a list of arguments to insert into the stri 
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public TransformException(Throwable cause, String text, Object...list)
	}
}