/*
* Hierarchy.java April 2007
*
* Copyright (C) 2007, Niall Gallagher <niallg@users.sf.net>
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
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Hierarchy</code> object is used to acquire the hierarchy
	/// of a specified class. This ensures that the iteration order of the
	/// hierarchy is from the base class to the most specialized class.
	/// It is used during scanning to ensure that the order of methods and
	/// fields written as XML is in declaration order from the most
	/// basic to the most specialized.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	//UPGRADE_TODO: Class 'java.util.LinkedList' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLinkedList'"
	[Serializable]
	class Hierarchy:System.Collections.ArrayList
	{
		/// <summary> Constructor for the <code>Hierarchy</code> object. This is used 
		/// to create the hierarchy of the specified class. It enables the
		/// scanning process to evaluate methods and fields in the order of
		/// most basic to most specialized.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be scanned
		/// </param>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Class >
		public Hierarchy(System.Type type)
		{
			InitBlock();
			scan(type);
		}
		
		/// <summary> This is used to scan the specified <code>Class</code> in such a
		/// way that the most basic type is at the head of the list and the
		/// most specialized is at the last, ensuring correct iteration.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be scanned
		/// </param>
		private void  scan(System.Type type)
		{
			while (type != null)
			{
				Insert(0, type);
				type = type.BaseType;
			}
			System.Boolean tempBoolean;
			tempBoolean = Contains(typeof(System.Object));
			Remove(typeof(System.Object));
			bool generatedAux = tempBoolean;
		}
	}
}