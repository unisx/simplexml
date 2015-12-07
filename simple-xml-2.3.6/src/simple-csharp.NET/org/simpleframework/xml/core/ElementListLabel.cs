/*
* ElementListLabel.java July 2006
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
using ElementList = org.simpleframework.xml.ElementList;
using Type = org.simpleframework.xml.strategy.Type;
using Style = org.simpleframework.xml.stream.Style;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>ElementListLabel</code> represents a label that is used
	/// to represent an XML element list in a class schema. This element 
	/// list label can be used to convert an XML node into a collection of
	/// composite objects. Each element converted with the converter this
	/// creates must be an XML serializable element.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.ElementList">
	/// </seealso>
	class ElementListLabel : Label
	{
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
		/// <summary> This is used to acquire the dependent type for the annotated
		/// list. This will simply return the type that the collection is
		/// composed to hold. This must be a serializable type, that is,
		/// a type that is annotated with the <code>Root</code> class.
		/// 
		/// </summary>
		/// <returns> this returns the component type for the collection
		/// </returns>
		virtual public Type Dependent
		{
			get
			{
				Contact contact = Contact;
				
				if (item == typeof(void ))
				{
					item = contact.Dependent;
				}
				if (item == null)
				{
					throw new ElementException("Unable to determine type for %s", contact);
				}
				return new ClassType(item);
			}
			
		}
		/// <summary> This acts as a convenience method used to determine the type of
		/// contact this represents. This is used when an object is written
		/// to XML. It determines whether a <code>class</code> attribute
		/// is required within the serialized XML element, that is, if the
		/// class returned by this is different from the actual value of the
		/// object to be serialized then that type needs to be remembered.
		/// 
		/// </summary>
		/// <returns> this returns the type of the contact class
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				return type;
			}
			
		}
		/// <summary> This is used to acquire the contact object for this label. The 
		/// contact retrieved can be used to set any object or primitive that
		/// has been deserialized, and can also be used to acquire values to
		/// be serialized in the case of object persistence. All contacts 
		/// that are retrieved from this method will be accessible. 
		/// 
		/// </summary>
		/// <returns> returns the contact that this label is representing
		/// </returns>
		virtual public Contact Contact
		{
			get
			{
				return detail.Contact;
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
				return name;
			}
			
		}
		/// <summary> This is used to determine whether the annotation requires it
		/// and its children to be written as a CDATA block. This is done
		/// when a primitive or other such element requires a text value
		/// and that value needs to be encapsulated within a CDATA block.
		/// 
		/// </summary>
		/// <returns> currently the element list does not require CDATA
		/// </returns>
		virtual public bool Data
		{
			get
			{
				return label.data();
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
				return false;
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
				return true;
			}
			
		}
		/// <summary> This is used to determine whether the XML element is required. 
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
				return label.required();
			}
			
		}
		/// <summary> This is used to determine whether the list has been specified
		/// as inline. If the list is inline then no overrides are needed
		/// and the outer XML element for the list is not used.
		/// 
		/// </summary>
		/// <returns> this returns whether the annotation is inline
		/// </returns>
		virtual public bool Inline
		{
			get
			{
				return label.inline();
			}
			
		}
		
		/// <summary> This is the decorator that is associated with the element.</summary>
		private Decorator decorator;
		
		/// <summary> This references the annotation that the field uses.</summary>
		private ElementList label;
		
		/// <summary> This contains the details of the annotated contact object.</summary>
		private Signature detail;
		
		/// <summary> This is the type of collection this list will instantiate.</summary>
		private System.Type type;
		
		/// <summary> Represents the type of objects this list will hold.</summary>
		private System.Type item;
		
		/// <summary> This is the name of the XML entry from the annotation.</summary>
		private System.String entry;
		
		/// <summary> This is the name of the element for this label instance.</summary>
		private System.String name;
		
		/// <summary> Constructor for the <code>ElementListLabel</code> object. This
		/// creates a label object, which can be used to convert an XML 
		/// node to a <code>Collection</code> of XML serializable objects.
		/// 
		/// </summary>
		/// <param name="contact">this is the contact that this label represents
		/// </param>
		/// <param name="label">the annotation that contains the schema details
		/// </param>
		public ElementListLabel(Contact contact, ElementList label)
		{
			this.detail = new Signature(contact, this);
			this.decorator = new Qualifier(contact);
			this.type = contact.getType();
			this.entry = label.entry();
			this.item = label.type();
			this.name = label.name();
			this.label = label;
		}
		
		/// <summary> This will create a <code>Converter</code> for transforming an XML
		/// element into a collection of XML serializable objects. The XML
		/// schema class for these objects must be present the element list
		/// annotation. 
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// 
		/// </param>
		/// <returns> this returns the converter for creating a collection 
		/// </returns>
		public virtual Converter getConverter(Context context)
		{
			System.String entry = getEntry(context);
			
			if (!label.inline())
			{
				return getConverter(context, entry);
			}
			return getInlineConverter(context, entry);
		}
		
		/// <summary> This will create a <code>Converter</code> for transforming an XML
		/// element into a collection of XML serializable objects. The XML
		/// schema class for these objects must be present the element list
		/// annotation. 
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// </param>
		/// <param name="name">this is the name of the XML entry element to use
		/// 
		/// </param>
		/// <returns> this returns the converter for creating a collection 
		/// </returns>
		private Converter getConverter(Context context, System.String name)
		{
			Type item = Dependent;
			Type type = Contact;
			
			if (!context.isPrimitive(item))
			{
				return new CompositeList(context, type, item, name);
			}
			return new PrimitiveList(context, type, item, name);
		}
		
		/// <summary> This will create a <code>Converter</code> for transforming an XML
		/// element into a collection of XML serializable objects. The XML
		/// schema class for these objects must be present the element list
		/// annotation. 
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// 
		/// </param>
		/// <returns> this returns the converter for creating a collection 
		/// </returns>
		private Converter getInlineConverter(Context context, System.String name)
		{
			Type item = Dependent;
			Type type = Contact;
			
			if (!context.isPrimitive(item))
			{
				return new CompositeInlineList(context, type, item, name);
			}
			return new PrimitiveInlineList(context, type, item, name);
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
		public virtual System.String getName(Context context)
		{
			Style style = context.Style;
			System.String name = detail.getName();
			
			return style.getElement(name);
		}
		
		/// <summary> This is used to either provide the entry value provided within
		/// the annotation or compute a entry value. If the entry string
		/// is not provided the the entry value is calculated as the type
		/// of primitive the object is as a simplified class name.
		/// 
		/// </summary>
		/// <param name="context">this is the context used to style the entry
		/// 
		/// </param>
		/// <returns> this returns the name of the XML entry element used 
		/// </returns>
		private System.String getEntry(Context context)
		{
			Style style = context.Style;
			System.String entry = getEntry();
			
			return style.getElement(entry);
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
			Type list = new ClassType(type);
			Factory factory = new CollectionFactory(context, list);
			
			if (!label.empty())
			{
				return factory.getInstance();
			}
			return null;
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
			if (detail.isEmpty(entry))
			{
				entry = detail.Entry;
			}
			return entry;
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
			return detail.getName();
		}
		
		/// <summary> This is used to describe the annotation and method or field
		/// that this label represents. This is used to provide error
		/// messages that can be used to debug issues that occur when
		/// processing a method. This will provide enough information
		/// such that the problem can be isolated correctly. 
		/// 
		/// </summary>
		/// <returns> this returns a string representation of the label
		/// </returns>
		public override System.String ToString()
		{
			return detail.ToString();
		}
	}
}