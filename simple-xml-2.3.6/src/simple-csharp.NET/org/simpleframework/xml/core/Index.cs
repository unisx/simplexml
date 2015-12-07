/*
* Index.java April 2009
*
* Copyright (C) 2009, Niall Gallagher <niallg@users.sf.net>
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
	
	/// <summary> The <code>Index</code> object is used to represent an index
	/// of parameters iterable in declaration order. This is used so
	/// that parameters can be acquired by name for validation. It is
	/// also used to create an array of <code>Parameter</code> objects
	/// that can be used to acquire the correct deserialized values
	/// to use in order to instantiate the object.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Index:LinkedHashMap
	{
		/// <summary> This is the type that the parameters are created for.</summary>
		private void  InitBlock()
		{
			
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new ArrayList < Parameter >(values());
		}
		/// <summary> This is the type that this class map represents. It can be 
		/// used to determine where the parameters stored are declared.
		/// 
		/// </summary>
		/// <returns> returns the type that the parameters are created for
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				return type;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< String, Parameter >
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type type;
		
		/// <summary> Constructor for the <code>Index</code> object. This is 
		/// used to create a hash map that can be used to acquire
		/// parameters by name. It also provides the parameters in
		/// declaration order within a for each loop.
		/// 
		/// </summary>
		/// <param name="type">this is the type the map is created for
		/// </param>
		public Index(System.Type type)
		{
			InitBlock();
			this.type = type;
		}
		
		/// <summary> This is used to acquire a <code>Parameter</code> using the
		/// position of that parameter within the constructor. This 
		/// allows a builder to determine which parameters to use..
		/// 
		/// </summary>
		/// <param name="ordinal">this is the position of the parameter
		/// 
		/// </param>
		/// <returns> this returns the parameter for the position
		/// </returns>
		public virtual Parameter getParameter(int ordinal)
		{
			return getParameters().get_Renamed(ordinal);
		}
		
		/// <summary> This is used to acquire an list of <code>Parameter</code>
		/// objects in declaration order. This list will help with the
		/// resolution of the correct constructor for deserialization
		/// of the XML. It also provides a faster method of iteration.
		/// 
		/// </summary>
		/// <returns> this returns the parameters in declaration order
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < Parameter > getParameters()
	}
}