/*
* OutputNodeMap.java July 2006
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
	
	/// <summary> The <code>OutputNodeMap</code> is used to collect attribute nodes
	/// for an output node. This will create a generic node to add to the
	/// map. The nodes created will be used by the output node to write
	/// attributes for an element.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class OutputNodeMap:LinkedHashMap
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
		virtual public OutputNode Node
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
		< String, OutputNode > implements NodeMap < OutputNode >
		//UPGRADE_NOTE: Final was removed from the declaration of 'source '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private OutputNode source;
		
		/// <summary> Constructor for the <code>OutputNodeMap</code> object. This is
		/// used to create a node map that is used to create and collect
		/// nodes, which will be used as attributes for an output element.
		/// </summary>
		public OutputNodeMap(OutputNode source)
		{
			InitBlock();
			this.source = source;
		}
		
		/// <summary> This is used to add a new <code>Node</code> to the map. The
		/// node that is created is a simple name value pair. Once the
		/// node is created it can be retrieved by its given name.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the node to be created
		/// </param>
		/// <param name="value">this is the value to be given to the node
		/// 
		/// </param>
		/// <returns> this is the node that has been added to the map
		/// </returns>
		public virtual OutputNode put(System.String name, System.String value_Renamed)
		{
			OutputNode node = new OutputAttribute(source, name, value_Renamed);
			
			if (source != null)
			{
				put(name, node);
			}
			return node;
		}
		
		/// <summary> This is used to remove the <code>Node</code> mapped to the
		/// given name.  This returns a name value pair that represents
		/// an attribute. If no node is mapped to the specified name 
		/// then this method will a return null value.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the node to remove
		/// 
		/// </param>
		/// <returns> this will return the node mapped to the given name
		/// </returns>
		public virtual OutputNode remove(System.String name)
		{
			return base.remove(name);
		}
		
		/// <summary> This is used to acquire the <code>Node</code> mapped to the
		/// given name. This returns a name value pair that represents
		/// an element. If no node is mapped to the specified name then 
		/// this method will return a null value.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the node to retrieve
		/// 
		/// </param>
		/// <returns> this will return the node mapped to the given name
		/// </returns>
		public virtual OutputNode get_Renamed(System.String name)
		{
			return base.get_Renamed(name);
		}
		
		/// <summary> This returns an iterator for the names of all the nodes in
		/// this <code>OutputNodeMap</code>. This allows the names to be 
		/// iterated within a for each loop in order to extract nodes.
		/// 
		/// </summary>
		/// <returns> this returns the names of the nodes in the map
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < String > iterator()
	}
}