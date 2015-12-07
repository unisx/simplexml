/*
* MethodContact.java April 2007
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
//UPGRADE_TODO: The type 'java.lang.annotation.Annotation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Annotation = java.lang.annotation.Annotation;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>MethodContact</code> object is acts as a contact that
	/// can set and get data to and from an object using methods. This 
	/// requires a get method and a set method that share the same class
	/// type for the return and parameter respectively.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.MethodScanner">
	/// </seealso>
	class MethodContact : Contact
	{
		private void  InitBlock()
		{
			T result = get_Renamed_Field.Annotation;
			
			if (type == label.annotationType())
			{
				return (T) label;
			}
			if (result == null && set_Renamed_Field != null)
			{
				return set_Renamed_Field.Annotation;
			}
			return result;
		}
		/// <summary> This is used to determine if the annotated contact is for a
		/// read only variable. A read only variable is a field that
		/// can be set from within the constructor such as a blank final
		/// variable. It can also be a method with no set counterpart.
		/// 
		/// </summary>
		/// <returns> this returns true if the contact is a constant one
		/// </returns>
		virtual public bool ReadOnly
		{
			get
			{
				return set_Renamed_Field == null;
			}
			
		}
		/// <summary> This is the annotation associated with the point of contact.
		/// This will be an XML annotation that describes how the contact
		/// should be serialized and deserialized from the object.
		/// 
		/// </summary>
		/// <returns> this provides the annotation associated with this
		/// </returns>
		virtual public Annotation Annotation
		{
			get
			{
				return label;
			}
			
		}
		/// <summary> This provides the dependent class for the contact. This will
		/// actually represent a generic type for the actual type. For
		/// contacts that use a <code>Collection</code> type this will
		/// be the generic type parameter for that collection.
		/// 
		/// </summary>
		/// <returns> this returns the dependent type for the contact
		/// </returns>
		virtual public System.Type Dependent
		{
			get
			{
				return item;
			}
			
		}
		/// <summary> This provides the dependent classes for the contact. This will
		/// typically represent a generic types for the actual type. For
		/// contacts that use a <code>Map</code> type this will be the 
		/// generic type parameter for that map type declaration.
		/// 
		/// </summary>
		/// <returns> this returns the dependent type for the contact
		/// </returns>
		virtual public System.Type[] Dependents
		{
			get
			{
				return items;
			}
			
		}
		
		/// <summary> This is the label that marks both the set and get methods.</summary>
		private Annotation label;
		
		/// <summary> This is the set method which is used to set the value.</summary>
		private MethodPart set_Renamed_Field;
		
		/// <summary> This is the dependent types as taken from the get method.</summary>
		private System.Type[] items;
		
		/// <summary> This is the dependent type as taken from the get method.</summary>
		private System.Type item;
		
		/// <summary> This is the type associated with this point of contact.</summary>
		private System.Type type;
		
		/// <summary> This is the get method which is used to get the value.</summary>
		private System.Reflection.MethodInfo get_Renamed_Field;
		
		/// <summary> This represents the name of the method for this contact.</summary>
		private System.String name;
		
		/// <summary> Constructor for the <code>MethodContact</code> object. This is
		/// used to compose a point of contact that makes use of a get and
		/// set method on a class. The specified methods will be invoked
		/// during the serialization process to get and set values.
		/// 
		/// </summary>
		/// <param name="get">this forms the get method for the object
		/// </param>
		public MethodContact(MethodPart get_Renamed):this(get_Renamed, null)
		{
		}
		
		/// <summary> Constructor for the <code>MethodContact</code> object. This is
		/// used to compose a point of contact that makes use of a get and
		/// set method on a class. The specified methods will be invoked
		/// during the serialization process to get and set values.
		/// 
		/// </summary>
		/// <param name="get">this forms the get method for the object
		/// </param>
		/// <param name="set">this forms the get method for the object 
		/// </param>
		public MethodContact(MethodPart get_Renamed, MethodPart set_Renamed)
		{
			InitBlock();
			this.label = get_Renamed.Annotation;
			this.items = get_Renamed.Dependents;
			this.item = get_Renamed.Dependent;
			this.get_Renamed_Field = get_Renamed.getMethod();
			this.type = get_Renamed.getType();
			this.name = get_Renamed.Name;
			this.set_Renamed_Field = set_Renamed;
		}
		
		/// <summary> This is the annotation associated with the point of contact.
		/// This will be an XML annotation that describes how the contact
		/// should be serialized and deserialized from the object.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the annotation to acquire
		/// 
		/// </param>
		/// <returns> this provides the annotation associated with this
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T extends Annotation > T getAnnotation(Class < T > type)
		
		/// <summary> This will provide the contact type. The contact type is the
		/// class that is to be set and get on the object. This represents
		/// the return type for the get and the parameter for the set.
		/// 
		/// </summary>
		/// <returns> this returns the type that this contact represents
		/// </returns>
		public virtual System.Type getType()
		{
			return type;
		}
		
		/// <summary> This is used to acquire the name of the method. This returns
		/// the name of the method without the get, set or is prefix that
		/// represents the Java Bean method type. Also this decapitalizes
		/// the resulting name. The result is used to represent the XML
		/// attribute of element within the class schema represented.
		/// 
		/// </summary>
		/// <returns> this returns the name of the method represented
		/// </returns>
		public virtual System.String getName()
		{
			return name;
		}
		
		/// <summary> This is used to set the specified value on the provided object.
		/// The value provided must be an instance of the contact class so
		/// that it can be set without a runtime class compatibility error.
		/// 
		/// </summary>
		/// <param name="source">this is the object to set the value on
		/// </param>
		/// <param name="value">this is the value that is to be set on the object
		/// </param>
		public virtual void  set_Renamed(System.Object source, System.Object value_Renamed)
		{
			System.Type type = getType();
			
			if (set_Renamed_Field == null)
			{
				throw new MethodException("Method '%s' of '%s' is read only", name, type);
			}
			set_Renamed_Field.getMethod().invoke(source, value_Renamed);
		}
		
		/// <summary> This is used to get the specified value on the provided object.
		/// The value returned from this method will be an instance of the
		/// contact class type. If the returned object is of a different
		/// type then the serialization process will fail.
		/// 
		/// </summary>
		/// <param name="source">this is the object to acquire the value from
		/// 
		/// </param>
		/// <returns> this is the value that is acquired from the object
		/// </returns>
		public virtual System.Object get_Renamed(System.Object source)
		{
			return get_Renamed_Field.invoke(source);
		}
		
		/// <summary> This is used to describe the contact as it exists within the
		/// owning class. It is used to provide error messages that can
		/// be used to debug issues that occur when processing a contact.
		/// The string provided contains both the set and get methods.
		/// 
		/// </summary>
		/// <returns> this returns a string representation of the contact
		/// </returns>
		public override System.String ToString()
		{
			return String.format("method '%s'", name);
		}
	}
}