/*
* ValueInstance.java January 2007
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
	
	/// <summary> The <code>ValueInstance</code> object is used to create an object
	/// by using a <code>Value</code> instance to determine the type. If
	/// the provided value instance represents a reference then this will
	/// simply provide the value of the reference, otherwise it will
	/// instantiate a new object and return that.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ValueInstance : Instance
	{
		/// <summary> This is used to determine if the type is a reference type.
		/// A reference type is a type that does not require any XML
		/// deserialization based on its annotations. Values that are
		/// references could be substitutes objects or existing ones. 
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
		
		/// <summary> This is the instantiator used to create the objects.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'creator '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Instantiator creator;
		
		/// <summary> This is the internal value that contains the criteria.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Value value_Renamed;
		
		/// <summary> This is the type that is to be instantiated by this.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type type;
		
		/// <summary> Constructor for the <code>ValueInstance</code> object. This 
		/// is used to represent an instance that delegates to the given
		/// value object in order to acquire the object. 
		/// 
		/// </summary>
		/// <param name="creator">this is the instantiator used to create objects
		/// </param>
		/// <param name="value">this is the value object that contains the data
		/// </param>
		public ValueInstance(Instantiator creator, Value value_Renamed)
		{
			this.type = value_Renamed.Type;
			this.creator = creator;
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
			System.Object object_Renamed = creator.getObject(type);
			
			if (value_Renamed != null)
			{
				value_Renamed.setValue(object_Renamed);
			}
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