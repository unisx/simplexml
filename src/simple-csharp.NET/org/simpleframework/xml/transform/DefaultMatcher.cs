/*
* DefaultMatcher.java May 2007
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
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>DefaultMatcher</code> is a delegation object that uses
	/// several matcher implementations to correctly resolve both the
	/// stock <code>Transform</code> implementations and implementations
	/// that have been overridden by the user with a custom matcher. This
	/// will perform the resolution of the transform using the specified 
	/// matcher, if this results in no transform then this will look for
	/// a transform within the collection of implementations.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.Transformer">
	/// </seealso>
	class DefaultMatcher : Matcher
	{
		
		/// <summary> Matcher used to resolve stock transforms for primitive types.</summary>
		private Matcher primitive;
		
		/// <summary> Matcher used to resolve user specified transform overrides.</summary>
		private Matcher matcher;
		
		/// <summary> Matcher used to resolve all the core Java object transforms.</summary>
		private Matcher stock;
		
		/// <summary> Matcher used to resolve transforms for array type objects.</summary>
		private Matcher array;
		
		/// <summary> Constructor for the <code>DefaultMatcher</code> object. This
		/// performs resolution of <code>Transform</code> implementations 
		/// using the specified matcher. If that matcher fails to resolve
		/// a suitable transform then the stock implementations are used.
		/// 
		/// </summary>
		/// <param name="matcher">this is the user specified matcher object
		/// </param>
		public DefaultMatcher(Matcher matcher)
		{
			this.primitive = new PrimitiveMatcher();
			this.stock = new PackageMatcher();
			this.array = new ArrayMatcher(this);
			this.matcher = matcher;
		}
		
		/// <summary> This is used to match a <code>Transform</code> for the given
		/// type. If a transform cannot be resolved this this will throw an
		/// exception to indicate that resolution of a transform failed. A
		/// transform is resolved by first searching for a transform within
		/// the user specified matcher then searching the stock transforms.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a transform object for
		/// 
		/// </param>
		/// <returns> this returns a transform used to transform the type
		/// </returns>
		public virtual Transform match(System.Type type)
		{
			Transform value_Renamed = matcher.match(type);
			
			if (value_Renamed != null)
			{
				return value_Renamed;
			}
			return matchType(type);
		}
		
		/// <summary> This is used to match a <code>Transform</code> for the given
		/// type. If a transform cannot be resolved this this will throw an
		/// exception to indicate that resolution of a transform failed. A
		/// transform is resolved by first searching for a transform within
		/// the user specified matcher then searching the stock transforms.
		/// 
		/// </summary>
		/// <param name="type">this is the type to resolve a transform object for
		/// 
		/// </param>
		/// <returns> this returns a transform used to transform the type
		/// </returns>
		private Transform matchType(System.Type type)
		{
			if (type.IsArray)
			{
				return array.match(type);
			}
			if (type.IsPrimitive)
			{
				return primitive.match(type);
			}
			return stock.match(type);
		}
	}
}