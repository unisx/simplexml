/*
* ScannerBuilder.java January 2010
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
//UPGRADE_TODO: The type 'java.lang.annotation.Annotation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Annotation = java.lang.annotation.Annotation;
using WeakCache = org.simpleframework.xml.util.WeakCache;
namespace org.simpleframework.xml.convert
{
	
	/// <summary> The <code>ScannerBuilder</code> is used to build and cache each
	/// scanner requested. Building and caching scanners ensures that 
	/// annotations can be acquired from a class quickly as a scan only 
	/// needs to be performed once. Each scanner built scans the class 
	/// provided as well as all the classes in the hierarchy.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.convert.ConverterScanner">
	/// </seealso>
	class ScannerBuilder:WeakCache
	{
		/// <summary> Constructor for the <code>ScannerBuilder</code> object. This
		/// will create a builder for annotation scanners. Each of the
		/// scanners build will be cached internally to ensure that any
		/// further requests for the scanner are quicker.
		/// </summary>
		private void  InitBlock()
		{
			
			Scanner scanner = fetch(type);
			
			if (scanner == null)
			{
				scanner = new Entry(type);
				cache(type, scanner);
			}
			return scanner;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Class, Scanner >
		public ScannerBuilder():base()
		{
			InitBlock();
		}
		
		/// <summary> This is used to build <code>Scanner</code> objects that are
		/// used to scan the provided class for annotations. Each scanner
		/// instance is cached once created to ensure it does not need to
		/// be built twice, which improves the performance.
		/// 
		/// </summary>
		/// <param name="type">this is the type to build a scanner object for
		/// 
		/// </param>
		/// <returns> this will return a scanner instance for the given type
		/// </returns>
		public Scanner build;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Class < ? > type)
		
		/// <summary> The <code>Entry</code> object represents a scanner that is
		/// used to scan a specified type for annotations. All annotations
		/// scanned from the type are cached so that they do not need to
		/// be looked up twice. This ensures scanning is much quicker.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class Entry:WeakCache
		{
			/// <summary> This class is the subject for all annotation scans performed.</summary>
			private void  InitBlock()
			{
				
				if (!contains(type))
				{
					T value_Renamed = find(type);
					
					if (type != null)
					{
						cache(type, value_Renamed);
					}
				}
				return (T) fetch(type);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				Class < ? > type = root;
				
				while (type != null)
				{
					T value_Renamed = type.getAnnotation(label);
					
					if (value_Renamed != null)
					{
						return value_Renamed;
					}
					type = type.getSuperclass();
				}
				return null;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< Class, Annotation > implements Scanner
			//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Type root;
			
			/// <summary> Constructor for the <code>Entry</code> object is used to 
			/// create a scanner that will scan the specified type. All
			/// annotations that are scanned are cached to ensure that they
			/// do not need to be looked up twice. This ensures that scans
			/// are quicker including ones that result in null.
			/// 
			/// </summary>
			/// <param name="root">this is the root class that is to be scanned
			/// </param>
			public Entry(System.Type root)
			{
				this.root = root;
			}
			
			/// <summary> This method will scan a class for the specified annotation. 
			/// If the annotation is found on the class, or on one of the 
			/// super types then it is returned. All scans will be cached 
			/// to ensure scanning is only performed once.
			/// 
			/// </summary>
			/// <param name="type">this is the annotation type to be scanned for
			/// 
			/// </param>
			/// <returns> this will return the annotation if it is found
			/// </returns>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public < T extends Annotation > T scan(Class < T > type)
			
			/// <summary> This method will scan a class for the specified annotation. 
			/// If the annotation is found on the class, or on one of the 
			/// super types then it is returned. All scans will be cached 
			/// to ensure scanning is only performed once.
			/// 
			/// </summary>
			/// <param name="label">this is the annotation type to be scanned for
			/// 
			/// </param>
			/// <returns> this will return the annotation if it is found
			/// </returns>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private < T extends Annotation > T find(Class < T > label)
		}
	}
}