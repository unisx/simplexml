/*
* ClassMap.java January 2010
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
using WeakCache = org.simpleframework.xml.util.WeakCache;
namespace org.simpleframework.xml.convert
{
	
	/// <summary> The <code>ClassCache</code> is used to cache converter bindings. 
	/// It is used so the overhead of instantiating a converters each time
	/// an object of the specified type requires conversion is removed.
	/// Essentially this acts as a typedef for the generic hash map.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ClassCache:WeakCache
	{
		/// <summary> Constructor for the <code>ClassCache</code> object. This is
		/// a concurrent hash table that maps class types to the converter
		/// classes they represent. To enable reloading of classes by the
		/// system this will drop the mapping if the class in unloaded.
		/// </summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Class, Class >
		public ClassCache():base()
		{
			InitBlock();
		}
	}
}