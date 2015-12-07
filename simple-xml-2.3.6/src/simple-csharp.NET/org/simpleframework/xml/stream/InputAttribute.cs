/*
* InputAttribute.java July 2006
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
	
	/// <summary> The <code>InputAttribute</code> is used to represent an attribute
	/// within an element. Rather than representing an attribute as a
	/// name value pair of strings, an attribute is instead represented
	/// as an input node, in the same manner as an element. The reason
	/// for representing an attribute in this way is such that a uniform
	/// means of extracting and parsing values can be used for inputs.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class InputAttribute : InputNode
	{
		private void  InitBlock()
		{
			return new InputNodeMap(this);
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
				return source;
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
				return parent.Position;
			}
			
		}
		/// <summary> Returns the name of the node that this represents. This is
		/// an immutable property and will not change for this node. 
		/// 
		/// </summary>
		/// <returns> returns the name of the node that this represents
		/// </returns>
		virtual public System.String Name
		{
			get
			{
				return name;
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
				return prefix;
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
				return reference;
			}
			
		}
		/// <summary> This method is used to determine if this node is the root 
		/// node for the XML document. This will return false as this 
		/// node can never be the root node because it is an attribute.
		/// 
		/// </summary>
		/// <returns> this will always return false for attribute nodes
		/// </returns>
		virtual public bool Root
		{
			get
			{
				return false;
			}
			
		}
		/// <summary> This is used to determine if this node is an element. This
		/// node instance can not be an element so this method returns
		/// false. Returning null tells the users of this node that any
		/// attributes added to the node map will be permenantly lost.
		/// 
		/// </summary>
		/// <returns> this returns false as this is an attribute node
		/// </returns>
		virtual public bool Element
		{
			get
			{
				return false;
			}
			
		}
		/// <summary> This is used to determine if this input node is empty. An
		/// empty node is one with no attributes or children. This can
		/// be used to determine if a given node represents an empty
		/// entity, with which no extra data can be extracted.
		/// 
		/// </summary>
		/// <returns> this will always return false as it has a value
		/// </returns>
		virtual public bool Empty
		{
			get
			{
				return false;
			}
			
		}
		
		/// <summary> This is the parent node to this attribute instance.</summary>
		private InputNode parent;
		
		/// <summary> This is the reference associated with this attribute node.</summary>
		private System.String reference;
		
		/// <summary> This is the prefix associated with this attribute node.</summary>
		private System.String prefix;
		
		/// <summary> Represents the name of this input attribute instance.</summary>
		private System.String name;
		
		/// <summary> Represents the value for this input attribute instance.</summary>
		private System.String value_Renamed;
		
		/// <summary> This is the source associated with this input attribute.</summary>
		private System.Object source;
		
		/// <summary> Constructor for the <code>InputAttribute</code> object. This
		/// is used to create an input attribute using the provided name
		/// and value, all other values for this input node will be null.
		/// 
		/// </summary>
		/// <param name="parent">this is the parent node to this attribute
		/// </param>
		/// <param name="name">this is the name for this attribute object
		/// </param>
		/// <param name="value">this is the value for this attribute object
		/// </param>
		public InputAttribute(InputNode parent, System.String name, System.String value_Renamed)
		{
			InitBlock();
			this.parent = parent;
			this.value_Renamed = value_Renamed;
			this.name = name;
		}
		
		/// <summary> Constructor for the <code>InputAttribute</code> object. This
		/// is used to create an input attribute using the provided name
		/// and value, all other values for this input node will be null.
		/// 
		/// </summary>
		/// <param name="parent">this is the parent node to this attribute
		/// </param>
		/// <param name="attribute">this is the attribute containing the details
		/// </param>
		public InputAttribute(InputNode parent, Attribute attribute)
		{
			InitBlock();
			this.reference = attribute.Reference;
			this.prefix = attribute.Prefix;
			this.source = attribute.Source;
			this.value_Renamed = attribute.Value;
			this.name = attribute.Name;
			this.parent = parent;
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
		
		/// <summary> Returns the value for the node that this represents. This 
		/// is an immutable value for the node and cannot be changed.
		/// 
		/// </summary>
		/// <returns> the name of the value for this node instance
		/// </returns>
		public virtual System.String getValue()
		{
			return value_Renamed;
		}
		
		/// <summary> Because the <code>InputAttribute</code> object represents an
		/// attribute this method will return null. If nodes are added 
		/// to the node map the values will not be available here.
		/// 
		/// </summary>
		/// <returns> this always returns null for a requested attribute
		/// </returns>
		public virtual InputNode getAttribute(System.String name)
		{
			return null;
		}
		
		/// <summary> Because the <code>InputAttribute</code> object represents an
		/// attribute this method will return an empty map. If nodes are
		/// added to the node map the values will not be maintained.
		/// 
		/// </summary>
		/// <returns> this always returns an empty node map of attributes
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public NodeMap < InputNode > getAttributes()
		
		/// <summary> Because the <code>InputAttribute</code> object represents an
		/// attribute this method will return null. An attribute is a
		/// simple name value pair an so can not contain any child nodes.
		/// 
		/// </summary>
		/// <returns> this always returns null for a requested child node
		/// </returns>
		public virtual InputNode getNext()
		{
			return null;
		}
		
		/// <summary> Because the <code>InputAttribute</code> object represents an
		/// attribute this method will return null. An attribute is a
		/// simple name value pair an so can not contain any child nodes.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the next expected element
		/// 
		/// </param>
		/// <returns> this always returns null for a requested child node
		/// </returns>
		public virtual InputNode getNext(System.String name)
		{
			return null;
		}
		
		/// <summary> This method is used to skip all child elements from this
		/// element. This allows elements to be effectively skipped such
		/// that when parsing a document if an element is not required
		/// then that element can be completely removed from the XML.
		/// </summary>
		public virtual void  skip()
		{
			return ;
		}
		
		/// <summary> This is the string representation of the attribute. It is
		/// used for debugging purposes. When evaluating the attribute
		/// the to string can be used to print out the attribute name.
		/// 
		/// </summary>
		/// <returns> this returns a text description of the attribute
		/// </returns>
		public override System.String ToString()
		{
			return String.format("attribute %s='%s'", name, value_Renamed);
		}
	}
}