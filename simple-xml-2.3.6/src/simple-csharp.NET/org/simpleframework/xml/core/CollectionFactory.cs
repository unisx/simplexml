/*
* CollectionFactory.java July 2006
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
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>CollectionFactory</code> is used to create collection
	/// instances that are compatible with the field type. This performs
	/// resolution of the collection class by firstly consulting the
	/// specified <code>Strategy</code> implementation. If the strategy
	/// cannot resolve the collection class then this will select a type
	/// from the Java Collections framework, if a compatible one exists.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class CollectionFactory:Factory
	{
		
		/// <summary> Constructor for the <code>CollectionFactory</code> object. This
		/// is given the field type as taken from the owning object. The
		/// given type is used to determine the collection instance created.
		/// 
		/// </summary>
		/// <param name="context">this is the context associated with this factory
		/// </param>
		/// <param name="type">this is the class for the owning object
		/// </param>
		public CollectionFactory(Context context, Type type):base(context, type)
		{
		}
		
		/// <summary> Creates a collection that is determined from the field type. 
		/// This is used for the <code>ElementList</code> to get a
		/// collection that does not have any overrides. This must be
		/// done as the inline list does not contain an outer element.
		/// 
		/// </summary>
		/// <returns> a type which is used to instantiate the collection     
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getInstance()
		{
			System.Type type = Type;
			System.Type real = type;
			
			if (!isInstantiable(real))
			{
				real = getConversion(type);
			}
			if (!isCollection(real))
			{
				throw new InstantiationException("Type is not a collection %s", type);
			}
			//UPGRADE_TODO: Method 'java.lang.Class.newInstance' was converted to 'System.Activator.CreateInstance' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassnewInstance'"
			return System.Activator.CreateInstance(real);
		}
		
		/// <summary> Creates the collection to use. The <code>Strategy</code> object
		/// is consulted for the collection class, if one is not resolved
		/// by the strategy implementation or if the collection resolved is
		/// abstract then the Java Collections framework is consulted.
		/// 
		/// </summary>
		/// <param name="node">this is the input node representing the list
		/// 
		/// </param>
		/// <returns> this is the collection instantiated for the field
		/// </returns>
		public virtual Instance getInstance(InputNode node)
		{
			Value value_Renamed = getOverride(node);
			System.Type type = Type;
			
			if (value_Renamed != null)
			{
				return getInstance(value_Renamed);
			}
			if (!isInstantiable(type))
			{
				type = getConversion(type);
			}
			if (!isCollection(type))
			{
				throw new InstantiationException("Type is not a collection %s", type);
			}
			return context.getInstance(type);
		}
		
		/// <summary> This creates a <code>Collection</code> instance from the type
		/// provided. If the type provided is abstract or an interface then
		/// this can promote the type to a collection type that can be 
		/// instantiated. This is done by asking the type to convert itself.
		/// 
		/// </summary>
		/// <param name="value">the type used to instantiate the collection
		/// 
		/// </param>
		/// <returns> this returns a compatible collection instance 
		/// </returns>
		public virtual Instance getInstance(Value value_Renamed)
		{
			System.Type type = value_Renamed.Type;
			
			if (!isInstantiable(type))
			{
				type = getConversion(type);
			}
			if (!isCollection(type))
			{
				throw new InstantiationException("Type is not a collection %s", type);
			}
			return new ConversionInstance(context, value_Renamed, type);
		}
		
		/// <summary> This is used to convert the provided type to a collection type
		/// from the Java Collections framework. This will check to see if
		/// the type is a <code>List</code> or <code>Set</code> and return
		/// an <code>ArrayList</code> or <code>HashSet</code> type. If no
		/// suitable match can be found this throws an exception.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be converted
		/// 
		/// </param>
		/// <returns> a collection that is assignable to the provided type
		/// </returns>
		public virtual System.Type getConversion(System.Type type)
		{
			if (type.IsAssignableFrom(typeof(System.Collections.ArrayList)))
			{
				return typeof(System.Collections.ArrayList);
			}
			//UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashSet'"
			if (type.IsAssignableFrom(typeof(SupportClass.HashSetSupport)))
			{
				//UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashSet'"
				return typeof(SupportClass.HashSetSupport);
			}
			//UPGRADE_TODO: Class 'java.util.TreeSet' was converted to 'SupportClass.TreeSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilTreeSet'"
			if (type.IsAssignableFrom(typeof(SupportClass.TreeSetSupport)))
			{
				//UPGRADE_TODO: Class 'java.util.TreeSet' was converted to 'SupportClass.TreeSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilTreeSet'"
				return typeof(SupportClass.TreeSetSupport);
			}
			throw new InstantiationException("Cannot instantiate %s", type);
		}
		
		/// <summary> This determines whether the type provided is a collection type.
		/// If the type is assignable to a <code>Collection</code> then 
		/// this returns true, otherwise this returns false.
		/// 
		/// </summary>
		/// <param name="type">given to determine whether it is a collection  
		/// 
		/// </param>
		/// <returns> true if the provided type is a collection type
		/// </returns>
		private bool isCollection(System.Type type)
		{
			return typeof(System.Collections.ICollection).IsAssignableFrom(type);
		}
	}
}