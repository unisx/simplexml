/*
* Variable.java December 2007
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
using Type = org.simpleframework.xml.strategy.Type;
using InputNode = org.simpleframework.xml.stream.InputNode;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
using Position = org.simpleframework.xml.stream.Position;
using Style = org.simpleframework.xml.stream.Style;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Variable</code> object is used to represent a variable 
	/// for a method or field of a deserialized object. It has the value
	/// for the field or method as well as the details from the annotation.
	/// This is used by the <code>Collector</code> to populate an object
	/// once all the values for that object have been collected. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Collector">
	/// </seealso>
	class Variable : Label
	{
		/// <summary> This is used to acquire the value associated with the variable.
		/// Once fully deserialized the value is used to set the value for 
		/// a field or method of the object. This value can be repeatedly
		/// read if the <code>Converter</code> is acquired a second time.
		/// 
		/// </summary>
		/// <returns> this returns the value that has been deserialized
		/// </returns>
		virtual public System.Object Value
		{
			get
			{
				return value_Renamed;
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
				return label.Decorator;
			}
			
		}
		/// <summary> This is used to acquire the contact object for this label. The 
		/// contact retrieved can be used to set any object or primitive that
		/// has been deserialized, and can also be used to acquire values to
		/// be serialized in the case of object persistence. All contacts 
		/// that are retrieved from this method will be accessible. 
		/// 
		/// </summary>
		/// <returns> returns the field that this label is representing
		/// </returns>
		virtual public Contact Contact
		{
			get
			{
				return label.Contact;
			}
			
		}
		/// <summary> This returns the dependent type for the annotation. This type
		/// is the type other than the annotated field or method type that
		/// the label depends on. For the <code>ElementList</code> and 
		/// the <code>ElementArray</code> this is the component type that
		/// is deserialized individually and inserted into the container. 
		/// 
		/// </summary>
		/// <returns> this is the type that the annotation depends on
		/// </returns>
		virtual public Type Dependent
		{
			get
			{
				return label.Dependent;
			}
			
		}
		/// <summary> This is used to acquire the name of the element or attribute
		/// as taken from the annotation. If the element or attribute
		/// explicitly specifies a name then that name is used for the
		/// XML element or attribute used. If however no overriding name
		/// is provided then the method or field is used for the name. 
		/// 
		/// </summary>
		/// <returns> returns the name of the annotation for the contact
		/// </returns>
		virtual public System.String Override
		{
			get
			{
				return label.Override;
			}
			
		}
		/// <summary> This acts as a convenience method used to determine the type of
		/// the field this represents. This is used when an object is written
		/// to XML. It determines whether a <code>class</code> attribute
		/// is required within the serialized XML element, that is, if the
		/// class returned by this is different from the actual value of the
		/// object to be serialized then that type needs to be remembered.
		/// 
		/// </summary>
		/// <returns> this returns the type of the field class
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				return label.Type;
			}
			
		}
		/// <summary> This is used to determine whether the annotation requires it
		/// and its children to be written as a CDATA block. This is done
		/// when a primitive or other such element requires a text value
		/// and that value needs to be encapsulated within a CDATA block.
		/// 
		/// </summary>
		/// <returns> this returns true if the element requires CDATA
		/// </returns>
		virtual public bool Data
		{
			get
			{
				return label.Data;
			}
			
		}
		/// <summary> This is used to determine whether the label represents an
		/// inline XML entity. The <code>ElementList</code> annotation
		/// and the <code>Text</code> annotation represent inline 
		/// items. This means that they contain no containing element
		/// and so can not specify overrides or special attributes.
		/// 
		/// </summary>
		/// <returns> this returns true if the annotation is inline
		/// </returns>
		virtual public bool Inline
		{
			get
			{
				return label.Inline;
			}
			
		}
		/// <summary> This method is used to determine if the label represents an
		/// attribute. This is used to style the name so that elements
		/// are styled as elements and attributes are styled as required.
		/// 
		/// </summary>
		/// <returns> this is used to determine if this is an attribute
		/// </returns>
		virtual public bool Attribute
		{
			get
			{
				return label.Attribute;
			}
			
		}
		/// <summary> This is used to determine if the label is a collection. If the
		/// label represents a collection then any original assignment to
		/// the field or method can be written to without the need to 
		/// create a new collection. This allows obscure collections to be
		/// used and also allows initial entries to be maintained.
		/// 
		/// </summary>
		/// <returns> true if the label represents a collection value
		/// </returns>
		virtual public bool Collection
		{
			get
			{
				return label.Collection;
			}
			
		}
		/// <summary> Determines whether the XML attribute or element is required. 
		/// This ensures that if an XML element is missing from a document
		/// that deserialization can continue. Also, in the process of
		/// serialization, if a value is null it does not need to be 
		/// written to the resulting XML document.
		/// 
		/// </summary>
		/// <returns> true if the label represents a some required data
		/// </returns>
		virtual public bool Required
		{
			get
			{
				return label.Required;
			}
			
		}
		
		/// <summary> This is the object that has been deserialized from the XML.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Object value_Renamed;
		
		/// <summary> This contains the details for the annotated field or method.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Label label;
		
		/// <summary> Constructor for the <code>Variable</code> object. This is used
		/// to create an object that holds a deserialized value, as well as
		/// the details of the annotated method or field it is to be set to.
		/// This allows the value to be repeatedly deserialized.
		/// 
		/// </summary>
		/// <param name="label">this is the label for the field or method used
		/// </param>
		/// <param name="value">the deserialized object for the method or field
		/// </param>
		public Variable(Label label, System.Object value_Renamed)
		{
			this.label = label;
			this.value_Renamed = value_Renamed;
		}
		
		/// <summary> This method returns a <code>Converter</code> which can be used to
		/// convert an XML node into an object value and vice versa. The 
		/// converter requires only the context object in order to perform
		/// serialization or deserialization of the provided XML node.
		/// 
		/// </summary>
		/// <param name="context">this is the context object for the serialization
		/// 
		/// </param>
		/// <returns> this returns an object that is used for conversion
		/// </returns>
		public virtual Converter getConverter(Context context)
		{
			Converter reader = label.getConverter(context);
			
			if (reader is Adapter)
			{
				return reader;
			}
			return new Adapter(this, reader, value_Renamed);
		}
		
		/// <summary> This is used to acquire the name of the element or attribute
		/// that is used by the class schema. The name is determined by
		/// checking for an override within the annotation. If it contains
		/// a name then that is used, if however the annotation does not
		/// specify a name the the field or method name is used instead.
		/// 
		/// </summary>
		/// <param name="context">this is the context used to style the name
		/// 
		/// </param>
		/// <returns> returns the name that is used for the XML property
		/// </returns>
		public virtual System.String getName(Context context)
		{
			System.String name = label.getName(context);
			Style style = context.Style;
			
			return style.getElement(name);
		}
		
		/// <summary> This is used to provide a configured empty value used when the
		/// annotated value is null. This ensures that XML can be created
		/// with required details regardless of whether values are null or
		/// not. It also provides a means for sensible default values.
		/// 
		/// </summary>
		/// <param name="context">this is the context object for the serialization
		/// 
		/// </param>
		/// <returns> this returns the string to use for default values
		/// </returns>
		public virtual System.Object getEmpty(Context context)
		{
			return label.getEmpty(context);
		}
		
		/// <summary> This is used to either provide the entry value provided within
		/// the annotation or compute a entry value. If the entry string
		/// is not provided the the entry value is calculated as the type
		/// of primitive the object is as a simplified class name.
		/// 
		/// </summary>
		/// <returns> this returns the name of the XML entry element used 
		/// </returns>
		public virtual System.String getEntry()
		{
			return label.getEntry();
		}
		
		/// <summary> This is used to acquire the name of the element or attribute
		/// that is used by the class schema. The name is determined by
		/// checking for an override within the annotation. If it contains
		/// a name then that is used, if however the annotation does not
		/// specify a name the the field or method name is used instead.
		/// 
		/// </summary>
		/// <returns> returns the name that is used for the XML property
		/// </returns>
		public virtual System.String getName()
		{
			return label.getName();
		}
		
		/// <summary> This is used to describe the annotation and method or field
		/// that this label represents. This is used to provide error
		/// messages that can be used to debug issues that occur when
		/// processing a method. This should provide enough information
		/// such that the problem can be isolated correctly. 
		/// 
		/// </summary>
		/// <returns> this returns a string representation of the label
		/// </returns>
		public override System.String ToString()
		{
			return label.ToString();
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Adapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> The <code>Adapter</code> object is used to call the repeater
		/// with the original deserialized object. Using this object the
		/// converter interface can be used to perform repeat reads for
		/// the object. This must be given a <code>Repeater</code> in 
		/// order to invoke the repeat read method.
		/// 
		/// </summary>
		/// <author>  Niall Gallagher
		/// </author>
		private class Adapter : Repeater
		{
			private void  InitBlock(Variable enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Variable enclosingInstance;
			public Variable Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			/// <summary> This is the converter object used to perform a repeat read.</summary>
			//UPGRADE_NOTE: Final was removed from the declaration of 'reader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private Converter reader;
			
			/// <summary> This is the originally deserialized object value to use.</summary>
			//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Object value_Renamed;
			
			/// <summary> Constructor for the <code>Adapter</code> object. This will
			/// create an adapter between the converter an repeater such
			/// that the reads will read from the XML to the original.
			/// 
			/// </summary>
			/// <param name="reader">this is the converter object to be used      
			/// </param>
			/// <param name="value">this is the originally deserialized object
			/// </param>
			public Adapter(Variable enclosingInstance, Converter reader, System.Object value_Renamed)
			{
				InitBlock(enclosingInstance);
				this.reader = reader;
				this.value_Renamed = value_Renamed;
			}
			
			/// <summary> This <code>read</code> method will perform a read using the
			/// provided object with the repeater. Reading with this method
			/// ensures that any additional XML elements within the source
			/// will be added to the value.
			/// 
			/// </summary>
			/// <param name="node">this is the node that contains the extra data
			/// 
			/// </param>
			/// <returns> this will return the original deserialized object
			/// </returns>
			public virtual System.Object read(InputNode node)
			{
				return read(node, value_Renamed);
			}
			
			/// <summary> This <code>read</code> method will perform a read using the
			/// provided object with the repeater. Reading with this method
			/// ensures that any additional XML elements within the source
			/// will be added to the value.
			/// 
			/// </summary>
			/// <param name="node">this is the node that contains the extra data
			/// 
			/// </param>
			/// <returns> this will return the original deserialized object
			/// </returns>
			public virtual System.Object read(InputNode node, System.Object value_Renamed)
			{
				Position line = node.Position;
				System.String name = node.Name;
				
				if (reader is Repeater)
				{
					Repeater repeat = (Repeater) reader;
					
					return repeat.read(node, value_Renamed);
				}
				throw new PersistenceException("Element '%s' declared twice at %s", name, line);
			}
			
			/// <summary> This <code>read</code> method will perform a read using the
			/// provided object with the repeater. Reading with this method
			/// ensures that any additional XML elements within the source
			/// will be added to the value.
			/// 
			/// </summary>
			/// <param name="node">this is the node that contains the extra data
			/// 
			/// </param>
			/// <returns> this will return the original deserialized object
			/// </returns>
			public virtual bool validate(InputNode node)
			{
				Position line = node.Position;
				System.String name = node.Name;
				
				if (reader is Repeater)
				{
					Repeater repeat = (Repeater) reader;
					
					return repeat.validate(node);
				}
				throw new PersistenceException("Element '%s' declared twice at %s", name, line);
			}
			
			/// <summary> This <code>write</code> method acts like any other write
			/// in that it passes on the node and source object to write.
			/// Typically this will not be used as the repeater object is
			/// used for repeat reads of scattered XML elements.
			/// 
			/// </summary>
			/// <param name="node">this is the node to write the data to
			/// </param>
			/// <param name="value">this is the source object to be written
			/// </param>
			public virtual void  write(OutputNode node, System.Object value_Renamed)
			{
				write(node, value_Renamed);
			}
		}
	}
}