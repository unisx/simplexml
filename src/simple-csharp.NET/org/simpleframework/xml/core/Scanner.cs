/*
* Scanner.java July 2006
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
//UPGRADE_TODO: The type 'java.lang.annotation.Annotation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Annotation = java.lang.annotation.Annotation;
using Attribute = org.simpleframework.xml.Attribute;
using Default = org.simpleframework.xml.Default;
//UPGRADE_TODO: The type 'org.simpleframework.xml.DefaultType' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DefaultType = org.simpleframework.xml.DefaultType;
using Element = org.simpleframework.xml.Element;
using ElementArray = org.simpleframework.xml.ElementArray;
using ElementList = org.simpleframework.xml.ElementList;
using ElementMap = org.simpleframework.xml.ElementMap;
using Order = org.simpleframework.xml.Order;
using Root = org.simpleframework.xml.Root;
using Text = org.simpleframework.xml.Text;
using Version = org.simpleframework.xml.Version;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Scanner</code> object performs the reflective inspection
	/// of a class and builds a map of attributes and elements for each
	/// annotated field. This acts as a cachable container for reflection
	/// actions performed on a specific type. When scanning the provided
	/// class this inserts the scanned field as a <code>Label</code> in to
	/// a map so that it can be retrieved by name. Annotations classified
	/// as attributes have the <code>Attribute</code> annotation, all other
	/// annotated fields are stored as elements.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.core.Schema">
	/// </seealso>
	class Scanner
	{
		private void  InitBlock()
		{
			System.String real = type.getSimpleName();
			Root root = scanner.Root;
			System.String text = real;
			
			if (root != null)
			{
				text = root.name();
				
				if (isEmpty(text))
				{
					text = Reflector.getName(real);
				}
				name = String.Intern(text);
			}
			Order order = scanner.Order;
			
			if (order != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String name: order.elements())
				{
					elements.put(name, null);
				}
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String name: order.attributes())
				{
					attributes.put(name, null);
				}
			}
			Default holder = scanner.Default;
			
			if (holder != null)
			{
				access = holder.value_Renamed();
			}
		}
		/// <summary> This is used to acquire the type that this scanner scans for
		/// annotations to be used in a schema. Exposing the class that
		/// this represents allows the schema it creates to be known.
		/// 
		/// </summary>
		/// <returns> this is the type that this creator will represent
		/// </returns>
		virtual public System.Type Type
		{
			get
			{
				return type;
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
				return scanner.Decorator;
			}
			
		}
		/// <summary> This is the <code>Version</code> for the scanned class. It 
		/// allows the deserialization process to be configured such that
		/// if the version is different from the schema class none of
		/// the fields and methods are required and unmatched elements
		/// and attributes will be ignored.
		/// 
		/// </summary>
		/// <returns> this returns the version of the class that is scanned
		/// </returns>
		virtual public Version Revision
		{
			get
			{
				if (version_Renamed_Field != null)
				{
					return version_Renamed_Field.Contact.getAnnotation(typeof(Version));
				}
				return null;
			}
			
		}
		/// <summary> This returns the <code>Label</code> that represents the version
		/// annotation for the scanned class. Only a single version can
		/// exist within the class if more than one exists an exception is
		/// thrown. This will read only floating point types such as double.
		/// 
		/// </summary>
		/// <returns> this returns the label used for reading the version
		/// </returns>
		virtual public Label Version
		{
			get
			{
				return version_Renamed_Field;
			}
			
		}
		/// <summary> This returns the <code>Label</code> that represents the text
		/// annotation for the scanned class. Only a single text annotation
		/// can be used per class, so this returns only a single label
		/// rather than a <code>LabelMap</code> object. Also if this is
		/// not null then the elements label map must be empty.
		/// 
		/// </summary>
		/// <returns> this returns the text label for the scanned class
		/// </returns>
		virtual public Label Text
		{
			get
			{
				return text_Renamed_Field;
			}
			
		}
		/// <summary> This returns the name of the class processed by this scanner.
		/// The name is either the name as specified in the last found
		/// <code>Root</code> annotation, or if a name was not specified
		/// within the discovered root then the Java Bean class name of
		/// the last class annotated with a root annotation.
		/// 
		/// </summary>
		/// <returns> this returns the name of the object being scanned
		/// </returns>
		virtual public System.String Name
		{
			get
			{
				return name;
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
				return scanner.Commit;
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
				return scanner.Validate;
			}
			
		}
		/// <summary> This method is used to retrieve the schema class persistence
		/// method. This is invoked during the serialization process to
		/// get the object a chance to perform an nessecary preparation
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
				return scanner.Persist;
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
				return scanner.Complete;
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
				return scanner.Replace;
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
				return scanner.Resolve;
			}
			
		}
		/// <summary> This is used to determine whether the scanned class represents
		/// a primitive type. A primitive type is a type that contains no
		/// XML annotations and so cannot be serialized with an XML form.
		/// Instead primitives a serialized using transformations.
		/// 
		/// </summary>
		/// <returns> this returns true if no XML annotations were found
		/// </returns>
		virtual public bool Primitive
		{
			get
			{
				return primitive;
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
				return scanner.Strict;
			}
			
		}
		
		/// <summary> This method acts as a pointer to the types commit process.</summary>
		private ClassScanner scanner;
		
		/// <summary> This is the default access type to be used for this scanner.</summary>
		private DefaultType access;
		
		/// <summary> This is used to store all labels that are XML attributes.</summary>
		private LabelMap attributes;
		
		/// <summary> This is used to store all labels that are XML elements.</summary>
		private LabelMap elements;
		
		/// <summary> This is used to compare the annotations being scanned.</summary>
		private Comparer comparer;
		
		/// <summary> This is the version label used to read the version attribute.</summary>
		private Label version_Renamed_Field;
		
		/// <summary> This is used to store all labels that are XML text values.</summary>
		private Label text_Renamed_Field;
		
		/// <summary> This is the name of the class as taken from the root class.</summary>
		private System.String name;
		
		/// <summary> This is the type that is being scanned by this scanner.</summary>
		private System.Type type;
		
		/// <summary> This is used to specify whether the type is a primitive class.</summary>
		private bool primitive;
		
		/// <summary> Constructor for the <code>Scanner</code> object. This is used 
		/// to scan the provided class for annotations that are used to
		/// build a schema for an XML file to follow. 
		/// 
		/// </summary>
		/// <param name="type">this is the type that is scanned for a schema
		/// </param>
		public Scanner(System.Type type)
		{
			InitBlock();
			this.scanner = new ClassScanner(type);
			this.attributes = new LabelMap(this);
			this.elements = new LabelMap(this);
			this.comparer = new Comparer();
			this.type = type;
			this.scan(type);
		}
		
		/// <summary> This method is used to return the <code>Caller</code> for this
		/// class. The caller is a means to deliver invocations to the
		/// object for the persister callback methods. It aggregates all of
		/// the persister callback methods in to a single object.
		/// 
		/// </summary>
		/// <returns> this returns a caller used for delivering callbacks
		/// </returns>
		public virtual Caller getCaller(Context context)
		{
			return new Caller(this, context);
		}
		
		/// <summary> Returns a <code>LabelMap</code> that contains the details for
		/// all fields marked as XML attributes. This returns a new map
		/// each time the method is called, the goal is to ensure that any
		/// object using the label map can manipulate it without changing
		/// the core details of the schema, allowing it to be cached.
		/// 
		/// </summary>
		/// <param name="context">this is the context used to style the names 
		/// 
		/// </param>
		/// <returns> map with the details extracted from the schema class
		/// </returns>
		public virtual LabelMap getAttributes(Context context)
		{
			return attributes.clone(context);
		}
		
		/// <summary> Returns a <code>LabelMap</code> that contains the details for
		/// all fields marked as XML elements. The annotations that are
		/// considered elements are the <code>ElementList</code> and the
		/// <code>Element</code> annotations. This returns a copy of the
		/// details extracted from the schema class so this can be cached.
		/// 
		/// </summary>
		/// <param name="context">this is the context used to style the names
		/// 
		/// </param>
		/// <returns> a map containing the details for XML elements
		/// </returns>
		public virtual LabelMap getElements(Context context)
		{
			return elements.clone(context);
		}
		
		/// <summary> This is used to determine whether the scanned class represents
		/// a primitive type. A primitive type is a type that contains no
		/// XML annotations and so cannot be serialized with an XML form.
		/// Instead primitives a serialized using transformations.
		/// 
		/// </summary>
		/// <returns> this returns true if no XML annotations were found
		/// </returns>
		private bool isEmpty()
		{
			Root root = scanner.Root;
			
			if (!elements.isEmpty())
			{
				return false;
			}
			if (!attributes.isEmpty())
			{
				return false;
			}
			if (text_Renamed_Field != null)
			{
				return false;
			}
			return root == null;
		}
		
		/// <summary> This is used to scan the specified object to extract the fields
		/// and methods that are to be used in the serialization process.
		/// This will acquire all fields and getter setter pairs that have
		/// been annotated with the XML annotations.
		/// 
		/// </summary>
		/// <param name="type">this is the object type that is to be scanned
		/// </param>
		private void  scan(System.Type type)
		{
			root(type);
			order(type);
			access(type);
			field(type);
			method(type);
			validate(type);
		}
		
		/// <summary> This is used to validate the configuration of the scanned class.
		/// If a <code>Text</code> annotation has been used with elements
		/// then validation will fail and an exception will be thrown. 
		/// 
		/// </summary>
		/// <param name="type">this is the object type that is being scanned
		/// 
		/// </param>
		/// <throws>  Exception if text and element annotations are present </throws>
		private void  validate(System.Type type)
		{
			Creator creator = scanner.Creator;
			Order order = scanner.Order;
			
			validateElements(type, order);
			validateAttributes(type, order);
			validateParameters(creator);
			validateText(type);
		}
		
		/// <summary> This is used to validate the configuration of the scanned class.
		/// If a <code>Text</code> annotation has been used with elements
		/// then validation will fail and an exception will be thrown. 
		/// 
		/// </summary>
		/// <param name="type">this is the object type that is being scanned
		/// 
		/// </param>
		/// <throws>  Exception if text and element annotations are present </throws>
		private void  validateText(System.Type type)
		{
			if (text_Renamed_Field != null)
			{
				if (!elements.isEmpty())
				{
					throw new TextException("Elements used with %s in %s", text_Renamed_Field, type);
				}
			}
			else
			{
				primitive = isEmpty();
			}
		}
		
		/// <summary> This is used to validate the configuration of the scanned class.
		/// If an ordered element is specified but does not refer to an
		/// existing element then this will throw an exception.
		/// 
		/// </summary>
		/// <param name="type">this is the object type that is being scanned
		/// 
		/// </param>
		/// <throws>  Exception if an ordered element does not exist </throws>
		private void  validateElements(System.Type type, Order order)
		{
			Creator factory = scanner.Creator;
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Builder builder: builders)
			{
				validateConstructor(builder, elements);
			}
			if (order != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String name: order.elements())
				{
					Label label = elements.get_Renamed(name);
					
					if (label == null)
					{
						throw new ElementException("Ordered element '%s' missing for %s", name, type);
					}
				}
			}
		}
		
		/// <summary> This is used to validate the configuration of the scanned class.
		/// If an ordered attribute is specified but does not refer to an
		/// existing attribute then this will throw an exception.
		/// 
		/// </summary>
		/// <param name="type">this is the object type that is being scanned
		/// 
		/// </param>
		/// <throws>  Exception if an ordered attribute does not exist </throws>
		private void  validateAttributes(System.Type type, Order order)
		{
			Creator factory = scanner.Creator;
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Builder builder: builders)
			{
				validateConstructor(builder, elements);
			}
			if (order != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String name: order.attributes())
				{
					Label label = attributes.get_Renamed(name);
					
					if (label == null)
					{
						throw new AttributeException("Ordered attribute '%s' missing for %s", name, type);
					}
				}
			}
		}
		
		/// <summary> This is used to ensure that final methods and fields have a 
		/// constructor parameter that allows the value to be injected in
		/// to. Validating the constructor in this manner ensures that the
		/// class schema remains fully serializable and deserializable.
		/// 
		/// </summary>
		/// <param name="builder">this is the builder to validate the labels with
		/// </param>
		/// <param name="map">this is the map that contains the labels to validate
		/// 
		/// </param>
		/// <throws>  Exception this is thrown if the validation fails </throws>
		private void  validateConstructor(Builder builder, LabelMap map)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Label label: map)
			{
				if (label != null)
				{
					Contact contact = label.getContact();
					System.String name = label.Name;
					
					if (contact.ReadOnly)
					{
						Parameter value_Renamed = builder.getParameter(name);
						
						if (value_Renamed == null)
						{
							throw new ConstructorException("No match found for %s in %s", contact, type);
						}
					}
				}
			}
		}
		
		/// <summary> This is used to ensure that for each parameter in the builder
		/// there is a matching method or field. This ensures that the
		/// class schema is fully readable and writable. If not method or
		/// field annotation exists for the parameter validation fails.
		/// 
		/// </summary>
		/// <param name="creator">this is the creator to validate the labels with
		/// 
		/// </param>
		/// <throws>  Exception this is thrown if the validation fails </throws>
		private void  validateParameters(Creator creator)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Parameter parameter: list)
			{
				System.String name = parameter.Name;
				Label label = elements.get_Renamed(name);
				
				if (label == null)
				{
					label = attributes.get_Renamed(name);
				}
				if (label == null)
				{
					throw new ConstructorException("Parameter '%s' does not have a match in %s", name, type);
				}
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
		
		/// <summary> This is used to determine the access type for the class. The
		/// access type is specified by the <code>DefaultType</code>
		/// enumeration. Setting a default access tells this scanner to
		/// synthesize an XML annotation for all fields or methods that
		/// do not have associated annotations. 
		/// 
		/// </summary>
		/// <param name="type">this is the type to acquire the default type for
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void access(Class < ? > type)
		
		/// <summary> This method is used to determine if a root annotation value is
		/// an empty value. Rather than determining if a string is empty
		/// be comparing it to an empty string this method allows for the
		/// value an empty string represents to be changed in future.
		/// 
		/// </summary>
		/// <param name="value">this is the value to determine if it is empty
		/// 
		/// </param>
		/// <returns> true if the string value specified is an empty value
		/// </returns>
		private bool isEmpty(System.String value_Renamed)
		{
			return value_Renamed.Length == 0;
		}
		
		/// <summary> This is used to acquire the contacts for the annotated fields 
		/// within the specified class. The field contacts are added to
		/// either the attributes or elements map depending on annotation.
		/// 
		/// </summary>
		/// <param name="type">this is the object type that is to be scanned
		/// </param>
		public virtual void  field(System.Type type)
		{
			ContactList list = new FieldScanner(type, access);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Contact contact: list)
			{
				scan(contact, contact.getAnnotation());
			}
		}
		
		/// <summary> This is used to acquire the contacts for the annotated fields 
		/// within the specified class. The field contacts are added to
		/// either the attributes or elements map depending on annotation.
		/// 
		/// </summary>
		/// <param name="type">this is the object type that is to be scanned
		/// </param>
		public virtual void  method(System.Type type)
		{
			ContactList list = new MethodScanner(type, access);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Contact contact: list)
			{
				scan(contact, contact.getAnnotation());
			}
		}
		
		/// <summary> This reflectively checks the annotation to determine the type 
		/// of annotation it represents. If it represents an XML schema
		/// annotation it is used to create a <code>Label</code> which can
		/// be used to represent the field within the context object.
		/// 
		/// </summary>
		/// <param name="field">the field that the annotation comes from
		/// </param>
		/// <param name="label">the annotation used to model the XML schema
		/// 
		/// </param>
		/// <throws>  Exception if there is more than one text annotation </throws>
		private void  scan(Contact field, Annotation label)
		{
			if (label is Attribute)
			{
				process(field, label, attributes);
			}
			if (label is ElementList)
			{
				process(field, label, elements);
			}
			if (label is ElementArray)
			{
				process(field, label, elements);
			}
			if (label is ElementMap)
			{
				process(field, label, elements);
			}
			if (label is Element)
			{
				process(field, label, elements);
			}
			if (label is Version)
			{
				version(field, label);
			}
			if (label is Text)
			{
				text(field, label);
			}
		}
		
		/// <summary> This is used to process the <code>Text</code> annotations that
		/// are present in the scanned class. This will set the text label
		/// for the class and an ensure that if there is more than one
		/// text label within the class an exception is thrown.
		/// 
		/// </summary>
		/// <param name="field">the field the annotation was extracted from
		/// </param>
		/// <param name="type">the annotation extracted from the field
		/// 
		/// </param>
		/// <throws>  Exception if there is more than one text annotation </throws>
		private void  text(Contact field, Annotation type)
		{
			Label label = LabelFactory.getInstance(field, type);
			
			if (text_Renamed_Field != null)
			{
				throw new TextException("Multiple text annotations in %s", type);
			}
			text_Renamed_Field = label;
		}
		
		/// <summary> This is used to process the <code>Text</code> annotations that
		/// are present in the scanned class. This will set the text label
		/// for the class and an ensure that if there is more than one
		/// text label within the class an exception is thrown.
		/// 
		/// </summary>
		/// <param name="field">the field the annotation was extracted from
		/// </param>
		/// <param name="type">the annotation extracted from the field
		/// 
		/// </param>
		/// <throws>  Exception if there is more than one text annotation </throws>
		private void  version(Contact field, Annotation type)
		{
			Label label = LabelFactory.getInstance(field, type);
			
			if (version_Renamed_Field != null)
			{
				throw new AttributeException("Multiple version annotations in %s", type);
			}
			version_Renamed_Field = label;
		}
		
		/// <summary> This is used when all details from a field have been gathered 
		/// and a <code>Label</code> implementation needs to be created. 
		/// This will build a label instance based on the field annotation.
		/// If a label with the same name was already inserted then it is
		/// ignored and the value for that field will not be serialized. 
		/// 
		/// </summary>
		/// <param name="field">the field the annotation was extracted from
		/// </param>
		/// <param name="type">the annotation extracted from the field
		/// </param>
		/// <param name="map">this is used to collect the label instance created
		/// 
		/// </param>
		/// <throws>  Exception thrown if the label can not be created </throws>
		private void  process(Contact field, Annotation type, LabelMap map)
		{
			Label label = LabelFactory.getInstance(field, type);
			System.String name = label.getName();
			
			if (map.get_Renamed(name) != null)
			{
				throw new PersistenceException("Annotation of name '%s' declared twice", name);
			}
			map.put(name, label);
			validate(label, name);
		}
		
		/// <summary> This is used to validate the <code>Parameter</code> object that
		/// exist in the constructors. Validation is performed against the
		/// annotated methods and fields to ensure that they match up.
		/// 
		/// </summary>
		/// <param name="field">this is the annotated method or field to validate
		/// </param>
		/// <param name="name">this is the name of the parameter to validate with
		/// 
		/// </param>
		/// <throws>  Exception thrown if the validation fails </throws>
		private void  validate(Label field, System.String name)
		{
			Creator factory = scanner.Creator;
			Parameter parameter = factory.getParameter(name);
			
			if (parameter != null)
			{
				validate(field, parameter);
			}
		}
		
		/// <summary> This is used to validate the <code>Parameter</code> object that
		/// exist in the constructors. Validation is performed against the
		/// annotated methods and fields to ensure that they match up.
		/// 
		/// </summary>
		/// <param name="field">this is the annotated method or field to validate
		/// </param>
		/// <param name="parameter">this is the parameter to validate with
		/// 
		/// </param>
		/// <throws>  Exception thrown if the validation fails </throws>
		private void  validate(Label field, Parameter parameter)
		{
			Contact contact = field.Contact;
			Annotation label = contact.Annotation;
			Annotation match = parameter.Annotation;
			System.String name = field.getName();
			
			if (!comparer.equals(label, match))
			{
				throw new ConstructorException("Annotation does not match for '%s' in %s", name, type);
			}
			System.Type expect = contact.getType();
			
			if (expect != parameter.Type)
			{
				throw new ConstructorException("Parameter does not match field for '%s' in %s", name, type);
			}
		}
	}
}