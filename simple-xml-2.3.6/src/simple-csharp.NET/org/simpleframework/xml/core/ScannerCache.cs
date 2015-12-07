/*
* ScannerCache.java July 2006
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
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentHashMap = java.util.concurrent.ConcurrentHashMap;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ScannerCache</code> is used to cache schema objects. It 
	/// is used so the overhead of reflectively interrogating each class 
	/// is not required each time an instance of that class is serialized 
	/// or deserialized. This acts as a typedef for the generic type.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ScannerCache:ConcurrentHashMap
	{
		/// <summary> Constructor for the <code>ScannerCache</code> object. This is
		/// a concurrent hash map that maps class types to the XML schema
		/// objects they represent. To enable reloading of classes by the
		/// system this will drop the scanner if the class in unloaded.
		/// </summary>
		private void  InitBlock()
		{
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Class, Scanner >
		public ScannerCache():base()
		{
			InitBlock();
		}
	}
}