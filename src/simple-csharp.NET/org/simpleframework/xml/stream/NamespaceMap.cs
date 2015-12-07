/*
* NamespaceMap.java July 2008
*
* Copyright (C) 2008, Niall Gallagher <niallg@users.sf.net>
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
	
	/// <summary> The <code>NamespaceMap</code> object is used store the namespaces
	/// for an element. Each namespace added to this map can be added
	/// with a prefix. A prefix is added only if the associated reference
	/// has not been added to a parent element. If a parent element has
	/// the associated reference, then the parents prefix is the one that
	/// will be returned when requested from this map. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	interface NamespaceMap extends Iterable < String >
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	
	/// <summary> This is the prefix that is associated with the source element.
	/// If the source element does not contain a namespace reference
	/// then this will return its parents namespace. This ensures 
	/// that if a namespace has been declared its child elements will
	/// inherit its prefix.
	/// 
	/// </summary>
	/// <returns> this returns the prefix that is currently in scope
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getPrefix();
	
	/// <summary> This acquires the prefix for the specified namespace reference.
	/// If the namespace reference has been set on this node with a
	/// given prefix then that prefix is returned, however if it has
	/// not been set this will search the parent elements to find the
	/// prefix that is in scope for the specified reference.
	/// 
	/// </summary>
	/// <param name="reference">the reference to find a matching prefix for
	/// 
	/// </param>
	/// <returns> this will return the prefix that is is scope
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String get(String reference);
	
	/// <summary> This is used to remove the prefix that is matched to the 
	/// given reference. If no prefix is matched to the reference then
	/// this will silently return. This will only remove mappings
	/// from the current map, and will ignore the parent nodes.
	/// 
	/// </summary>
	/// <param name="reference">this is the reference that is to be removed 
	/// 
	/// </param>
	/// <returns> this returns the prefix that was matched to this
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String remove(String reference);
	
	/// <summary> This returns an iterator for the namespace of all the nodes 
	/// in this <code>NamespaceMap</code>. This allows the namespaces 
	/// to be iterated within a for each loop in order to extract the
	/// prefix values associated with the map.
	/// 
	/// </summary>
	/// <returns> this returns the namespaces contained in this map
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Iterator < String > iterator();
	
	/// <summary> This is used to add the namespace reference to the namespace
	/// map. If the namespace has been added to a parent node then
	/// this will not add the reference. The prefix added to the map
	/// will be the default namespace, which is an empty prefix.
	/// 
	/// </summary>
	/// <param name="reference">this is the reference to be added 
	/// 
	/// </param>
	/// <returns> this returns the prefix that has been replaced
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String put(String reference);
	
	/// <summary> This is used to add the namespace reference to the namespace
	/// map. If the namespace has been added to a parent node then
	/// this will not add the reference. 
	/// 
	/// </summary>
	/// <param name="reference">this is the reference to be added 
	/// </param>
	/// <param name="prefix">this is the prefix to be added to the reference
	/// 
	/// </param>
	/// <returns> this returns the prefix that has been replaced
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String put(String reference, String prefix);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}