/*
* InputStack.java July 2006
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
	
	/// <summary> The <code>InputStack</code> is used to keep track of the nodes 
	/// that have been read from the document. This ensures that when
	/// nodes are read from the source document that the reader can tell
	/// whether a child node for a given <code>InputNode</code> can be
	/// created. Each created node is pushed, and popped when ended.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.stream.InputNode">
	/// </seealso>
	class InputStack:Stack
	{
		/// <summary> Constructor for the <code>InputStack</code> object. This is
		/// used to create a stack that can be used to keep track of the
		/// elements that have been read from the source XML document.
		/// </summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< InputNode >
		public InputStack():base(6)
		{
			InitBlock();
		}
		
		/// <summary> This is used to determine if the specified node is relevant
		/// with respect to the state of the input stack. This returns
		/// true if there are no elements in the stack, which accounts
		/// for a new root node. Also this returns true if the specified
		/// node exists within the stack and is thus an active node.
		/// 
		/// </summary>
		/// <param name="value">this is the input node value to be checked
		/// 
		/// </param>
		/// <returns> returns true if the node is relevant in the stack
		/// </returns>
		public virtual bool isRelevant(InputNode value_Renamed)
		{
			return contains(value_Renamed) || isEmpty();
		}
	}
}