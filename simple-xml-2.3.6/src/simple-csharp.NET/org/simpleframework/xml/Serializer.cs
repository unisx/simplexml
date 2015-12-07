/*
* Serializer.java July 2006
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
using InputNode = org.simpleframework.xml.stream.InputNode;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
namespace org.simpleframework.xml
{
	
	/// <summary> The <code>Serializer</code> interface is used to represent objects
	/// that can serialize and deserialize objects to an from XML. This 
	/// exposes several <code>read</code> and <code>write</code> methods
	/// that can read from and write to various sources. Typically an
	/// object will be read from an XML file and written to some other 
	/// file or stream. 
	/// <p>
	/// An implementation of the <code>Serializer</code> interface is free
	/// to use any desired XML parsing framework. If a framework other 
	/// than the Java streaming API for XML is required then it should be
	/// wrapped within the <code>org.simpleframework.xml.stream</code> API,
	/// which offers a framework neutral facade.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public interface Serializer
	{
		
		/// <summary> This <code>read</code> method will read the contents of the XML
		/// document from the provided source and convert it into an object
		/// of the specified type. If the XML source cannot be deserialized
		/// or there is a problem building the object graph an exception
		/// is thrown. The instance deserialized is returned.
		/// 
		/// </summary>
		/// <param name="type">this is the class type to be deserialized from XML
		/// </param>
		/// <param name="source">this provides the source of the XML document
		/// 
		/// </param>
		/// <returns> the object deserialized from the XML document 
		/// 
		/// </returns>
		/// <throws>  Exception if the object cannot be fully deserialized </throws>
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, String source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, File source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, InputStream source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, Reader source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, InputNode source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, String source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode 
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, File source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, InputStream source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, Reader source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and convert it into an object
	/// of the specified type. If the XML source cannot be deserialized
	/// or there is a problem building the object graph an exception
	/// is thrown. The instance deserialized is returned.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be deserialized from XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the object deserialized from the XML document 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(Class < ? extends T > type, InputNode source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished  
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, String source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, File source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, InputStream source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, Reader source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, InputNode source) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished  
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, String source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, File source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, InputStream source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, Reader source, boolean strict) throws Exception;
	
	/// <summary> This <code>read</code> method will read the contents of the XML
	/// document from the provided source and populate the object with
	/// the values deserialized. This is used as a means of injecting an
	/// object with values deserialized from an XML document. If the
	/// XML source cannot be deserialized or there is a problem building
	/// the object graph an exception is thrown.
	/// 
	/// </summary>
	/// <param name="value">this is the object to deserialize the XML in to
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> the same instance provided is returned when finished 
	/// 
	/// </returns>
	/// <throws>  Exception if the object cannot be fully deserialized </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T read(T value, InputNode source, boolean strict) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, String source) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, File source) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, InputStream source) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, Reader source) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, InputNode source) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, String source, boolean strict) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, File source, boolean strict) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, InputStream source, boolean strict) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, Reader source, boolean strict) throws Exception;
	
	/// <summary> This <code>validate</code> method will validate the contents of
	/// the XML document against the specified XML class schema. This is
	/// used to perform a read traversal of the class schema such that 
	/// the document can be tested against it. This is preferred to
	/// reading the document as it does not instantiate the objects or
	/// invoke any callback methods, thus making it a safe validation.
	/// 
	/// </summary>
	/// <param name="type">this is the class type to be validated against XML
	/// </param>
	/// <param name="source">this provides the source of the XML document
	/// </param>
	/// <param name="strict">this determines whether to read in strict mode
	/// 
	/// </param>
	/// <returns> true if the document matches the class XML schema 
	/// 
	/// </returns>
	/// <throws>  Exception if the class XML schema does not fully match </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean validate(Class type, InputNode source, boolean strict) throws Exception;
	
	/// <summary> This <code>write</code> method will traverse the provided object
	/// checking for field annotations in order to compose the XML data.
	/// This uses the <code>getClass</code> method on the object to
	/// determine the class file that will be used to compose the schema.
	/// If there is no <code>Root</code> annotation for the class then
	/// this will throw an exception. The root annotation is the only
	/// annotation required for an object to be serialized.  
	/// 
	/// </summary>
	/// <param name="source">this is the object that is to be serialized
	/// </param>
	/// <param name="out">this is where the serialized XML is written to
	/// 
	/// </param>
	/// <throws>  Exception if the schema for the object is not valid </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void write(Object source, File out) throws Exception;
	
	/// <summary> This <code>write</code> method will traverse the provided object
	/// checking for field annotations in order to compose the XML data.
	/// This uses the <code>getClass</code> method on the object to
	/// determine the class file that will be used to compose the schema.
	/// If there is no <code>Root</code> annotation for the class then
	/// this will throw an exception. The root annotation is the only
	/// annotation required for an object to be serialized.  
	/// 
	/// </summary>
	/// <param name="source">this is the object that is to be serialized
	/// </param>
	/// <param name="out">this is where the serialized XML is written to
	/// 
	/// </param>
	/// <throws>  Exception if the schema for the object is not valid </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void write(Object source, OutputStream out) throws Exception;
	
	/// <summary> This <code>write</code> method will traverse the provided object
	/// checking for field annotations in order to compose the XML data.
	/// This uses the <code>getClass</code> method on the object to
	/// determine the class file that will be used to compose the schema.
	/// If there is no <code>Root</code> annotation for the class then
	/// this will throw an exception. The root annotation is the only
	/// annotation required for an object to be serialized.  
	/// 
	/// </summary>
	/// <param name="source">this is the object that is to be serialized
	/// </param>
	/// <param name="out">this is where the serialized XML is written to
	/// 
	/// </param>
	/// <throws>  Exception if the schema for the object is not valid </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void write(Object source, Writer out) throws Exception;
	
	/// <summary> This <code>write</code> method will traverse the provided object
	/// checking for field annotations in order to compose the XML data.
	/// This uses the <code>getClass</code> method on the object to
	/// determine the class file that will be used to compose the schema.
	/// If there is no <code>Root</code> annotation for the class then
	/// this will throw an exception. The root annotation is the only
	/// annotation required for an object to be serialized.  
	/// 
	/// </summary>
	/// <param name="source">this is the object that is to be serialized
	/// </param>
	/// <param name="root">this is where the serialized XML is written to
	/// 
	/// </param>
	/// <throws>  Exception if the schema for the object is not valid </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void write(Object source, OutputNode root) throws Exception;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}