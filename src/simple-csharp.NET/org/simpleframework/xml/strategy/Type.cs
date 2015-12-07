/*
* Type.java January 2010
*
* Copyright (C) 2010, Niall Gallagher <niallg@users.sf.net>
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
namespace org.simpleframework.xml.strategy
{
	
	/// <summary> The <code>Type</code> interface is used to represent a method or
	/// field that has been annotated for serialization. Representing 
	/// methods and fields as a generic type object allows various
	/// common details to be extracted in a uniform way. It allows all
	/// annotations on the method or field to be exposed. This can 
	/// also wrap classes that represent entries to a list or map.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public interface Type
	{
		
		/// <summary> This will provide the method or field type. The type is the
		/// class that is to be read and written on the object. Typically 
		/// the type will be a serializable object or a primitive type.
		/// 
		/// </summary>
		/// <returns> this returns the type for this method o field
		/// </returns>
		System.Type getType();
		
		/// <summary> This is the annotation associated with the method or field
		/// that has been annotated. If this represents an entry to a 
		/// Java collection such as a <code>java.util.List</code> then
		/// this will return null for any annotation requested.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the annotation to acquire
		/// 
		/// </param>
		/// <returns> this provides the annotation associated with this
		/// </returns>
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T extends Annotation > T getAnnotation(Class < T > type);
	
	/// <summary> This is used to describe the type as it exists within the
	/// owning class. This is used to provide error messages that can
	/// be used to debug issues that occur when processing.  
	/// 
	/// </summary>
	/// <returns> this returns a string representation of the type
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String toString();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}