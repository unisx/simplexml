/*
* EmptyMatcher.java May 2007
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
using Matcher = org.simpleframework.xml.transform.Matcher;
//UPGRADE_TODO: The type 'org.simpleframework.xml.transform.Transform' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Transform = org.simpleframework.xml.transform.Transform;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>EmptyMatcher</code> object is used as a delegate type 
	/// that is used when no user specific matcher is specified. This
	/// ensures that no transform is resolved for a specified type, and
	/// allows the normal resolution of the stock transforms.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.Transformer">
	/// </seealso>
	class EmptyMatcher : Matcher
	{
		
		/// <summary> This method is used to return a null value for the transform.
		/// Returning a null value allows the normal resolution of the
		/// stock transforms to be used when no matcher is specified.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is expecting a transform
		/// 
		/// </param>
		/// <returns> this transform will always return a null value
		/// </returns>
		public virtual Transform match(System.Type type)
		{
			return null;
		}
	}
}