/*
* LabelMap.java July 2006
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
//UPGRADE_TODO: The type 'java.util.LinkedHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedHashMap = java.util.LinkedHashMap;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>LabelMap</code> object represents a map that contains 
	/// string label mappings. This is used for convenience as a typedef
	/// like construct to avoid having declare the generic type whenever
	/// it is referenced. Also this allows <code>Label</code> values 
	/// from the map to be iterated within for each loops.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Label">
	/// </seealso>
	class LabelMap:LinkedHashMap
	{
		/// <summary> This is the scanner object that represents the scanner used.</summary>
		private void  InitBlock()
		{
			
			return values().iterator();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< String, Label > implements Iterable < Label >
		//UPGRADE_NOTE: Final was removed from the declaration of 'source '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Scanner source;
		
		/// <summary> Constructor for the <code>LabelMap</code> object is used to 
		/// create an empty map. This is used for convenience as a typedef
		/// like construct which avoids having to use the generic type.
		/// </summary>
		public LabelMap(Scanner source)
		{
			InitBlock();
			this.source = source;
		}
		
		/// <summary> This allows the <code>Label</code> objects within the label map
		/// to be iterated within for each loops. This will provide all
		/// remaining label objects within the map. The iteration order is
		/// not maintained so label objects may be given in any sequence.
		/// 
		/// </summary>
		/// <returns> this returns an iterator for existing label objects
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < Label > iterator()
		
		/// <summary> This performs a <code>remove</code> that will remove the label
		/// from the map and return that label. This method allows the 
		/// values within the map to be exclusively taken one at a time,
		/// which enables the user to determine which labels remain.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the element of attribute
		/// 
		/// </param>
		/// <returns> this is the label object representing the XML node
		/// </returns>
		public virtual Label take(System.String name)
		{
			return remove(name);
		}
		
		/// <summary> This method is used to clone the label map such that mappings
		/// can be maintained in the original even if they are modified
		/// in the clone. This is used to that the <code>Schema</code> can
		/// remove mappings from the label map as they are visited. 
		/// 
		/// </summary>
		/// <param name="context">this is the context used to style the XML names
		/// 
		/// </param>
		/// <returns> this returns a cloned representation of this map
		/// </returns>
		public virtual LabelMap clone(Context context)
		{
			LabelMap clone = new LabelMap(source);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Label label: this)
			{
				System.String name = label.getName(context);
				
				clone.put(name, label);
			}
			return clone;
		}
		
		/// <summary> This method is used to determine whether strict mappings are
		/// required. Strict mapping means that all labels in the class
		/// schema must match the XML elements and attributes in the
		/// source XML document. When strict mapping is disabled, then
		/// XML elements and attributes that do not exist in the schema
		/// class will be ignored without breaking the parser.
		/// 
		/// </summary>
		/// <param name="context">this is used to determine if this is strict
		/// 
		/// </param>
		/// <returns> true if strict parsing is enabled, false otherwise
		/// </returns>
		public virtual bool isStrict(Context context)
		{
			return context.Strict && source.Strict;
		}
	}
}