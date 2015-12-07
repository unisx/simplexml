/*
* TransformCache.java May 2007
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
using WeakCache = org.simpleframework.xml.util.WeakCache;
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>TransformCache</code> is used to cache transform objects. 
	/// It is used so the overhead of instantiating a transform each time
	/// an object of the specified type requires transformation is removed.
	/// Essentially this acts as a typedef for the generic hash map.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class TransformCache:WeakCache
	{
		/// <summary> Constructor for the <code>TransformCache</code> object. This is
		/// a concurrent hash table that maps class types to the transform
		/// objects they represent. To enable reloading of classes by the
		/// system this will drop the transform if the class in unloaded.
		/// </summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Class, Transform >
		public TransformCache():base()
		{
			InitBlock();
		}
	}
}