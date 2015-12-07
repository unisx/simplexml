/*
* ArrayMatcher.java May 2007
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
	
	/// <summary> The <code>ArrayMatcher</code> object performs matching of array
	/// types to array transforms. This uses the array component type to
	/// determine the transform to be used. All array transforms created
	/// by this will be <code>ArrayTransform</code> object instances. 
	/// These will use a type transform for the array component to add
	/// values to the individual array indexes. Also such transforms are
	/// typically treated as a comma separated list of individual values.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.ArrayTransform">
	/// </seealso>
	class ArrayMatcher : Matcher
	{
		
		/// <summary> This is the primary matcher that can resolve transforms.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'primary '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Matcher primary;
		
		/// <summary> Constructor for the <code>ArrayTransform</code> object. This
		/// is used to match array types to their respective transform
		/// using the <code>ArrayTransform</code> object. This will use
		/// a comma separated list of tokens to populate the array.
		/// 
		/// </summary>
		/// <param name="primary">this is the primary matcher to be used 
		/// </param>
		public ArrayMatcher(Matcher primary)
		{
			this.primary = primary;
		}
		
		/// <summary> This is used to match a <code>Transform</code> based on the
		/// array component type of an object to be transformed. This will
		/// attempt to match the transform using the fully qualified class
		/// name of the array component type. If a transform can not be
		/// found then this method will throw an exception.
		/// 
		/// </summary>
		/// <param name="type">this is the array to find the transform for
		/// 
		/// </param>
		/// <throws>  Exception thrown if a transform can not be matched </throws>
		public virtual Transform match(System.Type type)
		{
			System.Type entry = type.GetElementType();
			
			if (entry == typeof(char))
			{
				return new CharacterArrayTransform(entry);
			}
			if (entry == typeof(System.Char))
			{
				return new CharacterArrayTransform(entry);
			}
			if (entry == typeof(System.String))
			{
				return new StringArrayTransform();
			}
			return matchArray(entry);
		}
		
		/// <summary> This is used to match a <code>Transform</code> based on the
		/// array component type of an object to be transformed. This will
		/// attempt to match the transform using the fully qualified class
		/// name of the array component type. If a transform can not be
		/// found then this method will throw an exception.
		/// 
		/// </summary>
		/// <param name="entry">this is the array component type to be matched
		/// 
		/// </param>
		/// <throws>  Exception thrown if a transform can not be matched </throws>
		private Transform matchArray(System.Type entry)
		{
			Transform transform = primary.match(entry);
			
			if (transform == null)
			{
				return null;
			}
			return new ArrayTransform(transform, entry);
		}
	}
}