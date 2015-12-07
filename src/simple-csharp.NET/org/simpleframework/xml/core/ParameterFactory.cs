/*
* ParameterFactory.java July 2006
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
//UPGRADE_TODO: The type 'java.lang.annotation.Annotation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Annotation = java.lang.annotation.Annotation;
using Attribute = org.simpleframework.xml.Attribute;
using Element = org.simpleframework.xml.Element;
using ElementArray = org.simpleframework.xml.ElementArray;
using ElementList = org.simpleframework.xml.ElementList;
using ElementMap = org.simpleframework.xml.ElementMap;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ParameterFactory</code> object is used to create instances 
	/// of the <code>Parameter</code> object. Each parameter created can be
	/// used to validate against the annotated fields and methods to ensure
	/// that the annotations are compatible. 
	/// <p>
	/// The <code>Parameter</code> objects created by this are selected
	/// using the XML annotation type. If the annotation type is not known
	/// the factory will throw an exception, otherwise a parameter instance
	/// is created that will expose the properties of the annotation.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	sealed class ParameterFactory
	{
		
		/// <summary> Creates a <code>Parameter</code> using the provided constructor
		/// and the XML annotation. The parameter produced contains all 
		/// information related to the constructor parameter. It knows the 
		/// name of the XML entity, as well as the type. 
		/// 
		/// </summary>
		/// <param name="method">this is the constructor the parameter exists in
		/// </param>
		/// <param name="label">represents the XML annotation for the contact
		/// 
		/// </param>
		/// <returns> returns the parameter instantiated for the field
		/// </returns>
		public static Parameter getInstance(System.Reflection.ConstructorInfo method, Annotation label, int index)
		{
			System.Reflection.ConstructorInfo factory = getConstructor(label);
			
			//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.isAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
			if (!factory.isAccessible())
			{
				//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.setAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
				factory.setAccessible(true);
			}
			return (Parameter) factory.newInstance(method, label, index);
		}
		
		/// <summary> Creates a constructor that is used to instantiate the parameter
		/// used to represent the specified annotation. The constructor
		/// created by this method takes three arguments, a constructor, 
		/// an annotation, and the parameter index.
		/// 
		/// </summary>
		/// <param name="label">the XML annotation representing the label
		/// 
		/// </param>
		/// <returns> returns a constructor for instantiating the parameter 
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the annotation is not supported </throws>
		private static System.Reflection.ConstructorInfo getConstructor(Annotation label)
		{
			return getEntry(label).getConstructor();
		}
		
		/// <summary> Creates an entry that is used to select the constructor for the
		/// parameter. Each parameter must implement a constructor that takes 
		/// a constructor, and annotation, and the index of the parameter. If
		/// the annotation is not know this method throws an exception.
		/// 
		/// </summary>
		/// <param name="label">the XML annotation used to create the parameter
		/// 
		/// </param>
		/// <returns> this returns the entry used to create a suitable
		/// constructor for the parameter
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the annotation is not supported </throws>
		private static Entry getEntry(Annotation label)
		{
			if (label is Element)
			{
				return new Entry(typeof(ElementParameter), typeof(Element));
			}
			if (label is ElementList)
			{
				return new Entry(typeof(ElementListParameter), typeof(ElementList));
			}
			if (label is ElementArray)
			{
				return new Entry(typeof(ElementArrayParameter), typeof(ElementArray));
			}
			if (label is ElementMap)
			{
				return new Entry(typeof(ElementMapParameter), typeof(ElementMap));
			}
			if (label is Attribute)
			{
				return new Entry(typeof(AttributeParameter), typeof(Attribute));
			}
			throw new PersistenceException("Annotation %s not supported", label);
		}
		
		/// <summary> The <code>Entry<code> object is used to create a constructor 
		/// that can be used to instantiate the correct parameter for the 
		/// XML annotation specified. The constructor requires three 
		/// arguments, the constructor, the annotation, and the index.
		/// 
		/// </summary>
		/// <seealso cref="java.lang.reflect.Constructor">
		/// </seealso>
		private class Entry
		{
			private void  InitBlock()
			{
				return create.Constructor;
			}
			/// <summary> Creates the constructor used to instantiate the parameter
			/// for the XML annotation. The constructor returned will take 
			/// two arguments, a contact and the XML annotation type. 
			/// 
			/// </summary>
			/// <returns> returns the constructor for the parameter object
			/// </returns>
			public System.Reflection.ConstructorInfo Constructor
			{
				get
				{
					return Constructor;
				}
				
			}
			
			/// <summary> This is the parameter type that is to be instantiated.</summary>
			public System.Type create;
			
			/// <summary> This is the XML annotation type within the constructor.</summary>
			public System.Type type;
			
			/// <summary> Constructor for the <code>Entry</code> object. This pairs
			/// the parameter type with the annotation argument used within
			/// the constructor. This allows constructor to be selected.
			/// 
			/// </summary>
			/// <param name="create">this is the label type to be instantiated
			/// </param>
			/// <param name="type">the type that is used within the constructor
			/// </param>
			public Entry(System.Type create, System.Type type)
			{
				this.create = create;
				this.type = type;
			}
			
			/// <summary> Creates the constructor used to instantiate the parameter 
			/// for the XML annotation. The constructor returned will take 
			/// three arguments, a constructor, an annotation and a type.
			/// 
			/// </summary>
			/// <param name="types">these are the arguments for the constructor
			/// 
			/// </param>
			/// <returns> returns the constructor for the parameter object
			/// </returns>
			private System.Reflection.ConstructorInfo getConstructor_Renamed_Field;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			(Class...types) throws Exception
		}
	}
}