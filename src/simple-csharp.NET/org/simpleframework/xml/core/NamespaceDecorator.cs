/*
* NamespaceDecorator.java July 2008
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
using Namespace = org.simpleframework.xml.Namespace;
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.NamespaceMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using NamespaceMap = org.simpleframework.xml.stream.NamespaceMap;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>NamespaceDecorator</code> object is used to decorate
	/// any output node with namespaces. All namespaces added to this are
	/// applied to nodes that require decoration. This can add namespaces
	/// to the node as well as setting the primary namespace reference
	/// for the node. This results in qualification for the node.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Qualifier">
	/// </seealso>
	class NamespaceDecorator : Decorator
	{
		
		/// <summary> This is used to contain the namespaces used for scoping.</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Namespace > scope;
		
		/// <summary> This is used to set the primary namespace reference used.</summary>
		private Namespace primary;
		
		/// <summary> Constructor for the <code>NamespaceDecorator</code> object. A
		/// namespace decorator can be used for applying namespaces to a
		/// specified node. It can add namespaces to set the scope of the
		/// namespace reference to the node and it can also be used to set
		/// the primary namespace reference used for the node.
		/// </summary>
		public NamespaceDecorator()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			this.scope = new ArrayList < Namespace >();
		}
		
		/// <summary> This is used to set the primary namespace for nodes that will
		/// be decorated by the namespace decorator. If no namespace is set
		/// using this method then this decorator will leave the namespace
		/// reference unchanged and only add namespaces for scoping.
		/// 
		/// </summary>
		/// <param name="namespace">this is the primary namespace to be set
		/// </param>
		public virtual void  set_Renamed(Namespace namespace_Renamed)
		{
			if (namespace_Renamed != null)
			{
				add(namespace_Renamed);
			}
			primary = namespace_Renamed;
		}
		
		/// <summary> This is used to add a namespace to the decorator so that it can
		/// be added to decorated nodes. Namespaces that are added will be
		/// set on the element so that child elements can reference the
		/// namespace and will thus inherit the prefix from that elment.
		/// 
		/// </summary>
		/// <param name="namespace">this is the namespace to be added for scoping
		/// </param>
		public virtual void  add(Namespace namespace_Renamed)
		{
			scope.add(namespace_Renamed);
		}
		
		/// <summary> This method is used to decorate the provided node. This node 
		/// can be either an XML element or an attribute. Decorations that
		/// can be applied to the node by invoking this method include
		/// things like comments and namespaces.
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to be decorated by this
		/// </param>
		public virtual void  decorate(OutputNode node)
		{
			decorate(node, null);
		}
		
		/// <summary> This method is used to decorate the provided node. This node 
		/// can be either an XML element or an attribute. Decorations that
		/// can be applied to the node by invoking this method include
		/// things like namespaces and namespace lists. This can also be 
		/// given another <code>Decorator</code> which is applied before 
		/// this decorator, any common data can then be overwritten.
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to be decorated by this
		/// </param>
		/// <param name="decorator">this is a secondary decorator to be applied
		/// </param>
		public virtual void  decorate(OutputNode node, Decorator decorator)
		{
			if (decorator != null)
			{
				decorator.decorate(node);
			}
			scope(node);
			namespace_Renamed(node);
		}
		
		/// <summary> This is use to apply for <code>NamespaceList</code> annotations 
		/// on the node. If there is no namespace list then this will return 
		/// and the node will be left unchanged. If however the namespace 
		/// list is not empty the the namespaces are added.
		/// 
		/// </summary>
		/// <param name="node">this is the node to apply the namespace list to
		/// </param>
		private void  scope(OutputNode node)
		{
			NamespaceMap map = node.getNamespaces();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Namespace next: scope)
			{
				System.String reference = next.reference();
				System.String prefix = next.prefix();
				
				map.put(reference, prefix);
			}
		}
		
		/// <summary> This is use to apply the <code>Namespace</code> annotations on
		/// the node. If there is no namespace then this will return and
		/// the node will be left unchanged. If however the namespace is 
		/// not null then the reference is applied to the specified node.
		/// 
		/// </summary>
		/// <param name="node">this is the node to apply the namespace to
		/// </param>
		private void  namespace_Renamed(OutputNode node)
		{
			if (primary != null)
			{
				System.String reference = primary.reference();
				
				node.setReference(reference);
			}
		}
	}
}