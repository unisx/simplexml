/*
* ElementListParameter.java July 2009
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
using ElementList = org.simpleframework.xml.ElementList;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ElementListParameter</code> represents a constructor 
	/// parameter. It contains the XML annotation used on the parameter
	/// as well as the name of the parameter and its position index.
	/// A parameter is used to validate against the annotated methods 
	/// and fields and also to determine the deserialized values that
	/// should be injected in to the constructor to instantiate it.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ElementListParameter : Parameter
	{
		/// <summary> This is used to acquire the name of the parameter that this
		/// represents. The name is determined using annotation and 
		/// the name attribute of that annotation, if one is provided.
		/// 
		/// </summary>
		/// <returns> this returns the name of the annotated parameter
		/// </returns>
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		/// <summary> This is used to acquire the annotated type class. The class
		/// is the type that is to be deserialized from the XML. This
		/// is used to validate against annotated fields and methods.
		/// 
		/// </summary>
		/// <returns> this returns the type used for the parameter
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Constructor.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return factory.GetParameters()[index];
			}
			
		}
		/// <summary> This is used to acquire the annotation that is used for the
		/// parameter. The annotation provided will be an XML annotation
		/// such as the <code>Element</code> or <code>Attribute</code>
		/// annotation.
		/// 
		/// </summary>
		/// <returns> this returns the annotation used on the parameter
		/// </returns>
		virtual public Annotation Annotation
		{
			get
			{
				return contact.Annotation;
			}
			
		}
		/// <summary> This returns the index position of the parameter in the
		/// constructor. This is used to determine the order of values
		/// that are to be injected in to the constructor.
		/// 
		/// </summary>
		/// <returns> this returns the index for the parameter
		/// </returns>
		virtual public int Index
		{
			get
			{
				return index;
			}
			
		}
		
		/// <summary> This is the constructor the parameter was declared in.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Reflection.ConstructorInfo factory;
		
		/// <summary> This is the contact used to determine the parameter name.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'contact '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Contact contact;
		
		/// <summary> This is the label that will create the parameter name. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Label label;
		
		/// <summary> This is the actual name that has been determined.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String name;
		
		/// <summary> This is the index that the parameter was declared at.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int index;
		
		/// <summary> Constructor for the <code>ElementListParameter</code> object.
		/// This is used to create a parameter that can be used to 
		/// determine a consistent name using the provided XML annotation.
		/// 
		/// </summary>
		/// <param name="factory">this is the constructor the parameter is in
		/// </param>
		/// <param name="value">this is the annotation used for the parameter
		/// </param>
		/// <param name="index">this is the index the parameter appears at
		/// </param>
		public ElementListParameter(System.Reflection.ConstructorInfo factory, ElementList value_Renamed, int index)
		{
			this.contact = new Contact(value_Renamed, factory, index);
			this.label = new ElementListLabel(contact, value_Renamed);
			this.name = label.getName();
			this.factory = factory;
			this.index = index;
		}
		
		/// <summary> The <code>Contact</code> represents a contact object that is
		/// to be used for a label in order to extract a parameter name.
		/// The parameter name is taken from the XML annotation.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class Contact:ParameterContact
		{
			/// <summary> Constructor for the <code>Contact</code> object. This is 
			/// used to create an object that acts like an adapter so that
			/// the label can create a consistent name for the parameter.
			/// 
			/// </summary>
			/// <param name="label">this is the annotation for the parameter
			/// </param>
			/// <param name="factory">this is the constructor the parameter is in
			/// </param>
			/// <param name="index">this is the index for the parameter
			/// </param>
			private void  InitBlock()
			{
				
			}
			/// <summary> This returns the name of the parameter as taken from the XML
			/// annotation. The name provided here is taken by the label
			/// and used to compose a name consistent with how fields and
			/// methods are named by the system.
			/// 
			/// </summary>
			/// <returns> this returns the name of the annotated parameter
			/// </returns>
			override public System.String Name
			{
				get
				{
					return label.name();
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< ElementList >
			public Contact(ElementList label, System.Reflection.ConstructorInfo factory, int index):base(label, factory, index)
			{
			}
		}
	}
}