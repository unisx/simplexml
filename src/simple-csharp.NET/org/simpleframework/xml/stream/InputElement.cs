/*
* InputElement.java July 2006
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
	
	/// <summary> The <code>InputElement</code> represents a self contained element
	/// that will allow access to its child elements. If the next element
	/// read from the <code>NodeReader</code> is not a child then this
	/// will return null. The input element node also allows the attribute
	/// values associated with the node to be accessed.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.stream.NodeReader">
	/// </seealso>
	class InputElement : InputNode
	{
		private void  InitBlock()
		{
			return map;
		}
		/// <summary> This is used to return the source object for this node. This
		/// is used primarily as a means to determine which XML provider
		/// is parsing the source document and producing the nodes. It
		/// is useful to be able to determine the XML provider like this.
		/// 
		/// </summary>
		/// <returns> this returns the source of this input node
		/// </returns>
		virtual public System.Object Source
		{
			get
			{
				return node.Source;
			}
			
		}
		/// <summary> This provides the position of this node within the document.
		/// This allows the user of this node to report problems with
		/// the location within the document, allowing the XML to be
		/// debugged if it does not match the class schema.
		/// 
		/// </summary>
		/// <returns> this returns the position of the XML read cursor
		/// </returns>
		virtual public Position Position
		{
			get
			{
				return new InputPosition(node);
			}
			
		}
		/// <summary> Returns the name of the node that this represents. This is
		/// an immutable property and should not change for any node.  
		/// This provides the name without the name space part.
		/// 
		/// </summary>
		/// <returns> returns the name of the node that this represents
		/// </returns>
		virtual public System.String Name
		{
			get
			{
				return node.Name;
			}
			
		}
		/// <summary> This is used to acquire the namespace prefix for the node.
		/// If there is no namespace prefix for the node then this will
		/// return null. Acquiring the prefix enables the qualification
		/// of the node to be determined. It also allows nodes to be
		/// grouped by its prefix and allows group operations.
		/// 
		/// </summary>
		/// <returns> this returns the prefix associated with this node
		/// </returns>
		virtual public System.String Prefix
		{
			get
			{
				return node.Prefix;
			}
			
		}
		/// <summary> This allows the namespace reference URI to be determined.
		/// A reference is a globally unique string that allows the
		/// node to be identified. Typically the reference will be a URI
		/// but it can be any unique string used to identify the node.
		/// This allows the node to be identified within the namespace.
		/// 
		/// </summary>
		/// <returns> this returns the associated namespace reference URI 
		/// </returns>
		virtual public System.String Reference
		{
			get
			{
				return node.Reference;
			}
			
		}
		/// <summary> This method is used to determine if this node is the root 
		/// node for the XML document. The root node is the first node
		/// in the document and has no sibling nodes. This is false
		/// if the node has a parent node or a sibling node.
		/// 
		/// </summary>
		/// <returns> true if this is the root node within the document
		/// </returns>
		virtual public bool Root
		{
			get
			{
				return reader.isRoot(this);
			}
			
		}
		/// <summary> This is used to determine if this node is an element. This
		/// allows users of the framework to make a distinction between
		/// nodes that represent attributes and nodes that represent
		/// elements. This is particularly useful given that attribute
		/// nodes do not maintain a node map of attributes.
		/// 
		/// </summary>
		/// <returns> this returns true as this instance is an element
		/// </returns>
		virtual public bool Element
		{
			get
			{
				return true;
			}
			
		}
		/// <summary> This is used to determine if this input node is empty. An
		/// empty node is one with no attributes or children. This can
		/// be used to determine if a given node represents an empty
		/// entity, with which no extra data can be extracted.
		/// 
		/// </summary>
		/// <returns> this returns true if the node is an empty element
		/// 
		/// </returns>
		/// <throws>  Exception thrown if there was a parse error </throws>
		virtual public bool Empty
		{
			get
			{
				if (!map.Empty)
				{
					return false;
				}
				return reader.isEmpty(this);
			}
			
		}
		
		/// <summary> This contains all the attributes associated with the element.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private InputNodeMap map;
		
		/// <summary> This is the node reader that reads from the XML document.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'reader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private NodeReader reader;
		
		/// <summary> This is the parent node for this XML input element node.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private InputNode parent;
		
		/// <summary> This is the XML element that this node provides access to.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private EventNode node;
		
		/// <summary> Constructor for the <code>InputElement</code> object. This 
		/// is used to create an input node that will provide access to 
		/// an XML element. All attributes associated with the element 
		/// given are extracted and exposed via the attribute node map.
		/// 
		/// </summary>
		/// <param name="parent">this is the parent XML element for this 
		/// </param>
		/// <param name="reader">this is the reader used to read XML elements
		/// </param>
		/// <param name="node">this is the XML element wrapped by this node
		/// </param>
		public InputElement(InputNode parent, NodeReader reader, EventNode node)
		{
			InitBlock();
			this.map = new InputNodeMap(this, node);
			this.reader = reader;
			this.parent = parent;
			this.node = node;
		}
		
		/// <summary> This is used to acquire the <code>Node</code> that is the
		/// parent of this node. This will return the node that is
		/// the direct parent of this node and allows for siblings to
		/// make use of nodes with their parents if required.  
		/// 
		/// </summary>
		/// <returns> this returns the parent node for this node
		/// </returns>
		public virtual InputNode getParent()
		{
			return parent;
		}
		
		/// <summary> Provides an attribute from the element represented. If an
		/// attribute for the specified name does not exist within the
		/// element represented then this method will return null.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the attribute to retrieve
		/// 
		/// </param>
		/// <returns> this returns the value for the named attribute
		/// </returns>
		public virtual InputNode getAttribute(System.String name)
		{
			return map.get_Renamed(name);
		}
		
		/// <summary> This returns a map of the attributes contained within the
		/// element. If no elements exist within the element then this
		/// returns an empty map. 
		/// 
		/// </summary>
		/// <returns> this returns a map of attributes for the element
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public NodeMap < InputNode > getAttributes()
		
		/// <summary> Returns the value for the node that this represents. This 
		/// is an immutable value for the node and cannot be changed.
		/// If there is a problem reading an exception is thrown.
		/// 
		/// </summary>
		/// <returns> the name of the value for this node instance
		/// </returns>
		public virtual System.String getValue()
		{
			return reader.readValue(this);
		}
		
		/// <summary> The method is used to acquire the next child attribute of this 
		/// element. If the next element from the <code>NodeReader</code> 
		/// is not a child node to the element that this represents then
		/// this will return null, which ensures each element represents
		/// a self contained collection of child nodes.
		/// 
		/// </summary>
		/// <returns> this returns the next child element of this node
		/// 
		/// </returns>
		/// <exception cref="Exception">thrown if there is a problem reading
		/// </exception>
		public virtual InputNode getNext()
		{
			return reader.readElement(this);
		}
		
		/// <summary> The method is used to acquire the next child attribute of this 
		/// element. If the next element from the <code>NodeReader</code> 
		/// is not a child node to the element that this represents then
		/// this will return null, also if the next element does not match
		/// the specified name then this will return null.
		/// 
		/// </summary>
		/// <param name="name">this is the name expected fromt he next element
		/// 
		/// </param>
		/// <returns> this returns the next child element of this node
		/// 
		/// </returns>
		/// <exception cref="Exception">thrown if there is a problem reading
		/// </exception>
		public virtual InputNode getNext(System.String name)
		{
			return reader.readElement(this, name);
		}
		
		/// <summary> This method is used to skip all child elements from this
		/// element. This allows elements to be effectively skipped such
		/// that when parsing a document if an element is not required
		/// then that element can be completely removed from the XML.
		/// 
		/// </summary>
		/// <exception cref="Exception">thrown if there was a parse error
		/// </exception>
		public virtual void  skip()
		{
			reader.skipElement(this);
		}
		
		/// <summary> This is the string representation of the element. It is
		/// used for debugging purposes. When evaluating the element
		/// the to string can be used to print out the element name.
		/// 
		/// </summary>
		/// <returns> this returns a text description of the element
		/// </returns>
		public override System.String ToString()
		{
			return String.format("element %s", Name);
		}
	}
}