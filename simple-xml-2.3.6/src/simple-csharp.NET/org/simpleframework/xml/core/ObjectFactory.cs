/*
* ObjectFactory.java July 2006
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
	
	/// <summary> The <code>ObjectFactory</code> is the most basic factory. This will
	/// basically check to see if there is an override type within the XML
	/// node provided, if there is then that is instantiated, otherwise the
	/// field type is instantiated. Any type created must have a default
	/// no argument constructor. If the override type is an abstract class
	/// or an interface then this factory throws an exception.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ObjectFactory:PrimitiveFactory
	{
		
		/// <summary> Constructor for the <code>ObjectFactory</code> class. This is
		/// given the field class that this should create object instances
		/// of. If the field type is abstract then the XML node must have
		/// sufficient information for the <code>Strategy</code> object to
		/// resolve the implementation class to be instantiated.
		/// 
		/// </summary>
		/// <param name="context">the contextual object used by the persister 
		/// </param>
		/// <param name="type">this is the object type to use for this factory 
		/// </param>
		public ObjectFactory(Context context, Type type):base(context, type)
		{
		}
		
		/// <summary> This method will instantiate an object of the field type, or if
		/// the <code>Strategy</code> object can resolve a class from the
		/// XML element then this is used instead. If the resulting type is
		/// abstract or an interface then this method throws an exception.
		/// 
		/// </summary>
		/// <param name="node">this is the node to check for the override
		/// 
		/// </param>
		/// <returns> this returns an instance of the resulting type
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override Instance getInstance(InputNode node)
		{
			Value value_Renamed = getOverride(node);
			System.Type type = Type;
			
			if (value_Renamed == null)
			{
				if (!isInstantiable(type))
				{
					throw new InstantiationException("Cannot instantiate %s", type);
				}
				return context.getInstance(type);
			}
			return new ObjectInstance(context, value_Renamed);
		}
	}
}