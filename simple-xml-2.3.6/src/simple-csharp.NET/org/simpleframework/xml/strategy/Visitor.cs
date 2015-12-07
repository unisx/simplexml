/*
* Visitor.java January 2010
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
using InputNode = org.simpleframework.xml.stream.InputNode;
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.NodeMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using NodeMap = org.simpleframework.xml.stream.NodeMap;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
namespace org.simpleframework.xml.strategy
{
	
	/// <summary> The <code>Visitor</code> interface represents an object that is 
	/// used to visit each XML element during serialization. For the
	/// deserialization process each XML element is visited before 
	/// control is returned to the serializer. This allows a visitor
	/// implementation to perform some operation based on the node 
	/// that is being deserialized. Typically a visitor is used to
	/// edit the node, for example it may remove or insert attributes.
	/// <p>
	/// In effect this can act much like a transformer that sits
	/// between a <code>Strategy</code> implementation and the core
	/// serializer. It enables interception and manipulation of the
	/// node so that the resulting XML document can be customized in 
	/// a way that can not be performed by the underlying strategy.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.strategy.VisitorStrategy">
	/// </seealso>
	public interface Visitor
	{
		
		/// <summary> This is used to intercept an XML element before it is read
		/// by the underlying <code>Strategy</code> implementation. When
		/// a node is intercepted it can be manipulated in such a way
		/// that its semantics change. For example, this could be used 
		/// to change the way a "class" attribute is represented, which
		/// would allow the XML to appear in a language neutral format.
		/// 
		/// </summary>
		/// <param name="type">this is the type that represents the element
		/// </param>
		/// <param name="node">this is the XML element to be intercepted
		/// </param>
		void  read(Type type);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	NodeMap < InputNode > node) throws Exception;
	
	/// <summary> This is used to intercept an XML element after it is written
	/// by the underlying <code>Strategy</code> implementation. When
	/// a node is intercepted it can be manipulated in such a way
	/// that its semantics change. For example, this could be used 
	/// to change the way a "class" attribute is represented, which
	/// would allow the XML to appear in a language neutral format.
	/// 
	/// </summary>
	/// <param name="type">this is the type that represents the element
	/// </param>
	/// <param name="node">this is the XML element to be intercepted
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void write(Type type, NodeMap < OutputNode > node) throws Exception;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}