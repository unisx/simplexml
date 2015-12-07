/*
* ScannerFactory.java July 2006
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
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ScannerFactory</code> is used to create scanner objects
	/// that will scan a class for its XML class schema. Caching is done
	/// by this factory so that repeat retrievals of a <code>Scanner</code>
	/// will not require repeat scanning of the class for its XML schema.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Context">
	/// </seealso>
	class ScannerFactory
	{
		
		/// <summary> This is used to cache all schemas built to represent a class.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ScannerCache cache;
		
		/// <summary> Constructor for the <code>ScannerFactory</code> object. This is
		/// used to create a factory that will create and cache scanned 
		/// data for a given class. Scanning the class is required to find
		/// the fields and methods that have been annotated.
		/// </summary>
		public ScannerFactory()
		{
			this.cache = new ScannerCache();
		}
		
		/// <summary> This creates a <code>Scanner</code> object that can be used to
		/// examine the fields within the XML class schema. The scanner
		/// maintains information when a field from within the scanner is
		/// visited, this allows the serialization and deserialization
		/// process to determine if all required XML annotations are used.
		/// 
		/// </summary>
		/// <param name="type">the schema class the scanner is created for
		/// 
		/// </param>
		/// <returns> a scanner that can maintains information on the type
		/// 
		/// </returns>
		/// <throws>  Exception if the class contains an illegal schema  </throws>
		public virtual Scanner getInstance(System.Type type)
		{
			Scanner schema = cache.get_Renamed(type);
			
			if (schema == null)
			{
				schema = new Scanner(type);
				cache.put(type, schema);
			}
			return schema;
		}
	}
}