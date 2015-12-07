/*
* NodeExtractor.java January 2010
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
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
* implied. See the License for the specific language governing 
* permissions and limitations under the License.
*/
using System;
namespace org.simpleframework.xml.stream
{
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.w3c.dom.Node.COMMENT_NODE;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.LinkedList;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.w3c.dom.Document;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.w3c.dom.Node;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.w3c.dom.NodeList;
	
	/// <summary> The <code>NodeExtractor</code> object is used to extract nodes 
	/// from a provided DOM document. This is used so that the nodes of
	/// a given document can be read with queue like semantics, such 
	/// that the first node encountered is the first node taken from 
	/// the queue. Queue semantics help transform DOM documents to an
	/// event stream much like the StAX framework.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class NodeExtractor:LinkedList
	{
		/// <summary> Constructor for the <code>NodeExtractor</code> object. This
		/// is used to instantiate an object that flattens a document
		/// in to a queue so that the nodes can be used for streaming.
		/// 
		/// </summary>
		/// <param name="source">this is the source document to be flattened
		/// </param>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Node >
		public NodeExtractor(Document source)
		{
			InitBlock();
			this.extract(source);
		}
		
		/// <summary> This is used to extract the nodes of the document in such a
		/// way that it can be navigated as a queue. In order to do this
		/// each node encountered is pushed in to the queue so that
		/// when finished the nodes can be dealt with as a stream.
		/// 
		/// </summary>
		/// <param name="source">this is the source document to be flattened
		/// </param>
		private void  extract(Document source)
		{
			Node node = source.getDocumentElement();
			
			if (node != null)
			{
				offer(node);
				extract(node);
			}
		}
		
		/// <summary> This is used to extract the nodes of the element in such a
		/// way that it can be navigated as a queue. In order to do this
		/// each node encountered is pushed in to the queue so that
		/// when finished the nodes can be dealt with as a stream.
		/// 
		/// </summary>
		/// <param name="source">this is the source element to be flattened
		/// </param>
		private void  extract(Node source)
		{
			NodeList list = source.getChildNodes();
			int length = list.getLength();
			
			for (int i = 0; i < length; i++)
			{
				Node node = list.item(i);
				short type = node.getNodeType();
				
				if (type != COMMENT_NODE)
				{
					offer(node);
					extract(node);
				}
			}
		}
	}
}