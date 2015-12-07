/*
* InputNodeMap.java July 2006
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
//UPGRADE_TODO: The type 'java.util.LinkedHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedHashMap = java.util.LinkedHashMap;
namespace org.simpleframework.xml.stream
{
	
	/// <summary> The <code>InputNodeMap</code> object represents a map to contain
	/// attributes used by an input node. This can be used as an empty
	/// node map, it can be used to extract its values from a start
	/// element. This creates <code>InputAttribute</code> objects for 
	/// each node added to the map, these can then be used by an element
	/// input node to represent attributes as input nodes.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class InputNodeMap:LinkedHashMap
	{
		/// <summary> This is the source node that this node map belongs to.</summary>
		private void  InitBlock()
		{
			
			return keySet().iterator();
		}
		/// <summary> This is used to acquire the actual node this map represents.
		/// The source node provides further details on the context of
		/// the node, such as the parent name, the namespace, and even
		/// the value in the node. Care should be taken when using this. 
		/// 
		/// </summary>
		/// <returns> this returns the node that this map represents
		/// </returns>
		virtual public InputNode Node
		{
			get
			{
				return source;
			}
			
		}
		/// <summary> This is used to get the name of the element that owns the
		/// nodes for the specified map. This can be used to determine
		/// which element the node map belongs to.
		/// 
		/// </summary>
		/// <returns> this returns the name of the owning element
		/// </returns>
		virtual public System.String Name
		{
			get
			{
				return source.Name;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< String, InputNode > implements NodeMap < InputNode >
		//UPGRADE_NOTE: Final was removed from the declaration of 'source '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private InputNode source;
		
		/// <summary> Constructor for the <code>InputNodeMap</code> object. This
		/// is used to create an empty input node map, which will create
		/// <code>InputAttribute</code> object for each inserted node.
		/// 
		/// </summary>
		/// <param name="source">this is the node this node map belongs to
		/// </param>
		protected internal InputNodeMap(InputNode source)
		{
			InitBlock();
			this.source = source;
		}
		
		/// <summary> Constructor for the <code>InputNodeMap</code> object. This
		/// is used to create an input node map, which will be populated
		/// with the attributes from the <code>StartElement</code> that
		/// is specified.
		/// 
		/// </summary>
		/// <param name="source">this is the node this node map belongs to
		/// </param>
		/// <param name="element">the element to populate the node map with
		/// </param>
		public InputNodeMap(InputNode source, EventNode element)
		{
			InitBlock();
			this.source = source;
			this.build(element);
		}
		
		/// <summary> This is used to insert all attributes belonging to the start
		/// element to the map. All attributes acquired from the element
		/// are converted into <code>InputAttribute</code> objects so 
		/// that they can be used as input nodes by an input node.
		/// 
		/// </summary>
		/// <param name="element">the element to acquire attributes from
		/// </param>
		private void  build(EventNode element)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Attribute entry: element)
			{
				InputAttribute value_Renamed = new InputAttribute(source, entry);
				
				if (!entry.isReserved())
				{
					put(value_Renamed.Name, value_Renamed);
				}
			}
		}
		
		/// <summary> This is used to add a new <code>InputAttribute</code> node to
		/// the map. The created node can be used by an input node to
		/// to represent the attribute as another input node. Once the 
		/// node is created it can be acquired using the specified name.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the node to be created
		/// </param>
		/// <param name="value">this is the value to be given to the node
		/// 
		/// </param>
		/// <returns> this returns the node that has just been added
		/// </returns>
		public virtual InputNode put(System.String name, System.String value_Renamed)
		{
			InputNode node = new InputAttribute(source, name, value_Renamed);
			
			if (name != null)
			{
				put(name, node);
			}
			return node;
		}
		
		/// <summary> This is used to remove the <code>Node</code> mapped to the
		/// given name.  This returns a name value pair that represents
		/// an attribute. If no node is mapped to the specified name 
		/// then this method will return a null value.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the node to remove
		/// 
		/// </param>
		/// <returns> this will return the node mapped to the given name
		/// </returns>
		public virtual InputNode remove(System.String name)
		{
			return base.remove(name);
		}
		
		/// <summary> This is used to acquire the <code>Node</code> mapped to the
		/// given name. This returns a name value pair that represents
		/// an attribute. If no node is mapped to the specified name 
		/// then this method will return a null value.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the node to retrieve
		/// 
		/// </param>
		/// <returns> this will return the node mapped to the given name
		/// </returns>
		public virtual InputNode get_Renamed(System.String name)
		{
			return base.get_Renamed(name);
		}
		
		/// <summary> This returns an iterator for the names of all the nodes in
		/// this <code>NodeMap</code>. This allows the names to be 
		/// iterated within a for each loop in order to extract nodes.
		/// 
		/// </summary>
		/// <returns> this returns the names of the nodes in the map
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < String > iterator()
	}
}