/*
* Factory.java July 2006
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
using Type = org.simpleframework.xml.strategy.Type;
using Value = org.simpleframework.xml.strategy.Value;
using InputNode = org.simpleframework.xml.stream.InputNode;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Factory</code> object provides a base class for factories 
	/// used to produce field values from XML elements. The goal of this 
	/// type of factory is to make use of the <code>Strategy</code> object
	/// to determine the type of the field value. The strategy class must be 
	/// assignable to the field class type, that is, it must extend it or
	/// implement it if it represents an interface. If the strategy returns
	/// a null <code>Value</code> then the subclass implementation determines 
	/// the type used to populate the object field value.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	abstract class Factory
	{
		/// <summary> This is used to extract the type this factory is using. Each
		/// factory represents a specific class, which it instantiates if
		/// required. This method provides the represented class.
		/// 
		/// </summary>
		/// <returns> this returns the class represented by the factory
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				return type.getType();
			}
			
		}
		
		/// <summary> This is the context object used for the serialization process.</summary>
		protected internal Context context;
		
		/// <summary> This is used to translate all of the primitive type strings.</summary>
		protected internal Support support;
		
		/// <summary> This is the field type that the class must be assignable to.</summary>
		protected internal Type type;
		
		/// <summary> Constructor for the <code>Factory</code> object. This is given 
		/// the class type for the field that this factory will determine
		/// the actual type for. The actual type must be assignable to the
		/// field type to insure that any instance can be set. 
		/// 
		/// </summary>
		/// <param name="context">the contextual object used by the persister
		/// </param>
		/// <param name="type">this is the property representing the field 
		/// </param>
		protected internal Factory(Context context, Type type)
		{
			this.support = context.Support;
			this.context = context;
			this.type = type;
		}
		
		/// <summary> This is used to create a default instance of the field type. It
		/// is up to the subclass to determine how to best instantiate an
		/// object of the field type that best suits. This is used when the
		/// empty value is required or to create the default type instance.
		/// 
		/// </summary>
		/// <returns> a type which is used to instantiate the collection     
		/// </returns>
		public virtual System.Object getInstance()
		{
			System.Type type = Type;
			
			if (!isInstantiable(type))
			{
				throw new InstantiationException("Type %s can not be instantiated", type);
			}
			//UPGRADE_TODO: Method 'java.lang.Class.newInstance' was converted to 'System.Activator.CreateInstance' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassnewInstance'"
			return System.Activator.CreateInstance(type);
		}
		
		/// <summary> This is used to get a possible override from the provided node.
		/// If the node provided is an element then this checks for a  
		/// specific class override using the <code>Strategy</code> object.
		/// If the strategy cannot resolve a class then this will return 
		/// null. If the resolved <code>Value</code> is not assignable to 
		/// the field then this will thrown an exception.
		/// 
		/// </summary>
		/// <param name="node">this is the node used to search for the override
		/// 
		/// </param>
		/// <returns> this returns null if no override type can be found
		/// 
		/// </returns>
		/// <throws>  Exception if the override type is not compatible </throws>
		protected internal virtual Value getOverride(InputNode node)
		{
			Value value_Renamed = getConversion(node);
			
			if (value_Renamed != null)
			{
				System.Type type = value_Renamed.Type;
				System.Type expect = Type;
				
				if (!isCompatible(expect, type))
				{
					throw new InstantiationException("Type %s is not compatible with %s", type, expect);
				}
			}
			return value_Renamed;
		}
		
		/// <summary> This method is used to set the override class within an element.
		/// This delegates to the <code>Strategy</code> implementation, which
		/// depending on the implementation may add an attribute of a child
		/// element to describe the type of the object provided to this.
		/// 
		/// </summary>
		/// <param name="type">this is the class of the field type being serialized
		/// </param>
		/// <param name="node">the XML element that is to be given the details
		/// 
		/// </param>
		/// <throws>  Exception thrown if an error occurs within the strategy </throws>
		public virtual bool setOverride(Type type, System.Object value_Renamed, OutputNode node)
		{
			System.Type expect = type.getType();
			
			if (!expect.IsPrimitive)
			{
				return context.setOverride(type, value_Renamed, node);
			}
			return false;
		}
		
		/// <summary> This performs the conversion from the element node to a type. This
		/// is where the <code>Strategy</code> object is consulted and asked
		/// for a class that will represent the provided XML element. This will,
		/// depending on the strategy implementation, make use of attributes
		/// and/or elements to determine the type for the field.
		/// 
		/// </summary>
		/// <param name="node">this is the element used to extract the override
		/// 
		/// </param>
		/// <returns> this returns null if no override type can be found
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the override class cannot be loaded     </throws>
		public virtual Value getConversion(InputNode node)
		{
			return context.getOverride(type, node);
		}
		
		/// <summary> This is used to determine whether the provided base class can be
		/// assigned from the issued type. For an override to be compatible
		/// with the field type an instance of the override type must be 
		/// assignable to the field value. 
		/// 
		/// </summary>
		/// <param name="expect">this is the field value present the the object    
		/// </param>
		/// <param name="type">this is the specialized type that will be assigned
		/// 
		/// </param>
		/// <returns> true if the field type can be assigned the type value
		/// </returns>
		public static bool isCompatible(System.Type expect, System.Type type)
		{
			if (expect.IsArray)
			{
				expect = expect.GetElementType();
			}
			return expect.IsAssignableFrom(type);
		}
		
		/// <summary> This is used to determine whether the type given is instantiable,
		/// that is, this determines if an instance of that type can be
		/// created. If the type is an interface or an abstract class then 
		/// this will return false.
		/// 
		/// </summary>
		/// <param name="type">this is the type to check the modifiers of
		/// 
		/// </param>
		/// <returns> false if the type is an interface or an abstract class
		/// </returns>
		public static bool isInstantiable(System.Type type)
		{
			//UPGRADE_ISSUE: Method 'java.lang.Class.getModifiers' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetModifiers'"
			int modifiers = type.getModifiers();
			
			//UPGRADE_ISSUE: Method 'java.lang.reflect.Modifier.isAbstract' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectModifier'"
			if (Modifier.isAbstract(modifiers))
			{
				return false;
			}
			//UPGRADE_ISSUE: Method 'java.lang.reflect.Modifier.isInterface' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectModifier'"
			return !Modifier.isInterface(modifiers);
		}
	}
}