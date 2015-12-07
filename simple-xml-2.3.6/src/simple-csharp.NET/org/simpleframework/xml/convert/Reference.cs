/*
* Reference.java January 2010
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
using Value = org.simpleframework.xml.strategy.Value;
namespace org.simpleframework.xml.convert
{
	
	/// <summary> The <code>Reference</code> object represents a value that holds
	/// an object instance. If an object instance is to be provided from
	/// a <code>Strategy</code> implementation it must be wrapped in a 
	/// value object. The value object can then provide the details of
	/// the instance and the actual object instance to the serializer.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Reference : Value
	{
		/// <summary> This will return the length of an array reference. Because
		/// the value will represent the value itself the length is 
		/// never used, as no instance needs to be created.
		/// 
		/// </summary>
		/// <returns> this will always return zero for a reference
		/// </returns>
		virtual public int Length
		{
			get
			{
				return 0;
			}
			
		}
		virtual public System.Type Type
		{
			get
			{
				return data.GetType();
			}
			
		}
		
		/// <summary> This represents the original value returned from a strategy.</summary>
		private Value value_Renamed;
		
		/// <summary> This represents the object instance that this represents.</summary>
		private System.Object data;
		
		/// <summary> Constructor for a <code>Reference</code> object. To create
		/// this a value and an object instance is required. The value
		/// provided may be null, but the instance should be a valid
		/// object instance to be used by the serializer.
		/// 
		/// </summary>
		/// <param name="value">this is the original value from a strategy
		/// </param>
		/// <param name="data">this is the object instance that is wrapped
		/// </param>
		public Reference(Value value_Renamed, System.Object data)
		{
			this.value_Renamed = value_Renamed;
			this.data = data;
		}
		
		/// <summary> This returns the actual object instance that is held by this
		/// reference object.
		/// </summary>
		public virtual System.Object getValue()
		{
			return data;
		}
		
		/// <summary> This will always return true as this <code>Value</code> object
		/// will always contain an object instance. Returning true from 
		/// this method tells the serializer that there is no need to
		/// actually perform any further deserialization.
		/// 
		/// </summary>
		/// <returns> this always returns true as this will be a reference
		/// </returns>
		public virtual bool isReference()
		{
			return true;
		}
		
		/// <summary> This is used to set the value of the object. If the internal
		/// <code>Value</code> is not null then the internal value will 
		/// have the instance set also. 
		/// 
		/// </summary>
		/// <param name="data">this is the object instance that is to be set
		/// </param>
		public virtual void  setValue(System.Object data)
		{
			if (value_Renamed != null)
			{
				value_Renamed.setValue(data);
			}
			this.data = data;
		}
	}
}