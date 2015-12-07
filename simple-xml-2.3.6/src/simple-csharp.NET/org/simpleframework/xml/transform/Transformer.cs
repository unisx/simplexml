/*
* Transformer.java May 2007
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
//UPGRADE_TODO: The type 'org.simpleframework.xml.util.Cache' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Cache = org.simpleframework.xml.util.Cache;
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>Transformer</code> object is used to convert strings to
	/// and from object instances. This is used during the serialization
	/// and deserialization process to transform types from the Java class
	/// libraries, as well as other types which do not contain XML schema
	/// annotations. Typically this will be used to transform primitive
	/// types to and from strings, such as <code>int</code> values.
	/// <pre>
	/// 
	/// &#64;Element
	/// private String[] value;
	/// 
	/// </pre>
	/// For example taking the above value the array of strings needs to 
	/// be converted in to a single string value that can be inserted in 
	/// to the element in such a way that in can be read later. In this
	/// case the serialized value of the string array would be as follows.
	/// <pre>
	/// 
	/// &lt;value&gt;one, two, three&lt;/value&gt;
	/// 
	/// </pre>
	/// Here each non-null string is inserted in to a comma separated  
	/// list of values, which can later be deserialized. Just to note the
	/// above array could be annotated with <code>ElementList</code> just
	/// as easily, in which case each entry would have its own element.
	/// The choice of which annotation to use is up to the developer. A
	/// more obvious benefit to transformations like this can be seen for
	/// values annotated with the <code>Attribute</code> annotation.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public class Transformer
	{
		
		/// <summary> This is used to cache all transforms matched to a given type.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private TransformCache cache;
		
		/// <summary> This is used to perform the matching of types to transforms.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'matcher '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Matcher matcher;
		
		/// <summary> This is used to cache the types that to not have a transform.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'error '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Cache error;
		
		/// <summary> Constructor for the <code>Transformer</code> object. This is
		/// used to create a transformer which will transform specified
		/// types using transforms loaded from the class path. Transforms
		/// are matched to types using the specified matcher object.
		/// 
		/// </summary>
		/// <param name="matcher">this is used to match types to transforms
		/// </param>
		public Transformer(Matcher matcher)
		{
			this.matcher = new DefaultMatcher(matcher);
			this.cache = new TransformCache();
			this.error = new WeakCache();
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="value">this is the string representation of the value
		/// </param>
		/// <param name="type">this is the type to convert the string value to
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		public virtual System.Object read(System.String value_Renamed, System.Type type)
		{
			Transform transform = lookup(type);
			
			if (transform == null)
			{
				throw new TransformException("Transform of %s not supported", type);
			}
			return transform.read(value_Renamed);
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="value">this is the value to be converted to a string
		/// </param>
		/// <param name="type">this is the type to convert to a string value
		/// 
		/// </param>
		/// <returns> this is the string representation of the given value
		/// </returns>
		public virtual System.String write(System.Object value_Renamed, System.Type type)
		{
			Transform transform = lookup(type);
			
			if (transform == null)
			{
				throw new TransformException("Transform of %s not supported", type);
			}
			return transform.write(value_Renamed);
		}
		
		/// <summary> This method is used to determine if the type specified can be
		/// transformed. This will use the <code>Matcher</code> to find a
		/// suitable transform, if one exists then this returns true, if
		/// not then this returns false. This is used during serialization
		/// to determine how to convert a field or method parameter. 
		/// 
		/// </summary>
		/// <param name="type">the type to determine whether its transformable
		/// 
		/// </param>
		/// <returns> true if the type specified can be transformed by this
		/// </returns>
		public virtual bool valid(System.Type type)
		{
			return lookup(type) != null;
		}
		
		/// <summary> This method is used to acquire a <code>Transform</code> for 
		/// the the specified type. If there is no transform for the type
		/// then this will return null. Once acquired once the transform
		/// is cached so that subsequent lookups will be performed faster.
		/// 
		/// </summary>
		/// <param name="type">the type to determine whether its transformable
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform lookup(System.Type type)
		{
			Transform transform = cache.fetch(type);
			
			if (transform != null)
			{
				return transform;
			}
			if (error.contains(type))
			{
				return null;
			}
			return match(type);
		}
		
		/// <summary> This method is used to acquire a <code>Transform</code> for 
		/// the the specified type. If there is no transform for the type
		/// then this will return null. Once acquired once the transform
		/// is cached so that subsequent lookups will be performed faster.
		/// 
		/// </summary>
		/// <param name="type">the type to determine whether its transformable
		/// 
		/// </param>
		/// <returns> this will return a transform for the specified type
		/// </returns>
		private Transform match(System.Type type)
		{
			Transform transform = matcher.match(type);
			
			if (transform != null)
			{
				cache.cache(type, transform);
			}
			else
			{
				error.cache(type, this);
			}
			return transform;
		}
	}
}