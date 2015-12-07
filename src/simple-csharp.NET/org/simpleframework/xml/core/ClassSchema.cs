/*
* Schema.java July 2006
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
using Version = org.simpleframework.xml.Version;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Schema</code> object is used to track which fields within
	/// an object have been visited by a converter. This object is necessary
	/// for processing <code>Composite</code> objects. In particular it is
	/// necessary to keep track of which required nodes have been visited 
	/// and which have not, if a required not has not been visited then the
	/// XML source does not match the XML class schema and serialization
	/// must fail before processing any further. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ClassSchema : Schema
	{
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
				return factory;
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
				return version;
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
				return revision;
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
		/// <summary> This is used to acquire the <code>Caller</code> object. This
		/// is used to call the callback methods within the object. If the
		/// object contains no callback methods then this will return an
		/// object that does not invoke any methods that are invoked. 
		/// 
		/// </summary>
		/// <returns> this returns the caller for the specified type
		/// </returns>
		virtual public Caller Caller
		{
			get
			{
				return caller;
			}
			
		}
		/// <summary> Returns a <code>LabelMap</code> that contains the details for
		/// all fields marked as XML attributes. Labels contained within
		/// this map are used to convert primitive types only.
		/// 
		/// </summary>
		/// <returns> map with the details extracted from the schema class
		/// </returns>
		virtual public LabelMap Attributes
		{
			get
			{
				return attributes;
			}
			
		}
		/// <summary> Returns a <code>LabelMap</code> that contains the details for
		/// all fields marked as XML elements. The annotations that are
		/// considered elements are the <code>ElementList</code> and the
		/// <code>Element</code> annotations. 
		/// 
		/// </summary>
		/// <returns> a map containing the details for XML elements
		/// </returns>
		virtual public LabelMap Elements
		{
			get
			{
				return elements;
			}
			
		}
		/// <summary> This returns the <code>Label</code> that represents the text
		/// annotation for the scanned class. Only a single text annotation
		/// can be used per class, so this returns only a single label
		/// rather than a <code>LabelMap</code> object. Also if this is
		/// not null then the elements label map will be empty.
		/// 
		/// </summary>
		/// <returns> this returns the text label for the scanned class
		/// </returns>
		virtual public Label Text
		{
			get
			{
				return text;
			}
			
		}
		
		/// <summary> This is the decorator associated with this schema object.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'decorator '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Decorator decorator;
		
		/// <summary> Contains a map of all attributes present within the schema.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'attributes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private LabelMap attributes;
		
		/// <summary> Contains a map of all elements present within the schema.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'elements '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private LabelMap elements;
		
		/// <summary> This is the version annotation for the XML class schema.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'revision '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Version revision;
		
		/// <summary> This is the scanner that is used to acquire the constructor.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Creator factory;
		
		/// <summary> This is the pointer to the schema class replace method.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'caller '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Caller caller;
		
		/// <summary> This is the version label used to read the version attribute.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'version '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Label version;
		
		/// <summary> This is used to represent a text value within the schema.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'text '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Label text;
		
		/// <summary> This is the type that this class schema is representing.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Type type;
		
		/// <summary> This is used to specify whether the type is a primitive class.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'primitive '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private bool primitive;
		
		/// <summary> Constructor for the <code>Schema</code> object. This is used 
		/// to wrap the element and attribute XML annotations scanned from
		/// a class schema. The schema tracks all fields visited so that
		/// a converter can determine if all fields have been serialized.
		/// 
		/// </summary>
		/// <param name="schema">this contains all labels scanned from the class
		/// </param>
		/// <param name="context">this is the context object for serialization
		/// </param>
		public ClassSchema(Scanner schema, Context context)
		{
			this.attributes = schema.getAttributes(context);
			this.elements = schema.getElements(context);
			this.caller = schema.getCaller(context);
			this.factory = schema.Creator;
			this.revision = schema.Revision;
			this.decorator = schema.Decorator;
			this.primitive = schema.Primitive;
			this.version = schema.Version;
			this.text = schema.Text;
			this.type = schema.Type;
		}
		
		/// <summary> This is used to acquire a description of the schema. This is
		/// useful when debugging an issue as it allows a representation
		/// of the instance to be viewed with the class it represents.
		/// 
		/// </summary>
		/// <returns> this returns a visible description of the schema
		/// </returns>
		public override System.String ToString()
		{
			return String.format("schema for %s", type);
		}
	}
}