/*
* RegistryStrategy.java January 2010
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
using Strategy = org.simpleframework.xml.strategy.Strategy;
using TreeStrategy = org.simpleframework.xml.strategy.TreeStrategy;
using Type = org.simpleframework.xml.strategy.Type;
using Value = org.simpleframework.xml.strategy.Value;
using InputNode = org.simpleframework.xml.stream.InputNode;
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.NodeMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using NodeMap = org.simpleframework.xml.stream.NodeMap;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
namespace org.simpleframework.xml.convert
{
	
	/// <summary> The <code>RegistryStrategy</code> object is used to intercept
	/// the serialization process and delegate to custom converters. The
	/// custom converters are resolved from a <code>Registry</code>
	/// object, which is provided to the constructor. If there is no
	/// binding for a particular object then serialization is delegated
	/// to an internal strategy. All converters resolved by this are
	/// instantiated once and cached internally for performance.
	/// <p>
	/// By default the <code>TreeStrategy</code> is used to perform the
	/// normal serialization process should there be no class binding
	/// specifying a converter to use. However, any implementation can
	/// be used, including the <code>CycleStrategy</code>, which handles
	/// cycles in the object graph. To specify the internal strategy to
	/// use it can be provided in the constructor.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.convert.Registry">
	/// </seealso>
	public class RegistryStrategy : Strategy
	{
		private void  InitBlock()
		{
			Value value_Renamed = strategy.read(type, node, map);
			
			if (isReference(value_Renamed))
			{
				return value_Renamed;
			}
			return read(type, node, value_Renamed);
			Converter converter = lookup(type, value_Renamed);
			InputNode source = node.getNode();
			
			if (converter != null)
			{
				System.Object data = converter.read(source);
				
				if (value_Renamed != null)
				{
					value_Renamed.setValue(data);
				}
				return new Reference(value_Renamed, data);
			}
			return value_Renamed;
			bool reference = strategy.write(type, value_Renamed, node, map);
			
			if (!reference)
			{
				return write(type, value_Renamed, node);
			}
			return reference;
			Converter converter = lookup(type, value_Renamed);
			OutputNode source = node.getNode();
			
			if (converter != null)
			{
				converter.write(source, value_Renamed);
				return true;
			}
			return false;
		}
		
		/// <summary> This is the registry that is used to resolve bindings.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'registry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Registry registry;
		
		/// <summary> This is the strategy used if there is no bindings.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'strategy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Strategy strategy;
		
		/// <summary> Constructor for the <code>RegistryStrategy</code> object. This
		/// is used to create a strategy that will intercept the normal
		/// serialization process by searching for bindings within the
		/// provided <code>Registry</code> instance.
		/// 
		/// </summary>
		/// <param name="registry">this is the registry instance with bindings
		/// </param>
		public RegistryStrategy(Registry registry):this(registry, new TreeStrategy())
		{
		}
		
		/// <summary> Constructor for the <code>RegistryStrategy</code> object. This
		/// is used to create a strategy that will intercept the normal
		/// serialization process by searching for bindings within the
		/// provided <code>Registry</code> instance.
		/// 
		/// </summary>
		/// <param name="registry">this is the registry instance with bindings
		/// </param>
		/// <param name="strategy">this is the strategy to delegate to
		/// </param>
		public RegistryStrategy(Registry registry, Strategy strategy)
		{
			InitBlock();
			this.registry = registry;
			this.strategy = strategy;
		}
		
		/// <summary> This is used to read the <code>Value</code> which will be used 
		/// to represent the deserialized object. If there is an binding
		/// present then the value will contain an object instance. If it
		/// does not then it is up to the internal strategy to determine 
		/// what the returned value contains.
		/// 
		/// </summary>
		/// <param name="type">this is the type that represents a method or field
		/// </param>
		/// <param name="node">this is the node representing the XML element
		/// </param>
		/// <param name="map">this is the session map that contain variables
		/// 
		/// </param>
		/// <returns> the value representing the deserialized value
		/// </returns>
		public Value read;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Type type, NodeMap < InputNode > node, Map map) throws Exception
		
		/// <summary> This is used to read the <code>Value</code> which will be used 
		/// to represent the deserialized object. If there is an binding
		/// present then the value will contain an object instance. If it
		/// does not then it is up to the internal strategy to determine 
		/// what the returned value contains.
		/// 
		/// </summary>
		/// <param name="type">this is the type that represents a method or field
		/// </param>
		/// <param name="node">this is the node representing the XML element
		/// </param>
		/// <param name="value">this is the value from the internal strategy
		/// 
		/// </param>
		/// <returns> the value representing the deserialized value
		/// </returns>
		private Value read;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Type type, NodeMap < InputNode > node, Value value) throws Exception
		
		/// <summary> This is used to serialize a representation of the object value
		/// provided. If there is a <code>Registry</code> binding present
		/// for the provided type then this will use the converter specified
		/// to serialize a representation of the object. If however there
		/// is no binding present then this will delegate to the internal 
		/// strategy. This returns true if the serialization has completed.
		/// 
		/// </summary>
		/// <param name="type">this is the type that represents the field or method
		/// </param>
		/// <param name="value">this is the object instance to be serialized
		/// </param>
		/// <param name="node">this is the XML element to be serialized to
		/// </param>
		/// <param name="map">this is the session map used by the serializer
		/// 
		/// </param>
		/// <returns> this returns true if it was serialized, false otherwise
		/// </returns>
		public bool write;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Type type, Object value, NodeMap < OutputNode > node, Map map) throws Exception
		
		/// <summary> This is used to serialize a representation of the object value
		/// provided. If there is a <code>Registry</code> binding present
		/// for the provided type then this will use the converter specified
		/// to serialize a representation of the object. If however there
		/// is no binding present then this will delegate to the internal 
		/// strategy. This returns true if the serialization has completed.
		/// 
		/// </summary>
		/// <param name="type">this is the type that represents the field or method
		/// </param>
		/// <param name="value">this is the object instance to be serialized
		/// </param>
		/// <param name="node">this is the XML element to be serialized to
		/// 
		/// </param>
		/// <returns> this returns true if it was serialized, false otherwise
		/// </returns>
		private bool write;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Type type, Object value, NodeMap < OutputNode > node) throws Exception
		
		/// <summary> This is used to acquire a <code>Converter</code> instance for 
		/// the provided value object. The value object is used to resolve
		/// the converter to use for the serialization process.
		/// 
		/// </summary>
		/// <param name="type">this is the type representing the field or method
		/// </param>
		/// <param name="value">this is the value that is to be serialized
		/// 
		/// </param>
		/// <returns> this returns the converter instance that is matched
		/// </returns>
		private Converter lookup(Type type, Value value_Renamed)
		{
			System.Type real = type.getType();
			
			if (value_Renamed != null)
			{
				real = value_Renamed.Type;
			}
			return registry.lookup(real);
		}
		
		/// <summary> This is used to acquire a <code>Converter</code> instance for 
		/// the provided object instance. The instance class is used to
		/// resolve the converter to use for the serialization process.
		/// 
		/// </summary>
		/// <param name="type">this is the type representing the field or method
		/// </param>
		/// <param name="value">this is the value that is to be serialized
		/// 
		/// </param>
		/// <returns> this returns the converter instance that is matched
		/// </returns>
		private Converter lookup(Type type, System.Object value_Renamed)
		{
			System.Type real = type.getType();
			
			if (value_Renamed != null)
			{
				real = value_Renamed.GetType();
			}
			return registry.lookup(real);
		}
		
		/// <summary> This is used to determine if the <code>Value</code> provided
		/// represents a reference. If it does represent a reference then
		/// this will return true, if it does not then this returns false.
		/// 
		/// </summary>
		/// <param name="value">this is the value instance to be evaluated
		/// 
		/// </param>
		/// <returns> this returns true if the value represents a reference
		/// </returns>
		private bool isReference(Value value_Renamed)
		{
			return value_Renamed != null && value_Renamed.isReference();
		}
	}
}