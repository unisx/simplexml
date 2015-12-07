/*
* Root.java July 2006
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
//UPGRADE_TODO: The type 'java.lang.annotation.RetentionPolicy' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using RetentionPolicy = java.lang.annotation.RetentionPolicy;
//UPGRADE_TODO: The type 'java.lang.annotation.Retention' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Retention = java.lang.annotation.Retention;
namespace org.simpleframework.xml
{
	
	/// <summary> This <code>Root</code> annotation is used to annotate classes that
	/// need to be serialized. Also, elements within an element list, as
	/// represented by the <code>ElementList</code> annotation need this
	/// annotation so that the element names can be determined. All other
	/// field or method names can be determined using the annotation and 
	/// so the <code>Root</code> annotation is not needed for such objects. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Retention(RetentionPolicy.RUNTIME)
	public interface Root
	{
		
		/// <summary> This represents the name of the XML element. This is optional
		/// an is used when the name of the class is not suitable as an
		/// element name. If this is not specified then the name of the
		/// XML element will be the name of the class. If specified the
		/// class will be serialized and deserialized with the given name.
		/// 
		/// </summary>
		/// <returns> the name of the XML element this represents
		/// </returns>
		System.String name();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default ;
	
	/// <summary> This is used to determine whether the object represented
	/// should be parsed in a strict manner. Strict parsing requires
	/// that each element and attribute in the XML document match a 
	/// field in the class schema. If an element or attribute does
	/// not match a field then the parsing fails with an exception.
	/// Setting strict parsing to false allows details within the
	/// source XML document to be skipped during deserialization.
	/// 
	/// </summary>
	/// <returns> true if strict parsing is enabled, false otherwise
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean strict()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}