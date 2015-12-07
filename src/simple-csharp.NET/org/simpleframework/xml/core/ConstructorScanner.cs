/*
* ConstructorScanner.java July 2009
*
* Copyright (C) 2009, Niall Gallagher <niallg@users.sf.net>
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
//UPGRADE_TODO: The type 'java.lang.annotation.Annotation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Annotation = java.lang.annotation.Annotation;
using Attribute = org.simpleframework.xml.Attribute;
using Element = org.simpleframework.xml.Element;
using ElementArray = org.simpleframework.xml.ElementArray;
using ElementList = org.simpleframework.xml.ElementList;
using ElementMap = org.simpleframework.xml.ElementMap;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ConstructorScanner</code> object is used to scan all 
	/// all constructors that have XML annotations for their parameters. 
	/// parameters. Each constructor scanned is converted in to a 
	/// <code>Builder</code> object. In order to ensure consistency
	/// amongst the annotated parameters each named parameter must have
	/// the exact same type and annotation attributes across the 
	/// constructors. This ensures a consistent XML representation.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Scanner">
	/// </seealso>
	class ConstructorScanner
	{
		/// <summary> This is used to create the object instance. It does this by
		/// either delegating to the default no argument constructor or by
		/// using one of the annotated constructors for the object. This
		/// allows deserialized values to be injected in to the created
		/// object if that is required by the class schema.
		/// 
		/// </summary>
		/// <returns> this returns the creator for the class object
		/// </returns>
		virtual public Creator Creator
		{
			get
			{
				return new ClassCreator(list, index, primary);
			}
			
		}
		
		/// <summary> This contains a list of all the builders for the class.</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Builder > list;
		
		/// <summary> This represents the default no argument constructor used.</summary>
		private Builder primary;
		
		/// <summary> This is used to acquire a parameter by the parameter name.</summary>
		private Index index;
		
		/// <summary> This is the type that is scanner for annotated constructors.</summary>
		private System.Type type;
		
		/// <summary> Constructor for the <code>ConstructorScanner</code> object. 
		/// This is used to scan the specified class for constructors that
		/// can be used to instantiate the class. Only constructors that
		/// have all parameters annotated will be considered.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be scanned
		/// </param>
		public ConstructorScanner(System.Type type)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			this.list = new ArrayList < Builder >();
			this.index = new Index(type);
			this.type = type;
			this.scan(type);
		}
		
		/// <summary> This is used to scan the specified class for constructors that
		/// can be used to instantiate the class. Only constructors that
		/// have all parameters annotated will be considered.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be scanned
		/// </param>
		private void  scan(System.Type type)
		{
			System.Reflection.ConstructorInfo[] array = type.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly);
			
			if (!isInstantiable(type))
			{
				throw new ConstructorException("Can not construct inner %s", type);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Constructor factory: array)
			{
				Index index = new Index(type);
				
				if (!type.IsPrimitive)
				{
					scan(factory, index);
				}
			}
		}
		
		/// <summary> This is used to scan the specified constructor for annotations
		/// that it contains. Each parameter annotation is evaluated and 
		/// if it is an XML annotation it is considered to be a valid
		/// parameter and is added to the parameter map.
		/// 
		/// </summary>
		/// <param name="factory">this is the constructor that is to be scanned
		/// </param>
		/// <param name="map">this is the parameter map that contains parameters
		/// </param>
		private void  scan(System.Reflection.ConstructorInfo factory, Index map)
		{
			Annotation[][] labels = factory.getParameterAnnotations();
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Constructor.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] types = factory.GetParameters();
			
			for (int i = 0; i < types.Length; i++)
			{
				for (int j = 0; j < labels[i].length; j++)
				{
					Parameter value_Renamed = process(factory, labels[i][j], i);
					
					if (value_Renamed != null)
					{
						System.String name = value_Renamed.Name;
						
						if (map.containsKey(name))
						{
							throw new PersistenceException("Parameter '%s' is a duplicate in %s", name, factory);
						}
						index.put(name, value_Renamed);
						map.put(name, value_Renamed);
					}
				}
			}
			if (types.Length == map.size())
			{
				build(factory, map);
			}
		}
		
		/// <summary> This is used to build the <code>Builder</code> object that is
		/// to be used to instantiate the object. The builder contains 
		/// the constructor at the parameters in the declaration order.
		/// 
		/// </summary>
		/// <param name="factory">this is the constructor that is to be scanned
		/// </param>
		/// <param name="map">this is the parameter map that contains parameters
		/// </param>
		private void  build(System.Reflection.ConstructorInfo factory, Index map)
		{
			Builder builder = new Builder(factory, map);
			
			if (builder.Default)
			{
				primary = builder;
			}
			list.add(builder);
		}
		
		/// <summary> This is used to create a <code>Parameter</code> object which is
		/// used to represent a parameter to a constructor. Each parameter
		/// contains an annotation an the index it appears in.
		/// 
		/// </summary>
		/// <param name="factory">this is the constructor the parameter is in
		/// </param>
		/// <param name="label">this is the annotation used for the parameter
		/// </param>
		/// <param name="ordinal">this is the position the parameter appears at
		/// 
		/// </param>
		/// <returns> this returns the parameter for the constructor
		/// </returns>
		private Parameter process(System.Reflection.ConstructorInfo factory, Annotation label, int ordinal)
		{
			if (label is Attribute)
			{
				return create(factory, label, ordinal);
			}
			if (label is ElementList)
			{
				return create(factory, label, ordinal);
			}
			if (label is ElementArray)
			{
				return create(factory, label, ordinal);
			}
			if (label is ElementMap)
			{
				return create(factory, label, ordinal);
			}
			if (label is Element)
			{
				return create(factory, label, ordinal);
			}
			return null;
		}
		
		/// <summary> This is used to create a <code>Parameter</code> object which is
		/// used to represent a parameter to a constructor. Each parameter
		/// contains an annotation an the index it appears in.
		/// 
		/// </summary>
		/// <param name="factory">this is the constructor the parameter is in
		/// </param>
		/// <param name="label">this is the annotation used for the parameter
		/// </param>
		/// <param name="ordinal">this is the position the parameter appears at
		/// 
		/// </param>
		/// <returns> this returns the parameter for the constructor
		/// </returns>
		private Parameter create(System.Reflection.ConstructorInfo factory, Annotation label, int ordinal)
		{
			Parameter value_Renamed = ParameterFactory.getInstance(factory, label, ordinal);
			System.String name = value_Renamed.Name;
			
			if (index.containsKey(name))
			{
				validate(value_Renamed, name);
			}
			return value_Renamed;
		}
		
		/// <summary> This is used to validate the parameter against all the other
		/// parameters for the class. Validating each of the parameters
		/// ensures that the annotations for the parameters remain
		/// consistent throughout the class.
		/// 
		/// </summary>
		/// <param name="parameter">this is the parameter to be validated
		/// </param>
		/// <param name="name">this is the name of the parameter to validate
		/// </param>
		private void  validate(Parameter parameter, System.String name)
		{
			Parameter other = index.get_Renamed(name);
			Annotation label = other.Annotation;
			
			if (!parameter.Annotation.equals(label))
			{
				throw new MethodException("Annotations do not match for '%s' in %s", name, type);
			}
			System.Type expect = other.Type;
			
			if (expect != parameter.Type)
			{
				throw new MethodException("Method types do not match for '%s' in %s", name, type);
			}
		}
		
		/// <summary> This is used to determine if the class is an inner class. If
		/// the class is a inner class and not static then this returns
		/// false. Only static inner classes can be instantiated using
		/// reflection as they do not require a "this" argument.
		/// 
		/// </summary>
		/// <param name="type">this is the class that is to be evaluated
		/// 
		/// </param>
		/// <returns> this returns true if the class is a static inner
		/// </returns>
		private bool isInstantiable(System.Type type)
		{
			//UPGRADE_ISSUE: Method 'java.lang.Class.getModifiers' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetModifiers'"
			int modifiers = type.getModifiers();
			
			//UPGRADE_ISSUE: Method 'java.lang.reflect.Modifier.isStatic' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectModifier'"
			if (Modifier.isStatic(modifiers))
			{
				return true;
			}
			return !type.isMemberClass();
		}
	}
}