/*
* DateTransform.java May 2007
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
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>DateFactory</code> object is used to create instances
	/// or subclasses of the <code>Date</code> object. This will create
	/// the instances of the date objects using a constructor that takes
	/// a single <code>long</code> parameter value. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.DateTransform">
	/// </seealso>
	class DateFactory
	{
		public DateFactory()
		{
			InitBlock();
		}
		/// <summary> This is used to create instances of the date object required.</summary>
		private void  InitBlock()
		{
			
			this(type, typeof(long));
			this.factory = type.getDeclaredConstructor(list);
			return factory.newInstance(list);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< T extends Date >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final Constructor < T > factory;
		
		/// <summary> Constructor for the <code>DateFactory</code> object. This is
		/// used to create instances of the specified type. All objects
		/// created by this instance must take a single long parameter.
		/// 
		/// </summary>
		/// <param name="type">this is the date implementation to be created
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public DateFactory(Class < T > type) throws Exception
		
		/// <summary> Constructor for the <code>DateFactory</code> object. This is
		/// used to create instances of the specified type. All objects
		/// created by this instance must take the specified parameter.
		/// 
		/// </summary>
		/// <param name="type">this is the date implementation to be created
		/// </param>
		/// <param name="list">is basically the list of accepted parameters
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public DateFactory(Class < T > type, Class...list) throws Exception
		
		/// <summary> This is used to create instances of the date using a delegate
		/// date. A <code>long</code> parameter is extracted from the 
		/// given date an used to instantiate a date of the required type.
		/// 
		/// </summary>
		/// <param name="list">this is the type used to provide the long value
		/// 
		/// </param>
		/// <returns> this returns an instance of the required date type
		/// </returns>
		public T getInstance;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Object...list) throws Exception
	}
}