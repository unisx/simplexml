/*
* NodeStack.java January 2010
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
	
	/// <summary> The <code>NodeStack</code> object is used to represent a stack
	/// of DOM nodes. Stacking DOM nodes is required to determine where
	/// within a stream of nodes you currently are. It allows a reader
	/// to produce end events when a node is popped from the stack.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.stream.DocumentReader">
	/// </seealso>
	class NodeStack:Stack
	{
		/// <summary> Constructor for the <code>NodeStack</code> object. This will
		/// create a stack for holding DOM nodes. To ensure that the
		/// initial size is enough to cope with typical XML documents it
		/// is set large enough to cope with reasonably deep elements.
		/// </summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Node >
		public NodeStack():base(6)
		{
			InitBlock();
		}
	}
}