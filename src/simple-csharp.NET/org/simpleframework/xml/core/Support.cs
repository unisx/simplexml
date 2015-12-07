/*
* Support.java May 2006
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
using Filter = org.simpleframework.xml.filter.Filter;
using PlatformFilter = org.simpleframework.xml.filter.PlatformFilter;
using Value = org.simpleframework.xml.strategy.Value;
using Matcher = org.simpleframework.xml.transform.Matcher;
//UPGRADE_TODO: The type 'org.simpleframework.xml.transform.Transform' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Transform = org.simpleframework.xml.transform.Transform;
using Transformer = org.simpleframework.xml.transform.Transformer;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Support</code> object is used to provide support to the
	/// serialization engine for processing and transforming strings. This
	/// contains a <code>Transformer</code> which will create objects from
	/// strings and will also reverse this process converting an object
	/// to a string. This is used in the conversion of primitive types.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.transform.Transformer">
	/// </seealso>
	class Support : Filter
	{
		
		/// <summary> This will perform the scanning of types are provide scanners.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ScannerFactory factory;
		
		/// <summary> This is the factory that is used to create the scanners.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'creator '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Instantiator creator;
		
		/// <summary> This is the transformer used to transform objects to text.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'transform '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Transformer transform;
		
		/// <summary> This is the matcher used to acquire the transform objects.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'matcher '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Matcher matcher;
		
		/// <summary> This is the filter used to transform the template variables.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'filter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Filter filter;
		
		/// <summary> Constructor for the <code>Support</code> object. This will
		/// create a support object with a default matcher and default
		/// platform filter. This ensures it contains enough information
		/// to process a template and transform basic primitive types.
		/// </summary>
		public Support():this(new PlatformFilter())
		{
		}
		
		/// <summary> Constructor for the <code>Support</code> object. This will
		/// create a support object with a default matcher and the filter
		/// provided. This ensures it contains enough information to 
		/// process a template and transform basic primitive types.
		/// 
		/// </summary>
		/// <param name="filter">this is the filter to use with this support
		/// </param>
		public Support(Filter filter):this(filter, new EmptyMatcher())
		{
		}
		
		/// <summary> Constructor for the <code>Support</code> object. This will
		/// create a support object with the matcher and filter provided.
		/// This allows the user to override the transformations that
		/// are used to convert types to strings and back again.
		/// 
		/// </summary>
		/// <param name="filter">this is the filter to use with this support
		/// </param>
		/// <param name="matcher">this is the matcher used for transformations
		/// </param>
		public Support(Filter filter, Matcher matcher)
		{
			this.transform = new Transformer(matcher);
			this.factory = new ScannerFactory();
			this.creator = new Instantiator();
			this.matcher = matcher;
			this.filter = filter;
		}
		
		/// <summary> Replaces the text provided with some property. This method 
		/// acts much like a the get method of the <code>Map</code>
		/// object, in that it uses the provided text as a key to some 
		/// value. However it can also be used to evaluate expressions
		/// and output the result for inclusion in the generated XML.
		/// 
		/// </summary>
		/// <param name="text">this is the text value that is to be replaced
		/// </param>
		/// <returns> returns a replacement for the provided text value
		/// </returns>
		public virtual System.String replace(System.String text)
		{
			return filter.replace(text);
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
			return creator.getInstance(value_Renamed);
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
			return creator.getInstance(type);
		}
		
		/// <summary> This is used to match a <code>Transform</code> using the type
		/// specified. If no transform can be acquired then this returns
		/// a null value indicating that no transform could be found.
		/// 
		/// </summary>
		/// <param name="type">this is the type to acquire the transform for
		/// 
		/// </param>
		/// <returns> returns a transform for processing the type given
		/// </returns>
		public virtual Transform getTransform(System.Type type)
		{
			return matcher.match(type);
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
		/// </returns>
		public virtual Scanner getScanner(System.Type type)
		{
			return factory.getInstance(type);
		}
		
		/// <summary> This method is used to convert the string value given to an
		/// appropriate representation. This is used when an object is
		/// being deserialized from the XML document and the value for
		/// the string representation is required.
		/// 
		/// </summary>
		/// <param name="value">this is the string representation of the value
		/// </param>
		/// <param name="type">this is the type to convert the string value to
		/// 
		/// </param>
		/// <returns> this returns an appropriate instanced to be used
		/// </returns>
		public virtual System.Object read(System.String value_Renamed, System.Type type)
		{
			return transform.read(value_Renamed, type);
		}
		
		/// <summary> This method is used to convert the provided value into an XML
		/// usable format. This is used in the serialization process when
		/// there is a need to convert a field value in to a string so 
		/// that that value can be written as a valid XML entity.
		/// 
		/// </summary>
		/// <param name="value">this is the value to be converted to a string
		/// </param>
		/// <param name="type">this is the type to convert to a string value
		/// 
		/// </param>
		/// <returns> this is the string representation of the given value
		/// </returns>
		public virtual System.String write(System.Object value_Renamed, System.Type type)
		{
			return transform.write(value_Renamed, type);
		}
		
		/// <summary> This method is used to determine if the type specified can be
		/// transformed. This will use the <code>Matcher</code> to find a
		/// suitable transform, if one exists then this returns true, if
		/// not then this returns false. This is used during serialization
		/// to determine how to convert a field or method parameter. 
		/// 
		/// </summary>
		/// <param name="type">the type to determine whether its transformable
		/// 
		/// </param>
		/// <returns> true if the type specified can be transformed by this
		/// </returns>
		public virtual bool valid(System.Type type)
		{
			return transform.valid(type);
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
		/// 
		/// </returns>
		/// <throws>  Exception if the class contains an illegal schema </throws>
		public virtual System.String getName(System.Type type)
		{
			Scanner schema = getScanner(type);
			System.String name = schema.Name;
			
			if (name != null)
			{
				return name;
			}
			return getClassName(type);
		}
		
		/// <summary> This returns the name of the class specified. If there is a root
		/// annotation on the type, then this is ignored in favor of the 
		/// actual class name. This is typically used when the type is a
		/// primitive or if there is no <code>Root</code> annotation present. 
		/// 
		/// </summary>
		/// <param name="type">this is the type to acquire the root name for
		/// 
		/// </param>
		/// <returns> this returns the name of the type from the root
		/// </returns>
		private System.String getClassName(System.Type type)
		{
			if (type.IsArray)
			{
				type = type.GetElementType();
			}
			System.String name = type.getSimpleName();
			
			if (type.IsPrimitive)
			{
				return name;
			}
			return Reflector.getName(name);
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
			if (type == typeof(System.String))
			{
				return true;
			}
			if (type.isEnum())
			{
				return true;
			}
			if (type.IsPrimitive)
			{
				return true;
			}
			return transform.valid(type);
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
			if (type == typeof(System.Double))
			{
				return true;
			}
			if (type == typeof(System.Single))
			{
				return true;
			}
			if (type == typeof(float))
			{
				return true;
			}
			if (type == typeof(double))
			{
				return true;
			}
			return false;
		}
	}
}