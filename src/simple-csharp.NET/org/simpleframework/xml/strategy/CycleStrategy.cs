/*
* CycleStrategy.java April 2007
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
namespace org.simpleframework.xml.strategy
{
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.simpleframework.xml.strategy.Name.LABEL;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.simpleframework.xml.strategy.Name.LENGTH;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.simpleframework.xml.strategy.Name.MARK;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.simpleframework.xml.strategy.Name.REFER;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.Map;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.stream.NodeMap;
	
	/// <summary> The <code>CycleStrategy</code> represents a strategy that is used
	/// to augment the deserialization and serialization process such that
	/// cycles in an object graph can be supported. This adds additional 
	/// attributes to the serialized XML elements so that during the 
	/// deserialization process an objects cycles can be created. Without
	/// the use of a strategy such as this, cycles could cause an infinite
	/// loop during the serialization process while traversing the graph.
	/// <pre>
	/// 
	/// &lt;root id="1"&gt;
	/// &lt;object id="2"&gt;
	/// &lt;object id="3" name="name"&gt;Example&lt;/item&gt;
	/// &lt;object reference="2"/&gt;
	/// &lt;/object&gt;
	/// &lt;/root&gt;
	/// 
	/// </pre>
	/// In the above serialized XML there is a circular reference, where
	/// the XML element with id "2" contains a reference to itself. In
	/// most data binding frameworks this will cause an infinite loop, 
	/// or in some cases will just fail to represent the references well.
	/// With this strategy you can ensure that cycles in complex object
	/// graphs will be maintained and can be serialized safely.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Persister">
	/// </seealso>
	/// <seealso cref="org.simpleframework.xml.strategy.Strategy">
	/// </seealso>
	public class CycleStrategy : Strategy
	{
		
		/// <summary> This is used to maintain session state for writing the graph.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'write '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private WriteState write_Renamed_Field;
		
		/// <summary> This is used to maintain session state for reading the graph.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'read '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ReadState read_Renamed_Field;
		
		/// <summary> This is used to provide the names of the attributes to use.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'contract '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Contract contract;
		
		/// <summary> Constructor for the <code>CycleStrategy</code> object. This is
		/// used to create a strategy with default values. By default the
		/// values used are "id" and "reference". These values will be
		/// added to XML elements during the serialization process. And 
		/// will be used to deserialize the object cycles fully.
		/// </summary>
		public CycleStrategy():this(MARK, REFER)
		{
		}
		
		/// <summary> Constructor for the <code>CycleStrategy</code> object. This is
		/// used to create a strategy with the specified attributes, which
		/// will be added to serialized XML elements. These attributes 
		/// are used to serialize the objects in such a way the cycles in
		/// the object graph can be deserialized and used fully. 
		/// 
		/// </summary>
		/// <param name="mark">this is used to mark the identity of an object
		/// </param>
		/// <param name="refer">this is used to refer to an existing object
		/// </param>
		public CycleStrategy(System.String mark, System.String refer):this(mark, refer, LABEL)
		{
		}
		
		/// <summary> Constructor for the <code>CycleStrategy</code> object. This is
		/// used to create a strategy with the specified attributes, which
		/// will be added to serialized XML elements. These attributes 
		/// are used to serialize the objects in such a way the cycles in
		/// the object graph can be deserialized and used fully. 
		/// 
		/// </summary>
		/// <param name="mark">this is used to mark the identity of an object
		/// </param>
		/// <param name="refer">this is used to refer to an existing object
		/// </param>
		/// <param name="label">this is used to specify the class for the field
		/// </param>
		public CycleStrategy(System.String mark, System.String refer, System.String label):this(mark, refer, label, LENGTH)
		{
		}
		
		/// <summary> Constructor for the <code>CycleStrategy</code> object. This is
		/// used to create a strategy with the specified attributes, which
		/// will be added to serialized XML elements. These attributes 
		/// are used to serialize the objects in such a way the cycles in
		/// the object graph can be deserialized and used fully. 
		/// 
		/// </summary>
		/// <param name="mark">this is used to mark the identity of an object
		/// </param>
		/// <param name="refer">this is used to refer to an existing object
		/// </param>
		/// <param name="label">this is used to specify the class for the field
		/// </param>
		/// <param name="length">this is the length attribute used for arrays
		/// </param>
		public CycleStrategy(System.String mark, System.String refer, System.String label, System.String length)
		{
			this.contract = new Contract(mark, refer, label, length);
			this.write_Renamed_Field = new WriteState(contract);
			this.read_Renamed_Field = new ReadState(contract);
		}
		
		/// <summary> This method is used to read an object from the specified node.
		/// In order to get the root type the field and node map are 
		/// specified. The field represents the annotated method or field
		/// within the deserialized object. The node map is used to get
		/// the attributes used to describe the objects identity, or in
		/// the case of an existing object it contains an object reference.
		/// 
		/// </summary>
		/// <param name="type">the method or field in the deserialized object
		/// </param>
		/// <param name="node">this is the XML element attributes to read
		/// </param>
		/// <param name="map">this is the session map used for deserialization
		/// 
		/// </param>
		/// <returns> this returns an instance to insert into the object 
		/// </returns>
		public virtual Value read(Type type, NodeMap node, Map map)
		{
			ReadGraph graph = read_Renamed_Field.find(map);
			
			if (graph != null)
			{
				return graph.read(type, node);
			}
			return null;
		}
		
		/// <summary> This is used to write the reference in to the XML element that 
		/// is to be written. This will either insert an object identity if
		/// the object has not previously been written, or, if the object
		/// has already been written in a previous element, this will write
		/// the reference to that object. This allows all cycles within the
		/// graph to be serialized so that they can be fully deserialized. 
		/// 
		/// </summary>
		/// <param name="type">the type of the field or method in the object
		/// </param>
		/// <param name="value">this is the actual object that is to be written
		/// </param>
		/// <param name="node">this is the XML element attribute map to use
		/// </param>
		/// <param name="map">this is the session map used for the serialization
		/// 
		/// </param>
		/// <returns> returns true if the object has been fully serialized
		/// </returns>
		public virtual bool write(Type type, System.Object value_Renamed, NodeMap node, Map map)
		{
			WriteGraph graph = write_Renamed_Field.find(map);
			
			if (graph != null)
			{
				return graph.write(type, value_Renamed, node);
			}
			return false;
		}
	}
}