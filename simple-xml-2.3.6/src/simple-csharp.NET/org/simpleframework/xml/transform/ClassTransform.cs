/*
* ClassTransform.java May 2007
*
* Copyright (C) 2007, Niall Gallagher <niallg@users.sf.net>
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
namespace org.simpleframework.xml.transform
{
	
	/// <summary> The <code>ClassTransform</code> object is used to transform class
	/// values to and from string representations, which will be inserted
	/// in the generated XML document as the value place holder. The
	/// value must be readable and writable in the same format. Fields
	/// and methods annotated with the XML attribute annotation will use
	/// this to persist and retrieve the value to and from the XML source.
	/// <pre>
	/// 
	/// &#64;Attribute
	/// private Class target;
	/// 
	/// </pre>
	/// As well as the XML attribute values using transforms, fields and
	/// methods annotated with the XML element annotation will use this.
	/// Aside from the obvious difference, the element annotation has an
	/// advantage over the attribute annotation in that it can maintain
	/// any references using the <code>CycleStrategy</code> object. 
	/// 
	/// </summary>
	/// <author>  Ben Wolfe
	/// </author>
	/// <author>  Niall Gallagher
	/// </author>
	class ClassTransform : Transform
	{
		public ClassTransform()
		{
			InitBlock();
		}
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="target">this is the string representation of the class
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		private void  InitBlock()
		{
			
		}
		/// <summary> This is used to acquire the caller class loader for this object.
		/// Typically this is only used if the thread context class loader
		/// is set to null. This ensures that there is at least some class
		/// loader available to the strategy to load the class.
		/// 
		/// </summary>
		/// <returns> this returns the loader that loaded this class     
		/// </returns>
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		private ClassLoader CallerClassLoader
		{
			get
			{
				//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
				return GetType().getClassLoader();
			}
			
		}
		/// <summary> This is used to acquire the thread context class loader. This
		/// is the default class loader used by the cycle strategy. When
		/// using the thread context class loader the caller can switch the
		/// class loader in use, which allows class loading customization.
		/// 
		/// </summary>
		/// <returns> this returns the loader used by the calling thread
		/// </returns>
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		private static ClassLoader ClassLoader
		{
			get
			{
				//UPGRADE_ISSUE: Method 'java.lang.Thread.getContextClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangThreadgetContextClassLoader'"
				return SupportClass.ThreadClass.Current().getContextClassLoader();
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Class >
		public virtual System.Type read(System.String target)
		{
			//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
			ClassLoader loader = ClassLoader;
			
			if (loader == null)
			{
				loader = CallerClassLoader;
			}
			//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
			return loader.loadClass(target);
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="target">this is the value to be converted to a string
		/// 
		/// </param>
		/// <returns> this is the string representation of the given value
		/// </returns>
		public virtual System.String write(System.Type target)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return target.FullName;
		}
	}
}