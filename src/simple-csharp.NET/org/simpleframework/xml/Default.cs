/*
* Default.java January 2010
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
//UPGRADE_TODO: The type 'java.lang.annotation.Retention' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Retention = java.lang.annotation.Retention;
//UPGRADE_TODO: The type 'java.lang.annotation.RetentionPolicy' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using RetentionPolicy = java.lang.annotation.RetentionPolicy;
namespace org.simpleframework.xml
{
	
	/// <summary> The <code>Default</code> annotation is used to specify that all
	/// fields or methods should be serialized in a default manner. This
	/// basically allows an objects fields or properties to be serialized
	/// without the need to annotate them. This has advantages if the
	/// format of the serialized object is not important, as it allows
	/// the object to be serialized with a minimal use of annotations.
	/// <pre>
	/// 
	/// &#64;Root
	/// &#64;Default(DefaultType.FIELD)
	/// public class Example {
	/// ...
	/// }
	/// 
	/// </pre>
	/// Defaults can be applied to either fields or property methods. If
	/// this annotation is applied to a class, certain fields or methods
	/// can be ignored using the <code>Transient</code> annotation. If a
	/// member is marked as transient then it will not be serialized. The
	/// defaults are applied only to those members that are not otherwise
	/// annotated with an XML annotation.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.Transient">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Retention(RetentionPolicy.RUNTIME)
	public interface Default
	{
		
		/// <summary> This method is used to return the type of default that is to
		/// be applied to the class. Defaults can be applied to either
		/// fields or property methods. Any member with an XML annotation
		/// will not be treated as a default. 
		/// 
		/// </summary>
		/// <returns> this returns the type of defaults to be applied
		/// </returns>
		DefaultType value_Renamed();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default DefaultType.FIELD;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}