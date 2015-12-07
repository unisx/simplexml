/*
* NodeMap.java July 2006
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
namespace org.simpleframework.xml.stream
{
	
	/// <summary> The <code>NodeMap</code> object represents a map of nodes that
	/// can be set as name value pairs. This typically represents the
	/// attributes that belong to an element and is used as an neutral
	/// way to access an element for either an input or output event.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.stream.Node">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	interface NodeMap < T extends Node > extends Iterable < String >
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	
	/// <summary> This is used to acquire the actual node this map represents.
	/// The source node provides further details on the context of
	/// the node, such as the parent name, the namespace, and even
	/// the value in the node. Care should be taken when using this. 
	/// 
	/// </summary>
	/// <returns> this returns the node that this map represents
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public T getNode();
	
	/// <summary> This is used to get the name of the element that owns the
	/// nodes for the specified map. This can be used to determine
	/// which element the node map belongs to.
	/// 
	/// </summary>
	/// <returns> this returns the name of the owning element
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getName();
	
	/// <summary> This is used to acquire the <code>Node</code> mapped to the
	/// given name. This returns a name value pair that represents
	/// either an attribute or element. If no node is mapped to the
	/// specified name then this method will return null.
	/// 
	/// </summary>
	/// <param name="name">this is the name of the node to retrieve
	/// 
	/// </param>
	/// <returns> this will return the node mapped to the given name
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public T get(String name);
	
	/// <summary> This is used to remove the <code>Node</code> mapped to the
	/// given name.  This returns a name value pair that represents
	/// either an attribute or element. If no node is mapped to the
	/// specified name then this method will return null.
	/// 
	/// </summary>
	/// <param name="name">this is the name of the node to remove
	/// 
	/// </param>
	/// <returns> this will return the node mapped to the given name
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public T remove(String name);
	
	/// <summary> This returns an iterator for the names of all the nodes in
	/// this <code>NodeMap</code>. This allows the names to be 
	/// iterated within a for each loop in order to extract nodes.
	/// 
	/// </summary>
	/// <returns> this returns the names of the nodes in the map
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Iterator < String > iterator();
	
	/// <summary> This is used to add a new <code>Node</code> to the map. The
	/// type of node that is created an added is left up to the map
	/// implementation. Once a node is created with the name value
	/// pair it can be retrieved and used.
	/// 
	/// </summary>
	/// <param name="name">this is the name of the node to be created
	/// </param>
	/// <param name="value">this is the value to be given to the node
	/// 
	/// </param>
	/// <returns> this is the node that has been added to the map
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public T put(String name, String value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}