/*
* ContactMap.java January 2010
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
//UPGRADE_TODO: The type 'java.util.LinkedHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedHashMap = java.util.LinkedHashMap;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ContactMap</code> object is used to keep track of the
	/// contacts that have been processed. Keeping track of the contacts
	/// that have been processed ensures that no two contacts are used
	/// twice. This ensures a consistent XML class schema.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ContactMap:LinkedHashMap
	{
		public ContactMap()
		{
			InitBlock();
		}
		/// <summary> This is used to iterate over the <code>Contact</code> objects
		/// in a for each loop. Iterating over the contacts allows them
		/// to be easily added to a list of unique contacts.
		/// 
		/// </summary>
		/// <returns> this is used to return the contacts registered
		/// </returns>
		private void  InitBlock()
		{
			
			return values().iterator();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Object, Contact > implements Iterable < Contact >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < Contact > iterator()
	}
}