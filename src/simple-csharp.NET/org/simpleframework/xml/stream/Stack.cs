/*
* Stack.java July 2006
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
	
	/// <summary> The <code>Stack</code> object is used to provide a lightweight 
	/// stack implementation. To ensure top performance this stack is not
	/// synchronized and keeps track of elements using an array list. 
	/// A null from either a <code>pop</code> or <code>top</code> means
	/// that the stack is empty. This allows the stack to be peeked at
	/// even if it has not been populated with anything yet.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Stack
	{
		/// <summary> Constructor for the <code>Stack</code> object. This is used 
		/// to create a stack that can be used to keep track of values
		/// in a first in last out manner. Typically this is used to 
		/// determine if an XML element is in or out of context.
		/// 
		/// </summary>
		/// <param name="size">this is the initial size of the stack to use
		/// </param>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< T > extends ArrayList < T >
		public Stack(int size):base(size)
		{
			InitBlock();
		}
		
		/// <summary> This is used to remove the element from the top of this 
		/// stack. If the stack is empty then this will return null, as
		/// such it is not advisable to push null elements on the stack.
		/// 
		/// </summary>
		/// <returns> this returns the node element the top of the stack
		/// </returns>
		public virtual T pop()
		{
			int size = size();
			
			if (size <= 0)
			{
				return null;
			}
			return remove(size - 1);
		}
		
		/// <summary> This is used to peek at the element from the top of this 
		/// stack. If the stack is empty then this will return null, as
		/// such it is not advisable to push null elements on the stack.
		/// 
		/// </summary>
		/// <returns> this returns the node element the top of the stack
		/// </returns>
		public virtual T top()
		{
			int size = size();
			
			if (size <= 0)
			{
				return null;
			}
			return get_Renamed(size - 1);
		}
		
		/// <summary> This is used to acquire the node from the bottom of the stack.
		/// If the stack is empty then this will return null, as such it
		/// is not advisable to push null elements on the stack.
		/// 
		/// </summary>
		/// <returns> this returns the element from the bottom of the stack
		/// </returns>
		public virtual T bottom()
		{
			int size = size();
			
			if (size <= 0)
			{
				return null;
			}
			return get_Renamed(0);
		}
		
		/// <summary> This method is used to add an element to the top of the stack. 
		/// Although it is possible to add a null element to the stack it 
		/// is not advisable, as null is returned when the stack is empty.
		/// 
		/// </summary>
		/// <param name="value">this is the element to add to the stack
		/// 
		/// </param>
		/// <returns> this returns the actual node that has just been added
		/// </returns>
		public virtual T push(T value_Renamed)
		{
			add(value_Renamed);
			return value_Renamed;
		}
	}
}