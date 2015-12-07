/*
* PrimitiveMatcher.java May 2007
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
	
	/// <summary> The <code>PrimitiveMatcher</code> object is used to resolve the
	/// primitive types to a stock transform. This will basically use
	/// a transform that is used with the primitives language object.
	/// This will always return a suitable transform for a primitive.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.DefaultMatcher">
	/// </seealso>
	class PrimitiveMatcher : Matcher
	{
		
		/// <summary> Constructor for the <code>PrimitiveMatcher</code> object. The
		/// primitive matcher is used to resolve a transform instance to
		/// convert primitive types to an from strings. If a match is not
		/// found with this matcher then an exception is thrown.
		/// </summary>
		public PrimitiveMatcher():base()
		{
		}
		
		/// <summary> This method is used to match the specified type to primitive
		/// transform implementations. If this is given a primitive then
		/// it will always return a suitable <code>Transform</code>. If
		/// however it is given an object type an exception is thrown.
		/// 
		/// </summary>
		/// <param name="type">this is the primitive type to be transformed
		/// 
		/// </param>
		/// <returns> this returns a stock transform for the primitive
		/// </returns>
		public virtual Transform match(System.Type type)
		{
			if (type == typeof(int))
			{
				return new IntegerTransform();
			}
			if (type == typeof(bool))
			{
				return new BooleanTransform();
			}
			if (type == typeof(long))
			{
				return new LongTransform();
			}
			if (type == typeof(double))
			{
				return new DoubleTransform();
			}
			if (type == typeof(float))
			{
				return new FloatTransform();
			}
			if (type == typeof(short))
			{
				return new ShortTransform();
			}
			if (type == typeof(sbyte))
			{
				return new ByteTransform();
			}
			if (type == typeof(char))
			{
				return new CharacterTransform();
			}
			return null;
		}
	}
}