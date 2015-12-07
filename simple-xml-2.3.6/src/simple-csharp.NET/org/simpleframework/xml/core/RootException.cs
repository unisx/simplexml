/*
* RootException.java July 2006
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
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>RootException</code> is thrown if the <code>Root</code>
	/// annotation is missing from a root object that is to be serialized
	/// or deserialized. Not all objects require a root annotation, only
	/// those objects that are to be deserialized to or serialized from
	/// an <code>ElementList</code> field and root objects that are to be
	/// deserialized or serialized directly from the persister.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	[Serializable]
	public class RootException:PersistenceException
	{
		public RootException()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			base(text, list);
			base(cause, text, list);
		}
		
		/// <summary> Constructor for the <code>RootException</code> exception. This
		/// constructor takes a format string an a variable number of object
		/// arguments, which can be inserted into the format string. 
		/// 
		/// </summary>
		/// <param name="text">a format string used to present the error message
		/// </param>
		/// <param name="list">a list of arguments to insert into the string
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public RootException(String text, Object...list)
		
		/// <summary> Constructor for the <code>RootException</code> exception. This
		/// constructor takes a format string an a variable number of object
		/// arguments, which can be inserted into the format string. 
		/// 
		/// </summary>
		/// <param name="cause">the source exception this is used to represent
		/// </param>
		/// <param name="text">a format string used to present the error message
		/// </param>
		/// <param name="list">a list of arguments to insert into the string 
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public RootException(Throwable cause, String text, Object...list)
	}
}