/*
* NamespaceList.java July 2008
*
* Copyright (C) 2008, Niall Gallagher <niallg@users.sf.net>
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
	
	/// <summary> The <code>NamespaceList</code> annotation that is used to declare
	/// namespaces that can be added to an element. This is used when 
	/// there are several namespaces to add to the element without setting
	/// any namespace to the element. This is useful when the scope of a
	/// namespace needs to span several nodes. All prefixes declared in 
	/// the namespaces will be available to the child nodes.
	/// <pre>
	/// 
	/// &lt;example xmlns:root="http://www.example.com/root"&gt;
	/// &lt;anonymous&gt;anonymous element&lt;/anonymous&gt;
	/// &lt;/example&gt;
	/// 
	/// </pre>
	/// The above XML example shows how a prefixed namespace has been added
	/// to the element without qualifying that element. Such declarations
	/// will allow child elements to pick up the parents prefix when this
	/// is required, this avoids having to redeclare the same namespace.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.Namespace">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Retention(RetentionPolicy.RUNTIME)
	public interface NamespaceList
	{
		
		/// <summary> This is used to acquire the namespaces that are declared on
		/// the class. Any number of namespaces can be declared. None of
		/// the declared namespaces will be made the elements namespace,
		/// instead it will simply declare the namespaces so that the
		/// reference URI and prefix will be made available to children.
		/// 
		/// </summary>
		/// <returns> this returns the namespaces that are declared.
		/// </returns>
		Namespace[] value_Renamed();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	default
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}