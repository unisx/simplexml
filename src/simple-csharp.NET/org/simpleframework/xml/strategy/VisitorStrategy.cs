/*
* VisitorStrategy.java January 2010
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
	
	/// <summary> The <code>VisitorStrategy</code> object is a simplification of a
	/// strategy, which allows manipulation of the serialization process.
	/// Typically implementing a <code>Strategy</code> is impractical as
	/// it requires the implementation to determine the type a node
	/// represents. Instead it is often easier to visit each node that
	/// is being serialized or deserialized and manipulate it so that 
	/// the resulting XML can be customized. 
	/// <p>
	/// To perform customization in this way a <code>Visitor</code> can
	/// be implemented. This can be passed to this strategy which will 
	/// ensure the visitor is given each XML element as it is either 
	/// being serialized or deserialized. Such an inversion of control
	/// allows the nodes to be manipulated with little effort. By 
	/// default this used <code>TreeStrategy</code> object as a default
	/// strategy to delegate to. However, any strategy can be used.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.strategy.Visitor">
	/// </seealso>
	public class VisitorStrategy : Strategy
	{
		private void  InitBlock()
		{
			if (visitor != null)
			{
				visitor.read(type, node);
			}
			return strategy.read(type, node, map);
			bool result = strategy.write(type, value_Renamed, node, map);
			
			if (visitor != null)
			{
				visitor.write(type, node);
			}
			return result;
		}
		
		/// <summary> This is the strategy that is delegated to by this strategy.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'strategy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Strategy strategy;
		
		/// <summary> This is the visitor that is used to intercept serialization.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'visitor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Visitor visitor;
		
		/// <summary> Constructor for the <code>VisitorStrategy</code> object. This
		/// strategy requires a visitor implementation that can be used
		/// to intercept the serialization and deserialization process.
		/// 
		/// </summary>
		/// <param name="visitor">this is the visitor used for interception
		/// </param>
		public VisitorStrategy(Visitor visitor):this(visitor, new TreeStrategy())
		{
		}
		
		/// <summary> Constructor for the <code>VisitorStrategy</code> object. This
		/// strategy requires a visitor implementation that can be used
		/// to intercept the serialization and deserialization process.
		/// 
		/// </summary>
		/// <param name="visitor">this is the visitor used for interception
		/// </param>
		/// <param name="strategy">this is the strategy to be delegated to
		/// </param>
		public VisitorStrategy(Visitor visitor, Strategy strategy)
		{
			InitBlock();
			this.strategy = strategy;
			this.visitor = visitor;
		}
		
		/// <summary> This method will read with  an internal strategy after it has
		/// been intercepted by the visitor. Interception of the XML node
		/// before it is delegated to the internal strategy allows the 
		/// visitor to change some attributes or details before the node
		/// is interpreted by the strategy.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the root element expected
		/// </param>
		/// <param name="node">this is the node map used to resolve an override
		/// </param>
		/// <param name="map">this is used to maintain contextual information
		/// 
		/// </param>
		/// <returns> the value that should be used to describe the instance
		/// </returns>
		public Value read;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Type type, NodeMap < InputNode > node, Map map) throws Exception
		
		/// <summary> This method will write with an internal strategy before it has
		/// been intercepted by the visitor. Interception of the XML node
		/// before it is delegated to the internal strategy allows the 
		/// visitor to change some attributes or details before the node
		/// is interpreted by the strategy.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the root element expected
		/// </param>
		/// <param name="node">this is the node map used to resolve an override
		/// </param>
		/// <param name="map">this is used to maintain contextual information
		/// 
		/// </param>
		/// <returns> the value that should be used to describe the instance
		/// </returns>
		public bool write;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Type type, Object value, NodeMap < OutputNode > node, Map map) throws Exception
	}
}