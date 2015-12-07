/*
* ClassType.java January 2010
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
using Type = org.simpleframework.xml.strategy.Type;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ClassType</code> object is used to represent a type that
	/// is neither a field or method. Such a type is used when an object
	/// is to be used to populate a collection. In such a scenario there
	/// is no method or field annotations associated with the object.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ClassType : Type
	{
		private void  InitBlock()
		{
			return null;
		}
		
		/// <summary> This is the type that is represented by this instance.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type type;
		
		/// <summary> Constructor for the <code>ClassType</code> object. This will
		/// create a type used to represent a stand alone object, such
		/// as an object being inserted in to a Java collection.
		/// 
		/// </summary>
		/// <param name="type">this is the class that this type represents
		/// </param>
		public ClassType(System.Type type)
		{
			InitBlock();
			this.type = type;
		}
		
		/// <summary> This is the class associated with this type. This is used by
		/// the serialization framework to determine how the XML is to
		/// be converted in to an object and vice versa.
		/// 
		/// </summary>
		/// <returns> this returns the class associated with this type
		/// </returns>
		public virtual System.Type getType()
		{
			return type;
		}
		
		/// <summary> This is used to acquire an annotation of the specified type.
		/// If no such annotation exists for the type then this will
		/// return null. Currently for classes this will always be null.
		/// 
		/// </summary>
		/// <param name="type">this is the annotation type be be acquired
		/// 
		/// </param>
		/// <returns> currently this method will always return null
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T extends Annotation > T getAnnotation(Class < T > type)
		
		/// <summary> This is used to describe the type as it exists within the
		/// owning class. This is used to provide error messages that can
		/// be used to debug issues that occur when processing.  
		/// 
		/// </summary>
		/// <returns> this returns a string representation of the type
		/// </returns>
		public override System.String ToString()
		{
			return type.ToString();
		}
	}
}