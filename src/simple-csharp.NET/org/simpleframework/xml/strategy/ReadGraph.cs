/*
* ReadGraph.java April 2007
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
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.NodeMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using NodeMap = org.simpleframework.xml.stream.NodeMap;
using Node = org.simpleframework.xml.stream.Node;
namespace org.simpleframework.xml.strategy
{
	
	/// <summary> The <code>ReadGraph</code> object is used to build a graph of the
	/// objects that have been deserialized from the XML document. This is
	/// required so that cycles in the object graph can be recreated such
	/// that the deserialized object is an exact duplicate of the object
	/// that was serialized. Objects are stored in the graph using unique
	/// keys, which for this implementation are unique strings.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.strategy.WriteGraph">
	/// </seealso>
	//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
	[Serializable]
	class ReadGraph:System.Collections.Hashtable
	{
		
		/// <summary> This is the class loader that is used to load the types used.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'loader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Loader loader;
		
		/// <summary> This is used to represent the length of array object values.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'length '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String length;
		
		/// <summary> This is the label used to mark the type of an object.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String label;
		
		/// <summary> This is the attribute used to mark the identity of an object.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'mark '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String mark;
		
		/// <summary> This is the attribute used to refer to an existing instance.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'refer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String refer;
		
		/// <summary> Constructor for the <code>ReadGraph</code> object. This is used
		/// to create graphs that are used for reading objects from the XML
		/// document. The specified strategy is used to acquire the names
		/// of the special attributes used during the serialization.
		/// 
		/// </summary>
		/// <param name="contract">this is the name scheme used by the strategy
		/// </param>
		/// <param name="loader">this is the class loader to used for the graph 
		/// </param>
		public ReadGraph(Contract contract, Loader loader)
		{
			this.refer = contract.Reference;
			this.mark = contract.Identity;
			this.length = contract.Length;
			this.label = contract.Label;
			this.loader = loader;
		}
		
		/// <summary> This is used to recover the object references from the document
		/// using the special attributes specified. This allows the element
		/// specified by the <code>NodeMap</code> to be used to discover
		/// exactly which node in the object graph the element represents.
		/// 
		/// </summary>
		/// <param name="type">the type of the field or method in the instance
		/// </param>
		/// <param name="node">this is the XML element to be deserialized
		/// 
		/// </param>
		/// <returns> this is used to return the type to acquire the value
		/// </returns>
		public virtual Value read(Type type, NodeMap node)
		{
			Node entry = node.remove(label);
			System.Type expect = type.getType();
			
			if (expect.IsArray)
			{
				expect = expect.GetElementType();
			}
			if (entry != null)
			{
				System.String name = entry.getValue();
				expect = loader.load(name);
			}
			return readInstance(type, expect, node);
		}
		
		/// <summary> This is used to recover the object references from the document
		/// using the special attributes specified. This allows the element
		/// specified by the <code>NodeMap</code> to be used to discover
		/// exactly which node in the object graph the element represents.
		/// 
		/// </summary>
		/// <param name="type">the type of the field or method in the instance
		/// </param>
		/// <param name="real">this is the overridden type from the XML element
		/// </param>
		/// <param name="node">this is the XML element to be deserialized
		/// 
		/// </param>
		/// <returns> this is used to return the type to acquire the value
		/// </returns>
		private Value readInstance(Type type, System.Type real, NodeMap node)
		{
			Node entry = node.remove(mark);
			
			if (entry == null)
			{
				return readReference(type, real, node);
			}
			System.String key = entry.getValue();
			
			if (ContainsKey(key))
			{
				throw new CycleException("Element '%s' already exists", key);
			}
			return readValue(type, real, node, key);
		}
		
		/// <summary> This is used to recover the object references from the document
		/// using the special attributes specified. This allows the element
		/// specified by the <code>NodeMap</code> to be used to discover
		/// exactly which node in the object graph the element represents.
		/// 
		/// </summary>
		/// <param name="type">the type of the field or method in the instance
		/// </param>
		/// <param name="real">this is the overridden type from the XML element
		/// </param>
		/// <param name="node">this is the XML element to be deserialized    
		/// 
		/// </param>
		/// <returns> this is used to return the type to acquire the value
		/// </returns>
		private Value readReference(Type type, System.Type real, NodeMap node)
		{
			Node entry = node.remove(refer);
			
			if (entry == null)
			{
				return readValue(type, real, node);
			}
			System.String key = entry.getValue();
			//UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapget_javalangObject'"
			System.Object value_Renamed = this[key];
			
			if (!ContainsKey(key))
			{
				throw new CycleException("Invalid reference '%s' found", key);
			}
			return new Reference(value_Renamed, real);
		}
		
		/// <summary> This is used to acquire the <code>Value</code> which can be used 
		/// to represent the deserialized value. The type create cab be
		/// added to the graph of created instances if the XML element has
		/// an identification attribute, this allows cycles to be completed.
		/// 
		/// </summary>
		/// <param name="type">the type of the field or method in the instance
		/// </param>
		/// <param name="real">this is the overridden type from the XML element
		/// </param>
		/// <param name="node">this is the XML element to be deserialized    
		/// 
		/// </param>
		/// <returns> this is used to return the type to acquire the value
		/// </returns>
		private Value readValue(Type type, System.Type real, NodeMap node)
		{
			System.Type expect = type.getType();
			
			if (expect.IsArray)
			{
				return readArray(type, real, node);
			}
			return new ObjectValue(real);
		}
		
		/// <summary> This is used to acquire the <code>Value</code> which can be used 
		/// to represent the deserialized value. The type create cab be
		/// added to the graph of created instances if the XML element has
		/// an identification attribute, this allows cycles to be completed.
		/// 
		/// </summary>
		/// <param name="type">the type of the field or method in the instance
		/// </param>
		/// <param name="real">this is the overridden type from the XML element
		/// </param>
		/// <param name="node">this is the XML element to be deserialized
		/// </param>
		/// <param name="key">the key the instance is known as in the graph    
		/// 
		/// </param>
		/// <returns> this is used to return the type to acquire the value
		/// </returns>
		private Value readValue(Type type, System.Type real, NodeMap node, System.String key)
		{
			Value value_Renamed = readValue(type, real, node);
			
			if (key != null)
			{
				return new Allocate(value_Renamed, this, key);
			}
			return value_Renamed;
		}
		
		/// <summary> This is used to acquire the <code>Value</code> which can be used 
		/// to represent the deserialized value. The type create cab be
		/// added to the graph of created instances if the XML element has
		/// an identification attribute, this allows cycles to be completed.
		/// 
		/// </summary>
		/// <param name="type">the type of the field or method in the instance
		/// </param>
		/// <param name="real">this is the overridden type from the XML element
		/// </param>
		/// <param name="node">this is the XML element to be deserialized  
		/// 
		/// </param>
		/// <returns> this is used to return the type to acquire the value
		/// </returns>
		private Value readArray(Type type, System.Type real, NodeMap node)
		{
			Node entry = node.remove(length);
			int size = 0;
			
			if (entry != null)
			{
				System.String value_Renamed = entry.getValue();
				size = System.Int32.Parse(value_Renamed);
			}
			return new ArrayValue(real, size);
		}
	}
}