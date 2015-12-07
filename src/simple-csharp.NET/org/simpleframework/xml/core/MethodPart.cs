/*
* MethodPart.java April 2007
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
	
	/// <summary> The <code>MethodPart</code> interface is used to provide a point 
	/// of contact with an object. Typically this will be used to get a
	/// method from an object which is contains an XML annotation. This
	/// provides the type the method is associated with, this type is
	/// either the method return type or the single value parameter.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	internal interface MethodPart
	{
		/// <summary> This provides the name of the method part as acquired from the
		/// method name. The name represents the Java Bean property name
		/// of the method and is used to pair getter and setter methods.
		/// 
		/// </summary>
		/// <returns> this returns the Java Bean name of the method part
		/// </returns>
		System.String Name
		{
			get;
			
		}
		/// <summary> This is the annotation associated with the point of contact.
		/// This will be an XML annotation that describes how the contact
		/// should be serializaed and deserialized from the object.
		/// 
		/// </summary>
		/// <returns> this provides the annotation associated with this
		/// </returns>
		Annotation Annotation
		{
			get;
			
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
			
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T extends Annotation > T getAnnotation(Class < T > type);
	
	/// <summary> This will provide the contact type. The contact type is the
	/// class that is either the method return type or the single
	/// value parameter type associated with the method.
	/// 
	/// </summary>
	/// <returns> this returns the type that this contact represents
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class getType();
	
	/// <summary> This is used to acquire the dependent class for the method 
	/// part. The dependent type is the type that represents the 
	/// generic type of the type. This is used when collections are
	/// annotated as it allows a default entry class to be taken
	/// from the generic information provided.
	/// 
	/// </summary>
	/// <returns> this returns the generic dependent for the type
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class getDependent();
	
	/// <summary> This is used to acquire the dependent classes for the method 
	/// part. The dependent types are the types that represent the 
	/// generic types of the type. This is used when collections are 
	/// annotated as it allows a default entry class to be taken
	/// from the generic information provided.
	/// 
	/// </summary>
	/// <returns> this returns the generic dependent for the type
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class [] getDependents();
	
	/// <summary> This is the method for this point of contact. This is what
	/// will be invoked by the serialization or deserialization 
	/// process when an XML element or attribute is to be used.
	/// 
	/// </summary>
	/// <returns> this returns the method associated with this
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Method getMethod();
	
	/// <summary> This is the method type for the method part. This is used in
	/// the scanning process to determine which type of method a
	/// instance represents, this allows set and get methods to be
	/// paired.
	/// 
	/// </summary>
	/// <returns> the method type that this part represents
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public MethodType getMethodType();
	
	/// <summary> This is used to describe the method as it exists within the
	/// owning class. This is used to provide error messages that can
	/// be used to debug issues that occur when processing a method.
	/// This should return the method as a generic representation.  
	/// 
	/// </summary>
	/// <returns> this returns a string representation of the method
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String toString();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}