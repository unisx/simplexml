/*
* ConstructorCache.java July 2006
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
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentHashMap = java.util.concurrent.ConcurrentHashMap;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ConstructorCache</code> object is a typed hash map used
	/// to store the constructors used in converting a primitive type to
	/// a primitive value using a string. This cache is used to reduce the
	/// time taken to convert the primitives by reducing the amount of
	/// reflection required and eliminate type resolution.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.PrimitiveFactory">
	/// </seealso>
	class ConstructorCache:ConcurrentHashMap
	{
		/// <summary> Constructor for the <code>ConstructorCache</code> object. It
		/// is used to create a typed hash table that can be used to map
		/// the constructors used to convert strings to primitive types.
		/// If the class is unloaded then the cached constructor is lost.
		/// </summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Class, Constructor >
		public ConstructorCache():base()
		{
			InitBlock();
		}
	}
}