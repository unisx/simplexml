/*
* Scanner.java January 2010
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
namespace org.simpleframework.xml.convert
{
	
	/// <summary> The <code>Scanner</code> interface is used to scan a class for a
	/// given annotation. A scanner will cache all previous lookups to
	/// ensure the look time is reduced. Caches include misses, so if a
	/// class does not contain an annotation when scanned it will not
	/// be scanned again.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	internal interface Scanner
	{
		
		/// <summary> This method will scan a class for the specified annotation. 
		/// If the annotation is found on the class, or on one of the super
		/// types then it is returned. All scans should be cached to ensure
		/// scanning is only performed once.
		/// 
		/// </summary>
		/// <param name="type">this is the annotation type to be scanned for
		/// 
		/// </param>
		/// <returns> this will return the annotation if it is found
		/// </returns>
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T extends Annotation > T scan(Class < T > type);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}