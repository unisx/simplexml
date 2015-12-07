/*
* Instantiator.java July 2006
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
using Value = org.simpleframework.xml.strategy.Value;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Instantiator</code> is used to instantiate types that 
	/// will leverage a constructor cache to quickly create the objects.
	/// This is used by the various object factories to return type 
	/// instances that can be used by converters to create the objects 
	/// that will later be deserialized.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Instance">
	/// </seealso>
	class Instantiator
	{
		
		/// <summary> This is used to cache the constructors for the given types.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ConstructorCache cache;
		
		/// <summary> Constructor for the <code>Instantiator</code> object. This will
		/// create a constructor cache that can be used to cache all of 
		/// the constructors instantiated for the required types. 
		/// </summary>
		public Instantiator()
		{
			this.cache = new ConstructorCache();
		}
		
		/// <summary> This will create an <code>Instance</code> that can be used
		/// to instantiate objects of the specified class. This leverages
		/// an internal constructor cache to ensure creation is quicker.
		/// 
		/// </summary>
		/// <param name="value">this contains information on the object instance
		/// 
		/// </param>
		/// <returns> this will return an object for instantiating objects
		/// </returns>
		public virtual Instance getInstance(Value value_Renamed)
		{
			return new ValueInstance(this, value_Renamed);
		}
		
		/// <summary> This will create an <code>Instance</code> that can be used
		/// to instantiate objects of the specified class. This leverages
		/// an internal constructor cache to ensure creation is quicker.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be instantiated
		/// 
		/// </param>
		/// <returns> this will return an object for instantiating objects
		/// </returns>
		public virtual Instance getInstance(System.Type type)
		{
			return new ClassInstance(this, type);
		}
		
		/// <summary> This method will instantiate an object of the provided type. If
		/// the object or constructor does not have public access then this
		/// will ensure the constructor is accessible and can be used.
		/// 
		/// </summary>
		/// <param name="type">this is used to ensure the object is accessible
		/// 
		/// </param>
		/// <returns> this returns an instance of the specific class type
		/// </returns>
		public virtual System.Object getObject(System.Type type)
		{
			System.Reflection.ConstructorInfo method = cache.get_Renamed(type);
			
			if (method == null)
			{
				method = type.getDeclaredConstructor();
				
				//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.isAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
				if (!method.isAccessible())
				{
					//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.setAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
					method.setAccessible(true);
				}
				cache.put(type, method);
			}
			return method.newInstance();
		}
	}
}