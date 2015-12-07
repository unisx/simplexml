/*
* DocumentReader.java January 2010
*
* Copyright (C) 2010, Niall Gallagher <niallg@users.sf.net>
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
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.w3c.dom.Node.ELEMENT_NODE;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.w3c.dom.Document;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.w3c.dom.Element;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.w3c.dom.NamedNodeMap;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.w3c.dom.Node;
	
	/// <summary> The <code>DocumentReader</code> object provides an implementation
	/// for reading XML events using DOM. This reader flattens a document
	/// in to a series of nodes, and provides these nodes as events as
	/// they are encountered. Essentially what this does is adapt the 
	/// document approach to navigating the XML and provides a streaming
	/// approach. Having an implementation based on DOM ensures that the
	/// library can be used on a wider variety of platforms. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.stream.DocumentProvider">
	/// </seealso>
	class DocumentReader : EventReader
	{
		
		/// <summary> Any attribute beginning with this string has been reserved.</summary>
		private const System.String RESERVED = "xml";
		
		/// <summary> This is used to extract the nodes from the provided document.</summary>
		private NodeExtractor queue;
		
		/// <summary> This is used to keep track of which elements are in context.</summary>
		private NodeStack stack;
		
		/// <summary> This is used to keep track of any events that were peeked.</summary>
		private EventNode peek_Renamed_Field;
		
		/// <summary> Constructor for the <code>DocumentReader</code> object. This
		/// makes use of a DOM document to extract events and provide them
		/// to the core framework. All nodes will be extracted from the
		/// document and queued for extraction as they are requested. This
		/// will ignore any comment nodes as they should not be considered.
		/// 
		/// </summary>
		/// <param name="document">this is the document that is to be read
		/// </param>
		public DocumentReader(Document document)
		{
			this.queue = new NodeExtractor(document);
			this.stack = new NodeStack();
			this.stack.push(document);
		}
		
		/// <summary> This is used to peek at the node from the document. This will
		/// scan through the document, ignoring any comments to find the
		/// next relevant XML event to acquire. Typically events will be
		/// the start and end of an element, as well as any text nodes.
		/// 
		/// </summary>
		/// <returns> this returns the next event taken from the document
		/// </returns>
		public virtual EventNode peek()
		{
			if (peek_Renamed_Field == null)
			{
				peek_Renamed_Field = next();
			}
			return peek_Renamed_Field;
		}
		
		/// <summary> This is used to take the next node from the document. This will
		/// scan through the document, ignoring any comments to find the
		/// next relevant XML event to acquire. Typically events will be
		/// the start and end of an element, as well as any text nodes.
		/// 
		/// </summary>
		/// <returns> this returns the next event taken from the document
		/// </returns>
		public virtual EventNode next()
		{
			EventNode next = peek_Renamed_Field;
			
			if (next == null)
			{
				next = read();
			}
			else
			{
				peek_Renamed_Field = null;
			}
			return next;
		}
		
		/// <summary> This is used to read the next node from the document. This will
		/// scan through the document, ignoring any comments to find the
		/// next relevant XML event to acquire. Typically events will be
		/// the start and end of an element, as well as any text nodes.
		/// 
		/// </summary>
		/// <returns> this returns the next event taken from the document 
		/// </returns>
		private EventNode read()
		{
			Node node = queue.peek();
			
			if (node == null)
			{
				return end();
			}
			return read(node);
		}
		
		/// <summary> This is used to read the next node from the document. This will
		/// scan through the document, ignoring any comments to find the
		/// next relevant XML event to acquire. Typically events will be
		/// the start and end of an element, as well as any text nodes.
		/// 
		/// </summary>
		/// <param name="node">this is the XML node that has been read
		/// 
		/// </param>
		/// <returns> this returns the next event taken from the document 
		/// </returns>
		private EventNode read(Node node)
		{
			Node parent = node.getParentNode();
			Node top = stack.top();
			
			if (parent != top)
			{
				if (top != null)
				{
					stack.pop();
				}
				return end();
			}
			if (node != null)
			{
				queue.poll();
			}
			return convert(node);
		}
		
		/// <summary> This is used to convert the provided node in to an event. The
		/// conversion process ensures the node can be digested by the core
		/// reader and used to provide an <code>InputNode</code> that can
		/// be used to represent the XML elements or attributes. If the
		/// provided node is not an element then it is considered text.
		/// 
		/// </summary>
		/// <param name="node">the node that is to be converted to an event
		/// 
		/// </param>
		/// <returns> this returns an event created from the given node
		/// </returns>
		private EventNode convert(Node node)
		{
			short type = node.getNodeType();
			
			if (type == ELEMENT_NODE)
			{
				if (node != null)
				{
					stack.push(node);
				}
				return start(node);
			}
			return text(node);
		}
		
		/// <summary> This is used to convert the provided node to a start event. The
		/// conversion process ensures the node can be digested by the core
		/// reader and used to provide an <code>InputNode</code> that can
		/// be used to represent an XML elements within the source document.
		/// 
		/// </summary>
		/// <param name="node">the node that is to be converted to a start event
		/// 
		/// </param>
		/// <returns> this returns a start event created from the given node
		/// </returns>
		private Start start(Node node)
		{
			Start event_Renamed = new Start(node);
			
			if ((event_Renamed.Count == 0))
			{
				return build(event_Renamed);
			}
			return event_Renamed;
		}
		
		/// <summary> This is used to build the attributes that are to be used to 
		/// populate the start event. Populating the start event with the
		/// attributes it contains is required so that each element will
		/// contain its associated attributes. Only attributes that are
		/// not reserved will be added to the start event.
		/// 
		/// </summary>
		/// <param name="event">this is the start event that is to be populated
		/// 
		/// </param>
		/// <returns> this returns a start event with its attributes
		/// </returns>
		private Start build(Start event_Renamed)
		{
			NamedNodeMap list = event_Renamed.Attributes;
			int length = list.getLength();
			
			for (int i = 0; i < length; i++)
			{
				Node node = list.item(i);
				Attribute value_Renamed = attribute(node);
				
				if (!value_Renamed.Reserved)
				{
					event_Renamed.Add(value_Renamed);
				}
			}
			return event_Renamed;
		}
		
		/// <summary> This is used to convert the provided node to an attribute. The
		/// conversion process ensures the node can be digested by the core
		/// reader and used to provide an <code>InputNode</code> that can
		/// be used to represent an XML attribute within the source document.
		/// 
		/// </summary>
		/// <param name="node">the node that is to be converted to an attribute
		/// 
		/// </param>
		/// <returns> this returns an attribute created from the given node
		/// </returns>
		private Entry attribute(Node node)
		{
			return new Entry(node);
		}
		
		/// <summary> This is used to convert the provided node to a text event. The
		/// conversion process ensures the node can be digested by the core
		/// reader and used to provide an <code>InputNode</code> that can
		/// be used to represent an XML attribute within the source document.
		/// 
		/// </summary>
		/// <param name="node">the node that is to be converted to a text event
		/// 
		/// </param>
		/// <returns> this returns the text event created from the given node
		/// </returns>
		private Text text(Node node)
		{
			return new Text(node);
		}
		
		/// <summary> This is used to create a node event to signify that an element
		/// has just ended. End events are important as they allow the core
		/// reader to determine if a node is still in context. This provides
		/// a more convenient way to use <code>InputNode</code> objects as
		/// they should only ever be able to extract their children. 
		/// 
		/// </summary>
		/// <returns> this returns an end event to signify an element close
		/// </returns>
		private End end()
		{
			return new End();
		}
		
		/// <summary> The <code>Entry</code> object is used to represent an attribute
		/// within a start element. This holds the name and value of the
		/// attribute as well as the namespace prefix and reference. These
		/// details can be used to represent the attribute so that should
		/// the core reader require these details they can be acquired.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class Entry:EventAttribute
		{
			/// <summary> This provides the name of the attribute. This will be the
			/// name of the XML attribute without any namespace prefix. If
			/// the name begins with "xml" then this attribute is reserved.
			/// according to the namespaces for XML 1.0 specification.
			/// 
			/// </summary>
			/// <returns> this returns the name of this attribute object
			/// </returns>
			override public System.String Name
			{
				get
				{
					return node.getLocalName();
				}
				
			}
			/// <summary> This returns the value of the event. This will be the value
			/// that the attribute contains. If the attribute does not have
			/// a value then this returns null or an empty string.
			/// 
			/// </summary>
			/// <returns> this returns the value represented by this object
			/// </returns>
			override public System.String Value
			{
				get
				{
					return node.getNodeValue();
				}
				
			}
			/// <summary> This is used to acquire the namespace prefix associated with
			/// this attribute. A prefix is used to qualify the attribute
			/// within a namespace. So, if this has a prefix then it should
			/// have a reference associated with it.
			/// 
			/// </summary>
			/// <returns> this returns the namespace prefix for the attribute
			/// </returns>
			override public System.String Prefix
			{
				get
				{
					return node.Prefix;
				}
				
			}
			/// <summary> This is used to acquire the namespace reference that this 
			/// attribute is in. A namespace is normally associated with an
			/// attribute if that attribute is prefixed with a known token.
			/// If there is no prefix then this will return null.
			/// 
			/// </summary>
			/// <returns> this provides the associated namespace reference
			/// </returns>
			override public System.String Reference
			{
				get
				{
					return node.getNamespaceURI();
				}
				
			}
			/// <summary> This returns true if the attribute is reserved. An attribute
			/// is considered reserved if it begins with "xml" according to 
			/// the namespaces in XML 1.0 specification. Such attributes are
			/// used for namespaces and other such details.
			/// 
			/// </summary>
			/// <returns> this returns true if the attribute is reserved
			/// </returns>
			override public bool Reserved
			{
				get
				{
					System.String prefix = Prefix;
					System.String name = Name;
					
					if (prefix != null)
					{
						return prefix.StartsWith(org.simpleframework.xml.stream.DocumentReader.RESERVED);
					}
					return name.StartsWith(org.simpleframework.xml.stream.DocumentReader.RESERVED);
				}
				
			}
			/// <summary> This is used to return the node for the attribute. Because 
			/// this represents a DOM attribute the DOM node is returned.
			/// Returning the node helps with certain debugging issues.
			/// 
			/// </summary>
			/// <returns> this will return the source object for this
			/// </returns>
			override public System.Object Source
			{
				get
				{
					return node;
				}
				
			}
			
			/// <summary> This is the node that is to be represented as an attribute.</summary>
			//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private Node node;
			
			/// <summary> Constructor for the <code>Entry</code> object. This creates
			/// an attribute object that is used to extract the name, value
			/// namespace prefix, and namespace reference from the provided
			/// node. This is used to populate any start events created.
			/// 
			/// </summary>
			/// <param name="node">this is the node that represents the attribute
			/// </param>
			public Entry(Node node)
			{
				this.node = node;
			}
		}
		
		/// <summary> The <code>Start</code> object is used to represent the start of
		/// an XML element. This will hold the attributes associated with
		/// the element and will provide the name, the namespace reference
		/// and the namespace prefix. For debugging purposes the source XML
		/// element is provided for this start event.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		[Serializable]
		private class Start:EventElement
		{
			/// <summary> This provides the name of the event. This will be the name 
			/// of an XML element the event represents. If there is a prefix
			/// associated with the element, this extracts that prefix.
			/// 
			/// </summary>
			/// <returns> this returns the name without the namespace prefix
			/// </returns>
			virtual public System.String Name
			{
				get
				{
					return element.getLocalName();
				}
				
			}
			/// <summary> This is used to acquire the namespace prefix associated with
			/// this node. A prefix is used to qualify an XML element or
			/// attribute within a namespace. So, if this represents a text
			/// event then a namespace prefix is not required.
			/// 
			/// </summary>
			/// <returns> this returns the namespace prefix for this event
			/// </returns>
			virtual public System.String Prefix
			{
				get
				{
					return element.Prefix;
				}
				
			}
			/// <summary> This is used to acquire the namespace reference that this 
			/// node is in. A namespace is normally associated with an XML
			/// element or attribute, so text events and element close events
			/// are not required to contain any namespace references. 
			/// 
			/// </summary>
			/// <returns> this will provide the associated namespace reference
			/// </returns>
			virtual public System.String Reference
			{
				get
				{
					return element.getNamespaceURI();
				}
				
			}
			/// <summary> This is used to acquire the attributes associated with the
			/// element. Providing the attributes in this format allows 
			/// the reader to build a list of attributes for the event.
			/// 
			/// </summary>
			/// <returns> this returns the attributes associated with this
			/// </returns>
			virtual public NamedNodeMap Attributes
			{
				get
				{
					return element.Attributes;
				}
				
			}
			/// <summary> This is used to return the node for the event. Because this
			/// represents a DOM element node the DOM node will be returned.
			/// Returning the node helps with certain debugging issues.
			/// 
			/// </summary>
			/// <returns> this will return the source object for this event
			/// </returns>
			virtual public System.Object Source
			{
				get
				{
					return element;
				}
				
			}
			
			/// <summary> This is the element that is represented by this start event.</summary>
			//UPGRADE_NOTE: Final was removed from the declaration of 'element '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private Element element;
			
			/// <summary> Constructor for the <code>Start</code> object. This will 
			/// wrap the provided node and expose the required details such
			/// as the name, namespace prefix and namespace reference. The
			/// provided element node can be acquired for debugging purposes.
			/// 
			/// </summary>
			/// <param name="element">this is the element being wrapped by this
			/// </param>
			public Start(Node element)
			{
				this.element = (Element) element;
			}
		}
		
		/// <summary> The <code>Text</code> object is used to represent a text event.
		/// If wraps a node that holds text consumed from the document. 
		/// These are used by <code>InputNode</code> objects to extract the
		/// text values for elements For debugging this exposes the node.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class Text:EventToken
		{
			/// <summary> This returns the value of the event. This will return the
			/// text value contained within the node. If there is no
			/// text within the node this should return an empty string. 
			/// 
			/// </summary>
			/// <returns> this returns the value represented by this event
			/// </returns>
			override public System.String Value
			{
				get
				{
					return node.getNodeValue();
				}
				
			}
			/// <summary> This is used to return the node for the event. Because this
			/// represents a DOM text value the DOM node will be returned.
			/// Returning the node helps with certain debugging issues.
			/// 
			/// </summary>
			/// <returns> this will return the source object for this event
			/// </returns>
			override public System.Object Source
			{
				get
				{
					return node;
				}
				
			}
			
			/// <summary> This is the node that is used to represent the text value.</summary>
			//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private Node node;
			
			/// <summary> Constructor for the <code>Text</code> object. This creates
			/// an event that provides text to the core reader. Text can be
			/// in the form of a CDATA section or a normal text entry.
			/// 
			/// </summary>
			/// <param name="node">this is the node that represents the text value
			/// </param>
			public Text(Node node)
			{
				this.node = node;
			}
			
			/// <summary> This is true as this event represents a text token. Text 
			/// tokens are required to provide a value only. So namespace
			/// details and the node name will always return null.
			/// 
			/// </summary>
			/// <returns> this returns true as this event represents text  
			/// </returns>
			public override bool isText()
			{
				return true;
			}
		}
		
		/// <summary> The <code>End</code> object is used to represent the end of an
		/// element. It is used by the core reader to determine which nodes
		/// are in context and which ones are out of context. This allows
		/// the input nodes to determine if it can read any more children.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class End:EventToken
		{
			
			/// <summary> This is true as this event represents an element end. Such
			/// events are required by the core reader to determine if a 
			/// node is still in context. This helps to determine if there
			/// are any more children to be read from a specific node.
			/// 
			/// </summary>
			/// <returns> this returns true as this token represents an end
			/// </returns>
			public override bool isEnd()
			{
				return true;
			}
		}
	}
}