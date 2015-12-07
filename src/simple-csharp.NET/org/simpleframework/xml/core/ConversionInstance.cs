/*
* CoversionInstance.java April 2007
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
	
	/// <summary> The <code>ConversionInstance</code> object is used to promote the
	/// type to some more specialized type. For example if a field or 
	/// method that represents a <code>List</code> is annotated then this
	/// might create a specialized type such as a <code>Vector</code>. It
	/// typically used to promote a type either because it is abstract
	/// or because another type is required. 
	/// <p>
	/// This is used by the <code>CollectionFactory</code> to convert the
	/// type of a collection field from an abstract type to a instantiable
	/// type. This is used to simplify strategy implementations. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.CollectionFactory">
	/// </seealso>
	class ConversionInstance : Instance
	{
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
				return convert;
			}
			
		}
		/// <summary> This will return true if the <code>Value</code> object provided
		/// is a reference type. Typically a reference type refers to a 
		/// type that is substituted during the deserialization process 
		/// and so constitutes an object that does not need initialization.
		/// 
		/// </summary>
		/// <returns> this returns true if the type is a reference type
		/// </returns>
		virtual public bool Reference
		{
			get
			{
				return value_Renamed.isReference();
			}
			
		}
		
		/// <summary> This is the context that is used to create the instance.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> This is the new class that is used for the type conversion. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'convert '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type convert;
		
		/// <summary> This is the value object that will be wrapped by this.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Value value_Renamed;
		
		/// <summary> This is used to specify the creation of a conversion type that
		/// can be used for creating an instance with a class other than 
		/// the default class specified by the <code>Value</code> object.
		/// 
		/// </summary>
		/// <param name="context">this is the context used for instantiation
		/// </param>
		/// <param name="value">this is the type used to create the instance
		/// </param>
		/// <param name="convert">this is the class the type is converted to
		/// </param>
		public ConversionInstance(Context context, Value value_Renamed, System.Type convert)
		{
			this.context = context;
			this.convert = convert;
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
			System.Object created = getInstance(convert);
			
			if (created != null)
			{
				setInstance(created);
			}
			return created;
		}
		
		/// <summary> This method is used to acquire an instance of the type that
		/// is defined by this object. If for some reason the type can
		/// not be instantiated an exception is thrown from this.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the instance to create
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