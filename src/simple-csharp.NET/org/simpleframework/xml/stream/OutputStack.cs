/*
* OutputStack.java July 2006
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
	
	/// <summary> The <code>OutputStack</code> is used to keep track of the nodes 
	/// that have been written to the document. This ensures that when
	/// nodes are written to  the XML document that the writer can tell
	/// whether a child node for a given <code>OutputNode</code> can be
	/// created. Each created node is pushed, and popped when ended.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.stream.OutputNode">
	/// </seealso>
	[Serializable]
	class OutputStack:System.Collections.ArrayList
	{
		/// <summary> Represents the set of nodes that have not been committed.</summary>
		private void  InitBlock()
		{
			
			return new Sequence(this);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< OutputNode >
		//UPGRADE_NOTE: Final was removed from the declaration of 'active '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private SupportClass.SetSupport active;
		
		/// <summary> Constructor for the <code>OutputStack</code> object. This is
		/// used to create a stack that can be used to keep track of the
		/// elements that have been written to the XML document.
		/// </summary>
		public OutputStack(SupportClass.SetSupport active)
		{
			InitBlock();
			this.active = active;
		}
		
		/// <summary> This is used to remove the <code>OutputNode</code> from the
		/// top of the output stack. This is used when an element has been
		/// ended and the output writer wants to block child creation.
		/// 
		/// </summary>
		/// <returns> this returns the node from the top of the stack
		/// </returns>
		public virtual OutputNode pop()
		{
			int size = Count;
			
			if (size <= 0)
			{
				return null;
			}
			return purge(size - 1);
		}
		
		/// <summary> This is used to acquire the <code>OutputNode</code> from the
		/// top of the output stack. This is used when the writer wants to
		/// determine the current element written to the XML document.
		/// 
		/// </summary>
		/// <returns> this returns the node from the top of the stack
		/// </returns>
		public virtual OutputNode top()
		{
			int size = Count;
			
			if (size <= 0)
			{
				return null;
			}
			return this[size - 1];
		}
		
		/// <summary> This is used to acquire the <code>OutputNode</code> from the
		/// bottom of the output stack. This is used when the writer wants
		/// to determine the root element for the written XML document.
		/// 
		/// </summary>
		/// <returns> this returns the node from the bottom of the stack
		/// </returns>
		public virtual OutputNode bottom()
		{
			int size = Count;
			
			if (size <= 0)
			{
				return null;
			}
			return this[0];
		}
		
		/// <summary> This method is used to add an <code>OutputNode</code> to the
		/// top of the stack. This is used when an element is written to
		/// the XML document, and allows the writer to determine if a
		/// child node can be created from a given output node.
		/// 
		/// </summary>
		/// <param name="value">this is the output node to add to the stack
		/// </param>
		public virtual OutputNode push(OutputNode value_Renamed)
		{
			active.Add(value_Renamed);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.util.ArrayList.add' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			bool generatedAux = Add(value_Renamed) >= 0;
			return value_Renamed;
		}
		
		/// <summary> The <code>purge</code> method is used to purge a match from
		/// the provided position. This also ensures that the active set
		/// has the node removed so that it is no longer relevant.
		/// 
		/// </summary>
		/// <param name="index">the index of the node that is to be removed
		/// 
		/// </param>
		/// <returns> returns the node removed from the specified index
		/// </returns>
		public virtual OutputNode purge(int index)
		{
			System.Object tempObject;
			tempObject = this[index];
			RemoveAt(index);
			OutputNode node = tempObject;
			
			if (node != null)
			{
				active.Remove(node);
			}
			return node;
		}
		
		/// <summary> This is returns an <code>Iterator</code> that is used to loop
		/// through the ouptut nodes from the top down. This allows the
		/// node writer to determine what <code>Mode</code> should be used
		/// by an output node. This reverses the iteration of the list.
		/// 
		/// </summary>
		/// <returns> returns an iterator to iterate from the top down
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < OutputNode > iterator()
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Sequence' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> The is used to order the <code>OutputNode</code> objects from
		/// the top down. This is basically used to reverse the order of
		/// the linked list so that the stack can be iterated within a
		/// for each loop easily. This can also be used to remove a node.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class Sequence : System.Collections.IEnumerator
		{
			/// <summary> The cursor used to acquire objects from the stack.</summary>
			private void  InitBlock(OutputStack enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				
			}
			private OutputStack enclosingInstance;
			/// <summary> Returns the <code>OutputNode</code> object at the cursor
			/// position. If the cursor has reached the start of the list 
			/// then this returns null instead of the first output node.
			/// 
			/// </summary>
			/// <returns> this returns the node from the cursor position
			/// </returns>
			public virtual System.Object Current
			{
				get
				{
					//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
					if (MoveNext())
					{
						return Enclosing_Instance[--cursor];
					}
					return null;
				}
				
			}
			public OutputStack Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< OutputNode >
			private int cursor;
			
			/// <summary> Constructor for the <code>Sequence</code> object. This is
			/// used to position the cursor at the end of the list so the
			/// last inserted output node is the first returned from this.
			/// </summary>
			public Sequence(OutputStack enclosingInstance)
			{
				InitBlock(enclosingInstance);
				this.cursor = Enclosing_Instance.Count;
			}
			
			/// <summary> This is used to determine if the cursor has reached the
			/// start of the list. When the cursor reaches the start of
			/// the list then this method returns false.
			/// 
			/// </summary>
			/// <returns> this returns true if there are more nodes left
			/// </returns>
			public virtual bool MoveNext()
			{
				return cursor > 0;
			}
			
			/// <summary> Removes the match from the cursor position. This also
			/// ensures that the node is removed from the active set so
			/// that it is not longer considered a relevant output node.
			/// </summary>
			//UPGRADE_NOTE: The equivalent of method 'java.util.Iterator.remove' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public virtual void  remove()
			{
				Enclosing_Instance.purge(cursor);
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public void  Reset()
			{
			}
		}
	}
}