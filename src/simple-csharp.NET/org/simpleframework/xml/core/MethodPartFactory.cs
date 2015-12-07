/*
* MethodPartFactory.java April 2007
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
//UPGRADE_TODO: The type 'java.lang.annotation.Annotation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Annotation = java.lang.annotation.Annotation;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>MethodPartFactory</code> is used to create method parts
	/// based on the method signature and the XML annotation. This is 
	/// effectively where a method is classified as either a getter or a
	/// setter method within an object. In order to determine the type of
	/// method the method name is checked to see if it is prefixed with
	/// either the "get", "is", or "set" tokens.
	/// <p>
	/// Once the method is determined to be a Java Bean method according 
	/// to conventions the method signature is validated. If the method
	/// signature does not follow a return type with no arguments for the
	/// get method, and a single argument for the set method then this
	/// will throw an exception.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.MethodScanner">
	/// </seealso>
	class MethodPartFactory
	{
		
		/// <summary> This is used to create the synthetic annotations for methods.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private AnnotationFactory factory;
		
		/// <summary> Constructor for the <code>MethodPartFactory</code> object. This
		/// is used to create method parts based on the method signature 
		/// and the XML annotation is uses. The created part can be used to
		/// either set or get values depending on its type.
		/// </summary>
		public MethodPartFactory()
		{
			this.factory = new AnnotationFactory();
		}
		
		/// <summary> This is used to acquire a <code>MethodPart</code> for the method
		/// provided. This will synthesize an XML annotation to be used for
		/// the method. If the method provided is not a setter or a getter
		/// then this will return null, otherwise it will return a part with
		/// a synthetic XML annotation. In order to be considered a valid
		/// method the Java Bean conventions must be followed by the method.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the part for
		/// 
		/// </param>
		/// <returns> this is the method part object for the method
		/// 
		/// </returns>
		/// <throws>  Exception if Java Bean conventions are not followed </throws>
		public virtual MethodPart getInstance(System.Reflection.MethodInfo method)
		{
			Annotation label = getAnnotation(method);
			
			if (label != null)
			{
				return getInstance(method, label);
			}
			return null;
		}
		
		/// <summary> This is used to acquire a <code>MethodPart</code> for the name
		/// and annotation of the provided method. This will determine the
		/// method type by examining its signature. If the method follows
		/// Java Bean conventions then either a setter method part or a
		/// getter method part is returned. If the method does not comply
		/// with the conventions an exception is thrown.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the part for
		/// </param>
		/// <param name="label">this is the annotation associated with the method
		/// 
		/// </param>
		/// <returns> this is the method part object for the method
		/// 
		/// </returns>
		/// <throws>  Exception if Java Bean conventions are not followed </throws>
		public virtual MethodPart getInstance(System.Reflection.MethodInfo method, Annotation label)
		{
			MethodName name = getName(method, label);
			MethodType type = name.Type;
			
			if (type == MethodType.SET)
			{
				return new SetPart(name, label);
			}
			return new GetPart(name, label);
		}
		
		/// <summary> This is used to acquire a <code>MethodName</code> for the name
		/// and annotation of the provided method. This will determine the
		/// method type by examining its signature. If the method follows
		/// Java Bean conventions then either a setter method name or a
		/// getter method name is returned. If the method does not comply
		/// with the conventions an exception is thrown.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the name for
		/// </param>
		/// <param name="label">this is the annotation associated with the method
		/// 
		/// </param>
		/// <returns> this is the method name object for the method
		/// 
		/// </returns>
		/// <throws>  Exception if Java Bean conventions are not followed </throws>
		private MethodName getName(System.Reflection.MethodInfo method, Annotation label)
		{
			MethodType type = getMethodType(method);
			
			if (type == MethodType.GET)
			{
				return getRead(method, type);
			}
			if (type == MethodType.IS)
			{
				return getRead(method, type);
			}
			if (type == MethodType.SET)
			{
				return getWrite(method, type);
			}
			throw new MethodException("Annotation %s must mark a set or get method", label);
		}
		
		/// <summary> This is used to acquire a <code>MethodType</code> for the name
		/// of the method provided. This will determine the method type by 
		/// examining its prefix. If the name follows Java Bean conventions 
		/// then either a setter method type is returned. If the name does
		/// not comply with the naming conventions then null is returned.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the type for
		/// 
		/// </param>
		/// <returns> this is the method name object for the method    
		/// </returns>
		private MethodType getMethodType(System.Reflection.MethodInfo method)
		{
			System.String name = method.Name;
			
			if (name.StartsWith("get"))
			{
				return MethodType.GET;
			}
			if (name.StartsWith("is"))
			{
				return MethodType.IS;
			}
			if (name.StartsWith("set"))
			{
				return MethodType.SET;
			}
			return MethodType.NONE;
		}
		
		/// <summary> This is used to synthesize an XML annotation given a method. The
		/// provided method must follow the Java Bean conventions and either
		/// be a getter or a setter. If this criteria is satisfied then a
		/// suitable XML annotation is created to be used. Typically a match
		/// is performed on whether the method type is a Java collection or
		/// an array, if neither criteria are true a normal XML element is
		/// used. Synthesizing in this way ensures the best results.
		/// 
		/// </summary>
		/// <param name="method">this is the method to extract the annotation for
		/// 
		/// </param>
		/// <returns> an XML annotation or null if the method is not suitable
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the annotation could not be created </throws>
		private Annotation getAnnotation(System.Reflection.MethodInfo method)
		{
			System.Type type = getType(method);
			
			if (type != null)
			{
				return factory.getInstance(type);
			}
			return null;
		}
		
		/// <summary> This is used to extract the type from a method. Type type of a
		/// method is the return type for a getter and a parameter type for
		/// a setter. Such a parameter will only be returned if the method
		/// observes the Java Bean conventions for a property method.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the type for
		/// 
		/// </param>
		/// <returns> this returns the type associated with the method
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the method type can not be found </throws>
		public virtual System.Type getType(System.Reflection.MethodInfo method)
		{
			MethodType type = getMethodType(method);
			
			if (type == MethodType.SET)
			{
				return getParameterType(method);
			}
			if (type == MethodType.GET)
			{
				return getReturnType(method);
			}
			if (type == MethodType.IS)
			{
				return getReturnType(method);
			}
			return null;
		}
		
		/// <summary> This is the parameter type associated with the provided method.
		/// The first parameter is returned if the provided method is a
		/// setter. If the method takes more than one parameter or if it
		/// takes no parameters then null is returned from this.
		/// 
		/// </summary>
		/// <param name="method">this is the method to get the parameter type for
		/// 
		/// </param>
		/// <returns> this returns the parameter type associated with it
		/// 
		/// </returns>
		/// <throws>  Exception if the parameter type can not be found </throws>
		private System.Type getParameterType(System.Reflection.MethodInfo method)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] list = method.GetParameters();
			
			if (list.Length == 1)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return method.GetParameters()[0];
			}
			return null;
		}
		
		/// <summary> This is the return type associated with the provided method.
		/// The return type of the method is provided only if the method
		/// adheres to the Java Bean conventions regarding getter methods.
		/// If the method takes a parameter then this will return null.
		/// 
		/// </summary>
		/// <param name="method">this is the method to get the return type for
		/// 
		/// </param>
		/// <returns> this returns the return type associated with it
		/// 
		/// </returns>
		/// <throws>  Exception if the return type can not be found </throws>
		private System.Type getReturnType(System.Reflection.MethodInfo method)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] list = method.GetParameters();
			
			if (list.Length == 0)
			{
				return method.ReturnType;
			}
			return null;
		}
		
		/// <summary> This is used to acquire a <code>MethodName</code> for the name
		/// and annotation of the provided method. This must be a getter
		/// method, and so must have a return type that is not void and 
		/// have not arguments. If the method has arguments an exception 
		/// is thrown, if not the Java Bean method name is provided.
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the name for
		/// </param>
		/// <param name="type">this is the method type to acquire the name for    
		/// 
		/// </param>
		/// <returns> this is the method name object for the method
		/// 
		/// </returns>
		/// <throws>  Exception if Java Bean conventions are not followed </throws>
		private MethodName getRead(System.Reflection.MethodInfo method, MethodType type)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] list = method.GetParameters();
			System.String real = method.Name;
			
			if (list.Length != 0)
			{
				throw new MethodException("Get method %s is not a valid property", method);
			}
			System.String name = getTypeName(real, type);
			
			if (name == null)
			{
				throw new MethodException("Could not get name for %s", method);
			}
			return new MethodName(method, type, name);
		}
		
		/// <summary> This is used to acquire a <code>MethodName</code> for the name
		/// and annotation of the provided method. This must be a setter
		/// method, and so must accept a single argument, if it contains 
		/// more or less than one argument an exception is thrown.
		/// return type that is not void and
		/// 
		/// </summary>
		/// <param name="method">this is the method to acquire the name for
		/// </param>
		/// <param name="type">this is the method type to acquire the name for    
		/// 
		/// </param>
		/// <returns> this is the method name object for the method
		/// 
		/// </returns>
		/// <throws>  Exception if Java Bean conventions are not followed </throws>
		private MethodName getWrite(System.Reflection.MethodInfo method, MethodType type)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] list = method.GetParameters();
			System.String real = method.Name;
			
			if (list.Length != 1)
			{
				throw new MethodException("Set method %s os not a valid property", method);
			}
			System.String name = getTypeName(real, type);
			
			if (name == null)
			{
				throw new MethodException("Could not get name for %s", method);
			}
			return new MethodName(method, type, name);
		}
		
		/// <summary> This is used to acquire the name of the method in a Java Bean
		/// property style. Thus any "get", "is", or "set" prefix is 
		/// removed from the name and the following character is changed
		/// to lower case if it does not represent an acronym.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the method to be converted
		/// </param>
		/// <param name="type">this is the type of method the name represents
		/// 
		/// </param>
		/// <returns> this returns the Java Bean name for the method
		/// </returns>
		private System.String getTypeName(System.String name, MethodType type)
		{
			int prefix = type.getPrefix();
			int size = name.Length;
			
			if (size > prefix)
			{
				name = name.Substring(prefix, (size) - (prefix));
			}
			return Reflector.getName(name);
		}
	}
}