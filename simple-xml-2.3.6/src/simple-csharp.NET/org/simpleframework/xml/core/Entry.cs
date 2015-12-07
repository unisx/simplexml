/*
* Entry.java July 2007
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
using ElementMap = org.simpleframework.xml.ElementMap;
using Type = org.simpleframework.xml.strategy.Type;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Entry</code> object is used to provide configuration for
	/// the serialization and deserialization of a map. Values taken from
	/// the <code>ElementMap</code> annotation provide a means to specify
	/// how to read and write the map as an XML element. Key and value
	/// objects can be written as composite or primitive values. Primitive
	/// key values can be written as attributes of the resulting entry
	/// and value objects can be written inline if desired.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class Entry
	{
		/// <summary> Represents whether the key value is to be an attribute or an
		/// element. This allows the key to be embedded within the entry
		/// XML element allowing for a more compact representation. Only
		/// primitive key objects can be represented as an attribute. For
		/// example a <code>java.util.Date</code> or a string could be
		/// represented as an attribute key for the generated XML. 
		/// 
		/// </summary>
		/// <returns> true if the key is to be inlined as an attribute
		/// </returns>
		virtual public bool Attribute
		{
			get
			{
				return attribute;
			}
			
		}
		/// <summary> Represents whether the value is to be written as an inline text
		/// value within the element. This is only possible if the key has
		/// been specified as an attribute. Also, the value can only be
		/// inline if there is no wrapping value XML element specified.
		/// 
		/// </summary>
		/// <returns> this returns true if the value can be written inline
		/// </returns>
		virtual public bool Inline
		{
			get
			{
				return Attribute;
			}
			
		}
		/// <summary> This is used to acquire the dependent key for the annotated
		/// map. This will simply return the type that the map object is
		/// composed to hold. This must be a serializable type, that is,
		/// it must be a composite or supported primitive type.
		/// 
		/// </summary>
		/// <returns> this returns the key object type for the map object
		/// </returns>
		virtual protected internal Type KeyType
		{
			get
			{
				if (keyType == null)
				{
					keyType = label.keyType();
					
					if (keyType == typeof(void ))
					{
						keyType = getDependent(0);
					}
				}
				return new ClassType(keyType);
			}
			
		}
		/// <summary> This is used to acquire the dependent value for the annotated
		/// map. This will simply return the type that the map object is
		/// composed to hold. This must be a serializable type, that is,
		/// it must be a composite or supported primitive type.
		/// 
		/// </summary>
		/// <returns> this returns the value object type for the map object
		/// </returns>
		virtual protected internal Type ValueType
		{
			get
			{
				if (valueType == null)
				{
					valueType = label.valueType();
					
					if (valueType == typeof(void ))
					{
						valueType = getDependent(1);
					}
				}
				return new ClassType(valueType);
			}
			
		}
		
		/// <summary> Provides the default name for entry XML elements of the map.</summary>
		private const System.String DEFAULT_NAME = "entry";
		
		/// <summary> Represents the annotation that the map object is labeled with.</summary>
		private ElementMap label;
		
		/// <summary> Provides the point of contact in the object to the map.</summary>
		private Contact contact;
		
		/// <summary> Provides the class XML schema used for the value objects.</summary>
		private System.Type valueType;
		
		/// <summary> Provides the class XML schema used for the key objects.</summary>
		private System.Type keyType;
		
		/// <summary> Specifies the name of the XML entry element used by the map.</summary>
		private System.String entry;
		
		/// <summary> Specifies the name of the XML value element used by the map.</summary>
		private System.String value_Renamed;
		
		/// <summary> Specifies the name of the XML key node used by the map.</summary>
		private System.String key;
		
		/// <summary> Determines whether the key object is written as an attribute.</summary>
		private bool attribute;
		
		/// <summary> Constructor for the <code>Entry</code> object. This takes the
		/// element map annotation that provides configuration as to how
		/// the map is serialized and deserialized from the XML document. 
		/// The entry object provides a convenient means to access the XML
		/// schema configuration using defaults where necessary.
		/// 
		/// </summary>
		/// <param name="contact">this is the point of contact to the map object
		/// </param>
		/// <param name="label">the annotation the map method or field uses
		/// </param>
		public Entry(Contact contact, ElementMap label)
		{
			this.attribute = label.attribute();
			this.entry = label.entry();
			this.value_Renamed = label.value_Renamed();
			this.key = label.key();
			this.key = label.key();
			this.contact = contact;
			this.label = label;
		}
		
		/// <summary> This is used to get the key converter for the entry. This knows
		/// whether the key type is a primitive or composite object and will
		/// provide the appropriate converter implementation. This allows 
		/// the root composite map converter to concern itself with only the
		/// details of the surrounding entry object. 
		/// 
		/// </summary>
		/// <param name="context">this is the root context for the serialization
		/// 
		/// </param>
		/// <returns> returns the converter used for serializing the key
		/// </returns>
		public virtual Converter getKey(Context context)
		{
			Type type = KeyType;
			
			if (context.isPrimitive(type))
			{
				return new PrimitiveKey(context, this, type);
			}
			return new CompositeKey(context, this, type);
		}
		
		/// <summary> This is used to get the value converter for the entry. This knows
		/// whether the value type is a primitive or composite object and will
		/// provide the appropriate converter implementation. This allows 
		/// the root composite map converter to concern itself with only the
		/// details of the surrounding entry object. 
		/// 
		/// </summary>
		/// <param name="context">this is the root context for the serialization
		/// 
		/// </param>
		/// <returns> returns the converter used for serializing the value
		/// </returns>
		public virtual Converter getValue(Context context)
		{
			Type type = ValueType;
			
			if (context.isPrimitive(type))
			{
				return new PrimitiveValue(context, this, type);
			}
			return new CompositeValue(context, this, type);
		}
		
		/// <summary> Provides the dependent class for the map as taken from the 
		/// specified index. This allows the entry to fall back on generic
		/// declarations of the map if no explicit dependent types are 
		/// given within the element map annotation.
		/// 
		/// </summary>
		/// <param name="index">this is the index to acquire the parameter from
		/// 
		/// </param>
		/// <returns> this returns the generic type at the specified index
		/// </returns>
		private System.Type getDependent(int index)
		{
			System.Type[] list = contact.Dependents;
			
			if (list.Length < index)
			{
				throw new PersistenceException("Could not find type for %s at index %s", contact, index);
			}
			return list[index];
		}
		
		/// <summary> This is used to provide a key XML element for each of the
		/// keys within the map. This essentially wraps the entity to
		/// be serialized such that there is an extra XML element present.
		/// This can be used to override the default names of primitive
		/// keys, however it can also be used to wrap composite keys. 
		/// 
		/// </summary>
		/// <returns> this returns the key XML element for each key
		/// </returns>
		public virtual System.String getKey()
		{
			if (key == null)
			{
				return key;
			}
			if (isEmpty(key))
			{
				key = null;
			}
			return key;
		}
		
		/// <summary> This is used to provide a value XML element for each of the
		/// values within the map. This essentially wraps the entity to
		/// be serialized such that there is an extra XML element present.
		/// This can be used to override the default names of primitive
		/// values, however it can also be used to wrap composite values. 
		/// 
		/// </summary>
		/// <returns> this returns the value XML element for each value
		/// </returns>
		public virtual System.String getValue()
		{
			if (value_Renamed == null)
			{
				return value_Renamed;
			}
			if (isEmpty(value_Renamed))
			{
				value_Renamed = null;
			}
			return value_Renamed;
		}
		
		/// <summary> This is used to provide a the name of the entry XML element 
		/// that wraps the key and value elements. If specified the entry
		/// value specified will be used instead of the default name of 
		/// the element. This is used to ensure the resulting XML is 
		/// configurable to the requirements of the generated XML. 
		/// 
		/// </summary>
		/// <returns> this returns the entry XML element for each entry
		/// </returns>
		public virtual System.String getEntry()
		{
			if (entry == null)
			{
				return entry;
			}
			if (isEmpty(entry))
			{
				entry = DEFAULT_NAME;
			}
			return entry;
		}
		
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
	}
}