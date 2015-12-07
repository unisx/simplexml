/*
* Source.java July 2006
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
using Filter = org.simpleframework.xml.filter.Filter;
using Type = org.simpleframework.xml.strategy.Type;
using Strategy = org.simpleframework.xml.strategy.Strategy;
using Value = org.simpleframework.xml.strategy.Value;
using InputNode = org.simpleframework.xml.stream.InputNode;
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.NodeMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using NodeMap = org.simpleframework.xml.stream.NodeMap;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
using Style = org.simpleframework.xml.stream.Style;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Source</code> object acts as a contextual object that is
	/// used to store all information regarding an instance of serialization
	/// or deserialization. This maintains the <code>Strategy</code> as
	/// well as the <code>Filter</code> used to replace template variables.
	/// When serialization and deserialization are performed the source is
	/// required as it acts as a factory for objects used in the process.
	/// <p>
	/// For serialization the source object is required as a factory for
	/// <code>Schema</code> objects, which are used to visit each field 
	/// in the class that can be serialized. Also this can be used to get
	/// any data entered into the session <code>Map</code> object.
	/// <p>
	/// When deserializing the source object provides the contextual data
	/// used to replace template variables extracted from the XML source.
	/// This is performed using the <code>Filter</code> object. Also, as 
	/// in serialization it acts as a factory for the <code>Schema</code> 
	/// objects used to examine the serializable fields of an object.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Source : Context
	{
		/// <summary> This is used to determine if the deserialization mode is strict
		/// or not. If this is not strict then deserialization will be done
		/// in such a way that additional elements and attributes can be
		/// ignored. This allows external XML formats to be used without 
		/// having to match the object structure to the XML fully.
		/// 
		/// </summary>
		/// <returns> this returns true if the deserialization is strict
		/// </returns>
		virtual public bool Strict
		{
			get
			{
				return strict;
			}
			
		}
		/// <summary> This is used to acquire the <code>Session</code> object that 
		/// is used to store the values used within the serialization
		/// process. This provides the internal map that is passed to all
		/// of the call back methods so that is can be populated.
		/// 
		/// </summary>
		/// <returns> this returns the session that is used by this source
		/// </returns>
		virtual public Session Session
		{
			get
			{
				return session;
			}
			
		}
		/// <summary> This is used to acquire the <code>Support</code> object.
		/// The support object is used to translate strings to and from
		/// their object representations and is also used to convert the
		/// strings to their template values. This is the single source 
		/// of translation for all of the strings encountered.
		/// 
		/// </summary>
		/// <returns> this returns the support used by the context
		/// </returns>
		virtual public Support Support
		{
			get
			{
				return support;
			}
			
		}
		/// <summary> This is used to acquire the <code>Style</code> for the format.
		/// If no style has been set a default style is used, which does 
		/// not modify the attributes and elements that are used to build
		/// the resulting XML document.
		/// 
		/// </summary>
		/// <returns> this returns the style used for this format object
		/// </returns>
		virtual public Style Style
		{
			get
			{
				if (style == null)
				{
					style = new DefaultStyle();
				}
				return style;
			}
			
		}
		
		/// <summary> This is used to replace variables within the XML source.</summary>
		private TemplateEngine engine;
		
		/// <summary> This is the strategy used to resolve the element classes.</summary>
		private Strategy strategy;
		
		/// <summary> This support is used to convert the strings encountered.</summary>
		private Support support;
		
		/// <summary> This is used to store the source context attribute values.</summary>
		private Session session;
		
		/// <summary> This is the filter used by this object for templating.</summary>
		private Filter filter;
		
		/// <summary> This is the style that is used by this source instance.</summary>
		private Style style;
		
		/// <summary> This is used to determine whether the read should be strict.</summary>
		private bool strict;
		
		/// <summary> Constructor for the <code>Source</code> object. This is used to
		/// maintain a context during the serialization process. It holds 
		/// the <code>Strategy</code> and <code>Context</code> used in the
		/// serialization process. The same source instance is used for 
		/// each XML element evaluated in a the serialization process. 
		/// 
		/// </summary>
		/// <param name="strategy">this is used to resolve the classes used   
		/// </param>
		/// <param name="support">this is the context used to process strings
		/// </param>
		/// <param name="style">this is the style used for the serialization
		/// </param>
		public Source(Strategy strategy, Support support, Style style):this(strategy, support, style, true)
		{
		}
		
		/// <summary> Constructor for the <code>Source</code> object. This is used to
		/// maintain a context during the serialization process. It holds 
		/// the <code>Strategy</code> and <code>Context</code> used in the
		/// serialization process. The same source instance is used for 
		/// each XML element evaluated in a the serialization process. 
		/// 
		/// </summary>
		/// <param name="strategy">this is used to resolve the classes used   
		/// </param>
		/// <param name="support">this is the context used to process strings
		/// </param>
		/// <param name="style">this is the style used for the serialization
		/// </param>
		/// <param name="strict">this determines whether to read in strict mode
		/// </param>
		public Source(Strategy strategy, Support support, Style style, bool strict)
		{
			this.filter = new TemplateFilter(this, support);
			this.engine = new TemplateEngine(filter);
			this.session = new Session();
			this.strategy = strategy;
			this.support = support;
			this.strict = strict;
			this.style = style;
		}
		
		/// <summary> This is used to determine if the type specified is a floating
		/// point type. Types that are floating point are the double and
		/// float primitives as well as the java types for this primitives.
		/// 
		/// </summary>
		/// <param name="type">this is the type to determine if it is a float
		/// 
		/// </param>
		/// <returns> this returns true if the type is a floating point
		/// </returns>
		public virtual bool isFloat(System.Type type)
		{
			return support.isFloat(type);
		}
		
		/// <summary> This is used to determine if the type specified is a floating
		/// point type. Types that are floating point are the double and
		/// float primitives as well as the java types for this primitives.
		/// 
		/// </summary>
		/// <param name="type">this is the type to determine if it is a float
		/// 
		/// </param>
		/// <returns> this returns true if the type is a floating point
		/// </returns>
		public virtual bool isFloat(Type type)
		{
			return isFloat(type.getType());
		}
		
		/// <summary> This is used to determine whether the scanned class represents
		/// a primitive type. A primitive type is a type that contains no
		/// XML annotations and so cannot be serialized with an XML form.
		/// Instead primitives a serialized using transformations.
		/// 
		/// </summary>
		/// <param name="type">this is the type to determine if it is primitive
		/// 
		/// </param>
		/// <returns> this returns true if no XML annotations were found
		/// </returns>
		public virtual bool isPrimitive(System.Type type)
		{
			return support.isPrimitive(type);
		}
		
		/// <summary> This is used to determine whether the scanned type represents
		/// a primitive type. A primitive type is a type that contains no
		/// XML annotations and so cannot be serialized with an XML form.
		/// Instead primitives a serialized using transformations.
		/// 
		/// </summary>
		/// <param name="type">this is the type to determine if it is primitive
		/// 
		/// </param>
		/// <returns> this returns true if no XML annotations were found
		/// </returns>
		public virtual bool isPrimitive(Type type)
		{
			return isPrimitive(type.getType());
		}
		
		/// <summary> This will create an <code>Instance</code> that can be used
		/// to instantiate objects of the specified class. This leverages
		/// an internal constructor cache to ensure creation is quicker.
		/// 
		/// </summary>
		/// <param name="type">this is the type that is to be instantiated
		/// 
		/// </param>
		/// <returns> this will return an object for instantiating objects
		/// </returns>
		public virtual Instance getInstance(System.Type type)
		{
			return support.getInstance(type);
		}
		
		/// <summary> This will create an <code>Instance</code> that can be used
		/// to instantiate objects of the specified class. This leverages
		/// an internal constructor cache to ensure creation is quicker.
		/// 
		/// </summary>
		/// <param name="value">this contains information on the object instance
		/// 
		/// </param>
		/// <returns> this will return an object for instantiating objects
		/// </returns>
		public virtual Instance getInstance(Value value_Renamed)
		{
			return support.getInstance(value_Renamed);
		}
		
		/// <summary> This is used to acquire the name of the specified type using
		/// the <code>Root</code> annotation for the class. This will 
		/// use either the name explicitly provided by the annotation or
		/// it will use the name of the class that the annotation was
		/// placed on if there is no explicit name for the root.
		/// 
		/// </summary>
		/// <param name="type">this is the type to acquire the root name for
		/// 
		/// </param>
		/// <returns> this returns the name of the type from the root
		/// </returns>
		public virtual System.String getName(System.Type type)
		{
			return support.getName(type);
		}
		
		/// <summary> This returns the version for the type specified. The version is
		/// used to determine how the deserialization process is performed.
		/// If the version of the type is different from the version for
		/// the XML document, then deserialization is done in a best effort.
		/// 
		/// </summary>
		/// <param name="type">this is the type to acquire the version for
		/// 
		/// </param>
		/// <returns> the version that has been set for this XML schema class
		/// </returns>
		public virtual Version getVersion(System.Type type)
		{
			return getScanner(type).Revision;
		}
		
		/// <summary> This creates a <code>Scanner</code> object that can be used to
		/// examine the fields within the XML class schema. The scanner
		/// maintains information when a field from within the scanner is
		/// visited, this allows the serialization and deserialization
		/// process to determine if all required XML annotations are used.
		/// 
		/// </summary>
		/// <param name="type">the schema class the scanner is created for
		/// 
		/// </param>
		/// <returns> a scanner that can maintains information on the type
		/// 
		/// </returns>
		/// <throws>  Exception if the class contains an illegal schema  </throws>
		private Scanner getScanner(System.Type type)
		{
			return support.getScanner(type);
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
		public virtual Decorator getDecorator(System.Type type)
		{
			return getScanner(type).Decorator;
		}
		
		/// <summary> This is used to acquire the <code>Caller</code> object. This
		/// is used to call the callback methods within the object. If the
		/// object contains no callback methods then this will return an
		/// object that does not invoke any methods that are invoked. 
		/// 
		/// </summary>
		/// <param name="type">this is the type to acquire the caller for
		/// 
		/// </param>
		/// <returns> this returns the caller for the specified type
		/// </returns>
		public virtual Caller getCaller(System.Type type)
		{
			return getScanner(type).getCaller(this);
		}
		
		/// <summary> This creates a <code>Schema</code> object that can be used to
		/// examine the fields within the XML class schema. The schema
		/// maintains information when a field from within the schema is
		/// visited, this allows the serialization and deserialization
		/// process to determine if all required XML annotations are used.
		/// 
		/// </summary>
		/// <param name="type">the schema class the schema is created for
		/// 
		/// </param>
		/// <returns> a new schema that can track visits within the schema
		/// 
		/// </returns>
		/// <throws>  Exception if the class contains an illegal schema  </throws>
		public virtual Schema getSchema(System.Type type)
		{
			Scanner schema = getScanner(type);
			
			if (schema == null)
			{
				throw new PersistenceException("Invalid schema class %s", type);
			}
			return new ClassSchema(schema, this);
		}
		
		/// <summary> This is used to acquire the attribute mapped to the specified
		/// key. In order for this to return a value it must have been
		/// previously placed into the context as it is empty by default.
		/// 
		/// </summary>
		/// <param name="key">this is the name of the attribute to retrieve
		/// 
		/// </param>
		/// <returns> this returns the value mapped to the specified key
		/// </returns>
		public virtual System.Object getAttribute(System.Object key)
		{
			return session[key];
		}
		
		/// <summary> This is used to resolve and load a class for the given element.
		/// The class should be of the same type or a subclass of the class
		/// specified. It can be resolved using the details within the
		/// provided XML element, if the details used do not represent any
		/// serializable values they should be removed so as not to disrupt
		/// the deserialization process. For example the default strategy
		/// removes all "class" attributes from the given elements.
		/// 
		/// </summary>
		/// <param name="type">this is the type of the root element expected
		/// </param>
		/// <param name="node">this is the element used to resolve an override
		/// 
		/// </param>
		/// <returns> returns the type that should be used for the object
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the class cannot be resolved   </throws>
		public virtual Value getOverride(Type type, InputNode node)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			if (map == null)
			{
				throw new PersistenceException("No attributes for %s", node);
			}
			return strategy.read(type, map, session);
		}
		
		/// <summary> This is used to attach elements or attributes to the given 
		/// element during the serialization process. This method allows
		/// the strategy to augment the XML document so that it can be
		/// deserialized using a similar strategy. For example the 
		/// default strategy adds a "class" attribute to the element.
		/// 
		/// </summary>
		/// <param name="type">this is the field type for the associated value 
		/// </param>
		/// <param name="value">this is the instance variable being serialized
		/// </param>
		/// <param name="node">this is the element used to represent the value
		/// 
		/// </param>
		/// <returns> this returns true if serialization has complete
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the details cannot be set </throws>
		public virtual bool setOverride(Type type, System.Object value_Renamed, OutputNode node)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			if (map == null)
			{
				throw new PersistenceException("No attributes for %s", node);
			}
			return strategy.write(type, value_Renamed, map, session);
		}
		
		/// <summary> Replaces any template variables within the provided string. 
		/// This is used in the deserialization process to replace 
		/// variables with system properties, environment variables, or
		/// used specified mappings. If a template variable does not have
		/// a mapping from the <code>Filter</code> is is left unchanged.  
		/// 
		/// </summary>
		/// <param name="text">this is processed by the template engine object
		/// 
		/// </param>
		/// <returns> this returns the text will all variables replaced
		/// </returns>
		public virtual System.String getProperty(System.String text)
		{
			return engine.process(text);
		}
	}
}