/*
* StackFilter.java May 2006
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
	
	/// <summary> The <code>StackFilter</code> object provides a filter that can
	/// be given a collection of filters which can be used to resolve a
	/// replacement. The order of the resolution used for this filter
	/// is last in first used. This order allows the highest priority
	/// filter to be added last within the stack. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher 
	/// </author>
	public class StackFilter : Filter
	{
		
		/// <summary> This is used to store the filters that are used.</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Stack < Filter > stack;
		
		/// <summary> Constructor for the <code>StackFilter</code> object. This will
		/// create an empty filter that initially resolves null for all
		/// replacements requested. As filters are pushed into the stack
		/// the <code>replace</code> method can resolve replacements. 
		/// </summary>
		public StackFilter()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			this.stack = new Stack < Filter >();
		}
		
		/// <summary> This pushes the the provided <code>Filter</code> on to the top
		/// of the stack. The last filter pushed on to the stack has the
		/// highes priority in the resolution of a replacement value.
		/// 
		/// </summary>
		/// <param name="filter">this is a filter to be pushed on to the stack
		/// </param>
		public virtual void  push(Filter filter)
		{
			stack.push(filter);
		}
		
		/// <summary> Replaces the text provided with the value resolved from the
		/// stacked filters. This attempts to resolve a replacement from
		/// the top down. So the last <code>Filter</code> pushed on to
		/// the stack will be the first filter queried for a replacement.
		/// 
		/// </summary>
		/// <param name="text">this is the text value to be replaced
		/// 
		/// </param>
		/// <returns> this will return the replacement text resolved
		/// </returns>
		public virtual System.String replace(System.String text)
		{
			for (int i = stack.size(); --i >= 0; )
			{
				System.String value_Renamed = stack.get_Renamed(i).replace(text);
				
				if (value_Renamed != null)
				{
					return value_Renamed;
				}
			}
			return null;
		}
	}
}