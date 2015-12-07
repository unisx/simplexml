/*
* Reference.java May 2006
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
namespace org.simpleframework.xml.strategy
{
	
	/// <summary> The <code>Reference</code> object represents an object that 
	/// is used to provide a reference to an already instantiated value.
	/// This is what is used if there is a cycle in the object graph. 
	/// The <code>getValue</code> method of this object will simply
	/// return the object instance that was previously created.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Reference : Value
	{
		/// <summary> This returns the type for the object that this references.
		/// This will basically return the <code>getClass</code> class
		/// from the referenced instance. This is used to ensure that
		/// the type this represents is compatible to the object field.
		/// 
		/// </summary>
		/// <returns> this returns the type for the referenced object
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				return type;
			}
			
		}
		/// <summary> This returns zero as this is a reference and will typically
		/// not be used to instantiate anything. If the reference is an
		/// an array then this can not be used to instantiate it.
		/// 
		/// </summary>
		/// <returns> this returns zero regardless of the value type
		/// </returns>
		virtual public int Length
		{
			get
			{
				return 0;
			}
			
		}
		
		/// <summary> This is the object instance that has already be created.</summary>
		private System.Object value_Renamed;
		
		/// <summary> This is the type of the object that this references.</summary>
		private System.Type type;
		
		/// <summary> Constructor for the <code>Reference</code> object. This 
		/// is used to create a value that will produce the specified 
		/// value when the <code>getValue</code> method is invoked.
		/// 
		/// </summary>
		/// <param name="value">the value for the reference this represents
		/// </param>
		/// <param name="type">this is the type value for the instance
		/// </param>
		public Reference(System.Object value_Renamed, System.Type type)
		{
			this.value_Renamed = value_Renamed;
			this.type = type;
		}
		
		/// <summary> This is used to acquire a reference to the instance that is
		/// taken from the created object graph. This enables any cycles
		/// in the graph to be reestablished from the persisted XML.
		/// 
		/// </summary>
		/// <returns> this returns a reference to the created instance
		/// </returns>
		public virtual System.Object getValue()
		{
			return value_Renamed;
		}
		
		/// <summary> This method is used set the value within this object. Once
		/// this is set then the <code>getValue</code> method will return
		/// the object that has been provided. Typically this will not 
		/// be set as this represents a reference value.
		/// 
		/// </summary>
		/// <param name="value">this is the value to insert as the type
		/// </param>
		public virtual void  setValue(System.Object value_Renamed)
		{
			this.value_Renamed = value_Renamed;
		}
		
		/// <summary> This always returns true for this object. This indicates to
		/// the deserialization process that there should be not further
		/// deserialization of the object from the XML source stream.
		/// 
		/// </summary>
		/// <returns> because this is a reference this is always true 
		/// </returns>
		public virtual bool isReference()
		{
			return true;
		}
	}
}