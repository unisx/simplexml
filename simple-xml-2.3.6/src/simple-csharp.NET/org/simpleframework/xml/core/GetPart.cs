/*
* GetPart.java April 2007
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
	
	/// <summary> The <code>GetPart</code> object represents the getter method for
	/// a Java Bean property. This composes the get part of the method
	/// contact for an object. The get part contains the method that is
	/// used to get the value in an object and the annotation that tells
	/// the serialization process how to serialize the value.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.MethodContact">
	/// </seealso>
	class GetPart : MethodPart
	{
		private void  InitBlock()
		{
			return method.Annotation;
		}
		/// <summary> This provides the name of the method part as acquired from the
		/// method name. The name represents the Java Bean property name
		/// of the method and is used to pair getter and setter methods.
		/// 
		/// </summary>
		/// <returns> this returns the Java Bean name of the method part
		/// </returns>
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		/// <summary> This is used to acquire the type for this method part. This
		/// is used by the serializer to determine the schema class that
		/// is used to match the XML elements to the object details.
		/// 
		/// </summary>
		/// <returns> this returns the schema class for this method
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				return method.ReturnType;
			}
			
		}
		/// <summary> This is used to acquire the dependent class for the method 
		/// part. The dependent type is the type that represents the 
		/// generic type of the type. This is used when collections are
		/// annotated as it allows a default entry class to be taken
		/// from the generic information provided.
		/// 
		/// </summary>
		/// <returns> this returns the generic dependent for the type
		/// </returns>
		virtual public System.Type Dependent
		{
			get
			{
				return Reflector.getReturnDependent(method);
			}
			
		}
		/// <summary> This is used to acquire the dependent classes for the method 
		/// part. The dependent types are the types that represent the 
		/// generic types of the type. This is used when collections are 
		/// annotated as it allows a default entry class to be taken
		/// from the generic information provided.
		/// 
		/// </summary>
		/// <returns> this returns the generic dependent for the type
		/// </returns>
		virtual public System.Type[] Dependents
		{
			get
			{
				return Reflector.getReturnDependents(method);
			}
			
		}
		/// <summary> This is used to acquire the annotation that was used to label
		/// the method this represents. This acts as a means to match the
		/// set method with the get method using an annotation comparison.
		/// 
		/// </summary>
		/// <returns> this returns the annotation used to mark the method
		/// </returns>
		virtual public Annotation Annotation
		{
			get
			{
				return label;
			}
			
		}
		/// <summary> This is the method type for the method part. This is used in
		/// the scanning process to determine which type of method a
		/// instance represents, this allows set and get methods to be
		/// paired.
		/// 
		/// </summary>
		/// <returns> the method type that this part represents
		/// </returns>
		virtual public MethodType MethodType
		{
			get
			{
				return type;
			}
			
		}
		/// <summary> This is used to acquire the method that can be used to invoke
		/// the Java Bean method on the object. If the method represented
		/// by this is inaccessible then this will set it as accessible.
		/// 
		/// </summary>
		/// <returns> returns the method used to interface with the object
		/// </returns>
		virtual public System.Reflection.MethodInfo Method
		{
			get
			{
				//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.isAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
				if (!method.isAccessible())
				{
					//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.setAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
					method.setAccessible(true);
				}
				return method;
			}
			
		}
		
		/// <summary> This is the annotation for the get method provided.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Annotation label;
		
		/// <summary> This represents the method type for the get part method.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private MethodType type;
		
		/// <summary> This method is used to get the value during serialization. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'method '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Reflection.MethodInfo method;
		
		/// <summary> This is the Java Bean name representation of the method.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String name;
		
		/// <summary> Constructor for the <code>GetPart</code> object. This is
		/// used to create a method part that will provide a means for 
		/// the serialization process to set a value to a object.
		/// 
		/// </summary>
		/// <param name="method">the method that is used to get the value
		/// </param>
		/// <param name="label">this describes how to serialize the value
		/// </param>
		public GetPart(MethodName method, Annotation label)
		{
			InitBlock();
			this.method = method.Method;
			this.name = method.Name;
			this.type = method.Type;
			this.label = label;
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
		
		/// <summary> This is used to describe the method as it exists within the
		/// owning class. This is used to provide error messages that can
		/// be used to debug issues that occur when processing a method.
		/// This returns the method as a generic string representation.  
		/// 
		/// </summary>
		/// <returns> this returns a string representation of the method
		/// </returns>
		public override System.String ToString()
		{
			return method.toGenericString();
		}
	}
}