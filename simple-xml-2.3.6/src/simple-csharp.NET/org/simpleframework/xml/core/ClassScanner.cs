/*
* ClassScanner.java July 2008
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
//UPGRADE_TODO: The type 'java.lang.annotation.Annotation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Annotation = java.lang.annotation.Annotation;
using Default = org.simpleframework.xml.Default;
using Namespace = org.simpleframework.xml.Namespace;
using NamespaceList = org.simpleframework.xml.NamespaceList;
using Order = org.simpleframework.xml.Order;
using Root = org.simpleframework.xml.Root;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ClassScanner</code> performs the reflective inspection
	/// of a class and extracts all the class level annotations. This will
	/// also extract the methods that are annotated. This ensures that the
	/// callback methods can be invoked during the deserialization process.
	/// Also, this will read the namespace annotations that are used.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Scanner">
	/// </seealso>
	class ClassScanner
	{
		private void  InitBlock()
		{
			if (type.isAnnotationPresent(typeof(Root)))
			{
				root = type.getAnnotation(typeof(Root));
			}
			if (type.isAnnotationPresent(typeof(Order)))
			{
				order = type.getAnnotation(typeof(Order));
			}
			if (type.isAnnotationPresent(typeof(Default)))
			{
				access = type.getAnnotation(typeof(Default));
			}
			if (type.isAnnotationPresent(typeof(Namespace)))
			{
				namespace_Renamed = type.getAnnotation(typeof(Namespace));
				
				if (namespace_Renamed != null)
				{
					decorator.add(namespace_Renamed);
				}
			}
			if (type.isAnnotationPresent(typeof(NamespaceList)))
			{
				NamespaceList scope = type.getAnnotation(typeof(NamespaceList));
				Namespace[] list = scope.value_Renamed();
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Namespace name: list)
				{
					decorator.add(name);
				}
			}
		}
		/// <summary> This is used to create the object instance. It does this by
		/// either delegating to the default no argument constructor or by
		/// using one of the annotated constructors for the object. This
		/// allows deserialized values to be injected in to the created
		/// object if that is required by the class schema.
		/// 
		/// </summary>
		/// <returns> this returns the creator for the class object
		/// </returns>
		virtual public Creator Creator
		{
			get
			{
				return scanner.Creator;
			}
			
		}
		/// <summary> This is used to acquire the <code>Decorator</code> for this.
		/// A decorator is an object that adds various details to the
		/// node without changing the overall structure of the node. For
		/// example comments and namespaces can be added to the node with
		/// a decorator as they do not affect the deserialization.
		/// 
		/// </summary>
		/// <returns> this returns the decorator associated with this
		/// </returns>
		virtual public Decorator Decorator
		{
			get
			{
				return decorator;
			}
			
		}
		/// <summary> This is used to provide the <code>Default</code> annotation
		/// that holds the default access type to use for the class. This
		/// is important in determining how the serializer will deal with
		/// methods or fields in the class that do not have annotations.
		/// 
		/// </summary>
		/// <returns> this returns the default annotation for access type
		/// </returns>
		virtual public Default Default
		{
			get
			{
				return access;
			}
			
		}
		/// <summary> This returns the order annotation used to determine the order
		/// of serialization of attributes and elements. The order is a
		/// class level annotation that can be used only once per class
		/// XML schema. If none exists then this will return null.
		/// of the class processed by this scanner.
		/// 
		/// </summary>
		/// <returns> this returns the name of the object being scanned
		/// </returns>
		virtual public Order Order
		{
			get
			{
				return order;
			}
			
		}
		/// <summary> This returns the root of the class processed by this scanner.
		/// The root determines the type of deserialization that is to
		/// be performed and also contains the name of the root element. 
		/// 
		/// </summary>
		/// <returns> this returns the name of the object being scanned
		/// </returns>
		virtual public Root Root
		{
			get
			{
				return root;
			}
			
		}
		/// <summary> This method is used to retrieve the schema class commit method
		/// during the deserialization process. The commit method must be
		/// marked with the <code>Commit</code> annotation so that when the
		/// object is deserialized the persister has a chance to invoke the
		/// method so that the object can build further data structures.
		/// 
		/// </summary>
		/// <returns> this returns the commit method for the schema class
		/// </returns>
		virtual public Function Commit
		{
			get
			{
				return commit_Renamed_Field;
			}
			
		}
		/// <summary> This method is used to retrieve the schema class validation
		/// method during the deserialization process. The validation method
		/// must be marked with the <code>Validate</code> annotation so that
		/// when the object is deserialized the persister has a chance to 
		/// invoke that method so that object can validate its field values.
		/// 
		/// </summary>
		/// <returns> this returns the validate method for the schema class
		/// </returns>
		virtual public Function Validate
		{
			get
			{
				return validate_Renamed_Field;
			}
			
		}
		/// <summary> This method is used to retrieve the schema class persistence
		/// method. This is invoked during the serialization process to
		/// get the object a chance to perform an necessary preparation
		/// before the serialization of the object proceeds. The persist
		/// method must be marked with the <code>Persist</code> annotation.
		/// 
		/// </summary>
		/// <returns> this returns the persist method for the schema class
		/// </returns>
		virtual public Function Persist
		{
			get
			{
				return persist_Renamed_Field;
			}
			
		}
		/// <summary> This method is used to retrieve the schema class completion
		/// method. This is invoked after the serialization process has
		/// completed and gives the object a chance to restore its state
		/// if the persist method required some alteration or locking.
		/// This is marked with the <code>Complete</code> annotation.
		/// 
		/// </summary>
		/// <returns> returns the complete method for the schema class
		/// </returns>
		virtual public Function Complete
		{
			get
			{
				return complete_Renamed_Field;
			}
			
		}
		/// <summary> This method is used to retrieve the schema class replacement
		/// method. The replacement method is used to substitute an object
		/// that has been deserialized with another object. This allows
		/// a seamless delegation mechanism to be implemented. This is
		/// marked with the <code>Replace</code> annotation. 
		/// 
		/// </summary>
		/// <returns> returns the replace method for the schema class
		/// </returns>
		virtual public Function Replace
		{
			get
			{
				return replace_Renamed_Field;
			}
			
		}
		/// <summary> This method is used to retrieve the schema class replacement
		/// method. The replacement method is used to substitute an object
		/// that has been deserialized with another object. This allows
		/// a seamless delegation mechanism to be implemented. This is
		/// marked with the <code>Replace</code> annotation. 
		/// 
		/// </summary>
		/// <returns> returns the replace method for the schema class
		/// </returns>
		virtual public Function Resolve
		{
			get
			{
				return resolve_Renamed_Field;
			}
			
		}
		/// <summary> This method is used to determine whether strict mappings are
		/// required. Strict mapping means that all labels in the class
		/// schema must match the XML elements and attributes in the
		/// source XML document. When strict mapping is disabled, then
		/// XML elements and attributes that do not exist in the schema
		/// class will be ignored without breaking the parser.
		/// 
		/// </summary>
		/// <returns> true if strict parsing is enabled, false otherwise
		/// </returns>
		virtual public bool Strict
		{
			get
			{
				if (root != null)
				{
					return root.strict();
				}
				return true;
			}
			
		}
		
		/// <summary> This is the namespace decorator associated with this scanner.</summary>
		private NamespaceDecorator decorator;
		
		/// <summary> This is the scanner that is used to acquire the constructors.</summary>
		private ConstructorScanner scanner;
		
		/// <summary> This is the namespace associated with the scanned class.</summary>
		private Namespace namespace_Renamed;
		
		/// <summary> This function acts as a pointer to the types commit process.</summary>
		private Function commit_Renamed_Field;
		
		/// <summary> This function acts as a pointer to the types validate process.</summary>
		private Function validate_Renamed_Field;
		
		/// <summary> This function acts as a pointer to the types persist process.</summary>
		private Function persist_Renamed_Field;
		
		/// <summary> This function acts as a pointer to the types complete process.</summary>
		private Function complete_Renamed_Field;
		
		/// <summary> This function is used as a pointer to the replacement method.</summary>
		private Function replace_Renamed_Field;
		
		/// <summary> This function is used as a pointer to the resolution method.</summary>
		private Function resolve_Renamed_Field;
		
		/// <summary> This is used to determine if there is a default type for this.</summary>
		private Default access;
		
		/// <summary> This is the optional order annotation for the scanned class.</summary>
		private Order order;
		
		/// <summary> This is the optional root annotation for the scanned class.</summary>
		private Root root;
		
		/// <summary> Constructor for the <code>ClassScanner</code> object. This is 
		/// used to scan the provided class for annotations that are used 
		/// to build a schema for an XML file to follow. 
		/// 
		/// </summary>
		/// <param name="type">this is the type that is scanned for a schema
		/// </param>
		public ClassScanner(System.Type type)
		{
			InitBlock();
			this.scanner = new ConstructorScanner(type);
			this.decorator = new NamespaceDecorator();
			this.scan(type);
		}
		
		/// <summary> Scan the fields and methods such that the given class is scanned 
		/// first then all super classes up to the root <code>Object</code>. 
		/// All fields and methods from the most specialized classes override 
		/// fields and methods from higher up the inheritance hierarchy. This
		/// means that annotated details can be overridden.
		/// 
		/// </summary>
		/// <param name="type">the class to extract method and class annotations
		/// </param>
		private void  scan(System.Type type)
		{
			System.Type real = type;
			
			while (type != null)
			{
				global(type);
				scope(type);
				scan(real, type);
				type = type.BaseType;
			}
			process(real);
		}
		
		/// <summary> This is used to acquire the annotations that apply globally to 
		/// the scanned class. Global annotations are annotations that
		/// are applied to the class, such annotations will be used to
		/// determine characteristics for the fields and methods of the
		/// class, which the serializer uses in the serialization process.  
		/// 
		/// </summary>
		/// <param name="type">this is the type to extract the annotations from
		/// </param>
		private void  global(System.Type type)
		{
			if (namespace_Renamed == null)
			{
				namespace_Renamed(type);
			}
			if (root == null)
			{
				root(type);
			}
			if (order == null)
			{
				order(type);
			}
			if (access == null)
			{
				access(type);
			}
		}
		
		/// <summary> This is used to scan the specified class for methods so that
		/// the persister callback annotations can be collected. These
		/// annotations help object implementations to validate the data
		/// that is injected into the instance during deserialization.
		/// 
		/// </summary>
		/// <param name="real">this is the actual type of the scanned class 
		/// </param>
		/// <param name="type">this is a type from within the class hierarchy
		/// 
		/// </param>
		/// <throws>  Exception thrown if the class schema is invalid </throws>
		private void  scan(System.Type real, System.Type type)
		{
			System.Reflection.MethodInfo[] method = type.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static);
			
			for (int i = 0; i < method.Length; i++)
			{
				scan(method[i]);
			}
		}
		
		/// <summary> This is used to acquire the optional <code>Root</code> from the
		/// specified class. The root annotation provides information as
		/// to how the object is to be parsed as well as other information
		/// such as the name of the object if it is to be serialized.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the class to be inspected
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void root(Class < ? > type)
		
		/// <summary> This is used to acquire the optional order annotation to provide
		/// order to the elements and attributes for the generated XML. This
		/// acts as an override to the order provided by the declaration of
		/// the types within the object.  
		/// 
		/// </summary>
		/// <param name="type">this is the type to be scanned for the order
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void order(Class < ? > type)
		
		/// <summary> This is used to extract the <code>Default</code> annotation from
		/// the class. If this annotation is present it provides the access
		/// type that should be used to determine default fields and methods.
		/// If it is not present no default annotations will be applied.
		/// 
		/// </summary>
		/// <param name="type">this is the type to extract the annotation from
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void access(Class < ? > type)
		
		/// <summary> This is use to scan for <code>Namespace</code> annotations on
		/// the class. Once a namespace has been located then it is used
		/// to populate the internal namespace decorator. This can then be
		/// used to decorate any output node that requires it.
		/// 
		/// </summary>
		/// <param name="type">this is the XML schema class to scan for namespaces
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void namespace(Class < ? > type)
		
		/// <summary> This is use to scan for <code>NamespaceList</code> annotations 
		/// on the class. Once a namespace list has been located then it is 
		/// used to populate the internal namespace decorator. This can then 
		/// be used to decorate any output node that requires it.
		/// 
		/// </summary>
		/// <param name="type">this is the XML class to scan for namespace lists
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void scope(Class < ? > type)
		
		/// <summary> This is used to scan the specified object to extract the fields
		/// and methods that are to be used in the serialization process.
		/// This will acquire all fields and getter setter pairs that have
		/// been annotated with the XML annotations.
		/// 
		/// </summary>
		/// <param name="type">this is the object type that is to be scanned
		/// </param>
		private void  process(System.Type type)
		{
			if (namespace_Renamed != null)
			{
				decorator.set_Renamed(namespace_Renamed);
			}
		}
		
		/// <summary> Scans the provided method for a persister callback method. If 
		/// the method contains an method annotated as a callback that 
		/// method is stored so that it can be invoked by the persister
		/// during the serialization and deserialization process.
		/// 
		/// </summary>
		/// <param name="method">this is the method to scan for callback methods
		/// </param>
		private void  scan(System.Reflection.MethodInfo method)
		{
			if (commit_Renamed_Field == null)
			{
				commit(method);
			}
			if (validate_Renamed_Field == null)
			{
				validate(method);
			}
			if (persist_Renamed_Field == null)
			{
				persist(method);
			}
			if (complete_Renamed_Field == null)
			{
				complete(method);
			}
			if (replace_Renamed_Field == null)
			{
				replace(method);
			}
			if (resolve_Renamed_Field == null)
			{
				resolve(method);
			}
		}
		
		/// <summary> This method is used to check the provided method to determine
		/// if it contains the <code>Replace</code> annotation. If the
		/// method contains the required annotation it is stored so that
		/// it can be invoked during the deserialization process.
		/// 
		/// </summary>
		/// <param name="method">this is the method checked for the annotation
		/// </param>
		private void  replace(System.Reflection.MethodInfo method)
		{
			Annotation mark = method.getAnnotation(typeof(Replace));
			
			if (mark != null)
			{
				replace_Renamed_Field = getFunction(method);
			}
		}
		
		/// <summary> This method is used to check the provided method to determine
		/// if it contains the <code>Resolve</code> annotation. If the
		/// method contains the required annotation it is stored so that
		/// it can be invoked during the deserialization process.
		/// 
		/// </summary>
		/// <param name="method">this is the method checked for the annotation
		/// </param>
		private void  resolve(System.Reflection.MethodInfo method)
		{
			Annotation mark = method.getAnnotation(typeof(Resolve));
			
			if (mark != null)
			{
				resolve_Renamed_Field = getFunction(method);
			}
		}
		
		/// <summary> This method is used to check the provided method to determine
		/// if it contains the <code>Commit</code> annotation. If the
		/// method contains the required annotation it is stored so that
		/// it can be invoked during the deserialization process.
		/// 
		/// </summary>
		/// <param name="method">this is the method checked for the annotation
		/// </param>
		private void  commit(System.Reflection.MethodInfo method)
		{
			Annotation mark = method.getAnnotation(typeof(Commit));
			
			if (mark != null)
			{
				commit_Renamed_Field = getFunction(method);
			}
		}
		
		/// <summary> This method is used to check the provided method to determine
		/// if it contains the <code>Validate</code> annotation. If the
		/// method contains the required annotation it is stored so that
		/// it can be invoked during the deserialization process.
		/// 
		/// </summary>
		/// <param name="method">this is the method checked for the annotation
		/// </param>
		private void  validate(System.Reflection.MethodInfo method)
		{
			Annotation mark = method.getAnnotation(typeof(Validate));
			
			if (mark != null)
			{
				validate_Renamed_Field = getFunction(method);
			}
		}
		
		/// <summary> This method is used to check the provided method to determine
		/// if it contains the <code>Persist</code> annotation. If the
		/// method contains the required annotation it is stored so that
		/// it can be invoked during the deserialization process.
		/// 
		/// </summary>
		/// <param name="method">this is the method checked for the annotation
		/// </param>
		private void  persist(System.Reflection.MethodInfo method)
		{
			Annotation mark = method.getAnnotation(typeof(Persist));
			
			if (mark != null)
			{
				persist_Renamed_Field = getFunction(method);
			}
		}
		
		/// <summary> This method is used to check the provided method to determine
		/// if it contains the <code>Complete</code> annotation. If the
		/// method contains the required annotation it is stored so that
		/// it can be invoked during the deserialization process.
		/// 
		/// </summary>
		/// <param name="method">this is the method checked for the annotation
		/// </param>
		private void  complete(System.Reflection.MethodInfo method)
		{
			Annotation mark = method.getAnnotation(typeof(Complete));
			
			if (mark != null)
			{
				complete_Renamed_Field = getFunction(method);
			}
		}
		
		/// <summary> This is used to acquire a <code>Function</code> object for the
		/// method provided. The function returned will allow the callback
		/// method to be invoked when given the context and target object.
		/// 
		/// </summary>
		/// <param name="method">this is the method that is to be invoked
		/// 
		/// </param>
		/// <returns> this returns the function that is to be invoked
		/// </returns>
		private Function getFunction(System.Reflection.MethodInfo method)
		{
			bool contextual = isContextual(method);
			
			//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.isAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
			if (!method.isAccessible())
			{
				//UPGRADE_ISSUE: Method 'java.lang.reflect.AccessibleObject.setAccessible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectAccessibleObject'"
				method.setAccessible(true);
			}
			return new Function(method, contextual);
		}
		
		/// <summary> This is used to determine whether the annotated method takes a
		/// contextual object. If the method takes a <code>Map</code> then
		/// this returns true, otherwise it returns false.
		/// 
		/// </summary>
		/// <param name="method">this is the method to check the parameters of
		/// 
		/// </param>
		/// <returns> this returns true if the method takes a map object
		/// </returns>
		private bool isContextual(System.Reflection.MethodInfo method)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] list = method.GetParameters();
			
			if (list.Length == 1)
			{
				return typeof(System.Collections.IDictionary).Equals(list[0]);
			}
			return false;
		}
	}
}