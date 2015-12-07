/*
* Registry.java January 2010
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
namespace org.simpleframework.xml.convert
{
	
	/// <summary> The <code>Registry</code> represents an object that is used to
	/// register bindings between a class and a converter implementation.
	/// Converter instances created by this registry are lazily created
	/// and cached so that they are instantiated only once. This ensures 
	/// that the overhead of serialization is reduced. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.convert.RegistryStrategy">
	/// </seealso>
	public class Registry
	{
		
		/// <summary> This is used to bind converter types to serializable types.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'binder '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private RegistryBinder binder;
		
		/// <summary> This is used to cache the converters based on object types.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ConverterCache cache;
		
		/// <summary> Constructor for the <code>Registry</code> object. This is used
		/// to create a registry between classes and the converters that
		/// should be used to serialize and deserialize the instances. All
		/// converters are instantiated once and cached for reuse.
		/// </summary>
		public Registry()
		{
			this.binder = new RegistryBinder();
			this.cache = new ConverterCache();
		}
		
		/// <summary> This is used to acquire a <code>Converter</code> instance from
		/// the registry. All instances are cache to reduce the overhead
		/// of lookups during the serialization process. Converters are
		/// lazily instantiated and so are only created if demanded.
		/// 
		/// </summary>
		/// <param name="type">this is the type to find the converter for
		/// 
		/// </param>
		/// <returns> this returns the converter instance for the type
		/// </returns>
		public virtual Converter lookup(System.Type type)
		{
			Converter converter = cache.fetch(type);
			
			if (converter == null)
			{
				return create(type);
			}
			return converter;
		}
		
		/// <summary> This is used to acquire a <code>Converter</code> instance from
		/// the registry. All instances are cached to reduce the overhead
		/// of lookups during the serialization process. Converters are
		/// lazily instantiated and so are only created if demanded.
		/// 
		/// </summary>
		/// <param name="type">this is the type to find the converter for
		/// 
		/// </param>
		/// <returns> this returns the converter instance for the type
		/// </returns>
		private Converter create(System.Type type)
		{
			Converter converter = binder.lookup(type);
			
			if (converter != null)
			{
				cache.cache(type, converter);
			}
			return converter;
		}
		
		/// <summary> This is used to register a binding between a type and the
		/// converter used to serialize and deserialize it. During the
		/// serialization process the converters are retrieved and 
		/// used to convert the object members to XML.
		/// 
		/// </summary>
		/// <param name="type">this is the object type to bind to a converter
		/// </param>
		/// <param name="converter">this is the converter class to be used
		/// 
		/// </param>
		/// <returns> this will return this registry instance to use
		/// </returns>
		public virtual Registry bind(System.Type type, System.Type converter)
		{
			if (type != null)
			{
				binder.bind(type, converter);
			}
			return this;
		}
		
		/// <summary> This is used to register a binding between a type and the
		/// converter used to serialize and deserialize it. During the
		/// serialization process the converters are retrieved and 
		/// used to convert the object properties to XML.
		/// 
		/// </summary>
		/// <param name="type">this is the object type to bind to a converter
		/// </param>
		/// <param name="converter">this is the converter instance to be used
		/// 
		/// </param>
		/// <returns> this will return this registry instance to use
		/// </returns>
		public virtual Registry bind(System.Type type, Converter converter)
		{
			if (type != null)
			{
				cache.cache(type, converter);
			}
			return this;
		}
	}
}