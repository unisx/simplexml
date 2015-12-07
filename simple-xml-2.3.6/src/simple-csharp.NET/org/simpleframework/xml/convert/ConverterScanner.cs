/*
* ConverterScanner.java January 2010
*
* Copyright (C) 2010, Niall Gallagher <niallg@users.sf.net>
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
using Element = org.simpleframework.xml.Element;
using Root = org.simpleframework.xml.Root;
using Type = org.simpleframework.xml.strategy.Type;
using Value = org.simpleframework.xml.strategy.Value;
namespace org.simpleframework.xml.convert
{
	
	/// <summary> The <code>ConverterScanner</code> is used to create a converter 
	/// given a method or field representation. Creation of the converter
	/// is done using the <code>Convert</code> annotation, which may
	/// be used to annotate a field, method or class. This describes the
	/// implementation to use for object serialization. To account for
	/// polymorphism the type scanned for annotations can be overridden
	/// from type provided in the <code>Type</code> object. This ensures
	/// that if a collection of objects are serialized the correct
	/// implementation will be used for each type or subtype.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	class ConverterScanner
	{
		private void  InitBlock()
		{
			return builder.build(type).scan(label);
		}
		
		/// <summary> This is used to instantiate converters given the type.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ConverterFactory factory;
		
		/// <summary> This is used to build a scanner to scan for annotations.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'builder '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ScannerBuilder builder;
		
		/// <summary> Constructor for the <code>ConverterScanner</code> object. This
		/// uses an internal factory to instantiate and cache all of the
		/// converters created. This will ensure that there is reduced
		/// overhead for a serialization process using converters.
		/// </summary>
		public ConverterScanner()
		{
			InitBlock();
			this.factory = new ConverterFactory();
			this.builder = new ScannerBuilder();
		}
		
		/// <summary> This method will lookup and instantiate a converter found from
		/// scanning the field or method type provided. If the type has
		/// been overridden then the <code>Value</code> object will provide
		/// the type to scan. If no annotation is found on the class, field
		/// or method then this will return null.
		/// 
		/// </summary>
		/// <param name="type">this is the type to search for the annotation
		/// </param>
		/// <param name="value">this contains the type if it was overridden
		/// 
		/// </param>
		/// <returns> a converter scanned from the provided field or method
		/// </returns>
		public virtual Converter getConverter(Type type, Value value_Renamed)
		{
			System.Type real = getType(type, value_Renamed);
			Convert convert = getConvert(type, real);
			
			if (convert != null)
			{
				return factory.getInstance(convert);
			}
			return null;
		}
		
		/// <summary> This method will lookup and instantiate a converter found from
		/// scanning the field or method type provided. If the type has
		/// been overridden then the object instance will provide the type 
		/// to scan. If no annotation is found on the class, field or 
		/// method then this will return null.
		/// 
		/// </summary>
		/// <param name="type">this is the type to search for the annotation
		/// </param>
		/// <param name="value">this contains the type if it was overridden
		/// 
		/// </param>
		/// <returns> a converter scanned from the provided field or method
		/// </returns>
		public virtual Converter getConverter(Type type, System.Object value_Renamed)
		{
			System.Type real = getType(type, value_Renamed);
			Convert convert = getConvert(type, real);
			
			if (convert != null)
			{
				return factory.getInstance(convert);
			}
			return null;
		}
		
		/// <summary> This method is used to scan the provided <code>Type</code> for
		/// an annotation. If the <code>Type</code> represents a field or
		/// method then the annotation can be taken directly from that
		/// field or method. If however the type represents a class then
		/// the class itself must contain the annotation. 
		/// 
		/// </summary>
		/// <param name="type">the field or method containing the annotation
		/// </param>
		/// <param name="real">the type that represents the field or method
		/// 
		/// </param>
		/// <returns> this returns the annotation on the field or method
		/// </returns>
		private Convert getConvert(Type type, System.Type real)
		{
			Convert convert = getConvert(type);
			
			if (convert == null)
			{
				return getConvert(real);
			}
			return convert;
		}
		
		/// <summary> This method is used to scan the provided <code>Type</code> for
		/// an annotation. If the <code>Type</code> represents a field or
		/// method then the annotation can be taken directly from that
		/// field or method. If however the type represents a class then
		/// the class itself must contain the annotation. 
		/// 
		/// </summary>
		/// <param name="type">the field or method containing the annotation
		/// 
		/// </param>
		/// <returns> this returns the annotation on the field or method
		/// </returns>
		private Convert getConvert(Type type)
		{
			Convert convert = type.getAnnotation(typeof(Convert));
			
			if (convert != null)
			{
				Element element = type.getAnnotation(typeof(Element));
				
				if (element == null)
				{
					throw new ConvertException("Element annotation required for %s", type);
				}
			}
			return convert;
		}
		
		/// <summary> This method is used to scan the provided <code>Type</code> for
		/// an annotation. If the <code>Type</code> represents a field or
		/// method then the annotation can be taken directly from that
		/// field or method. If however the type represents a class then
		/// the class itself must contain the annotation. 
		/// 
		/// </summary>
		/// <param name="real">the type that represents the field or method
		/// 
		/// </param>
		/// <returns> this returns the annotation on the field or method
		/// </returns>
		private Convert getConvert(System.Type real)
		{
			Convert convert = getAnnotation(real, typeof(Convert));
			
			if (convert != null)
			{
				Root root = getAnnotation(real, typeof(Root));
				
				if (root == null)
				{
					throw new ConvertException("Root annotation required for %s", real);
				}
			}
			return convert;
		}
		
		/// <summary> This is used to acquire the <code>Convert</code> annotation from
		/// the class provided. If the type does not contain the annotation
		/// then this scans all supertypes until either an annotation is
		/// found or there are no further supertypes. 
		/// 
		/// </summary>
		/// <param name="type">this is the type to scan for annotations
		/// </param>
		/// <param name="label">this is the annotation type that is to be found
		/// 
		/// </param>
		/// <returns> this returns the annotation if found otherwise null
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private < T extends Annotation > T getAnnotation(Class < ? > type, Class < T > label)
		
		/// <summary> This is used to acquire the class that should be scanned. The
		/// type is found either on the method or field, or should there
		/// be a subtype then the class is taken from the provided value.
		/// 
		/// </summary>
		/// <param name="type">this is the type representing the field or method
		/// </param>
		/// <param name="value">this contains the type if it was overridden
		/// 
		/// </param>
		/// <returns> this returns the class that has been scanned
		/// </returns>
		private System.Type getType(Type type, Value value_Renamed)
		{
			System.Type real = type.getType();
			
			if (value_Renamed != null)
			{
				return value_Renamed.Type;
			}
			return real;
		}
		
		/// <summary> This is used to acquire the class that should be scanned. The
		/// type is found either on the method or field, or should there
		/// be a subtype then the class is taken from the provided value.
		/// 
		/// </summary>
		/// <param name="type">this is the type representing the field or method
		/// </param>
		/// <param name="value">this contains the type if it was overridden
		/// 
		/// </param>
		/// <returns> this returns the class that has been scanned
		/// </returns>
		private System.Type getType(Type type, System.Object value_Renamed)
		{
			System.Type real = type.getType();
			
			if (value_Renamed != null)
			{
				return value_Renamed.GetType();
			}
			return real;
		}
	}
}