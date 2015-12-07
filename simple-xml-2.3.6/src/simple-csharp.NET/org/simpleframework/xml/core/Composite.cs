/*
* Composite.java July 2006
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
using Type = org.simpleframework.xml.strategy.Type;
using InputNode = org.simpleframework.xml.stream.InputNode;
//UPGRADE_TODO: The type 'org.simpleframework.xml.stream.NodeMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using NodeMap = org.simpleframework.xml.stream.NodeMap;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
using Position = org.simpleframework.xml.stream.Position;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Composite</code> object is used to perform serialization
	/// of objects that contain XML annotation. Composite objects are objects
	/// that are not primitive and contain references to serializable fields.
	/// This <code>Converter</code> will visit each field within the object
	/// and deserialize or serialize that field depending on the requested
	/// action. If a required field is not present when deserializing from
	/// an XML element this terminates the deserialization reports the error.
	/// <pre>
	/// 
	/// &lt;element name="test" class="some.package.Type"&gt;
	/// &lt;text&gt;string value&lt;/text&gt;
	/// &lt;integer&gt;1234&lt;/integer&gt;
	/// &lt;/element&gt;
	/// 
	/// </pre>
	/// To deserialize the above XML source this will attempt to match the
	/// attribute name with an <code>Attribute</code> annotation from the
	/// XML schema class, which is specified as "some.package.Type". This
	/// type must also contain <code>Element</code> annotations for the
	/// "text" and "integer" elements.
	/// <p>
	/// Serialization requires that contacts marked as required must have
	/// values that are not null. This ensures that the serialized object
	/// can be deserialized at a later stage using the same class schema.
	/// If a required value is null the serialization terminates and an
	/// exception is thrown.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Composite : Converter
	{
		
		/// <summary> This factory creates instances of the deserialized object.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ObjectFactory factory;
		
		/// <summary> This is used to convert any primitive values that are needed.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'primitive '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Primitive primitive;
		
		/// <summary> This is used to store objects so that they can be read again.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'criteria '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Criteria criteria;
		
		/// <summary> This is the current revision of this composite converter.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'revision '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Revision revision;
		
		/// <summary> This is the source object for the instance of serialization.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'context '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Context context;
		
		/// <summary> This is the type that this composite produces instances of.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Type type;
		
		/// <summary> Constructor for the <code>Composite</code> object. This creates 
		/// a converter object capable of serializing and deserializing root
		/// objects labeled with XML annotations. The XML schema class must 
		/// be given to the instance in order to perform deserialization.
		/// 
		/// </summary>
		/// <param name="context">the source object used to perform serialization
		/// </param>
		/// <param name="type">this is the XML schema type to use for this
		/// </param>
		public Composite(Context context, Type type)
		{
			this.factory = new ObjectFactory(context, type);
			this.primitive = new Primitive(context, type);
			this.criteria = new Collector(context);
			this.revision = new Revision();
			this.context = context;
			this.type = type;
		}
		
		/// <summary> This <code>read</code> method performs deserialization of the XML
		/// schema class type by traversing the contacts and instantiating them
		/// using details from the provided XML element. Because this will
		/// convert a non-primitive value it delegates to other converters to
		/// perform deserialization of lists and primitives.
		/// <p>
		/// If any of the required contacts are not present within the provided
		/// XML element this will terminate deserialization and throw an
		/// exception. The annotation missing is reported in the exception.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// 
		/// </param>
		/// <returns> this returns the fully deserialized object graph
		/// </returns>
		public virtual System.Object read(InputNode node)
		{
			Instance value_Renamed = factory.getInstance(node);
			System.Type type = value_Renamed.Type;
			
			if (value_Renamed.Reference)
			{
				return value_Renamed.getInstance();
			}
			if (context.isPrimitive(type))
			{
				return readPrimitive(node, value_Renamed);
			}
			return read(node, value_Renamed, type);
		}
		
		/// <summary> This <code>read</code> method performs deserialization of the XML
		/// schema class type by traversing the contacts and instantiating them
		/// using details from the provided XML element. Because this will
		/// convert a non-primitive value it delegates to other converters to
		/// perform deserialization of lists and primitives.
		/// <p>
		/// If any of the required contacts are not present within the provided
		/// XML element this will terminate deserialization and throw an
		/// exception. The annotation missing is reported in the exception.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// </param>
		/// <param name="source">the object whose contacts are to be deserialized
		/// 
		/// </param>
		/// <returns> this returns the fully deserialized object graph 
		/// </returns>
		public virtual System.Object read(InputNode node, System.Object source)
		{
			System.Type type = source.GetType();
			Schema schema = context.getSchema(type);
			Caller caller = schema.Caller;
			
			read(node, source, schema);
			criteria.commit(source);
			caller.validate(source);
			caller.commit(source);
			
			return readResolve(node, source, caller);
		}
		
		/// <summary> This <code>read</code> method performs deserialization of the XML
		/// schema class type by traversing the contacts and instantiating them
		/// using details from the provided XML element. Because this will
		/// convert a non-primitive value it delegates to other converters to
		/// perform deserialization of lists and primitives.
		/// <p>
		/// If any of the required contacts are not present within the provided
		/// XML element this will terminate deserialization and throw an
		/// exception. The annotation missing is reported in the exception.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// </param>
		/// <param name="value">this is the instance for the object within the graph
		/// </param>
		/// <param name="real">this is the real type that is to be evaluated
		/// 
		/// </param>
		/// <returns> this returns the fully deserialized object graph
		/// </returns>
		private System.Object read(InputNode node, Instance value_Renamed, System.Type real)
		{
			Schema schema = context.getSchema(real);
			Caller caller = schema.Caller;
			System.Object source = read(node, schema, value_Renamed);
			
			caller.validate(source);
			caller.commit(source);
			value_Renamed.setInstance(source);
			
			return readResolve(node, source, caller);
		}
		
		/// <summary> This <code>read</code> method performs deserialization of the XML
		/// schema class type by traversing the contacts and instantiating them
		/// using details from the provided XML element. Because this will
		/// convert a non-primitive value it delegates to other converters to
		/// perform deserialization of lists and primitives.
		/// <p>
		/// If any of the required contacts are not present within the provided
		/// XML element this will terminate deserialization and throw an
		/// exception. The annotation missing is reported in the exception.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// </param>
		/// <param name="schema">this is the schema for the class to be deserialized
		/// </param>
		/// <param name="value">this is the value used for the deserialization
		/// 
		/// </param>
		/// <returns> this returns the fully deserialized object graph
		/// </returns>
		private System.Object read(InputNode node, Schema schema, Instance value_Renamed)
		{
			Creator creator = schema.Creator;
			
			if (creator.Default)
			{
				return readDefault(node, schema, value_Renamed);
			}
			else
			{
				read(node, (System.Object) null, schema);
			}
			return readConstructor(node, schema, value_Renamed);
		}
		
		/// <summary> This <code>readDefault</code> method performs deserialization of the 
		/// XM schema class type by traversing the contacts and instantiating 
		/// them using details from the provided XML element. Because this will
		/// convert a non-primitive value it delegates to other converters to
		/// perform deserialization of lists and primitives.
		/// <p>
		/// This takes the approach that the object is instantiated first and
		/// then the annotated fields and methods are deserialized from the XML
		/// elements and attributes. When all the details have be deserialized
		/// they are set on the internal contacts of the object.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// </param>
		/// <param name="schema">this is the schema for the class to be deserialized
		/// </param>
		/// <param name="value">this is the value used for the deserialization
		/// 
		/// </param>
		/// <returns> this returns the fully deserialized object graph
		/// </returns>
		private System.Object readDefault(InputNode node, Schema schema, Instance value_Renamed)
		{
			System.Object source = value_Renamed.getInstance();
			
			if (value_Renamed != null)
			{
				value_Renamed.setInstance(source);
				read(node, source, schema);
				criteria.commit(source);
			}
			return source;
		}
		
		/// <summary> This <code>readConstructor</code> method performs deserialization of 
		/// the XML schema class type by traversing the contacts and creating 
		/// them using details from the provided XML element. Because this will
		/// convert a non-primitive value it delegates to other converters to
		/// perform deserialization of lists and primitives.
		/// <p>
		/// This takes the approach of reading the XML elements and attributes
		/// before instantiating the object. Instantiation is performed using a
		/// declared constructor. The parameters for the constructor are taken
		/// from the deserialized objects.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// </param>
		/// <param name="schema">this is the schema for the class to be deserialized
		/// </param>
		/// <param name="value">this is the value used for the deserialization
		/// 
		/// </param>
		/// <returns> this returns the fully deserialized object graph
		/// </returns>
		private System.Object readConstructor(InputNode node, Schema schema, Instance value_Renamed)
		{
			Creator creator = schema.Creator;
			System.Object source = creator.getInstance(criteria);
			
			if (value_Renamed != null)
			{
				value_Renamed.setInstance(source);
				criteria.commit(source);
			}
			return source;
		}
		
		/// <summary> This <code>readPrimitive</code> method will extract the text value
		/// from the node and replace any template variables before converting
		/// it to a primitive value. This uses a <code>Primitive</code> object
		/// to convert the node text to the resulting string. This will also
		/// respect all references on the node so cycle can be followed.
		/// 
		/// </summary>
		/// <param name="node">this is the node to be converted to a primitive
		/// </param>
		/// <param name="value">this is the type for the object within the graph
		/// 
		/// </param>
		/// <returns> this returns the primitive that has been deserialized
		/// </returns>
		private System.Object readPrimitive(InputNode node, Instance value_Renamed)
		{
			System.Type type = value_Renamed.Type;
			System.Object result = primitive.read(node, type);
			
			if (type != null)
			{
				value_Renamed.setInstance(result);
			}
			return result;
		}
		
		/// <summary> The <code>readResolve</code> method is used to determine if there 
		/// is a resolution method which can be used to substitute the object
		/// deserialized. The resolve method is used when an object wishes 
		/// to provide a substitute within the deserialized object graph.
		/// This acts as an equivalent to the Java Object Serialization
		/// <code>readResolve</code> method for the object deserialization.
		/// 
		/// </summary>
		/// <param name="node">the XML element object provided as a replacement
		/// </param>
		/// <param name="source">the type of the object that is being deserialized
		/// </param>
		/// <param name="caller">this is used to invoke the callback methods
		/// 
		/// </param>
		/// <returns> this returns a replacement for the deserialized object
		/// </returns>
		private System.Object readResolve(InputNode node, System.Object source, Caller caller)
		{
			if (source != null)
			{
				Position line = node.Position;
				System.Object value_Renamed = caller.resolve(source);
				System.Type expect = type.getType();
				System.Type real = value_Renamed.GetType();
				
				if (!expect.IsAssignableFrom(real))
				{
					throw new ElementException("Type %s does not match %s at %s", real, expect, line);
				}
				return value_Renamed;
			}
			return source;
		}
		
		/// <summary> This <code>read</code> method performs deserialization of the XML
		/// schema class type by traversing the contacts and instantiating them
		/// using details from the provided XML element. Because this will
		/// convert a non-primitive value it delegates to other converters to
		/// perform deserialization of lists and primitives.
		/// <p>
		/// If any of the required contacts are not present within the provided
		/// XML element this will terminate deserialization and throw an
		/// exception. The annotation missing is reported in the exception.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// </param>
		/// <param name="source">this type of the object that is to be deserialized
		/// </param>
		/// <param name="schema">this object visits the objects contacts
		/// </param>
		private void  read(InputNode node, System.Object source, Schema schema)
		{
			readVersion(node, source, schema);
			readText(node, source, schema);
			readAttributes(node, source, schema);
			readElements(node, source, schema);
		}
		
		/// <summary> This method is used to read the version from the provided input
		/// node. Once the version has been read it is used to determine how
		/// to deserialize the object. If the version is not the initial
		/// version then it is read in a manner that ignores excessive XML
		/// elements and attributes. Also none of the annotated fields or
		/// methods are required if the version is not the initial version.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// </param>
		/// <param name="source">this object whose contacts are to be deserialized
		/// </param>
		/// <param name="schema">this object visits the objects contacts
		/// </param>
		private void  readVersion(InputNode node, System.Object source, Schema schema)
		{
			Label label = schema.Version;
			System.Type expect = type.getType();
			
			if (label != null)
			{
				System.String name = label.getName();
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				InputNode value_Renamed = map.remove(name);
				
				if (value_Renamed != null)
				{
					readVersion(value_Renamed, source, label);
				}
				else
				{
					Version version = context.getVersion(expect);
					System.Double start = revision.Default;
					System.Double expected = version.revision();
					
					criteria.set_Renamed(label, (System.Object) start);
					revision.compare((System.Object) expected, (System.Object) start);
				}
			}
		}
		
		/// <summary> This method is used to read the version from the provided input
		/// node. Once the version has been read it is used to determine how
		/// to deserialize the object. If the version is not the initial
		/// version then it is read in a manner that ignores excessive XML
		/// elements and attributes. Also none of the annotated fields or
		/// methods are required if the version is not the initial version.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are deserialized from
		/// </param>
		/// <param name="source">the type of the object that is being deserialized
		/// </param>
		/// <param name="label">this is the label used to read the version attribute
		/// </param>
		private void  readVersion(InputNode node, System.Object source, Label label)
		{
			System.Object value_Renamed = read(node, source, label);
			System.Type expect = type.getType();
			
			if (value_Renamed != null)
			{
				Version version = context.getVersion(expect);
				System.Double actual = version.revision();
				
				if (!value_Renamed.Equals(revision))
				{
					revision.compare((System.Object) actual, value_Renamed);
				}
			}
		}
		
		/// <summary> This <code>readAttributes</code> method reads the attributes from
		/// the provided XML element. This will iterate over all attributes
		/// within the element and convert those attributes as primitives to
		/// contact values within the source object.
		/// <p>
		/// Once all attributes within the XML element have been evaluated
		/// the <code>Schema</code> is checked to ensure that there are no
		/// required contacts annotated with the <code>Attribute</code> that
		/// remain. If any required attribute remains an exception is thrown. 
		/// 
		/// </summary>
		/// <param name="node">this is the XML element to be evaluated
		/// </param>
		/// <param name="source">the type of the object that is being deserialized
		/// </param>
		/// <param name="schema">this is used to visit the attribute contacts
		/// 
		/// </param>
		/// <throws>  Exception thrown if any required attributes remain </throws>
		private void  readAttributes(InputNode node, System.Object source, Schema schema)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			LabelMap map = schema.Attributes;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String name: list)
			{
				readAttribute(node.getAttribute(name), source, map);
			}
			validate(node, map, source);
		}
		
		/// <summary> This <code>readElements</code> method reads the elements from 
		/// the provided XML element. This will iterate over all elements
		/// within the element and convert those elements to primitives or
		/// composite objects depending on the contact annotation.
		/// <p>
		/// Once all elements within the XML element have been evaluated
		/// the <code>Schema</code> is checked to ensure that there are no
		/// required contacts annotated with the <code>Element</code> that
		/// remain. If any required element remains an exception is thrown. 
		/// 
		/// </summary>
		/// <param name="node">this is the XML element to be evaluated
		/// </param>
		/// <param name="source">the type of the object that is being deserialized
		/// </param>
		/// <param name="schema">this is used to visit the element contacts
		/// 
		/// </param>
		/// <throws>  Exception thrown if any required elements remain </throws>
		private void  readElements(InputNode node, System.Object source, Schema schema)
		{
			LabelMap map = schema.Elements;
			
			while (true)
			{
				InputNode child = node.getNext();
				
				if (child == null)
				{
					break;
				}
				readElement(child, source, map);
			}
			validate(node, map, source);
		}
		
		/// <summary> This <code>readText</code> method is used to read the text value
		/// from the XML element node specified. This will check the class
		/// schema to determine if a <code>Text</code> annotation was
		/// specified. If one was specified then the text within the XML
		/// element input node is used to populate the contact value.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element to acquire the text from
		/// </param>
		/// <param name="source">the type of the object that is being deserialized
		/// </param>
		/// <param name="schema">this is used to visit the element contacts
		/// 
		/// </param>
		/// <throws>  Exception thrown if a required text value was null </throws>
		private void  readText(InputNode node, System.Object source, Schema schema)
		{
			Label label = schema.Text;
			
			if (label != null)
			{
				read(node, source, label);
			}
		}
		
		/// <summary> This <code>readAttribute</code> method is used for deserialization
		/// of the provided node object using a delegate converter. This is
		/// typically another <code>Composite</code> converter, or if the
		/// node is an attribute a <code>Primitive</code> converter. When
		/// the delegate converter has completed the deserialized value is
		/// assigned to the contact.
		/// 
		/// </summary>
		/// <param name="node">this is the node that contains the contact value
		/// </param>
		/// <param name="source">the type of the object that is being deserialized
		/// </param>
		/// <param name="map">this is the map that contains the label objects
		/// 
		/// </param>
		/// <throws>  Exception thrown if the the label object does not exist </throws>
		private void  readAttribute(InputNode node, System.Object source, LabelMap map)
		{
			System.String name = node.Name;
			Label label = map.take(name);
			
			if (label == null)
			{
				Position line = node.Position;
				System.Type type = source.GetType();
				
				if (map.isStrict(context) && revision.Equal)
				{
					throw new AttributeException("Attribute '%s' does not have a match in %s at %s", name, type, line);
				}
			}
			else
			{
				read(node, source, label);
			}
		}
		
		/// <summary> This <code>readElement</code> method is used for deserialization
		/// of the provided node object using a delegate converter. This is
		/// typically another <code>Composite</code> converter, or if the
		/// node is an attribute a <code>Primitive</code> converter. When
		/// the delegate converter has completed the deserialized value is
		/// assigned to the contact.
		/// 
		/// </summary>
		/// <param name="node">this is the node that contains the contact value
		/// </param>
		/// <param name="source">the type of the object that is being deserialized
		/// </param>
		/// <param name="map">this is the map that contains the label objects
		/// 
		/// </param>
		/// <throws>  Exception thrown if the the label object does not exist </throws>
		private void  readElement(InputNode node, System.Object source, LabelMap map)
		{
			System.String name = node.Name;
			Label label = map.take(name);
			
			if (label == null)
			{
				label = criteria.get_Renamed(name);
			}
			if (label == null)
			{
				Position line = node.Position;
				System.Type type = source.GetType();
				
				if (map.isStrict(context) && revision.Equal)
				{
					throw new ElementException("Element '%s' does not have a match in %s at %s", name, type, line);
				}
				else
				{
					node.skip();
				}
			}
			else
			{
				read(node, source, label);
			}
		}
		
		/// <summary> This <code>read</code> method is used to perform deserialization
		/// of the provided node object using a delegate converter. This is
		/// typically another <code>Composite</code> converter, or if the
		/// node is an attribute a <code>Primitive</code> converter. When
		/// the delegate converter has completed the deserialized value is
		/// assigned to the contact.
		/// 
		/// </summary>
		/// <param name="node">this is the node that contains the contact value
		/// </param>
		/// <param name="source">the type of the object that is being deserialized
		/// </param>
		/// <param name="label">this is the label used to create the converter
		/// 
		/// </param>
		/// <throws>  Exception thrown if the contact could not be deserialized </throws>
		private System.Object read(InputNode node, System.Object source, Label label)
		{
			System.Object object_Renamed = readObject(node, source, label);
			
			if (object_Renamed == null)
			{
				Position line = node.Position;
				System.Type expect = type.getType();
				
				if (source != null)
				{
					expect = source.GetType();
				}
				if (label.Required && revision.Equal)
				{
					throw new ValueRequiredException("Empty value for %s in %s at %s", label, expect, line);
				}
			}
			else
			{
				if (object_Renamed != label.getEmpty(context))
				{
					criteria.set_Renamed(label, object_Renamed);
				}
			}
			return object_Renamed;
		}
		
		/// <summary> This <code>readObject</code> method is used to perform the
		/// deserialization of the XML in to any original value. If there
		/// is no original value then this will do a read and instantiate
		/// a new value to deserialize in to. Reading in to the original
		/// ensures that existing lists or maps can be read in to.
		/// 
		/// </summary>
		/// <param name="node">this is the node that contains the contact value
		/// </param>
		/// <param name="source">the source object to assign the contact value to
		/// </param>
		/// <param name="label">this is the label used to create the converter
		/// 
		/// </param>
		/// <returns> this returns the original value deserialized in to
		/// 
		/// </returns>
		/// <throws>  Exception thrown if the contact could not be deserialized </throws>
		private System.Object readObject(InputNode node, System.Object source, Label label)
		{
			Converter reader = label.getConverter(context);
			System.String name = label.getName(context);
			
			if (label.Collection)
			{
				Variable variable = criteria.get_Renamed(name);
				Contact contact = label.Contact;
				
				if (variable != null)
				{
					System.Object value_Renamed = variable.Value;
					
					return reader.read(node, value_Renamed);
				}
				else
				{
					if (source != null)
					{
						System.Object value_Renamed = contact.get_Renamed(source);
						
						if (value_Renamed != null)
						{
							return reader.read(node, value_Renamed);
						}
					}
				}
			}
			return reader.read(node);
		}
		
		/// <summary> This method checks to see if there are any <code>Label</code>
		/// objects remaining in the provided map that are required. This is
		/// used when deserialization is performed to ensure the the XML
		/// element deserialized contains sufficient details to satisfy the
		/// XML schema class annotations. If there is a required label that
		/// remains it is reported within the exception thrown.
		/// 
		/// </summary>
		/// <param name="map">this is the map to check for remaining labels
		/// </param>
		/// <param name="source">this is the object that has been deserialized 
		/// 
		/// </param>
		/// <throws>  Exception thrown if an XML property was not declared </throws>
		private void  validate(InputNode node, LabelMap map, System.Object source)
		{
			Position line = node.Position;
			System.Type expect = type.getType();
			
			if (source != null)
			{
				expect = source.GetType();
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Label label: map)
			{
				if (label.isRequired() && revision.Equal)
				{
					throw new ValueRequiredException("Unable to satisfy %s for %s at %s", label, expect, line);
				}
				System.Object value_Renamed = label.getEmpty(context);
				
				if (value_Renamed != null)
				{
					criteria.set_Renamed(label, value_Renamed);
				}
			}
		}
		
		/// <summary> This <code>validate</code> method performs validation of the XML
		/// schema class type by traversing the contacts and validating them
		/// using details from the provided XML element. Because this will
		/// validate a non-primitive value it delegates to other converters 
		/// to perform validation of lists, maps, and primitives.
		/// <p>
		/// If any of the required contacts are not present within the given
		/// XML element this will terminate validation and throw an exception
		/// The annotation missing is reported in the exception.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are validated from
		/// 
		/// </param>
		/// <returns> true if the XML element matches the XML schema class given 
		/// </returns>
		public virtual bool validate(InputNode node)
		{
			Instance value_Renamed = factory.getInstance(node);
			
			if (!value_Renamed.Reference)
			{
				System.Object result = value_Renamed.setInstance((System.Object) null);
				System.Type type = value_Renamed.Type;
				
				return validate(node, type);
			}
			return true;
		}
		
		/// <summary> This <code>validate</code> method performs validation of the XML
		/// schema class type by traversing the contacts and validating them
		/// using details from the provided XML element. Because this will
		/// validate a non-primitive value it delegates to other converters 
		/// to perform validation of lists, maps, and primitives.
		/// <p>
		/// If any of the required contacts are not present within the given
		/// XML element this will terminate validation and throw an exception
		/// The annotation missing is reported in the exception.
		/// 
		/// </summary>
		/// <param name="node">the XML element contact values are validated from
		/// </param>
		/// <param name="type">this is the type to validate against the input node
		/// </param>
		private bool validate(InputNode node, System.Type type)
		{
			Schema schema = context.getSchema(type);
			
			validateText(node, schema);
			validateAttributes(node, schema);
			validateElements(node, schema);
			
			return node.Element;
		}
		
		/// <summary> This <code>validateAttributes</code> method validates the attributes 
		/// from the provided XML element. This will iterate over all attributes
		/// within the element and validate those attributes as primitives to
		/// contact values within the source object.
		/// <p>
		/// Once all attributes within the XML element have been evaluated the
		/// <code>Schema</code> is checked to ensure that there are no required 
		/// contacts annotated with the <code>Attribute</code> that remain. If 
		/// any required attribute remains an exception is thrown. 
		/// 
		/// </summary>
		/// <param name="node">this is the XML element to be validated
		/// </param>
		/// <param name="schema">this is used to visit the attribute contacts
		/// 
		/// </param>
		/// <throws>  Exception thrown if any required attributes remain </throws>
		private void  validateAttributes(InputNode node, Schema schema)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			LabelMap map = schema.Attributes;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String name: list)
			{
				validateAttribute(node.getAttribute(name), map);
			}
			validate(node, map);
		}
		
		/// <summary> This <code>validateElements</code> method validates the elements 
		/// from the provided XML element. This will iterate over all elements
		/// within the element and validate those elements as primitives or
		/// composite objects depending on the contact annotation.
		/// <p>
		/// Once all elements within the XML element have been evaluated
		/// the <code>Schema</code> is checked to ensure that there are no
		/// required contacts annotated with the <code>Element</code> that
		/// remain. If any required element remains an exception is thrown.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element to be evaluated
		/// </param>
		/// <param name="schema">this is used to visit the element contacts
		/// 
		/// </param>
		/// <throws>  Exception thrown if any required elements remain </throws>
		private void  validateElements(InputNode node, Schema schema)
		{
			LabelMap map = schema.Elements;
			
			while (true)
			{
				InputNode child = node.getNext();
				
				if (child == null)
				{
					break;
				}
				validateElement(child, map);
			}
			validate(node, map);
		}
		
		/// <summary> This <code>validateText</code> method validates the text value 
		/// from the XML element node specified. This will check the class
		/// schema to determine if a <code>Text</code> annotation was used. 
		/// If one was specified then the text within the XML element input 
		/// node is checked to determine if it is a valid entry.
		/// 
		/// </summary>
		/// <param name="node">this is the XML element to acquire the text from
		/// </param>
		/// <param name="schema">this is used to visit the element contacts
		/// 
		/// </param>
		/// <throws>  Exception thrown if a required text value was null </throws>
		private void  validateText(InputNode node, Schema schema)
		{
			Label label = schema.Text;
			
			if (label != null)
			{
				validate(node, label);
			}
		}
		
		/// <summary> This <code>validateAttribute</code> method performs a validation
		/// of the provided node object using a delegate converter. This is
		/// typically another <code>Composite</code> converter, or if the
		/// node is an attribute a <code>Primitive</code> converter. If this
		/// fails validation then an exception is thrown to report the issue.
		/// 
		/// </summary>
		/// <param name="node">this is the node that contains the contact value
		/// </param>
		/// <param name="map">this is the map that contains the label objects
		/// 
		/// </param>
		/// <throws>  Exception thrown if the the label object does not exist </throws>
		private void  validateAttribute(InputNode node, LabelMap map)
		{
			Position line = node.Position;
			System.String name = node.Name;
			Label label = map.take(name);
			
			if (label == null)
			{
				if (map.isStrict(context) && revision.Equal)
				{
					throw new AttributeException("Attribute '%s' does not exist at %s", name, line);
				}
			}
			else
			{
				validate(node, label);
			}
		}
		
		/// <summary> This <code>validateElement</code> method performs a validation
		/// of the provided node object using a delegate converter. This is
		/// typically another <code>Composite</code> converter, or if the
		/// node is an attribute a <code>Primitive</code> converter. If this
		/// fails validation then an exception is thrown to report the issue.
		/// 
		/// </summary>
		/// <param name="node">this is the node that contains the contact value
		/// </param>
		/// <param name="map">this is the map that contains the label objects
		/// 
		/// </param>
		/// <throws>  Exception thrown if the the label object does not exist </throws>
		private void  validateElement(InputNode node, LabelMap map)
		{
			System.String name = node.Name;
			Label label = map.take(name);
			
			if (label == null)
			{
				label = criteria.get_Renamed(name);
			}
			if (label == null)
			{
				Position line = node.Position;
				
				if (map.isStrict(context) && revision.Equal)
				{
					throw new ElementException("Element '%s' does not exist at %s", name, line);
				}
				else
				{
					node.skip();
				}
			}
			else
			{
				validate(node, label);
			}
		}
		
		/// <summary> This <code>validate</code> method is used to perform validation
		/// of the provided node object using a delegate converter. This is
		/// typically another <code>Composite</code> converter, or if the
		/// node is an attribute a <code>Primitive</code> converter. If this
		/// fails validation then an exception is thrown to report the issue.
		/// 
		/// </summary>
		/// <param name="node">this is the node that contains the contact value
		/// </param>
		/// <param name="label">this is the label used to create the converter
		/// 
		/// </param>
		/// <throws>  Exception thrown if the contact could not be deserialized </throws>
		private void  validate(InputNode node, Label label)
		{
			Converter reader = label.getConverter(context);
			Position line = node.Position;
			System.Type expect = type.getType();
			bool valid = reader.validate(node);
			
			if (valid == false)
			{
				throw new PersistenceException("Invalid value for %s in %s at %s", label, expect, line);
			}
			criteria.set_Renamed(label, (System.Object) null);
		}
		
		/// <summary> This method checks to see if there are any <code>Label</code>
		/// objects remaining in the provided map that are required. This is
		/// used when validation is performed to ensure the the XML element 
		/// validated contains sufficient details to satisfy the XML schema 
		/// class annotations. If there is a required label that remains it 
		/// is reported within the exception thrown.
		/// 
		/// </summary>
		/// <param name="node">this is the node that contains the composite data
		/// </param>
		/// <param name="map">this contains the converters to perform validation
		/// 
		/// </param>
		/// <throws>  Exception thrown if an XML property was not declared </throws>
		private void  validate(InputNode node, LabelMap map)
		{
			Position line = node.Position;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Label label: map)
			{
				if (label.isRequired() && revision.Equal)
				{
					throw new ValueRequiredException("Unable to satisfy %s at %s", label, line);
				}
			}
		}
		
		/// <summary> This <code>write</code> method is used to perform serialization of
		/// the given source object. Serialization is performed by appending
		/// elements and attributes from the source object to the provided XML
		/// element object. How the objects contacts are serialized is 
		/// determined by the XML schema class that the source object is an
		/// instance of. If a required contact is null an exception is thrown.
		/// 
		/// </summary>
		/// <param name="source">this is the source object to be serialized
		/// </param>
		/// <param name="node">the XML element the object is to be serialized to 
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		public virtual void  write(OutputNode node, System.Object source)
		{
			System.Type type = source.GetType();
			Schema schema = context.getSchema(type);
			Caller caller = schema.Caller;
			
			try
			{
				if (schema.Primitive)
				{
					primitive.write(node, source);
				}
				else
				{
					caller.persist(source);
					write(node, source, schema);
				}
			}
			finally
			{
				caller.complete(source);
			}
		}
		
		/// <summary> This <code>write</code> method is used to perform serialization of
		/// the given source object. Serialization is performed by appending
		/// elements and attributes from the source object to the provided XML
		/// element object. How the objects contacts are serialized is 
		/// determined by the XML schema class that the source object is an
		/// instance of. If a required contact is null an exception is thrown.
		/// 
		/// </summary>
		/// <param name="source">this is the source object to be serialized
		/// </param>
		/// <param name="node">the XML element the object is to be serialized to
		/// </param>
		/// <param name="schema">this is used to track the referenced contacts 
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  write(OutputNode node, System.Object source, Schema schema)
		{
			writeVersion(node, source, schema);
			writeAttributes(node, source, schema);
			writeElements(node, source, schema);
			writeText(node, source, schema);
		}
		
		/// <summary> This method is used to write the version attribute. A version is
		/// written only if it is not the initial version or if it required.
		/// The version is used to determine how to deserialize the XML. If
		/// the version is different from the expected version then it allows
		/// the object to be deserialized in a manner that does not require
		/// any attributes or elements, and unmatched nodes are ignored. 
		/// 
		/// </summary>
		/// <param name="node">this is the node to read the version attribute from
		/// </param>
		/// <param name="source">this is the source object that is to be written
		/// </param>
		/// <param name="schema">this is the schema that contains the version
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  writeVersion(OutputNode node, System.Object source, Schema schema)
		{
			Version version = schema.Revision;
			Label label = schema.Version;
			
			if (version != null)
			{
				System.Double start = revision.Default;
				System.Double value_Renamed = version.revision();
				
				if (revision.compare((System.Object) value_Renamed, (System.Object) start))
				{
					if (label.Required)
					{
						writeAttribute(node, (System.Object) value_Renamed, label);
					}
				}
				else
				{
					writeAttribute(node, (System.Object) value_Renamed, label);
				}
			}
		}
		
		/// <summary> This write method is used to write all the attribute contacts from
		/// the provided source object to the XML element. This visits all
		/// the contacts marked with the <code>Attribute</code> annotation in
		/// the source object. All annotated contacts are written as attributes
		/// to the XML element. This will throw an exception if a required
		/// contact within the source object is null. 
		/// 
		/// </summary>
		/// <param name="source">this is the source object to be serialized
		/// </param>
		/// <param name="node">this is the XML element to write attributes to
		/// </param>
		/// <param name="schema">this is used to track the referenced attributes
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  writeAttributes(OutputNode node, System.Object source, Schema schema)
		{
			LabelMap attributes = schema.Attributes;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Label label: attributes)
			{
				Contact contact = label.getContact();
				System.Object value_Renamed = contact.get_Renamed(source);
				
				if (value_Renamed == null)
				{
					value_Renamed = label.getEmpty(context);
				}
				if (value_Renamed == null && label.isRequired())
				{
					throw new AttributeException("Value for %s is null", label);
				}
				writeAttribute(node, value_Renamed, label);
			}
		}
		
		/// <summary> This write method is used to write all the element contacts from
		/// the provided source object to the XML element. This visits all
		/// the contacts marked with the <code>Element</code> annotation in
		/// the source object. All annotated contacts are written as children
		/// to the XML element. This will throw an exception if a required
		/// contact within the source object is null. 
		/// 
		/// </summary>
		/// <param name="source">this is the source object to be serialized
		/// </param>
		/// <param name="node">this is the XML element to write elements to
		/// </param>
		/// <param name="schema">this is used to track the referenced elements
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  writeElements(OutputNode node, System.Object source, Schema schema)
		{
			LabelMap elements = schema.Elements;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Label label: elements)
			{
				Contact contact = label.getContact();
				System.Object value_Renamed = contact.get_Renamed(source);
				
				if (value_Renamed == null && label.isRequired())
				{
					throw new ElementException("Value for %s is null", label);
				}
				System.Object replace = writeReplace(value_Renamed);
				
				if (replace != null)
				{
					writeElement(node, replace, label);
				}
			}
		}
		
		/// <summary> The <code>replace</code> method is used to replace an object
		/// before it is serialized. This is used so that an object can give
		/// a substitute to be written to the XML document in the event that
		/// the actual object is not suitable or desired for serialization. 
		/// This acts as an equivalent to the Java Object Serialization
		/// <code>writeReplace</code> method for the object serialization.
		/// 
		/// </summary>
		/// <param name="source">this is the source object that is to be replaced
		/// 
		/// </param>
		/// <returns> this returns the object to use as a replacement value
		/// 
		/// </returns>
		/// <throws>  Exception if the replacement object is not suitable </throws>
		private System.Object writeReplace(System.Object source)
		{
			if (source != null)
			{
				System.Type type = source.GetType();
				Caller caller = context.getCaller(type);
				
				return caller.replace(source);
			}
			return source;
		}
		
		
		/// <summary> This write method is used to write the text contact from the 
		/// provided source object to the XML element. This takes the text
		/// value from the source object and writes it to the single contact
		/// marked with the <code>Text</code> annotation. If the value is
		/// null and the contact value is required an exception is thrown.
		/// 
		/// </summary>
		/// <param name="source">this is the source object to be serialized
		/// </param>
		/// <param name="node">this is the XML element to write text value to
		/// </param>
		/// <param name="schema">this is used to track the referenced elements
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  writeText(OutputNode node, System.Object source, Schema schema)
		{
			Label label = schema.Text;
			
			if (label != null)
			{
				Contact contact = label.Contact;
				System.Object value_Renamed = contact.get_Renamed(source);
				System.Type type = source.GetType();
				
				if (value_Renamed == null)
				{
					value_Renamed = label.getEmpty(context);
				}
				if (value_Renamed == null && label.Required)
				{
					throw new TextException("Value for %s is null for %s", label, type);
				}
				writeText(node, value_Renamed, label);
			}
		}
		
		/// <summary> This write method is used to set the value of the provided object
		/// as an attribute to the XML element. This will acquire the string
		/// value of the object using <code>toString</code> only if the
		/// object provided is not an enumerated type. If the object is an
		/// enumerated type then the <code>Enum.name</code> method is used.
		/// 
		/// </summary>
		/// <param name="value">this is the value to be set as an attribute
		/// </param>
		/// <param name="node">this is the XML element to write the attribute to
		/// </param>
		/// <param name="label">the label that contains the contact details
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  writeAttribute(OutputNode node, System.Object value_Renamed, Label label)
		{
			if (value_Renamed != null)
			{
				Decorator decorator = label.Decorator;
				System.String name = label.getName(context);
				System.String text = factory.getText(value_Renamed);
				OutputNode done = node.setAttribute(name, text);
				
				decorator.decorate(done);
			}
		}
		
		/// <summary> This write method is used to append the provided object as an
		/// element to the given XML element object. This will recursively
		/// write the contacts from the provided object as elements. This is
		/// done using the <code>Converter</code> acquired from the contact
		/// label. If the type of the contact value is not of the same
		/// type as the XML schema class a "class" attribute is appended.
		/// <p>
		/// If the element being written is inline, then this will not 
		/// check to see if there is a "class" attribute specifying the
		/// name of the class. This is because inline elements do not have
		/// an outer class and thus could never have an override.
		/// 
		/// </summary>
		/// <param name="value">this is the value to be set as an element
		/// </param>
		/// <param name="node">this is the XML element to write the element to
		/// </param>
		/// <param name="label">the label that contains the contact details
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  writeElement(OutputNode node, System.Object value_Renamed, Label label)
		{
			if (value_Renamed != null)
			{
				System.String name = label.getName(context);
				OutputNode next = node.getChild(name);
				Type contact = label.Contact;
				System.Type type = contact.getType();
				
				if (!label.Inline)
				{
					writeNamespaces(next, type, label);
				}
				if (label.Inline || !isOverridden(next, value_Renamed, contact))
				{
					Converter convert = label.getConverter(context);
					bool data = label.Data;
					
					next.setData(data);
					writeElement(next, value_Renamed, convert);
				}
			}
		}
		
		/// <summary> This is used write the element specified using the specified
		/// converter. Writing the value using the specified converter will
		/// result in the node being populated with the elements, attributes,
		/// and text values to the provided node. If there is a problem
		/// writing the value using the converter an exception is thrown.
		/// 
		/// </summary>
		/// <param name="node">this is the node that the value is to be written to
		/// </param>
		/// <param name="value">this is the value that is to be written
		/// </param>
		/// <param name="convert">this is the converter used to perform writing
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  writeElement(OutputNode node, System.Object value_Renamed, Converter convert)
		{
			convert.write(node, value_Renamed);
		}
		
		/// <summary> This is used to apply <code>Decorator</code> objects to the
		/// provided node before it is written. Application of decorations
		/// before the node is written allows namespaces and comments to be
		/// applied to the node. Decorations such as this do not affect the
		/// overall structure of the XML that is written.
		/// 
		/// </summary>
		/// <param name="node">this is the node that decorations are applied to
		/// </param>
		/// <param name="type">this is the type to acquire the decoration for
		/// </param>
		/// <param name="label">this contains the primary decorator to be used
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a decoration problem </throws>
		private void  writeNamespaces(OutputNode node, System.Type type, Label label)
		{
			Decorator primary = context.getDecorator(type);
			Decorator decorator = label.Decorator;
			
			decorator.decorate(node, primary);
		}
		
		/// <summary> This write method is used to set the value of the provided object
		/// as the text for the XML element. This will acquire the string
		/// value of the object using <code>toString</code> only if the
		/// object provided is not an enumerated type. If the object is an
		/// enumerated type then the <code>Enum.name</code> method is used.
		/// 
		/// </summary>
		/// <param name="value">this is the value to set as the XML element text
		/// </param>
		/// <param name="node">this is the XML element to write the text value to
		/// </param>
		/// <param name="label">the label that contains the contact details
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is a serialization problem </throws>
		private void  writeText(OutputNode node, System.Object value_Renamed, Label label)
		{
			if (value_Renamed != null)
			{
				System.String text = factory.getText(value_Renamed);
				bool data = label.Data;
				
				node.setData(data);
				node.setValue(text);
			}
		}
		
		/// <summary> This is used to determine whether the specified value has been
		/// overridden by the strategy. If the item has been overridden
		/// then no more serialization is require for that value, this is
		/// effectively telling the serialization process to stop writing.
		/// 
		/// </summary>
		/// <param name="node">the node that a potential override is written to
		/// </param>
		/// <param name="value">this is the object instance to be serialized
		/// </param>
		/// <param name="type">this is the type of the object to be serialized
		/// 
		/// </param>
		/// <returns> returns true if the strategy overrides the object
		/// </returns>
		private bool isOverridden(OutputNode node, System.Object value_Renamed, Type type)
		{
			return factory.setOverride(type, value_Renamed, node);
		}
	}
}