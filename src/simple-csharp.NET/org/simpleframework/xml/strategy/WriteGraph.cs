/*
* WriteGraph.java April 2007
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
//UPGRADE_TODO: The type 'java.util.IdentityHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using IdentityHashMap = java.util.IdentityHashMap;
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.NodeMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using NodeMap = org.simpleframework.xml.stream.NodeMap;
namespace org.simpleframework.xml.strategy
{
	
	/// <summary> The <code>WriteGraph</code> object is used to build the graph that
	/// is used to represent the serialized object and its references. The
	/// graph is stored in an <code>IdentityHashMap</code> which will 
	/// store the objects in such a way that this graph object can tell if
	/// it has already been written to the XML document. If an object has
	/// already been written to the XML document an reference attribute
	/// is added to the element representing the object and serialization
	/// of that object is complete, that is, no more elements are written.
	/// <p>
	/// The attribute values written by this are unique strings, which 
	/// allows the deserialization process to identify object references
	/// easily. By default these references are incrementing integers 
	/// however for deserialization they can be any unique string value.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class WriteGraph:IdentityHashMap
	{
		/// <summary> This is used to specify the length of array instances.</summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Object, String >
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
		
		/// <summary> Constructor for the <code>WriteGraph</code> object. This is
		/// used to build the graph used for writing objects to the XML 
		/// document. The specified strategy is used to acquire the names
		/// of the special attributes used during the serialization.
		/// 
		/// </summary>
		/// <param name="contract">this is the name scheme used by the strategy 
		/// </param>
		public WriteGraph(Contract contract)
		{
			InitBlock();
			this.refer = contract.Reference;
			this.mark = contract.Identity;
			this.length = contract.Length;
			this.label = contract.Label;
		}
		
		/// <summary> This is used to write the XML element attributes representing
		/// the serialized object instance. If the object has already been
		/// serialized to the XML document then a reference attribute is
		/// inserted and this returns true, if not, then this will write
		/// a unique identity marker attribute and return false.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the object to be serialized
		/// </param>
		/// <param name="value">this is the instance that is to be serialized    
		/// </param>
		/// <param name="node">this is the node that contains the attributes
		/// 
		/// </param>
		/// <returns> returns true if the element has been fully written
		/// </returns>
		public virtual bool write(Type type, System.Object value_Renamed, NodeMap node)
		{
			System.Type actual = value_Renamed.GetType();
			System.Type expect = type.getType();
			System.Type real = actual;
			
			if (actual.IsArray)
			{
				real = writeArray(actual, value_Renamed, node);
			}
			if (actual != expect)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				node.put(label, real.FullName);
			}
			return writeReference(value_Renamed, node);
		}
		
		/// <summary> This is used to write the XML element attributes representing
		/// the serialized object instance. If the object has already been
		/// serialized to the XML document then a reference attribute is
		/// inserted and this returns true, if not, then this will write
		/// a unique identity marker attribute and return false.
		/// 
		/// </summary>
		/// <param name="value">this is the instance that is to be serialized    
		/// </param>
		/// <param name="node">this is the node that contains the attributes
		/// 
		/// </param>
		/// <returns> returns true if the element has been fully written
		/// </returns>
		private bool writeReference(System.Object value_Renamed, NodeMap node)
		{
			System.String name = get_Renamed(value_Renamed);
			int size = size();
			
			if (name != null)
			{
				node.put(refer, name);
				return true;
			}
			System.String unique = System.Convert.ToString(size);
			
			node.put(mark, unique);
			put(value_Renamed, unique);
			
			return false;
		}
		
		/// <summary> This is used to add a length attribute to the element due to
		/// the fact that the serialized value is an array. The length
		/// of the array is acquired and inserted in to the attributes.
		/// 
		/// </summary>
		/// <param name="field">this is the field type for the array to set
		/// </param>
		/// <param name="value">this is the actual value for the array to set
		/// </param>
		/// <param name="node">this is the map of attributes for the element
		/// 
		/// </param>
		/// <returns> returns the array component type that is set
		/// </returns>
		private System.Type writeArray(System.Type field, System.Object value_Renamed, NodeMap node)
		{
			int size = ((System.Array) value_Renamed).Length;
			
			if (!containsKey(value_Renamed))
			{
				node.put(length, System.Convert.ToString(size));
			}
			return field.GetElementType();
		}
	}
}