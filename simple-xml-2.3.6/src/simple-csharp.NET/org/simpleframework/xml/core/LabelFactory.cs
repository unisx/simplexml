/*
* LabelFactory.java July 2006
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
using Text = org.simpleframework.xml.Text;
using Version = org.simpleframework.xml.Version;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>LabelFactory</code> object is used to create instances of
	/// the <code>Label</code> object that can be used to convert an XML
	/// node into a Java object. Each label created requires the contact it
	/// represents and the XML annotation it is marked with.  
	/// <p>
	/// The <code>Label</code> objects created by this factory a selected
	/// using the XML annotation type. If the annotation type is not known
	/// the factory will throw an exception, otherwise a label instance
	/// is created that will expose the properties of the annotation.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	sealed class LabelFactory
	{
		
		/// <summary> Creates a <code>Label</code> using the provided contact and XML
		/// annotation. The label produced contains all information related
		/// to an object member. It knows the name of the XML entity, as
		/// well as whether it is required. Once created the converter can
		/// transform an XML node into Java object and vice versa.
		/// 
		/// </summary>
		/// <param name="contact">this is contact that the label is produced for
		/// </param>
		/// <param name="label">represents the XML annotation for the contact
		/// 
		/// </param>
		/// <returns> returns the label instantiated for the field
		/// </returns>
		public static Label getInstance(Contact contact, Annotation label)
		{
			Label value_Renamed = getLabel(contact, label);
			
			if (value_Renamed == null)
			{
				return value_Renamed;
			}
			return new CacheLabel(value_Renamed);
		}
		
		/// <summary> Creates a <code>Label</code> using the provided contact and XML
		/// annotation. The label produced contains all information related
		/// to an object member. It knows the name of the XML entity, as
		/// well as whether it is required. Once created the converter can
		/// transform an XML node into Java object and vice versa.
		/// 
		/// </summary>
		/// <param name="contact">this is contact that the label is produced for
		/// </param>
		/// <param name="label">represents the XML annotation for the contact
		/// 
		/// </param>
		/// <returns> returns the label instantiated for the field
		/// </returns>
		private static Label getLabel(Contact contact, Annotation label)
		{
			System.Reflection.ConstructorInfo factory = getConstructor(label);
			
			//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.isAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
			if (!factory.isAccessible())
			{
				//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.setAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
				factory.setAccessible(true);
			}
			return (Label) factory.newInstance(contact, label);
		}
		
		/// <summary> Creates a constructor that can be used to instantiate the label
		/// used to represent the specified annotation. The constructor
		/// created by this method takes two arguments, a contact object 
		/// and an <code>Annotation</code> of the type specified.
		/// 
		/// </summary>
		/// <param name="label">the XML annotation representing the label
		/// 
		/// </param>
		/// <returns> returns a constructor for instantiating the label 
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the annotation is not supported </throws>
		private static System.Reflection.ConstructorInfo getConstructor(Annotation label)
		{
			return getEntry(label).getConstructor();
		}
		
		/// <summary> Creates an entry that is used to select the constructor for the
		/// label. Each label must implement a constructor that takes a
		/// contact and the specific XML annotation for that field. If the
		/// annotation is not know this method throws an exception.
		/// 
		/// </summary>
		/// <param name="label">the XML annotation used to create the label
		/// 
		/// </param>
		/// <returns> this returns the entry used to create a suitable
		/// constructor for the label
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the annotation is not supported </throws>
		private static Entry getEntry(Annotation label)
		{
			if (label is Element)
			{
				return new Entry(typeof(ElementLabel), typeof(Element));
			}
			if (label is ElementList)
			{
				return new Entry(typeof(ElementListLabel), typeof(ElementList));
			}
			if (label is ElementArray)
			{
				return new Entry(typeof(ElementArrayLabel), typeof(ElementArray));
			}
			if (label is ElementMap)
			{
				return new Entry(typeof(ElementMapLabel), typeof(ElementMap));
			}
			if (label is Attribute)
			{
				return new Entry(typeof(AttributeLabel), typeof(Attribute));
			}
			if (label is Version)
			{
				return new Entry(typeof(VersionLabel), typeof(Version));
			}
			if (label is Text)
			{
				return new Entry(typeof(TextLabel), typeof(Text));
			}
			throw new PersistenceException("Annotation %s not supported", label);
		}
		
		/// <summary> The <code>Entry<code> object is used to create a constructor 
		/// that can be used to instantiate the correct label for the XML
		/// annotation specified. The constructor requires two arguments
		/// a <code>Contact</code> and the specified XML annotation.
		/// 
		/// </summary>
		/// <seealso cref="java.lang.reflect.Constructor">
		/// </seealso>
		private class Entry
		{
			
			/// <summary> This is the XML annotation type within the constructor.</summary>
			public System.Type argument;
			
			/// <summary> This is the label type that is to be instantiated.</summary>
			public System.Type label;
			
			/// <summary> Constructor for the <code>Entry</code> object. This pairs
			/// the label type with the XML annotation argument used within
			/// the constructor. This allows constructor to be selected.
			/// 
			/// </summary>
			/// <param name="label">this is the label type to be instantiated
			/// </param>
			/// <param name="argument">type that is used within the constructor
			/// </param>
			public Entry(System.Type label, System.Type argument)
			{
				this.argument = argument;
				this.label = label;
			}
			
			/// <summary> Creates the constructor used to instantiate the label for
			/// the XML annotation. The constructor returned will take two
			/// arguments, a contact and the XML annotation type. 
			/// 
			/// </summary>
			/// <returns> returns the constructor for the label object
			/// </returns>
			public System.Reflection.ConstructorInfo getConstructor()
			{
				return getConstructor(typeof(Contact));
			}
			
			/// <summary> Creates the constructor used to instantiate the label for
			/// the XML annotation. The constructor returned will take two
			/// arguments, a contact and the XML annotation type.
			/// 
			/// </summary>
			/// <param name="type">this is the XML annotation argument type used
			/// 
			/// </param>
			/// <returns> returns the constructor for the label object
			/// </returns>
			private System.Reflection.ConstructorInfo getConstructor(System.Type type)
			{
				return label.getConstructor(type, argument);
			}
		}
	}
}