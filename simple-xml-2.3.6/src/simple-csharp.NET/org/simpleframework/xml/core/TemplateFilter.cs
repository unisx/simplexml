/*
* TemplateFilter.java May 2005
*
* Copyright (C) 2005, Niall Gallagher <niallg@users.sf.net>
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
using Filter = org.simpleframework.xml.filter.Filter;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>TemplateFilter</code> class is used to provide variables
	/// to the template engine. This template acquires variables from two
	/// different sources. Firstly this will consult the user contextual
	/// <code>Context</code> object, which can contain variables that have
	/// been added during the deserialization process. If a variable is
	/// not present from this context it asks the <code>Filter</code> that
	/// has been specified by the user.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class TemplateFilter : Filter
	{
		
		/// <summary> This is the template context object used by the persister.</summary>
		private Context context;
		
		/// <summary> This is the filter object provided to the persister.</summary>
		private Filter filter;
		
		/// <summary> Constructor for the <code>TemplateFilter</code> object. This
		/// creates a filter object that acquires template values from
		/// two different contexts. Firstly the <code>Context</code> is
		/// queried for a variables followed by the <code>Filter</code>.
		/// 
		/// </summary>
		/// <param name="context">this is the context object for the persister
		/// </param>
		/// <param name="filter">the filter that has been given to the persister
		/// </param>
		public TemplateFilter(Context context, Filter filter)
		{
			this.context = context;
			this.filter = filter;
		}
		
		/// <summary> This will acquire the named variable value if it exists. If
		/// the named variable cannot be found in either the context or
		/// the user specified filter then this returns null.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the variable to acquire
		/// 
		/// </param>
		/// <returns> this returns the value mapped to the variable name
		/// </returns>
		public virtual System.String replace(System.String name)
		{
			System.Object value_Renamed = context.getAttribute(name);
			
			if (value_Renamed != null)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return value_Renamed.ToString();
			}
			return filter.replace(name);
		}
	}
}