/*
* NodeReader.java July 2006
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
	
	/// <summary> The <code>NodeReader</code> object is used to read elements from
	/// the specified XML event reader. This reads input node objects
	/// that represent elements within the source XML document. This will
	/// allow details to be read using input node objects, as long as
	/// the end elements for those input nodes have not been ended.
	/// <p>
	/// For example, if an input node represented the root element of a
	/// document then that input node could read all elements within the
	/// document. However, if the input node represented a child element
	/// then it would only be able to read its children.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class NodeReader
	{
		
		/// <summary> Represents the XML event reader used to read all elements.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'reader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private EventReader reader;
		
		/// <summary> This stack enables the reader to keep track of elements.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'stack '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private InputStack stack;
		
		/// <summary> Constructor for the <code>NodeReader</code> object. This is used
		/// to read XML events a input node objects from the event reader.
		/// 
		/// </summary>
		/// <param name="reader">this is the event reader for the XML document
		/// </param>
		public NodeReader(EventReader reader)
		{
			this.stack = new InputStack();
			this.reader = reader;
		}
		
		/// <summary> Returns the root input node for the document. This is returned
		/// only if no other elements have been read. Once the root element
		/// has been read from the event reader this will return null.
		/// 
		/// </summary>
		/// <returns> this returns the root input node for the document
		/// </returns>
		public virtual InputNode readRoot()
		{
			if (stack.isEmpty())
			{
				return readElement(null);
			}
			return null;
		}
		
		/// <summary> This method is used to determine if this node is the root 
		/// node for the XML document. The root node is the first node
		/// in the document and has no sibling nodes. This is false
		/// if the node has a parent node or a sibling node.
		/// 
		/// </summary>
		/// <returns> true if this is the root node within the document
		/// </returns>
		public virtual bool isRoot(InputNode node)
		{
			return stack.bottom() == node;
		}
		
		/// <summary> Returns the next input node from the XML document, if it is a
		/// child element of the specified input node. This essentially
		/// determines whether the end tag has been read for the specified
		/// node, if so then null is returned. If however the specified
		/// node has not had its end tag read then this returns the next
		/// element, if that element is a child of the that node.
		/// 
		/// </summary>
		/// <param name="from">this is the input node to read with 
		/// 
		/// </param>
		/// <returns> this returns the next input node from the document
		/// </returns>
		public virtual InputNode readElement(InputNode from)
		{
			if (!stack.isRelevant(from))
			{
				return null;
			}
			EventNode event_Renamed = reader.next();
			
			while (event_Renamed != null)
			{
				if (event_Renamed.isEnd())
				{
					if (stack.pop() == from)
					{
						return null;
					}
				}
				else if (event_Renamed.isStart())
				{
					return readStart(from, event_Renamed);
				}
				event_Renamed = reader.next();
			}
			return null;
		}
		
		/// <summary> Returns the next input node from the XML document, if it is a
		/// child element of the specified input node. This essentially
		/// the same as the <code>readElement(InputNode)</code> object 
		/// except that this will not read the element if it does not have
		/// the name specified. This essentially acts as a peak function.
		/// 
		/// </summary>
		/// <param name="from">this is the input node to read with 
		/// </param>
		/// <param name="name">this is the name expected from the next element
		/// 
		/// </param>
		/// <returns> this returns the next input node from the document
		/// </returns>
		public virtual InputNode readElement(InputNode from, System.String name)
		{
			if (!stack.isRelevant(from))
			{
				return null;
			}
			EventNode event_Renamed = reader.peek();
			
			while (event_Renamed != null)
			{
				if (event_Renamed.isEnd())
				{
					if (stack.top() == from)
					{
						return null;
					}
					else
					{
						stack.pop();
					}
				}
				else if (event_Renamed.isStart())
				{
					if (isName(event_Renamed, name))
					{
						return readElement(from);
					}
					break;
				}
				event_Renamed = reader.next();
				event_Renamed = reader.peek();
			}
			return null;
		}
		
		/// <summary> This is used to convert the start element to an input node.
		/// This will push the created input node on to the stack. The
		/// input node created contains a reference to this reader. so
		/// that it can be used to read child elements and values.
		/// 
		/// </summary>
		/// <param name="from">this is the parent element for the start event
		/// </param>
		/// <param name="event">this is the start element to be wrapped
		/// 
		/// </param>
		/// <returns> this returns an input node for the given element
		/// </returns>
		private InputNode readStart(InputNode from, EventNode event_Renamed)
		{
			InputElement input = new InputElement(from, this, event_Renamed);
			
			if (event_Renamed.isStart())
			{
				return stack.push(input);
			}
			return input;
		}
		
		/// <summary> This is used to determine the name of the node specified. The
		/// name of the node is determined to be the name of the element
		/// if that element is converts to a valid StAX start element.
		/// 
		/// </summary>
		/// <param name="node">this is the StAX node to acquire the name from
		/// </param>
		/// <param name="name">this is the name of the node to check against
		/// 
		/// </param>
		/// <returns> true if the specified node has the given local name
		/// </returns>
		private bool isName(EventNode node, System.String name)
		{
			System.String local = node.getName();
			
			if (local == null)
			{
				return false;
			}
			return local.Equals(name);
		}
		
		/// <summary> Read the contents of the characters between the specified XML
		/// element tags, if the read is currently at that element. This 
		/// allows characters associated with the element to be used. If
		/// the specified node is not the current node, null is returned.
		/// 
		/// </summary>
		/// <param name="from">this is the input node to read the value from
		/// 
		/// </param>
		/// <returns> this returns the characters from the specified node
		/// </returns>
		public virtual System.String readValue(InputNode from)
		{
			StringBuilder value_Renamed = new StringBuilder();
			
			while (stack.top() == from)
			{
				EventNode event_Renamed = reader.peek();
				
				if (!event_Renamed.isText())
				{
					if (value_Renamed.length() == 0)
					{
						return null;
					}
					return value_Renamed.toString();
				}
				System.String data = event_Renamed.getValue();
				
				value_Renamed.append(data);
				reader.next();
			}
			return null;
		}
		
		/// <summary> This is used to determine if this input node is empty. An
		/// empty node is one with no attributes or children. This can
		/// be used to determine if a given node represents an empty
		/// entity, with which no extra data can be extracted.
		/// 
		/// </summary>
		/// <param name="from">this is the input node to read the value from
		/// 
		/// </param>
		/// <returns> this returns true if the node is an empty element
		/// 
		/// </returns>
		/// <throws>  Exception thrown if there was a parse error </throws>
		public virtual bool isEmpty(InputNode from)
		{
			if (stack.top() == from)
			{
				EventNode event_Renamed = reader.peek();
				
				if (event_Renamed.isEnd())
				{
					return true;
				}
			}
			return false;
		}
		
		/// <summary> This method is used to skip an element within the XML document.
		/// This will simply read each element from the document until
		/// the specified element is at the top of the stack. When the
		/// specified element is at the top of the stack this returns.
		/// 
		/// </summary>
		/// <param name="from">this is the element to skip from the XML document
		/// </param>
		public virtual void  skipElement(InputNode from)
		{
			while (readElement(from) != null)
				;
		}
	}
}