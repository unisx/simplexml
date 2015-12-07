/*
* AnnotationHandler.java December 2009
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
	
	/// <summary> The <code>AnnotationHandler</code> object is used to handle all
	/// invocation made on a synthetic annotation. This is required so
	/// that annotations can be created without an implementation. The
	/// <code>java.lang.reflect.Proxy</code> object is used to wrap this
	/// invocation handler with the annotation interface. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	//UPGRADE_ISSUE: Interface 'java.lang.reflect.InvocationHandler' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectInvocationHandler'"
	class AnnotationHandler : InvocationHandler
	{
		
		/// <summary> This is the method used to acquire the associated type.</summary>
		private const System.String CLASS = "annotationType";
		
		/// <summary> This is used to acquire a string value for the annotation.</summary>
		private const System.String STRING = "toString";
		
		/// <summary> This is used to perform a comparison of the annotations.</summary>
		private const System.String EQUAL = "equals";
		
		/// <summary> This is used to perform a comparison of the annotations.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'comparer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Comparer comparer;
		
		/// <summary> This is annotation type associated with this handler. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type type;
		
		/// <summary> Constructor for the <code>AnnotationHandler</code> object. This 
		/// is used to create a handler for invocations on a synthetic 
		/// annotation. The annotation type wrapped must be provided.
		/// 
		/// </summary>
		/// <param name="type">this is the annotation type that this is wrapping
		/// </param>
		public AnnotationHandler(System.Type type)
		{
			this.comparer = new Comparer();
			this.type = type;
		}
		
		/// <summary> This is used to handle all invocations on the wrapped annotation.
		/// Typically the response to an invocation will result in the
		/// default value of the annotation attribute being returned. If the
		/// method is an <code>equals</code> or <code>toString</code> then
		/// this will be handled by an internal implementation.
		/// 
		/// </summary>
		/// <param name="proxy">this is the proxy object the invocation was made on
		/// </param>
		/// <param name="method">this is the method that was invoked on the proxy
		/// </param>
		/// <param name="list">this is the list of parameters to be used
		/// 
		/// </param>
		/// <returns> this is used to return the result of the invocation
		/// </returns>
		public virtual System.Object invoke(System.Object proxy, System.Reflection.MethodInfo method, System.Object[] list)
		{
			System.String name = method.Name;
			
			if (name.Equals(STRING))
			{
				return ToString();
			}
			if (name.Equals(EQUAL))
			{
				return equals(proxy, list);
			}
			if (name.Equals(CLASS))
			{
				return type;
			}
			return method.getDefaultValue();
		}
		
		/// <summary> This is used to determine if two annotations are equals based
		/// on the attributes of the annotation. The comparison done can
		/// ignore specific attributes, for instance the name attribute.
		/// 
		/// </summary>
		/// <param name="proxy">this is the annotation the invocation was made on
		/// </param>
		/// <param name="list">this is the parameters provided to the invocation
		/// 
		/// </param>
		/// <returns> this returns true if the annotations are equals
		/// </returns>
		private bool equals(System.Object proxy, System.Object[] list)
		{
			Annotation left = (Annotation) proxy;
			Annotation right = (Annotation) list[0];
			
			return comparer.equals(left, right);
		}
		
		/// <summary> This is used to build a string from the annotation. The string
		/// produces adheres to the typical string representation of a
		/// normal annotation. This ensures that an exceptions that are
		/// thrown with a string representation of the annotation are
		/// identical to those thrown with a normal annotation.
		/// 
		/// </summary>
		/// <returns> returns a string representation of the annotation 
		/// </returns>
		public override System.String ToString()
		{
			StringBuilder builder = new StringBuilder();
			
			if (type != null)
			{
				name(builder);
				attributes(builder);
			}
			return builder.toString();
		}
		
		/// <summary> This is used to build a string from the annotation. The string
		/// produces adheres to the typical string representation of a
		/// normal annotation. This ensures that an exceptions that are
		/// thrown with a string representation of the annotation are
		/// identical to those thrown with a normal annotation.
		/// 
		/// </summary>
		/// <param name="builder">this is the builder used to compose the text
		/// </param>
		private void  name(StringBuilder builder)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String name = type.FullName;
			
			if (name != null)
			{
				builder.append('@');
				builder.append(name);
				builder.append('(');
			}
		}
		
		/// <summary> This is used to build a string from the annotation. The string
		/// produces adheres to the typical string representation of a
		/// normal annotation. This ensures that an exceptions that are
		/// thrown with a string representation of the annotation are
		/// identical to those thrown with a normal annotation.
		/// 
		/// </summary>
		/// <param name="builder">this is the builder used to compose the text
		/// </param>
		private void  attributes(StringBuilder builder)
		{
			System.Reflection.MethodInfo[] list = type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static);
			
			for (int i = 0; i < list.Length; i++)
			{
				System.String attribute = list[i].Name;
				System.Object value_Renamed = list[i].getDefaultValue();
				
				if (i > 0)
				{
					builder.append(',');
					builder.append(' ');
				}
				builder.append(attribute);
				builder.append('=');
				builder.append(value_Renamed);
			}
			builder.append(')');
		}
	}
}