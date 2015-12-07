/*
* Reflector.java April 2007
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
//UPGRADE_TODO: The type 'java.lang.reflect.GenericArrayType' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using GenericArrayType = java.lang.reflect.GenericArrayType;
//UPGRADE_TODO: The type 'java.lang.reflect.ParameterizedType' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ParameterizedType = java.lang.reflect.ParameterizedType;
//UPGRADE_TODO: The type 'java.lang.reflect.Type' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Type = java.lang.reflect.Type;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Reflector</code> object is used to determine the type
	/// of a generic type. This is used when the type of an XML annotation
	/// is not explicitly and the schema scanner needs to determine via
	/// reflection what the generic parameters are of a specific type. In
	/// particular this is used to determine the parameters within a list
	/// annotated with the <code>ElementList</code> annotation. This also
	/// has special handling for arrays within generic collections.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	sealed class Reflector
	{
		
		/// <summary> This method is used to acquire a generic parameter dependent 
		/// from the specified field. This will acquire the field class and
		/// attempt to extract the first generic parameter type from that  
		/// field. If there is a generic parameter then the class of that 
		/// parameter is returned from this method.
		/// 
		/// </summary>
		/// <param name="field">this is the field to acquire the dependent class
		/// 
		/// </param>
		/// <returns> this returns the generic parameter class declared
		/// </returns>
		public static System.Type getDependent(System.Reflection.FieldInfo field)
		{
			ParameterizedType type = getType(field);
			
			if (type != null)
			{
				return getClass(type);
			}
			return null;
		}
		
		/// <summary> This method is used to acquire generic parameter dependents 
		/// from the specified field. This will acquire the field class and
		/// attempt to extract all of the generic parameter types from that  
		/// field. If there is a generic parameter then the class of that 
		/// parameter is returned from this method.
		/// 
		/// </summary>
		/// <param name="field">this is the field to acquire the dependent types
		/// 
		/// </param>
		/// <returns> this returns the generic parameter classes declared
		/// </returns>
		public static System.Type[] getDependents(System.Reflection.FieldInfo field)
		{
			ParameterizedType type = getType(field);
			
			if (type != null)
			{
				return getClasses(type);
			}
			return new System.Type[]{};
		}
		
		/// <summary> This is used to acquire the parameterized types from the given
		/// field. If the field class has been parameterized then this will
		/// return the parameters that have been declared on that class.
		/// 
		/// </summary>
		/// <param name="field">this is the field to acquire the parameters from
		/// 
		/// </param>
		/// <returns> this will return the parameterized types for the field
		/// </returns>
		private static ParameterizedType getType(System.Reflection.FieldInfo field)
		{
			Type type = field.getGenericType();
			
			if (type is ParameterizedType)
			{
				return (ParameterizedType) type;
			}
			return null;
		}
		
		/// <summary> This method is used to acquire a generic parameter dependent 
		/// from the method return type. This will acquire the return type
		/// and attempt to extract the first generic parameter type from 
		/// that type. If there is a generic parameter then the class of 
		/// that parameter is returned from this method.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the dependent of   
		/// 
		/// </param>
		/// <returns> this returns the generic parameter class declared
		/// </returns>
		public static System.Type getReturnDependent(System.Reflection.MethodInfo method)
		{
			ParameterizedType type = getReturnType(method);
			
			if (type != null)
			{
				return getClass(type);
			}
			return null;
		}
		
		/// <summary> This method is used to acquire a generic parameter dependent 
		/// from the method return type. This will acquire the return type
		/// and attempt to extract the first generic parameter type from 
		/// that type. If there is a generic parameter then the class of 
		/// that parameter is returned from this method.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the dependent of   
		/// 
		/// </param>
		/// <returns> this returns the generic parameter class declared
		/// </returns>
		public static System.Type[] getReturnDependents(System.Reflection.MethodInfo method)
		{
			ParameterizedType type = getReturnType(method);
			
			if (type != null)
			{
				return getClasses(type);
			}
			return new System.Type[]{};
		}
		
		/// <summary> This is used to acquire the parameterized types from the given
		/// methods return class. If the return type class is parameterized
		/// then this will return the parameters that have been declared on
		/// that class, otherwise null is returned.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the parameters from
		/// 
		/// </param>
		/// <returns> this  returns the parameterized types for the method
		/// </returns>
		private static ParameterizedType getReturnType(System.Reflection.MethodInfo method)
		{
			Type type = method.getGenericReturnType();
			
			if (type is ParameterizedType)
			{
				return (ParameterizedType) type;
			}
			return null;
		}
		
		/// <summary> This method is used to acquire a generic parameter dependent 
		/// from the specified parameter type. This will acquire the type
		/// for the parameter at the specified index and attempt to extract
		/// the first generic parameter type from that type. If there is a
		/// generic parameter then the class of that parameter is returned
		/// from this method, otherwise null is returned.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the dependent of
		/// </param>
		/// <param name="index">this is the index to acquire the parameter from    
		/// 
		/// </param>
		/// <returns> this returns the generic parameter class declared
		/// </returns>
		public static System.Type getParameterDependent(System.Reflection.MethodInfo method, int index)
		{
			ParameterizedType type = getParameterType(method, index);
			
			if (type != null)
			{
				return getClass(type);
			}
			return null;
		}
		
		/// <summary> This method is used to acquire a generic parameter dependent 
		/// from the specified parameter type. This will acquire the type
		/// for the parameter at the specified index and attempt to extract
		/// the first generic parameter type from that type. If there is a
		/// generic parameter then the class of that parameter is returned
		/// from this method, otherwise null is returned.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the dependent of
		/// </param>
		/// <param name="index">this is the index to acquire the parameter from    
		/// 
		/// </param>
		/// <returns> this returns the generic parameter class declared
		/// </returns>
		public static System.Type[] getParameterDependents(System.Reflection.MethodInfo method, int index)
		{
			ParameterizedType type = getParameterType(method, index);
			
			if (type != null)
			{
				return getClasses(type);
			}
			return new System.Type[]{};
		}
		
		/// <summary> This method is used to acquire a generic parameter dependent 
		/// from the specified parameter type. This will acquire the type
		/// for the parameter at the specified index and attempt to extract
		/// the first generic parameter type from that type. If there is a
		/// generic parameter then the class of that parameter is returned
		/// from this method, otherwise null is returned.
		/// 
		/// </summary>
		/// <param name="factory">this is the constructor to acquire the dependent
		/// </param>
		/// <param name="index">this is the index to acquire the parameter from    
		/// 
		/// </param>
		/// <returns> this returns the generic parameter class declared
		/// </returns>
		public static System.Type getParameterDependent(System.Reflection.ConstructorInfo factory, int index)
		{
			ParameterizedType type = getParameterType(factory, index);
			
			if (type != null)
			{
				return getClass(type);
			}
			return null;
		}
		
		/// <summary> This method is used to acquire a generic parameter dependent 
		/// from the specified parameter type. This will acquire the type
		/// for the parameter at the specified index and attempt to extract
		/// the first generic parameter type from that type. If there is a
		/// generic parameter then the class of that parameter is returned
		/// from this method, otherwise null is returned.
		/// 
		/// </summary>
		/// <param name="factory">this is the constructor to acquire the dependent 
		/// </param>
		/// <param name="index">this is the index to acquire the parameter from    
		/// 
		/// </param>
		/// <returns> this returns the generic parameter class declared
		/// </returns>
		public static System.Type[] getParameterDependents(System.Reflection.ConstructorInfo factory, int index)
		{
			ParameterizedType type = getParameterType(factory, index);
			
			if (type != null)
			{
				return getClasses(type);
			}
			return new System.Type[]{};
		}
		
		/// <summary> This is used to acquire the parameterized types from the given
		/// methods parameter class at the specified index position. If the
		/// parameter class is parameterized this returns the parameters 
		/// that have been declared on that class.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the parameters from
		/// </param>
		/// <param name="index">this is the index to acquire the parameter from     
		/// 
		/// </param>
		/// <returns> this  returns the parameterized types for the method
		/// </returns>
		private static ParameterizedType getParameterType(System.Reflection.MethodInfo method, int index)
		{
			Type[] list = method.getGenericParameterTypes();
			
			if (list.length > index)
			{
				Type type = list[index];
				
				if (type is ParameterizedType)
				{
					return (ParameterizedType) type;
				}
			}
			return null;
		}
		
		/// <summary> This is used to acquire the parameterized types from the given
		/// constructors parameter class at the specified index position. If 
		/// the parameter class is parameterized this returns the parameters 
		/// that have been declared on that class.
		/// 
		/// </summary>
		/// <param name="factory">this is constructor method to acquire the parameters 
		/// </param>
		/// <param name="index">this is the index to acquire the parameter from     
		/// 
		/// </param>
		/// <returns> this  returns the parameterized types for the method
		/// </returns>
		private static ParameterizedType getParameterType(System.Reflection.ConstructorInfo factory, int index)
		{
			Type[] list = factory.getGenericParameterTypes();
			
			if (list.length > index)
			{
				Type type = list[index];
				
				if (type is ParameterizedType)
				{
					return (ParameterizedType) type;
				}
			}
			return null;
		}
		
		/// <summary> This is used to extract the class from the specified type. If
		/// there are no actual generic type arguments to the specified
		/// type then this will return null. Otherwise this will return 
		/// the actual class, regardless of whether the class is an array.
		/// 
		/// </summary>
		/// <param name="type">this is the type to extract the class from
		/// 
		/// </param>
		/// <returns> this returns the class type from the first parameter
		/// </returns>
		private static System.Type getClass(ParameterizedType type)
		{
			Type[] list = type.getActualTypeArguments();
			
			if (list.length > 0)
			{
				return getClass(list[0]);
			}
			return null;
		}
		
		/// <summary> This is used to extract the class from the specified type. If
		/// there are no actual generic type arguments to the specified
		/// type then this will return null. Otherwise this will return 
		/// the actual class, regardless of whether the class is an array.
		/// 
		/// </summary>
		/// <param name="type">this is the type to extract the class from
		/// 
		/// </param>
		/// <returns> this returns the class type from the first parameter
		/// </returns>
		private static System.Type[] getClasses(ParameterizedType type)
		{
			Type[] list = type.getActualTypeArguments();
			System.Type[] types = new System.Type[list.length];
			
			for (int i = 0; i < list.length; i++)
			{
				types[i] = getClass(list[i]);
			}
			return types;
		}
		
		/// <summary> This is used to extract the class from the specified type. If
		/// there are no actual generic type arguments to the specified
		/// type then this will return null. Otherwise this will return 
		/// the actual class, regardless of whether the class is an array.
		/// 
		/// </summary>
		/// <param name="type">this is the type to extract the class from
		/// 
		/// </param>
		/// <returns> this returns the class type from the first parameter
		/// </returns>
		private static System.Type getClass(Type type)
		{
			if (type is System.Type)
			{
				return (System.Type) type;
			}
			return getGenericClass(type);
		}
		
		/// <summary> This is used to extract the class from the specified type. If
		/// there are no actual generic type arguments to the specified
		/// type then this will return null. Otherwise this will return 
		/// the actual class, regardless of whether the class is an array.
		/// 
		/// </summary>
		/// <param name="type">this is the type to extract the class from
		/// 
		/// </param>
		/// <returns> this returns the class type from the first parameter
		/// </returns>
		private static System.Type getGenericClass(Type type)
		{
			if (type is GenericArrayType)
			{
				return getArrayClass(type);
			}
			return typeof(System.Object);
		}
		
		/// <summary> This is used to extract an array class from the specified. If
		/// a class can be extracted from the type then the array class 
		/// is created by reflective creating a zero length array with 
		/// the component type of the array and returning the array class.
		/// 
		/// </summary>
		/// <param name="type">this is the type to extract the class from
		/// 
		/// </param>
		/// <returns> this returns the class type from the array type
		/// </returns>
		private static System.Type getArrayClass(Type type)
		{
			GenericArrayType generic = (GenericArrayType) type;
			Type array = generic.getGenericComponentType();
			System.Type entry = getClass(array);
			
			if (entry != null)
			{
				return System.Array.CreateInstance(entry, 0).GetType();
			}
			return null;
		}
		
		/// <summary> This is used to acquire a bean name for a method or field name.
		/// A bean name is the name of a method or field with the first
		/// character decapitalized. An exception to this is when a method
		/// or field starts with an acronym, in such a case the name will
		/// remain unchanged from the original name.
		/// 
		/// </summary>
		/// <param name="name">this is the name to convert to a bean name
		/// 
		/// </param>
		/// <returns> this returns the bean value for the given name
		/// </returns>
		public static System.String getName(System.String name)
		{
			int length = name.Length;
			
			if (length > 0)
			{
				char[] array = name.ToCharArray();
				char first = array[0];
				
				if (!isAcronym(array))
				{
					array[0] = toLowerCase(first);
				}
				return new System.String(array);
			}
			return name;
		}
		
		/// <summary> This is used to determine if the provided array of characters
		/// represents an acronym. The array of characters is considered
		/// an acronym if the first and second characters are upper case.
		/// 
		/// </summary>
		/// <param name="array">the array to evaluate whether it is an acronym
		/// 
		/// </param>
		/// <returns> this returns true if the provided array is an acronym
		/// </returns>
		private static bool isAcronym(char[] array)
		{
			if (array.Length < 2)
			{
				return false;
			}
			if (!isUpperCase(array[0]))
			{
				return false;
			}
			return isUpperCase(array[1]);
		}
		
		/// <summary> This is used to convert the provided character to lower case.
		/// The character conversion is done for all unicode characters. 
		/// 
		/// </summary>
		/// <param name="value">this is the value that is to be converted
		/// 
		/// </param>
		/// <returns> this returns the provided character in lower case
		/// </returns>
		private static char toLowerCase(char value_Renamed)
		{
			return System.Char.ToLower(value_Renamed);
		}
		
		/// <summary> This is used to determine if the provided character is an
		/// upper case character. This can deal with unicode characters.
		/// 
		/// </summary>
		/// <param name="value">this is the value that is to be evaluated
		/// 
		/// </param>
		/// <returns> this returns true if the character is upper case
		/// </returns>
		private static bool isUpperCase(char value_Renamed)
		{
			return System.Char.IsUpper(value_Renamed);
		}
	}
}