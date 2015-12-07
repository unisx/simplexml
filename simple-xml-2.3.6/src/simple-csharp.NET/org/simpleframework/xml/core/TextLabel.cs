/*
* TextLabel.java April 2007
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
using Text = org.simpleframework.xml.Text;
using Type = org.simpleframework.xml.strategy.Type;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>TextLabel</code> represents a label that is used to get
	/// a converter for a text entry within an XML element. This label is
	/// used to convert an XML text entry into a primitive value such as 
	/// a string or an integer, this will throw an exception if the field
	/// value does not represent a primitive object.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.Text">
	/// </seealso>
	class TextLabel : Label
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
				return null;
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
				return contact;
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
				return contact.ToString();
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
		/// <summary> This is used to acquire the dependent class for this label. 
		/// This returns null as there are no dependents to the XML text
		/// annotation as it can only hold primitives with no dependents.
		/// 
		/// </summary>
		/// <returns> this is used to return the dependent type of null
		/// </returns>
		virtual public Type Dependent
		{
			get
			{
				return null;
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
				return false;
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
		/// <summary> This is used to determine if the <code>Text</code> method or
		/// field is to have its value written as a CDATA block. This will
		/// set the output node to CDATA mode if this returns true, if it
		/// is false data will be written according to an inherited mode.
		/// By default inherited mode results in escaped XML text.
		/// 
		/// </summary>
		/// <returns> this returns true if the text is to be a CDATA block
		/// </returns>
		virtual public bool Data
		{
			get
			{
				return label.data();
			}
			
		}
		/// <summary> This method is used by the deserialization process to check
		/// to see if an annotation is inline or not. If an annotation
		/// represents an inline XML entity then the deserialization
		/// and serialization process ignores overrides and special 
		/// attributes. By default all text entities are inline.
		/// 
		/// </summary>
		/// <returns> this always returns true for text labels
		/// </returns>
		virtual public bool Inline
		{
			get
			{
				return true;
			}
			
		}
		
		/// <summary> This represents the signature of the annotated contact.</summary>
		private Signature detail;
		
		/// <summary> The contact that this annotation label represents.</summary>
		private Contact contact;
		
		/// <summary> References the annotation that was used by the contact.</summary>
		private Text label;
		
		/// <summary> This is the type of the class that the field references.</summary>
		private System.Type type;
		
		/// <summary> This is the default value to use if the real value is null.</summary>
		private System.String empty;
		
		/// <summary> Constructor for the <code>TextLabel</code> object. This is
		/// used to create a label that can convert a XML node into a 
		/// primitive value from an XML element text value.
		/// 
		/// </summary>
		/// <param name="contact">this is the contact this label represents
		/// </param>
		/// <param name="label">this is the annotation for the contact 
		/// </param>
		public TextLabel(Contact contact, Text label)
		{
			this.detail = new Signature(contact, this);
			this.type = contact.getType();
			this.empty = label.empty();
			this.contact = contact;
			this.label = label;
		}
		
		/// <summary> Creates a converter that can be used to transform an XML node to
		/// an object and vice versa. The converter created will handles
		/// only XML text and requires the context object to be provided. 
		/// 
		/// </summary>
		/// <param name="context">this is the context object used for serialization
		/// 
		/// </param>
		/// <returns> this returns a converter for serializing XML elements
		/// </returns>
		public virtual Converter getConverter(Context context)
		{
			System.String ignore = getEmpty(context);
			Type type = Contact;
			
			if (!context.isPrimitive(type))
			{
				throw new TextException("Cannot use %s to represent %s", label, type);
			}
			return new Primitive(context, type, ignore);
		}
		
		/// <summary> This is used to acquire the name of the element or attribute
		/// that is used by the class schema. The name is determined by
		/// checking for an override within the annotation. If it contains
		/// a name then that is used, if however the annotation does not
		/// specify a name the the field or method name is used instead.
		/// 
		/// </summary>
		/// <param name="context">this is the context that is used for styling
		/// 
		/// </param>
		/// <returns> returns the name that is used for the XML property
		/// </returns>
		public virtual System.String getName(Context context)
		{
			return contact.ToString();
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
		public virtual System.String getEmpty(Context context)
		{
			if (detail.isEmpty(empty))
			{
				return null;
			}
			return empty;
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
			return contact.ToString();
		}
		
		/// <summary> This is typically used to acquire the entry value as acquired
		/// from the annotation. However given that the annotation this
		/// represents does not have a entry attribute this will always
		/// provide a null value for the entry string.
		/// 
		/// </summary>
		/// <returns> this will always return null for the entry value 
		/// </returns>
		public virtual System.String getEntry()
		{
			return null;
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