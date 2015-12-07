/*
* TreeStrategy.java July 2006
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
namespace org.simpleframework.xml.strategy
{
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.simpleframework.xml.strategy.Name.LABEL;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.simpleframework.xml.strategy.Name.LENGTH;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.lang.reflect.Array;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.Map;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.stream.Node;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.stream.NodeMap;
	
	/// <summary> The <code>TreeStrategy</code> object is used to provide a simple
	/// strategy for handling object graphs in a tree structure. This does
	/// not resolve cycles in the object graph. This will make use of the
	/// specified class attribute to resolve the class to use for a given 
	/// element during the deserialization process. For the serialization 
	/// process the "class" attribute will be added to the element specified.
	/// If there is a need to use an attribute name other than "class" then 
	/// the name of the attribute to use can be specified.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.strategy.CycleStrategy">
	/// </seealso>
	public class TreeStrategy : Strategy
	{
		
		/// <summary> This is the loader that is used to load the specified class.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'loader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Loader loader;
		
		/// <summary> This is the attribute that is used to determine an array size.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'length '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String length;
		
		/// <summary> This is the attribute that is used to determine the real type.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String label;
		
		/// <summary> Constructor for the <code>TreeStrategy</code> object. This 
		/// is used to create a strategy that can resolve and load class
		/// objects for deserialization using a "class" attribute. Also
		/// for serialization this will add the appropriate "class" value.
		/// </summary>
		public TreeStrategy():this(LABEL, LENGTH)
		{
		}
		
		/// <summary> Constructor for the <code>TreeStrategy</code> object. This 
		/// is used to create a strategy that can resolve and load class
		/// objects for deserialization using the specified attribute. 
		/// The attribute value can be any legal XML attribute name.
		/// 
		/// </summary>
		/// <param name="label">this is the name of the attribute to use
		/// </param>
		/// <param name="length">this is used to determine the array length
		/// </param>
		public TreeStrategy(System.String label, System.String length)
		{
			this.loader = new Loader();
			this.length = length;
			this.label = label;
		}
		
		/// <summary> This is used to resolve and load a class for the given element.
		/// Resolution of the class to used is done by inspecting the
		/// XML element provided. If there is a "class" attribute on the
		/// element then its value is used to resolve the class to use.
		/// If no such attribute exists on the element this returns null.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the XML element expected
		/// </param>
		/// <param name="node">this is the element used to resolve an override
		/// </param>
		/// <param name="map">this is used to maintain contextual information
		/// 
		/// </param>
		/// <returns> returns the class that should be used for the object
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the class cannot be resolved </throws>
		public virtual Value read(Type type, NodeMap node, Map map)
		{
			System.Type actual = readValue(type, node);
			System.Type expect = type.getType();
			
			if (expect.IsArray)
			{
				return readArray(actual, node);
			}
			if (expect != actual)
			{
				return new ObjectValue(actual);
			}
			return null;
		}
		
		/// <summary> This is used to resolve and load a class for the given element.
		/// Resolution of the class to used is done by inspecting the
		/// XML element provided. If there is a "class" attribute on the
		/// element then its value is used to resolve the class to use.
		/// This also expects a "length" attribute for the array length.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the XML element expected
		/// </param>
		/// <param name="node">this is the element used to resolve an override
		/// 
		/// </param>
		/// <returns> returns the class that should be used for the object
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the class cannot be resolved </throws>
		private Value readArray(System.Type type, NodeMap node)
		{
			Node entry = node.remove(length);
			int size = 0;
			
			if (entry != null)
			{
				System.String value_Renamed = entry.getValue();
				size = System.Int32.Parse(value_Renamed);
			}
			return new ArrayValue(type, size);
		}
		
		/// <summary> This is used to resolve and load a class for the given element.
		/// Resolution of the class to used is done by inspecting the
		/// XML element provided. If there is a "class" attribute on the
		/// element then its value is used to resolve the class to use.
		/// If no such attribute exists the specified field is returned,
		/// or if the field type is an array then the component type.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the XML element expected
		/// </param>
		/// <param name="node">this is the element used to resolve an override
		/// 
		/// </param>
		/// <returns> returns the class that should be used for the object
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the class cannot be resolved </throws>
		private System.Type readValue(Type type, NodeMap node)
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
			return expect;
		}
		
		/// <summary> This is used to attach a attribute to the provided element
		/// that is used to identify the class. The attribute name is
		/// "class" and has the value of the fully qualified class 
		/// name for the object provided. This will only be invoked
		/// if the object class is different from the field class.
		/// 
		/// </summary>
		/// <param name="type">this is the declared class for the field used
		/// </param>
		/// <param name="value">this is the instance variable being serialized
		/// </param>
		/// <param name="node">this is the element used to represent the value
		/// </param>
		/// <param name="map">this is used to maintain contextual information
		/// 
		/// </param>
		/// <returns> this returns true if serialization is complete
		/// </returns>
		public virtual bool write(Type type, System.Object value_Renamed, NodeMap node, Map map)
		{
			System.Type actual = value_Renamed.GetType();
			System.Type expect = type.getType();
			System.Type real = actual;
			
			if (actual.IsArray)
			{
				real = writeArray(expect, value_Renamed, node);
			}
			if (actual != expect)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				node.put(label, real.FullName);
			}
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
			int size = Array.getLength(value_Renamed);
			
			if (length != null)
			{
				node.put(length, System.Convert.ToString(size));
			}
			return field.GetElementType();
		}
	}
}