/*
* ObjectInstance.java April 2007
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
using Value = org.simpleframework.xml.strategy.Value;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ObjectInstance</code> is used to instantiate an object
	/// from the criteria provided in the given <code>Value</code>. If
	/// the value contains a reference then the reference is provided
	/// from this type. For performance the <code>Context</code> object
	/// is used to instantiate the object as it contains a reflection
	/// cache of constructors.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ObjectInstance : Instance
	{
		/// <summary> This is used to determine if the type is a reference type.
		/// A reference type is a type that does not require any XML
		/// deserialization based on its annotations. Types that are
		/// references could be substitutes objects are existing ones. 
		/// 
		/// </summary>
		/// <returns> this returns true if the object is a reference
		/// </returns>
		virtual public bool Reference
		{
			get
			{
				return value_Renamed.isReference();
			}
			
		}
		/// <summary> This is the type of the object instance that will be created
		/// by the <code>getInstance</code> method. This allows the 
		/// deserialization process to perform checks against the field.
		/// 
		/// </summary>
		/// <returns> the type of the object that will be instantiated
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				return type;
			}
			
		}
		
		/// <summary> This is the context that is used to create the instance.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> This is the value object that will be wrapped by this.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Value value_Renamed;
		
		/// <summary> This is the new class that is used for the instantiation.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type type;
		
		/// <summary> Constructor for the <code>ObjectInstance</code> object. This
		/// is used to create an instance of the type described by the
		/// value object. If the value object contains a reference then
		/// this will simply provide that reference.
		/// 
		/// </summary>
		/// <param name="context">this is used to instantiate the object
		/// </param>
		/// <param name="value">this is the value describing the instance
		/// </param>
		public ObjectInstance(Context context, Value value_Renamed)
		{
			this.type = value_Renamed.Type;
			this.context = context;
			this.value_Renamed = value_Renamed;
		}
		
		/// <summary> This method is used to acquire an instance of the type that
		/// is defined by this object. If for some reason the type can
		/// not be instantiated an exception is thrown from this.
		/// 
		/// </summary>
		/// <returns> an instance of the type this object represents
		/// </returns>
		public virtual System.Object getInstance()
		{
			if (value_Renamed.isReference())
			{
				return value_Renamed.getValue();
			}
			System.Object object_Renamed = getInstance(type);
			
			if (value_Renamed != null)
			{
				value_Renamed.setValue(object_Renamed);
			}
			return object_Renamed;
		}
		
		/// <summary> This method is used to acquire an instance of the type that
		/// is defined by this object. If for some reason the type can
		/// not be instantiated an exception is thrown from this.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be instantiated
		/// 
		/// </param>
		/// <returns> an instance of the type this object represents
		/// </returns>
		public virtual System.Object getInstance(System.Type type)
		{
			Instance value_Renamed = context.getInstance(type);
			System.Object object_Renamed = value_Renamed.getInstance();
			
			return object_Renamed;
		}
		
		/// <summary> This method is used acquire the value from the type and if
		/// possible replace the value for the type. If the value can
		/// not be replaced then an exception should be thrown. This 
		/// is used to allow primitives to be inserted into a graph.
		/// 
		/// </summary>
		/// <param name="object">this is the object to insert as the value
		/// 
		/// </param>
		/// <returns> an instance of the type this object represents
		/// </returns>
		public virtual System.Object setInstance(System.Object object_Renamed)
		{
			if (value_Renamed != null)
			{
				value_Renamed.setValue(object_Renamed);
			}
			return object_Renamed;
		}
	}
}