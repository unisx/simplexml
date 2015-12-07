/*
* AnnotationFactory.java January 2010
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
using Element = org.simpleframework.xml.Element;
using ElementArray = org.simpleframework.xml.ElementArray;
using ElementList = org.simpleframework.xml.ElementList;
using ElementMap = org.simpleframework.xml.ElementMap;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>AnnotationFactory</code> is used to create annotations
	/// using a given class. This will classify the provided type as
	/// either a list, map, array, or a default object. Depending on the
	/// type provided a suitable annotation will be created. Annotations
	/// produced by this will have default attribute values.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.AnnotationHandler">
	/// </seealso>
	class AnnotationFactory
	{
		/// <summary> This is used to create a suitable class loader to be used to
		/// load the synthetic annotation classes. The class loader
		/// provided will be the same as the class loader that was used
		/// to load this class.
		/// 
		/// </summary>
		/// <returns> this returns the class loader that is to be used
		/// </returns>
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		private ClassLoader ClassLoader
		{
			get
			{
				//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
				return typeof(AnnotationFactory).getClassLoader();
			}
			
		}
		
		/// <summary> This is used to create an annotation for the provided type.
		/// Annotations created are used to match the type provided. So
		/// a <code>List</code> will have an <code>ElementList</code>
		/// annotation for example. Matching the annotation to the
		/// type ensures the best serialization for that type. 
		/// 
		/// </summary>
		/// <param name="type">the type to create the annotation for
		/// 
		/// </param>
		/// <returns> this returns the synthetic annotation to be used
		/// </returns>
		public virtual Annotation getInstance(System.Type type)
		{
			//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
			ClassLoader loader = ClassLoader;
			
			if (typeof(System.Collections.IDictionary).IsAssignableFrom(type))
			{
				return getInstance(loader, typeof(ElementMap));
			}
			if (typeof(System.Collections.ICollection).IsAssignableFrom(type))
			{
				return getInstance(loader, typeof(ElementList));
			}
			if (type.IsArray)
			{
				return getInstance(loader, typeof(ElementArray));
			}
			return getInstance(loader, typeof(Element));
		}
		
		/// <summary> This will create a synthetic annotation using the provided 
		/// interface. All attributes for the provided annotation will
		/// have their default values. 
		/// 
		/// </summary>
		/// <param name="loader">this is the class loader to load the annotation 
		/// </param>
		/// <param name="label">this is the annotation interface to be used
		/// 
		/// </param>
		/// <returns> this returns the synthetic annotation to be used
		/// </returns>
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		private Annotation getInstance(ClassLoader loader, System.Type label)
		{
			AnnotationHandler handler = new AnnotationHandler(label);
			System.Type[] list = new System.Type[]{label};
			
			//UPGRADE_ISSUE: Method 'java.lang.reflect.Proxy.newProxyInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectProxy'"
			return (Annotation) Proxy.newProxyInstance(loader, list, handler);
		}
	}
}