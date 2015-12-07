/*
* Traverser.java July 2006
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
using Type = org.simpleframework.xml.strategy.Type;
using InputNode = org.simpleframework.xml.stream.InputNode;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
using Style = org.simpleframework.xml.stream.Style;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Traverser</code> object is used to traverse the XML class
	/// schema and either serialize or deserialize an object. This is the
	/// root of all serialization and deserialization operations. It uses
	/// the <code>Root</code> annotation to ensure that the XML schema
	/// matches the provided XML element. If no root element is defined the
	/// serialization and deserialization cannot be performed.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Traverser
	{
		
		/// <summary> This is the context object used for the traversal performed.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> This is the style that is used to style the XML roots.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Style style;
		
		/// <summary> Constructor for the <code>Traverser</code> object. This creates
		/// a traverser that can be used to perform serialization or
		/// or deserialization of an object. This requires a source object.
		/// 
		/// </summary>
		/// <param name="context">the context object used for the traversal
		/// </param>
		public Traverser(Context context)
		{
			this.style = context.Style;
			this.context = context;
		}
		
		/// <summary> This will acquire the <code>Decorator</code> for the type.
		/// A decorator is an object that adds various details to the
		/// node without changing the overall structure of the node. For
		/// example comments and namespaces can be added to the node with
		/// a decorator as they do not affect the deserialization.
		/// 
		/// </summary>
		/// <param name="type">this is the type to acquire the decorator for 
		/// 
		/// </param>
		/// <returns> this returns the decorator associated with this
		/// </returns>
		private Decorator getDecorator(System.Type type)
		{
			return context.getDecorator(type);
		}
		
		/// <summary> This <code>read</code> method is used to deserialize an object 
		/// from the provided XML element. The class provided acts as the
		/// XML schema definition used to control the deserialization. If
		/// the XML schema does not have a <code>Root</code> annotation 
		/// this throws an exception. Also if the root annotation name is
		/// not the same as the XML element name an exception is thrown.  
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to be deserialized
		/// </param>
		/// <param name="type">this is the XML schema class to be used
		/// 
		/// </param>
		/// <returns> an object deserialized from the XML element 
		/// 
		/// </returns>
		/// <throws>  Exception if the XML schema does not match the node </throws>
		public virtual System.Object read(InputNode node, System.Type type)
		{
			Composite factory = getComposite(type);
			System.Object value_Renamed = factory.read(node);
			
			if (value_Renamed != null)
			{
				System.Type real = value_Renamed.GetType();
				
				return read(node, real, value_Renamed);
			}
			return null;
		}
		
		/// <summary> This <code>read</code> method will read the contents of the XML
		/// document from the provided source and populate the object with
		/// the values deserialized. This is used as a means of injecting an
		/// object with values deserialized from an XML document. If the
		/// XML source cannot be deserialized or there is a problem building
		/// the object graph an exception is thrown. 
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to be deserialized
		/// </param>
		/// <param name="value">this is the value that is to be deserialized
		/// 
		/// </param>
		/// <returns> an object deserialized from the XML element 
		/// 
		/// </returns>
		/// <throws>  Exception if the XML schema does not match the node </throws>
		public virtual System.Object read(InputNode node, System.Object value_Renamed)
		{
			System.Type type = value_Renamed.GetType();
			Composite factory = getComposite(type);
			System.Object real = factory.read(node, value_Renamed);
			
			return read(node, type, real);
		}
		
		/// <summary> This <code>read</code> method is used to deserialize an object 
		/// from the provided XML element. The class provided acts as the
		/// XML schema definition used to control the deserialization. If
		/// the XML schema does not have a <code>Root</code> annotation 
		/// this throws an exception. Also if the root annotation name is
		/// not the same as the XML element name an exception is thrown.  
		/// 
		/// </summary>
		/// <param name="node">this is the node that is to be deserialized
		/// </param>
		/// <param name="value">this is the XML schema object to be used
		/// 
		/// </param>
		/// <returns> an object deserialized from the XML element 
		/// 
		/// </returns>
		/// <throws>  Exception if the XML schema does not match the XML </throws>
		private System.Object read(InputNode node, System.Type type, System.Object value_Renamed)
		{
			System.String root = getName(type);
			
			if (root == null)
			{
				throw new RootException("Root annotation required for %s", type);
			}
			return value_Renamed;
		}
		
		/// <summary> This <code>validate</code> method will validate the contents of
		/// the XML document against the specified XML class schema. This is
		/// used to perform a read traversal of the class schema such that 
		/// the document can be tested against it. This is preferred to
		/// reading the document as it does not instantiate the objects or
		/// invoke any callback methods, thus making it a safe validation.
		/// 
		/// </summary>
		/// <param name="type">this is the class type to be validated against XML
		/// </param>
		/// <param name="node">this provides the source of the XML document
		/// 
		/// </param>
		/// <returns> true if the document matches the class XML schema 
		/// 
		/// </returns>
		/// <throws>  Exception if the class XML schema does not fully match </throws>
		public virtual bool validate(InputNode node, System.Type type)
		{
			Composite factory = getComposite(type);
			System.String root = getName(type);
			
			if (root == null)
			{
				throw new RootException("Root annotation required for %s", type);
			}
			return factory.validate(node);
		}
		
		/// <summary> This <code>write</code> method is used to convert the provided
		/// object to an XML element. This creates a child node from the
		/// given <code>OutputNode</code> object. Once this child element 
		/// is created it is populated with the fields of the source object
		/// in accordance with the XML schema class.  
		/// 
		/// </summary>
		/// <param name="source">this is the object to be serialized to XML
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a problem serializing </throws>
		public virtual void  write(OutputNode node, System.Object source)
		{
			write(node, source, source.GetType());
		}
		
		/// <summary> This <code>write</code> method is used to convert the provided
		/// object to an XML element. This creates a child node from the
		/// given <code>OutputNode</code> object. Once this child element 
		/// is created it is populated with the fields of the source object
		/// in accordance with the XML schema class.  
		/// 
		/// </summary>
		/// <param name="source">this is the object to be serialized to XML
		/// </param>
		/// <param name="expect">this is the class that is expected to be written
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a problem serializing </throws>
		public virtual void  write(OutputNode node, System.Object source, System.Type expect)
		{
			System.Type type = source.GetType();
			System.String root = getName(type);
			
			if (root == null)
			{
				throw new RootException("Root annotation required for %s", type);
			}
			write(node, source, expect, root);
		}
		
		/// <summary> This <code>write</code> method is used to convert the provided
		/// object to an XML element. This creates a child node from the
		/// given <code>OutputNode</code> object. Once this child element 
		/// is created it is populated with the fields of the source object
		/// in accordance with the XML schema class.  
		/// 
		/// </summary>
		/// <param name="source">this is the object to be serialized to XML
		/// </param>
		/// <param name="expect">this is the class that is expected to be written
		/// </param>
		/// <param name="name">this is the name of the root annotation used 
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a problem serializing </throws>
		public virtual void  write(OutputNode node, System.Object source, System.Type expect, System.String name)
		{
			OutputNode child = node.getChild(name);
			Type type = getType(expect);
			
			if (source != null)
			{
				System.Type actual = source.GetType();
				
				if (!context.setOverride(type, source, child))
				{
					Converter convert = getComposite(actual);
					Decorator decorator = getDecorator(actual);
					
					decorator.decorate(child);
					convert.write(child, source);
				}
			}
			child.commit();
		}
		
		/// <summary> This will create a <code>Composite</code> object using the XML 
		/// schema class provided. This makes use of the source object that
		/// this traverser has been given to create a composite converter. 
		/// 
		/// </summary>
		/// <param name="expect">this is the XML schema class to be used
		/// 
		/// </param>
		/// <returns> a converter for the specified XML schema class
		/// </returns>
		private Composite getComposite(System.Type expect)
		{
			Type type = getType(expect);
			
			if (expect == null)
			{
				throw new RootException("Can not instantiate null class");
			}
			return new Composite(context, type);
		}
		
		/// <summary> This is used to acquire a type for the provided class. This will
		/// wrap the class in a <code>Type</code> wrapper object. Wrapping
		/// the class allows it to be used within the framework.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be wrapped for use
		/// 
		/// </param>
		/// <returns> this returns the type that wraps the specified class
		/// </returns>
		private Type getType(System.Type type)
		{
			return new ClassType(type);
		}
		
		/// <summary> Extracts the <code>Root</code> annotation from the provided XML
		/// schema class. If no annotation exists in the provided class the
		/// super class is checked and so on until the <code>Object</code>
		/// is encountered, if no annotation is found this returns null.
		/// 
		/// </summary>
		/// <param name="type">this is the XML schema class to use
		/// 
		/// </param>
		/// <returns> this returns the root annotation for the XML schema
		/// </returns>
		protected internal virtual System.String getName(System.Type type)
		{
			System.String root = context.getName(type);
			System.String name = style.getElement(root);
			
			return name;
		}
	}
}