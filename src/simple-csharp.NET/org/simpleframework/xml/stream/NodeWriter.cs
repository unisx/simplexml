/*
* NodeWriter.java July 2006
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
	
	/// <summary> The <code>NodeWriter</code> object is used to create a writer that
	/// will write well formed indented XML for a given output node. This
	/// is used in the serialization process to convert an object into an
	/// XML document.
	/// <p>
	/// This keeps a stack of all the active output nodes so that if an
	/// output node has been committed it cannot write any further data to
	/// the XML document. This allows all output nodes to be independent
	/// of each other as the node write organizes the write access.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class NodeWriter
	{
		
		/// <summary> Represents the stack of output nodes that are not yet ended.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'stack '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private OutputStack stack;
		
		/// <summary> Formatter used to indent the XML elements and escape text.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'writer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Formatter writer;
		
		/// <summary> Contains the set of as yet uncommitted elements blocks.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'active '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private SupportClass.SetSupport active;
		
		/// <summary> This determines if we expand the namespace prefixes.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'verbose '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private bool verbose;
		
		/// <summary> Constructor for the <code>NodeWriter</code> object. This will
		/// create the object that is used to control an output elements
		/// access to the generated XML document. This keeps a stack of
		/// active and uncommitted elements.
		/// 
		/// </summary>
		/// <param name="result">this is the output for the resulting document
		/// </param>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public NodeWriter(System.IO.StreamWriter result):this(result, new Format())
		{
		}
		
		/// <summary> Constructor for the <code>NodeWriter</code> object. This will
		/// create the object that is used to control an output elements
		/// access to the generated XML document. This keeps a stack of
		/// active and uncommitted elements.
		/// 
		/// </summary>
		/// <param name="result">this is the output for the resulting document
		/// </param>
		/// <param name="format">this is used to format the generated document
		/// </param>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public NodeWriter(System.IO.StreamWriter result, Format format):this(result, format, false)
		{
		}
		
		/// <summary> Constructor for the <code>NodeWriter</code> object. This will
		/// create the object that is used to control an output elements
		/// access to the generated XML document. This keeps a stack of
		/// active and uncommitted elements.
		/// 
		/// </summary>
		/// <param name="result">this is the output for the resulting document
		/// </param>
		/// <param name="format">this is used to format the generated document
		/// </param>
		/// <param name="verbose">this determines if we expand the namespaces
		/// </param>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private NodeWriter(System.IO.StreamWriter result, Format format, bool verbose)
		{
			this.writer = new Formatter(result, format);
			//UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashSet'"
			this.active = new SupportClass.HashSetSupport();
			this.stack = new OutputStack(active);
			this.verbose = verbose;
		}
		/// <summary> This is used to acquire the root output node for the document.
		/// This will create an empty node that can be used to generate
		/// the root document element as a child to the document.
		/// <p>
		/// Depending on whether or not an encoding has been specified 
		/// this method will write a prolog to the generated XML document.
		/// Each prolog written uses an XML version of "1.0".
		/// 
		/// </summary>
		/// <returns> this returns an output element for the document
		/// </returns>
		public virtual OutputNode writeRoot()
		{
			OutputDocument root = new OutputDocument(this, stack);
			
			if ((stack.Count == 0))
			{
				writer.writeProlog();
			}
			return root;
		}
		
		/// <summary> This method is used to determine if the node is the root 
		/// node for the XML document. The root node is the first node
		/// in the document and has no sibling nodes. This is false
		/// if the node has a parent node or a sibling node.
		/// 
		/// </summary>
		/// <param name="node">this is the node that is check as the root 
		/// 
		/// </param>
		/// <returns> true if the node is the root node for the document
		/// </returns>
		public virtual bool isRoot(OutputNode node)
		{
			return stack.bottom() == node;
		}
		
		/// <summary> This is used to determine if the specified node has been 
		/// committed. If this returns tre then the node is committed
		/// and cannot be used to add further child elements.
		/// 
		/// </summary>
		/// <param name="node">this is the node to check for commit status
		/// 
		/// </param>
		/// <returns> this returns true if the node has been committed
		/// </returns>
		public virtual bool isCommitted(OutputNode node)
		{
			return !active.Contains(node);
		}
		
		/// <summary> This method is used to commit all nodes on the stack up to and
		/// including the specified node. This will effectively create end 
		/// tags for any nodes that are currently open up to the specified
		/// element. Once committed the output node can no longer be used
		/// to create child elements, nor can any of its child elements.
		/// 
		/// </summary>
		/// <param name="parent">this is the node that is to be committed
		/// </param>
		public virtual void  commit(OutputNode parent)
		{
			if (stack.Contains(parent))
			{
				OutputNode top = stack.top();
				
				if (!isCommitted(top))
				{
					writeStart(top);
				}
				while (stack.top() != parent)
				{
					writeEnd(stack.pop());
				}
				writeEnd(parent);
				stack.pop();
			}
		}
		
		/// <summary> This method is used to remove the output node from the output
		/// buffer if that node has not yet been committed. This allows a
		/// node that has been created to be deleted, ensuring that it
		/// will not affect the resulting XML document structure.
		/// 
		/// </summary>
		/// <param name="node">this is the output node that is to be removed    
		/// </param>
		public virtual void  remove(OutputNode node)
		{
			if (stack.top() != node)
			{
				throw new NodeException("Cannot remove node");
			}
			stack.pop();
		}
		
		/// <summary> This is used to create a new element under the specified node.
		/// This will effectively commit all nodes that are open until this
		/// node is encountered. Once the specified node is encountered on
		/// the stack a new element is created with the specified name.
		/// 
		/// </summary>
		/// <param name="parent">this is the node that is to be committed
		/// </param>
		/// <param name="name">this is the name of the start element to create
		/// 
		/// </param>
		/// <returns> this will return a child node for the given parent
		/// </returns>
		public virtual OutputNode writeElement(OutputNode parent, System.String name)
		{
			if ((stack.Count == 0))
			{
				return writeStart(parent, name);
			}
			if (stack.Contains(parent))
			{
				OutputNode top = stack.top();
				
				if (!isCommitted(top))
				{
					writeStart(top);
				}
				while (stack.top() != parent)
				{
					writeEnd(stack.pop());
				}
				return writeStart(parent, name);
			}
			return null;
		}
		
		/// <summary> This is used to begin writing on a new XML element. This is
		/// typically done by writing any comments required. This will 
		/// create an output node of the specified name before writing 
		/// the comment, if any exists. Once the comment has been written
		/// the node is pushed on to the head of the output node stack.
		/// 
		/// </summary>
		/// <param name="parent">this is the parent node to the next output node
		/// </param>
		/// <param name="name">this is the name of the node that is to be created
		/// 
		/// </param>
		/// <returns> this returns an output node used for writing content
		/// </returns>
		private OutputNode writeStart(OutputNode parent, System.String name)
		{
			OutputNode node = new OutputElement(parent, this, name);
			
			if (name == null)
			{
				throw new NodeException("Can not have a null name");
			}
			return stack.push(node);
		}
		
		/// <summary> This is used to write the XML element to the underlying buffer.
		/// The element is written in the order of element prefix and name
		/// followed by the attributes an finally the namespaces for the
		/// element. Once this is finished the element is committed to 
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to be fully written
		/// </param>
		private void  writeStart(OutputNode node)
		{
			writeComment(node);
			writeName(node);
			writeAttributes(node);
			writeNamespaces(node);
		}
		
		/// <summary> This is used to write a comment to the document. Comments
		/// appear just before the element name, this allows an logical
		/// association between the comment and the node to be made.
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to have its name written
		/// </param>
		private void  writeComment(OutputNode node)
		{
			System.String comment = node.getComment();
			
			if (comment != null)
			{
				writer.writeComment(comment);
			}
		}
		
		/// <summary> This is used to write a new start element to the resulting XML
		/// document. This will create an output node of the specified
		/// name before writing the start tag. Once the tag is written 
		/// the node is pushed on to the head of the output node stack.
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to have its name written
		/// </param>
		private void  writeName(OutputNode node)
		{
			System.String prefix = node.getPrefix(verbose);
			System.String name = node.Name;
			
			if (name != null)
			{
				writer.writeStart(name, prefix);
			}
		}
		
		/// <summary> This is used to write a new end element to the resulting XML
		/// document. This will acquire the name and value of the given
		/// node, if the node has a value that is written. Finally a new
		/// end tag is written to the document and the output is flushed.
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to have an end tag
		/// </param>
		private void  writeEnd(OutputNode node)
		{
			Mode mode = node.getMode();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(OutputNode next: stack)
			{
				if (mode != Mode.INHERIT)
				{
					break;
				}
				mode = next.getMode();
			}
			writeEnd(node, mode);
		}
		
		/// <summary> This is used to write a new end element to the resulting XML
		/// document. This will acquire the name and value of the given
		/// node, if the node has a value that is written. Finally a new
		/// end tag is written to the document and the output is flushed.
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to have an end tag
		/// </param>
		private void  writeEnd(OutputNode node, Mode mode)
		{
			System.String value_Renamed = node.getValue();
			
			if (value_Renamed != null)
			{
				writer.writeText(value_Renamed, mode);
			}
			System.String name = node.Name;
			System.String prefix = node.getPrefix(verbose);
			
			if (name != null)
			{
				writer.writeEnd(name, prefix);
				writer.flush();
			}
		}
		
		/// <summary> This is used to write the attributes of the specified node to
		/// the output. This will iterate over each node entered on to
		/// the node. Once written the node is considered inactive.
		/// 
		/// </summary>
		/// <param name="node">this is the node to have is attributes written
		/// </param>
		private void  writeAttributes(OutputNode node)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String name: map)
			{
				OutputNode entry = map.get_Renamed(name);
				System.String value_Renamed = entry.getValue();
				System.String prefix = entry.getPrefix(verbose);
				
				writer.writeAttribute(name, value_Renamed, prefix);
			}
			active.Remove(node);
		}
		
		/// <summary> This is used to write the namespaces of the specified node to
		/// the output. This will iterate over each namespace entered on 
		/// to the node. Once written the node is considered qualified.
		/// 
		/// </summary>
		/// <param name="node">this is the node to have is attributes written
		/// </param>
		private void  writeNamespaces(OutputNode node)
		{
			NamespaceMap map = node.getNamespaces();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String name: map)
			{
				System.String prefix = map.get_Renamed(name);
				
				writer.writeNamespace(name, prefix);
			}
		}
	}
}