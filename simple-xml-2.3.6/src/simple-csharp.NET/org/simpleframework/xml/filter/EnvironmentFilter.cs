/*
* EnvironmentFilter.java May 2006
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
namespace org.simpleframework.xml.filter
{
	
	/// <summary> The <code>EnvironmentFilter</code> object is used to provide a 
	/// filter that will replace the specified values with an environment
	/// variable from the OS. This can be given a delegate filter which 
	/// can be used to resolve replacements should the value requested 
	/// not match an environment variable from the OS. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public class EnvironmentFilter : Filter
	{
		
		/// <summary> Filter delegated to if no environment variable is resolved.</summary>
		private Filter filter;
		
		/// <summary> Constructor for the <code>EnvironmentFilter</code> object. This 
		/// creates a filter that resolves replacements using environment
		/// variables. Should the environment variables not contain the
		/// requested mapping this will return a null value.
		/// </summary>
		public EnvironmentFilter():this(null)
		{
		}
		
		/// <summary> Constructor for the <code>EnvironmentFilter</code> object. This 
		/// creates a filter that resolves replacements using environment
		/// variables. Should the environment variables not contain the
		/// requested mapping this will delegate to the specified filter.
		/// 
		/// </summary>
		/// <param name="filter">the filter delegated to should resolution fail
		/// </param>
		public EnvironmentFilter(Filter filter)
		{
			this.filter = filter;
		}
		
		/// <summary> Replaces the text provided with the value resolved from the
		/// environment variables. If the environment variables fail this 
		/// will delegate to the specified <code>Filter</code> if it is 
		/// not a null object. If no match is found a null is returned.
		/// 
		/// </summary>
		/// <param name="text">this is the text value to be replaced
		/// 
		/// </param>
		/// <returns> this will return the replacement text resolved
		/// </returns>
		public virtual System.String replace(System.String text)
		{
			System.String value_Renamed = System.Environment.GetEnvironmentVariable(text);
			
			if (value_Renamed != null)
			{
				return value_Renamed;
			}
			if (filter != null)
			{
				return filter.replace(text);
			}
			return null;
		}
	}
}