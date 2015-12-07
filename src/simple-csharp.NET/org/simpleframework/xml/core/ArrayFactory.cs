/*
* ArrayFactory.java July 2006
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
using Type = org.simpleframework.xml.strategy.Type;
using Value = org.simpleframework.xml.strategy.Value;
using InputNode = org.simpleframework.xml.stream.InputNode;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ArrayFactory</code> is used to create object array
	/// types that are compatible with the field type. This simply
	/// requires the type of the array in order to instantiate that
	/// array. However, this also performs a check on the field type 
	/// to ensure that the array component types are compatible.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ArrayFactory:Factory
	{
		/// <summary> This is used to extract the component type for the array class
		/// this factory represents. This is used when an array is to be
		/// instantiated. If the class provided to the factory is not an
		/// array then this will throw an exception.
		/// 
		/// </summary>
		/// <returns> this returns the component type for the array
		/// </returns>
		private System.Type ComponentType
		{
			get
			{
				System.Type type = Type;
				
				if (!type.IsArray)
				{
					throw new InstantiationException("The %s not an array", type);
				}
				return type.GetElementType();
			}
			
		}
		
		/// <summary> Constructor for the <code>ArrayFactory</code> object. This is
		/// given the array component type as taken from the field type 
		/// of the source object. Each request for an array will return 
		/// an array which uses a compatible component type.
		/// 
		/// </summary>
		/// <param name="context">this is the context object for serialization
		/// </param>
		/// <param name="type">the array component type for the field object
		/// </param>
		public ArrayFactory(Context context, Type type):base(context, type)
		{
		}
		
		/// <summary> This is used to create a default instance of the field type. It
		/// is up to the subclass to determine how to best instantiate an
		/// object of the field type that best suits. This is used when the
		/// empty value is required or to create the default type instance.
		/// 
		/// </summary>
		/// <returns> a type which is used to instantiate the collection     
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getInstance()
		{
			System.Type type = ComponentType;
			
			if (type != null)
			{
				return System.Array.CreateInstance(type, 0);
			}
			return null;
		}
		
		/// <summary> Creates the array type to use. This will use the provided
		/// XML element to determine the array type and provide a means
		/// for creating an array with the <code>Value</code> object. If
		/// the array size cannot be determined an exception is thrown.
		/// 
		/// </summary>
		/// <param name="node">this is the input node for the array element
		/// 
		/// </param>
		/// <returns> the object array type used for the instantiation
		/// </returns>
		public virtual Instance getInstance(InputNode node)
		{
			Value value_Renamed = getOverride(node);
			
			if (value_Renamed == null)
			{
				throw new ElementException("Array length required for %s", this.type);
			}
			System.Type type = value_Renamed.Type;
			
			return getInstance(value_Renamed, type);
		}
		
		/// <summary> Creates the array type to use. This will use the provided
		/// XML element to determine the array type and provide a means
		/// for creating an array with the <code>Value</code> object. If
		/// the array types are not compatible an exception is thrown.
		/// 
		/// </summary>
		/// <param name="value">this is the type object with the array details
		/// </param>
		/// <param name="type">this is the entry type for the array instance    
		/// 
		/// </param>
		/// <returns> this object array type used for the instantiation  
		/// </returns>
		private Instance getInstance(Value value_Renamed, System.Type type)
		{
			System.Type expect = ComponentType;
			
			if (!expect.IsAssignableFrom(type))
			{
				throw new InstantiationException("Array of type %s cannot hold %s", expect, type);
			}
			return new ArrayInstance(value_Renamed);
		}
	}
}