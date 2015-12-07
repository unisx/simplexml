/*
* Comparer.java December 2009
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
//UPGRADE_TODO: The type 'java.lang.annotation.Annotation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Annotation = java.lang.annotation.Annotation;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Comparer</code> is used to compare annotations on the
	/// attributes of that annotation. Unlike the <code>equals</code>
	/// method, this can ignore some attributes based on the name of the
	/// attributes. This is useful if some annotations have overridden
	/// values, such as the field or method name.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Comparer
	{
		private void  InitBlock()
		{
			this.ignore = ignore;
		}
		
		/// <summary> This is the default attribute to ignore for the comparer.</summary>
		private const System.String NAME = "name";
		
		/// <summary> This is the list of names to ignore for this instance.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'ignore '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String[] ignore;
		
		/// <summary> Constructor for the <code>Comparer</code> object. This is
		/// used to create a comparer that has a default set of names
		/// to be ignored during the comparison of annotations.
		/// </summary>
		public Comparer():this(NAME)
		{
		}
		
		/// <summary> Constructor for the <code>Comparer</code> object. This is
		/// used to create a comparer that has a default set of names
		/// to be ignored during the comparison of annotations.
		/// 
		/// </summary>
		/// <param name="ignore">this is the set of attributes to be ignored
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Comparer(String...ignore)
		
		/// <summary> This is used to determine if two annotations are equals based
		/// on the attributes of the annotation. The comparison done can
		/// ignore specific attributes, for instance the name attribute.
		/// 
		/// </summary>
		/// <param name="left">this is the left side of the comparison done
		/// </param>
		/// <param name="right">this is the right side of the comparison done
		/// 
		/// </param>
		/// <returns> this returns true if the annotations are equal
		/// </returns>
		public virtual bool equals(Annotation left, Annotation right)
		{
			System.Type type = left.annotationType();
			System.Reflection.MethodInfo[] list = type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Method method: list)
			{
				if (!isIgnore(method))
				{
					System.Object value_Renamed = method.invoke(left);
					System.Object other = method.invoke(right);
					
					if (!value_Renamed.Equals(other))
					{
						return false;
					}
				}
			}
			return true;
		}
		
		/// <summary> This is used to determine if the method for an attribute is 
		/// to be ignore. To determine if it should be ignore the method
		/// name is compared against the list of attributes to ignore.
		/// 
		/// </summary>
		/// <param name="method">this is the method to be evaluated
		/// 
		/// </param>
		/// <returns> this returns true if the method should be ignored
		/// </returns>
		private bool isIgnore(System.Reflection.MethodInfo method)
		{
			System.String name = method.Name;
			
			if (ignore != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String value: ignore)
				{
					if (name.Equals(value_Renamed))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}