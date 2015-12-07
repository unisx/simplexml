/*
* PlatformFilter.java May 2006
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
	
	/// <summary> The <code>PlatformFilter</code> object makes use of all filter
	/// types this resolves user specified properties first, followed
	/// by system properties, and finally environment variables. This
	/// filter will be the default filter used by most applications as
	/// it can make use of all values within the application platform.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher 
	/// </author>
	public class PlatformFilter:StackFilter
	{
		
		/// <summary> Constructor for the <code>PlatformFilter</code> object. This
		/// adds a filter which can be used to resolve environment 
		/// variables followed by one that can be used to resolve system
		/// properties and finally one to resolve user specified values.
		/// </summary>
		public PlatformFilter():this(null)
		{
		}
		
		/// <summary> Constructor for the <code>PlatformFilter</code> object. This
		/// adds a filter which can be used to resolve environment 
		/// variables followed by one that can be used to resolve system
		/// properties and finally one to resolve user specified values.
		/// 
		/// </summary>
		/// <param name="map">this is a map contain the user mappings
		/// </param>
		public PlatformFilter(System.Collections.IDictionary map)
		{
			this.push(new EnvironmentFilter());
			this.push(new SystemFilter());
			this.push(new MapFilter(map));
		}
	}
}