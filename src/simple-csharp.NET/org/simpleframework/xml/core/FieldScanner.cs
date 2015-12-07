/*
* FieldScanner.java April 2007
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
namespace org.simpleframework.xml.core
{
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static org.simpleframework.xml.DefaultType.FIELD;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.lang.annotation.Annotation;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.lang.reflect.Field;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.Attribute;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.DefaultType;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.Element;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.ElementArray;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.ElementList;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.ElementMap;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.Text;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.Transient;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.simpleframework.xml.Version;
	
	/// <summary> The <code>FieldScanner</code> object is used to scan an class for
	/// fields marked with an XML annotation. All fields that contain an
	/// XML annotation are added as <code>Contact</code> objects to the
	/// list of contacts for the class. This scans the object by checking
	/// the class hierarchy, this allows a subclass to override a super
	/// class annotated field, although this should be used rarely.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	[Serializable]
	class FieldScanner:ContactList
	{
		
		/// <summary> This is used to create the synthetic annotations for fields.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private AnnotationFactory factory;
		
		/// <summary> This is used to acquire the hierarchy for the class scanned.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'hierarchy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Hierarchy hierarchy;
		
		/// <summary> This is the default access type to be used for this scanner.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'access '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private DefaultType access;
		
		/// <summary> This is used to determine which fields have been scanned.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'done '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ContactMap done;
		
		/// <summary> Constructor for the <code>FieldScanner</code> object. This is
		/// used to perform a scan on the specified class in order to find
		/// all fields that are labeled with an XML annotation.
		/// 
		/// </summary>
		/// <param name="type">this is the schema class that is to be scanned
		/// </param>
		public FieldScanner(System.Type type):this(type, null)
		{
		}
		
		/// <summary> Constructor for the <code>FieldScanner</code> object. This is
		/// used to perform a scan on the specified class in order to find
		/// all fields that are labeled with an XML annotation.
		/// 
		/// </summary>
		/// <param name="type">this is the schema class that is to be scanned
		/// </param>
		/// <param name="access">this is the access type for the class
		/// </param>
		public FieldScanner(System.Type type, DefaultType access)
		{
			this.factory = new AnnotationFactory();
			this.hierarchy = new Hierarchy(type);
			this.done = new ContactMap();
			this.access = access;
			this.scan(type);
		}
		
		/// <summary> This method is used to scan the class hierarchy for each class
		/// in order to extract fields that contain XML annotations. If
		/// the field is annotated it is converted to a contact so that
		/// it can be used during serialization and deserialization.
		/// 
		/// </summary>
		/// <param name="type">this is the type to be scanned for fields
		/// 
		/// </param>
		/// <throws>  Exception thrown if the object schema is invalid </throws>
		private void  scan(System.Type type)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Class next: hierarchy)
			{
				scan(next, access);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Class next: hierarchy)
			{
				scan(next, type);
			}
			build();
		}
		
		/// <summary> This is used to scan the declared fields within the specified
		/// class. Each method will be check to determine if it contains
		/// an XML element and can be used as a <code>Contact</code> for
		/// an entity within the object.
		/// 
		/// </summary>
		/// <param name="real">this is the actual type of the object scanned
		/// </param>
		/// <param name="type">this is one of the super classes for the object
		/// </param>
		private void  scan(System.Type type, System.Type real)
		{
			Field[] list = type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Field field: list)
			{
				scan(field);
			}
		}
		
		/// <summary> This is used to scan all annotations within the given field.
		/// Each annotation is checked against the set of supported XML
		/// annotations. If the annotation is one of the XML annotations
		/// then the field is considered for acceptance as a contact.
		/// 
		/// </summary>
		/// <param name="field">the field to be scanned for XML annotations
		/// </param>
		private void  scan(Field field)
		{
			Annotation[] list = field.getDeclaredAnnotations();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Annotation label: list)
			{
				scan(field, label);
			}
		}
		
		/// <summary> This is used to scan all the fields of the class in order to
		/// determine if it should have a default annotation. If the field
		/// should have a default XML annotation then it is added to the
		/// list of contacts to be used to form the class schema.
		/// 
		/// </summary>
		/// <param name="type">this is the type to have its fields scanned
		/// </param>
		/// <param name="access">this is the default access type for the class
		/// </param>
		private void  scan(System.Type type, DefaultType access)
		{
			Field[] list = type.GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static);
			
			if (access == FIELD)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Field field: list)
				{
					System.Type real = field.getType();
					
					if (real != null)
					{
						process(field, real);
					}
				}
			}
		}
		
		/// <summary> This reflectively checks the annotation to determine the type 
		/// of annotation it represents. If it represents an XML schema
		/// annotation it is used to create a <code>Contact</code> which 
		/// can be used to represent the field within the source object.
		/// 
		/// </summary>
		/// <param name="field">the field that the annotation comes from
		/// </param>
		/// <param name="label">the annotation used to model the XML schema
		/// </param>
		private void  scan(Field field, Annotation label)
		{
			if (label is Attribute)
			{
				process(field, label);
			}
			if (label is ElementList)
			{
				process(field, label);
			}
			if (label is ElementArray)
			{
				process(field, label);
			}
			if (label is ElementMap)
			{
				process(field, label);
			}
			if (label is Element)
			{
				process(field, label);
			}
			if (label is Transient)
			{
				remove(field, label);
			}
			if (label is Version)
			{
				process(field, label);
			}
			if (label is Text)
			{
				process(field, label);
			}
		}
		
		/// <summary> This method is used to process the field an annotation given.
		/// This will check to determine if the field is accessible, if it
		/// is not accessible then it is made accessible so that private
		/// member fields can be used during the serialization process.
		/// 
		/// </summary>
		/// <param name="field">this is the field to be added as a contact
		/// </param>
		/// <param name="type">this is the type to acquire the annotation
		/// </param>
		private void  process(Field field, System.Type type)
		{
			Annotation label = factory.getInstance(type);
			
			if (label != null)
			{
				process(field, label);
			}
		}
		
		/// <summary> This method is used to process the field an annotation given.
		/// This will check to determine if the field is accessible, if it
		/// is not accessible then it is made accessible so that private
		/// member fields can be used during the serialization process.
		/// 
		/// </summary>
		/// <param name="field">this is the field to be added as a contact
		/// </param>
		/// <param name="label">this is the XML annotation used by the field
		/// </param>
		private void  process(Field field, Annotation label)
		{
			Contact contact = new FieldContact(field, label);
			
			if (!field.isAccessible())
			{
				field.setAccessible(true);
			}
			done.put(field, contact);
		}
		
		/// <summary> This is used to remove a field from the map of processed fields.
		/// A field is removed with the <code>Transient</code> annotation
		/// is used to indicate that it should not be processed by the
		/// scanner. This is required when default types are used.
		/// 
		/// </summary>
		/// <param name="field">this is the field to be removed from the map
		/// </param>
		/// <param name="label">this is the label associated with the field
		/// </param>
		private void  remove(Field field, Annotation label)
		{
			done.remove(field);
		}
		
		/// <summary> This is used to build a list of valid contacts for this scanner.
		/// Valid contacts are fields that are either defaulted or those
		/// that have an explicit XML annotation. Any field that has been
		/// marked as transient will not be considered as valid.
		/// </summary>
		private void  build()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Contact contact: done)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.util.ArrayList.add' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				bool generatedAux = Add(contact) >= 0;
			}
		}
	}
}