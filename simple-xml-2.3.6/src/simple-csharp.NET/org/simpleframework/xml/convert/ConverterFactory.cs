/*
* ConverterFactory.java January 2010
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
	
	/// <summary> The <code>ConverterFactory</code> is used to instantiate objects
	/// based on a provided type or annotation. This provides a single
	/// point of creation for all converters within the framework. For
	/// performance all the instantiated converters are cached against
	/// the class for that converter. This ensures the converters can
	/// be acquired without the overhead of instantiation.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.convert.ConverterCache">
	/// </seealso>
	class ConverterFactory
	{
		
		/// <summary> This is the cache that is used to cache converter instances.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ConverterCache cache;
		
		/// <summary> Constructor for the <code>ConverterFactory</code> object. 
		/// This will create an internal cache which is used to cache all
		/// instantiations made by the factory. Caching the converters
		/// ensures there is no overhead with instantiations.
		/// </summary>
		public ConverterFactory()
		{
			this.cache = new ConverterCache();
		}
		
		/// <summary> This is used to instantiate the converter based on the type
		/// provided. If the type provided can not be instantiated for
		/// some reason then an exception is thrown from this method.
		/// 
		/// </summary>
		/// <param name="type">this is the converter type to be instantiated
		/// 
		/// </param>
		/// <returns> this returns an instance of the provided type
		/// </returns>
		public virtual Converter getInstance(System.Type type)
		{
			Converter converter = cache.fetch(type);
			
			if (converter == null)
			{
				return getConverter(type);
			}
			return converter;
		}
		
		/// <summary> This is used to instantiate the converter based on the type
		/// of the <code>Convert</code> annotation provided. If the type 
		/// can not be instantiated for some reason then an exception is 
		/// thrown from this method.
		/// 
		/// </summary>
		/// <param name="convert">this is the annotation containing the type
		/// 
		/// </param>
		/// <returns> this returns an instance of the provided type
		/// </returns>
		public virtual Converter getInstance(Convert convert)
		{
			System.Type type = convert.value_Renamed();
			
			if (type.IsInterface)
			{
				throw new ConvertException("Can not instantiate %s", type);
			}
			return getInstance(type);
		}
		
		/// <summary> This is used to instantiate the converter based on the type
		/// provided. If the type provided can not be instantiated for
		/// some reason then an exception is thrown from this method.
		/// 
		/// </summary>
		/// <param name="type">this is the converter type to be instantiated
		/// 
		/// </param>
		/// <returns> this returns an instance of the provided type
		/// </returns>
		private Converter getConverter(System.Type type)
		{
			System.Reflection.ConstructorInfo factory = getConstructor(type);
			
			if (factory == null)
			{
				throw new ConvertException("No default constructor for %s", type);
			}
			return getConverter(type, factory);
		}
		
		/// <summary> This is used to instantiate the converter based on the type
		/// provided. If the type provided can not be instantiated for
		/// some reason then an exception is thrown from this method.
		/// 
		/// </summary>
		/// <param name="type">this is the converter type to be instantiated
		/// </param>
		/// <param name="factory">this is the constructor used to instantiate
		/// 
		/// </param>
		/// <returns> this returns an instance of the provided type
		/// </returns>
		private Converter getConverter(System.Type type, System.Reflection.ConstructorInfo factory)
		{
			Converter converter = (Converter) factory.newInstance();
			
			if (converter != null)
			{
				cache.cache(type, converter);
			}
			return converter;
		}
		
		/// <summary> This is used to acquire the default no argument constructor
		/// for the the provided type. If the constructor is not accessible
		/// then it will be made accessible so that it can be instantiated.
		/// 
		/// </summary>
		/// <param name="type">this is the type to acquire the constructor for
		/// 
		/// </param>
		/// <returns> this returns the constructor for the type provided
		/// </returns>
		private System.Reflection.ConstructorInfo getConstructor(System.Type type)
		{
			System.Reflection.ConstructorInfo factory = type.getDeclaredConstructor();
			
			//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.isAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
			if (!factory.isAccessible())
			{
				//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.setAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
				factory.setAccessible(true);
			}
			return factory;
		}
	}
}