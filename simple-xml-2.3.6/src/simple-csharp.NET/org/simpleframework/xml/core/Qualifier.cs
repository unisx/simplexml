/*
* Qualifier.java July 2008
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
using NamespaceList = org.simpleframework.xml.NamespaceList;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Qualifier</code> object is used to provide decorations
	/// to an output node for namespaces. This will scan a provided
	/// contact object for namespace annotations. If any are found they
	/// can then be used to apply these namespaces to the provided node.
	/// The <code>Contact</code> objects can represent fields or methods
	/// that have been annotated with XML annotations.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Qualifier : Decorator
	{
		
		/// <summary> This is the namespace decorator that is populated for use.</summary>
		private NamespaceDecorator decorator;
		
		/// <summary> Constructor for the <code>Qualifier</code> object. This is
		/// used to create a decorator that will scan the provided
		/// contact for <code>Namespace</code> annotations. These can
		/// then be applied to the output node to provide qualification.
		/// 
		/// </summary>
		/// <param name="contact">this is the contact to be scanned 
		/// </param>
		public Qualifier(Contact contact)
		{
			this.decorator = new NamespaceDecorator();
			this.scan(contact);
		}
		
		/// <summary> This method is used to decorate the provided node. This node 
		/// can be either an XML element or an attribute. Decorations that
		/// can be applied to the node by invoking this method include
		/// things like namespaces and namespace lists.
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to be decorated by this
		/// </param>
		public virtual void  decorate(OutputNode node)
		{
			decorator.decorate(node);
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
		/// <param name="secondary">this is a secondary decorator to be applied
		/// </param>
		public virtual void  decorate(OutputNode node, Decorator secondary)
		{
			decorator.decorate(node, secondary);
		}
		
		/// <summary> This method is used to scan the <code>Contact</code> provided
		/// to determine if there are any namespace annotations. If there
		/// are any annotations then these are added to the internal
		/// namespace decorator. This ensues that they can be applied to
		/// the node when that node requests decoration.
		/// 
		/// </summary>
		/// <param name="contact">this is the contact to be scanned for namespaces
		/// </param>
		private void  scan(Contact contact)
		{
			namespace_Renamed(contact);
			scope(contact);
		}
		
		/// <summary> This is use to scan for <code>Namespace</code> annotations on
		/// the contact. Once a namespace has been located then it is used
		/// to populate the internal namespace decorator. This can then be
		/// used to decorate any output node that requires it.
		/// 
		/// </summary>
		/// <param name="contact">this is the contact to scan for namespaces
		/// </param>
		private void  namespace_Renamed(Contact contact)
		{
			Namespace primary = contact.getAnnotation(typeof(Namespace));
			
			if (primary != null)
			{
				decorator.set_Renamed(primary);
				decorator.add(primary);
			}
		}
		
		/// <summary> This is use to scan for <code>NamespaceList</code> annotations 
		/// on the contact. Once a namespace list has been located then it is 
		/// used to populate the internal namespace decorator. This can then 
		/// be used to decorate any output node that requires it.
		/// 
		/// </summary>
		/// <param name="contact">this is the contact to scan for namespace lists
		/// </param>
		private void  scope(Contact contact)
		{
			NamespaceList scope = contact.getAnnotation(typeof(NamespaceList));
			
			if (scope != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Namespace name: scope.value())
				{
					decorator.add(name);
				}
			}
		}
	}
}