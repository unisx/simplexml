/*
* RegistryBinder.java January 2010
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
	
	/// <summary> The <code>RegistryBinder</code> object is used acquire converters
	/// using a binding between a type and its converter. All converters
	/// instantiated are cached internally to ensure that the overhead
	/// of acquiring a converter is reduced. Converters are created on
	/// demand to ensure they are instantiated only if required.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.convert.Registry">
	/// </seealso>
	class RegistryBinder
	{
		
		/// <summary> This is used to instantiate and cache the converter objects.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ConverterFactory factory;
		
		/// <summary> This is used to cache bindings between types and converters.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ClassCache cache;
		
		/// <summary> Constructor for the <code>RegistryBinder</code> object. This 
		/// is used to create bindings between classes and the converters 
		/// that should be used to serialize and deserialize the instances. 
		/// All converters are instantiated once and cached for reuse.
		/// </summary>
		public RegistryBinder()
		{
			this.factory = new ConverterFactory();
			this.cache = new ClassCache();
		}
		
		/// <summary> This is used to acquire a <code>Converter</code> instance from
		/// this binder. All instances are cached to reduce the overhead
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
			System.Type result = cache.fetch(type);
			
			if (result != null)
			{
				return create(result);
			}
			return null;
		}
		
		/// <summary> This is used to acquire a <code>Converter</code> instance from
		/// this binder. All instances are cached to reduce the overhead
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
			return factory.getInstance(type);
		}
		
		/// <summary> This is used to register a binding between a type and the
		/// converter used to serialize and deserialize it. During the
		/// serialization process the converters are retrieved and 
		/// used to convert the object properties to XML.
		/// 
		/// </summary>
		/// <param name="type">this is the object type to bind to a converter
		/// </param>
		/// <param name="converter">this is the converter class to be used
		/// </param>
		public virtual void  bind(System.Type type, System.Type converter)
		{
			cache.cache(type, converter);
		}
	}
}