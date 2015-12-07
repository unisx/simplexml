/*
* PrefixResolver.java July 2008
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
//UPGRADE_TODO: The type 'java.util.LinkedHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedHashMap = java.util.LinkedHashMap;
namespace org.simpleframework.xml.stream
{
	
	/// <summary> The <code>PrefixResolver</code> object will store the namespaces
	/// for an element. Each namespace added to this map can be added
	/// with a prefix. A prefix is added only if the associated reference
	/// has not been added to a parent element. If a parent element has
	/// the associated reference, then the parents prefix is the one that
	/// will be returned when requested from this map. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.stream.OutputElement">
	/// </seealso>
	class PrefixResolver:LinkedHashMap
	{
		/// <summary> Represents the actual XML element this is associated with.</summary>
		private void  InitBlock()
		{
			
			return keySet().iterator();
		}
		/// <summary> This is the prefix that is associated with the source element.
		/// If the source element does not contain a namespace reference
		/// then this will return its parents namespace. This ensures 
		/// that if a namespace has been declared its child elements will
		/// inherit its prefix.
		/// 
		/// </summary>
		/// <returns> this returns the prefix that is currently in scope
		/// </returns>
		virtual public System.String Prefix
		{
			get
			{
				return source.Prefix;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< String, String > implements NamespaceMap
		//UPGRADE_NOTE: Final was removed from the declaration of 'source '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private OutputNode source;
		
		/// <summary> Constructor for the <code>PrefixResolver</code> object. This
		/// is used to create a resolver for namespace prefixes using 
		/// the hierarchy of elements. Resolving the prefix in this way
		/// avoids having to redeclare the same namespace with another
		/// prefix in a child element if it has already been declared.
		/// 
		/// </summary>
		/// <param name="source">this is the XML element this is associated to
		/// </param>
		public PrefixResolver(OutputNode source)
		{
			InitBlock();
			this.source = source;
		}
		
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
		public virtual System.String put(System.String reference)
		{
			return put(reference, "");
		}
		
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
		public virtual System.String put(System.String reference, System.String prefix)
		{
			System.String parent = resolve(reference);
			
			if (parent != null)
			{
				return null;
			}
			return base.put(reference, prefix);
		}
		
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
		public virtual System.String remove(System.String reference)
		{
			return base.remove(reference);
		}
		
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
		public virtual System.String get_Renamed(System.String reference)
		{
			int size = size();
			
			if (size > 0)
			{
				System.String prefix = base.get_Renamed(reference);
				
				if (prefix != null)
				{
					return prefix;
				}
			}
			return resolve(reference);
		}
		
		/// <summary> This method will resolve the prefix or the specified reference
		/// by searching the parent nodes in order. This allows the prefix
		/// that is currently in scope for the reference to be acquired.
		/// 
		/// </summary>
		/// <param name="reference">the reference to find a matching prefix for
		/// 
		/// </param>
		/// <returns> this will return the prefix that is is scope
		/// </returns>
		private System.String resolve(System.String reference)
		{
			NamespaceMap parent = source.getNamespaces();
			
			if (parent != null)
			{
				System.String prefix = parent.get_Renamed(reference);
				
				if (!containsValue(prefix))
				{
					return prefix;
				}
			}
			return null;
		}
		
		/// <summary> This returns an iterator for the namespace of all the nodes 
		/// in this <code>NamespaceMap</code>. This allows the namespaces 
		/// to be iterated within a for each loop in order to extract the
		/// prefix values associated with the map.
		/// 
		/// </summary>
		/// <returns> this returns the namespaces contained in this map
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < String > iterator()
	}
}