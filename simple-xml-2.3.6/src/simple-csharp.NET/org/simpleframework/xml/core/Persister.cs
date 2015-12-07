/*
* Persister.java July 2006
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
using Serializer = org.simpleframework.xml.Serializer;
using Filter = org.simpleframework.xml.filter.Filter;
using PlatformFilter = org.simpleframework.xml.filter.PlatformFilter;
using Strategy = org.simpleframework.xml.strategy.Strategy;
using TreeStrategy = org.simpleframework.xml.strategy.TreeStrategy;
using Format = org.simpleframework.xml.stream.Format;
using InputNode = org.simpleframework.xml.stream.InputNode;
using NodeBuilder = org.simpleframework.xml.stream.NodeBuilder;
using OutputNode = org.simpleframework.xml.stream.OutputNode;
using Style = org.simpleframework.xml.stream.Style;
using Matcher = org.simpleframework.xml.transform.Matcher;
namespace org.simpleframework.xml.core
{
	
	/// <summary> The <code>Persister</code> object is used to provide an implementation
	/// of a serializer. This implements the <code>Serializer</code> interface
	/// and enables objects to be persisted and loaded from various sources. 
	/// This implementation makes use of <code>Filter</code> objects to
	/// replace template variables within the source XML document. It is fully
	/// thread safe and can be shared by multiple threads without concerns.
	/// <p>
	/// Deserialization is performed by passing an XML schema class into one
	/// of the <code>read</code> methods along with the source of an XML stream.
	/// The read method then reads the contents of the XML stream and builds
	/// the object using annotations within the XML schema class.
	/// <p>
	/// Serialization is performed by passing an object and an XML stream into
	/// one of the <code>write</code> methods. The serialization process will
	/// use the class of the provided object as the schema class. The object
	/// is traversed and all fields are marshalled to the result stream.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.Serializer">
	/// </seealso>
	public class Persister : Serializer
	{
		private void  InitBlock()
		{
			return read(type, source, true);
			return read(type, source, true);
			return read(type, source, true);
			return read(type, source, true);
			return read(type, source, true);
			return read(type, new StringReader(source), strict);
			System.IO.Stream file = new FileInputStream(source);
			
			try
			{
				return read(type, file, strict);
			}
			finally
			{
				file.Close();
			}
			return read(type, NodeBuilder.read(source), strict);
			return read(type, NodeBuilder.read(source), strict);
			return read(type, node, new Source(strategy, support, style, strict));
			return (T) new Traverser(context).read(node, type);
			return read(value_Renamed, source, true);
			return read(value_Renamed, source, true);
			return read(value_Renamed, source, true);
			return read(value_Renamed, source, true);
			return read(value_Renamed, source, true);
			return read(value_Renamed, new StringReader(source), strict);
			System.IO.Stream file = new FileInputStream(source);
			
			try
			{
				return read(value_Renamed, file, strict);
			}
			finally
			{
				file.Close();
			}
			return read(value_Renamed, NodeBuilder.read(source), strict);
			return read(value_Renamed, NodeBuilder.read(source), strict);
			return read(value_Renamed, node, new Source(strategy, support, style, strict));
			return (T) new Traverser(context).read(node, value_Renamed);
		}
		
		/// <summary> This is the strategy object used to load and resolve classes.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'strategy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Strategy strategy;
		
		/// <summary> This support is used to convert the strings encountered.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'support '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Support support;
		
		/// <summary> This object is used to format the the generated XML document.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'format '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Format format;
		
		/// <summary> This is the style that is used for the serialization process.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private Style style;
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use an empty filter.
		/// This means that template variables will remain unchanged within
		/// the XML document parsed when an object is deserialized.
		/// </summary>
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		public Persister():this(new System.Collections.Hashtable())
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided format
		/// instructions. The persister uses the <code>Format</code> object
		/// to structure the generated XML. It determines the indent size
		/// of the document and whether it should contain a prolog.
		/// 
		/// </summary>
		/// <param name="format">this is used to structure the generated XML
		/// </param>
		public Persister(Format format):this(new TreeStrategy(), format)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use a platform filter
		/// object using the overrides within the provided map. This means
		/// that template variables will be replaced firstly with mappings
		/// from within the provided map, followed by system properties. 
		/// 
		/// </summary>
		/// <param name="filter">this is the map that contains the overrides
		/// </param>
		public Persister(System.Collections.IDictionary filter):this(new PlatformFilter(filter))
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use a platform filter
		/// object using the overrides within the provided map. This means
		/// that template variables will be replaced firstly with mappings
		/// from within the provided map, followed by system properties. 
		/// 
		/// </summary>
		/// <param name="filter">this is the map that contains the overrides
		/// </param>
		/// <param name="format">this is the format used to format the documents
		/// </param>
		public Persister(System.Collections.IDictionary filter, Format format):this(new PlatformFilter(filter))
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided filter.
		/// This persister will replace all variables encountered when
		/// deserializing an object with mappings found in the filter.
		/// 
		/// </summary>
		/// <param name="filter">the filter used to replace template variables
		/// </param>
		public Persister(Filter filter):this(new TreeStrategy(), filter)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided filter.
		/// This persister will replace all variables encountered when
		/// deserializing an object with mappings found in the filter.
		/// 
		/// </summary>
		/// <param name="filter">the filter used to replace template variables
		/// </param>
		/// <param name="format">this is used to structure the generated XML
		/// </param>
		public Persister(Filter filter, Format format):this(new TreeStrategy(), filter, format)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided matcher
		/// for customizable transformations. The <code>Matcher</code> will
		/// enable the persister to determine the correct way to transform
		/// the types that are not annotated and considered primitives.
		/// 
		/// </summary>
		/// <param name="matcher">this is used to customize the transformations
		/// </param>
		public Persister(Matcher matcher):this(new TreeStrategy(), matcher)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided matcher
		/// for customizable transformations. The <code>Matcher</code> will
		/// enable the persister to determine the correct way to transform
		/// the types that are not annotated and considered primitives.
		/// 
		/// </summary>
		/// <param name="matcher">this is used to customize the transformations
		/// </param>
		/// <param name="format">this is used to structure the generated XML
		/// </param>
		public Persister(Matcher matcher, Format format):this(new TreeStrategy(), matcher, format)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use a strategy object. 
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes
		/// </param>
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		public Persister(Strategy strategy):this(strategy, new System.Collections.Hashtable())
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use a strategy object. 
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes
		/// </param>
		/// <param name="format">this is used to structure the generated XML
		/// </param>
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		public Persister(Strategy strategy, Format format):this(strategy, new System.Collections.Hashtable(), format)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided filter.
		/// This persister will replace all variables encountered when
		/// deserializing an object with mappings found in the filter.
		/// 
		/// </summary>
		/// <param name="filter">the filter used to replace template variables
		/// </param>
		/// <param name="matcher">this is used to customize the transformations
		/// </param>
		public Persister(Filter filter, Matcher matcher):this(new TreeStrategy(), filter, matcher)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided filter.
		/// This persister will replace all variables encountered when
		/// deserializing an object with mappings found in the filter.
		/// 
		/// </summary>
		/// <param name="filter">the filter used to replace template variables
		/// </param>
		/// <param name="matcher">this is used to customize the transformations
		/// </param>
		/// <param name="format">this is used to structure the generated XML
		/// </param>
		public Persister(Filter filter, Matcher matcher, Format format):this(new TreeStrategy(), filter, matcher, format)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use a platform filter
		/// object using the overrides within the provided map. This means
		/// that template variables will be replaced firstly with mappings
		/// from within the provided map, followed by system properties. 
		/// <p>
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes 
		/// </param>
		/// <param name="data">this is the map that contains the overrides
		/// </param>
		public Persister(Strategy strategy, System.Collections.IDictionary data):this(strategy, new PlatformFilter(data))
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided filter.
		/// This persister will replace all variables encountered when
		/// deserializing an object with mappings found in the filter.
		/// <p>
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes 
		/// </param>
		/// <param name="data">the filter data used to replace template variables
		/// </param>
		/// <param name="format">this is used to format the generated XML document
		/// </param>
		public Persister(Strategy strategy, System.Collections.IDictionary data, Format format):this(strategy, new PlatformFilter(data), format)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided filter.
		/// This persister will replace all variables encountered when
		/// deserializing an object with mappings found in the filter.
		/// <p>
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes 
		/// </param>
		/// <param name="filter">the filter used to replace template variables
		/// </param>
		public Persister(Strategy strategy, Filter filter):this(strategy, filter, new Format())
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided filter.
		/// This persister will replace all variables encountered when
		/// deserializing an object with mappings found in the filter.
		/// <p>
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes 
		/// </param>
		/// <param name="filter">the filter used to replace template variables
		/// </param>
		/// <param name="format">this is used to format the generated XML document
		/// </param>
		public Persister(Strategy strategy, Filter filter, Format format):this(strategy, filter, new EmptyMatcher(), format)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided matcher
		/// for customizable transformations. The <code>Matcher</code> will
		/// enable the persister to determine the correct way to transform
		/// the types that are not annotated and considered primitives.
		/// <p>
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes 
		/// </param>
		/// <param name="matcher">this is used to customize the transformations
		/// </param>
		public Persister(Strategy strategy, Matcher matcher):this(strategy, new PlatformFilter(), matcher)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided matcher
		/// for customizable transformations. The <code>Matcher</code> will
		/// enable the persister to determine the correct way to transform
		/// the types that are not annotated and considered primitives.
		/// <p>
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes 
		/// </param>
		/// <param name="matcher">this is used to customize the transformations
		/// </param>
		/// <param name="format">this is used to format the generated XML document
		/// </param>
		public Persister(Strategy strategy, Matcher matcher, Format format):this(strategy, new PlatformFilter(), matcher, format)
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided matcher
		/// for customizable transformations. The <code>Matcher</code> will
		/// enable the persister to determine the correct way to transform
		/// the types that are not annotated and considered primitives.
		/// <p>
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes 
		/// </param>
		/// <param name="matcher">this is used to customize the transformations
		/// </param>
		/// <param name="filter">the filter used to replace template variables
		/// </param>
		public Persister(Strategy strategy, Filter filter, Matcher matcher):this(strategy, filter, matcher, new Format())
		{
		}
		
		/// <summary> Constructor for the <code>Persister</code> object. This is used
		/// to create a serializer object that will use the provided matcher
		/// for customizable transformations. The <code>Matcher</code> will
		/// enable the persister to determine the correct way to transform
		/// the types that are not annotated and considered primitives.
		/// <p>
		/// This persister will use the provided <code>Strategy</code> to
		/// intercept the XML elements in order to read and write persistent
		/// data, such as the class name or version of the document.
		/// 
		/// </summary>
		/// <param name="strategy">this is the strategy used to resolve classes 
		/// </param>
		/// <param name="matcher">this is used to customize the transformations
		/// </param>
		/// <param name="filter">the filter used to replace template variables
		/// </param>
		public Persister(Strategy strategy, Filter filter, Matcher matcher, Format format)
		{
			InitBlock();
			this.support = new Support(filter, matcher);
			this.style = format.Style;
			this.strategy = strategy;
			this.format = format;
		}
		
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
		public < T > T read(Class < ? extends T > type, String source) throws Exception
		
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
		public < T > T read(Class < ? extends T > type, File source) throws Exception
		
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
		public < T > T read(Class < ? extends T > type, InputStream source) throws Exception
		
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
		public < T > T read(Class < ? extends T > type, Reader source) throws Exception
		
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
		public < T > T read(Class < ? extends T > type, InputNode source) throws Exception
		
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
		public < T > T read(Class < ? extends T > type, String source, boolean strict) throws Exception
		
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
		public < T > T read(Class < ? extends T > type, File source, boolean strict) throws Exception
		
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
		public < T > T read(Class < ? extends T > type, InputStream source, boolean strict) throws Exception
		
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
		public < T > T read(Class < ? extends T > type, Reader source, boolean strict) throws Exception
		
		/// <summary> This <code>read</code> method will read the contents of the XML
		/// document from the provided source and convert it into an object
		/// of the specified type. If the XML source cannot be deserialized
		/// or there is a problem building the object graph an exception
		/// is thrown. The instance deserialized is returned.
		/// 
		/// </summary>
		/// <param name="type">this is the class type to be deserialized from XML
		/// </param>
		/// <param name="node">this provides the source of the XML document
		/// </param>
		/// <param name="strict">this determines whether to read in strict mode
		/// 
		/// </param>
		/// <returns> the object deserialized from the XML document 
		/// 
		/// </returns>
		/// <throws>  Exception if the object cannot be fully deserialized </throws>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T > T read(Class < ? extends T > type, InputNode node, boolean strict) throws Exception
		
		/// <summary> This <code>read</code> method will read the contents of the XML
		/// document provided and convert it to an object of the specified
		/// type. If the XML document cannot be deserialized or there is a
		/// problem building the object graph an exception is thrown. The
		/// object graph deserialized is returned.
		/// 
		/// </summary>
		/// <param name="type">this is the XML schema class to be deserialized
		/// </param>
		/// <param name="node">this provides the source of the XML document
		/// </param>
		/// <param name="context">the contextual object used for deserialization 
		/// 
		/// </param>
		/// <returns> the object deserialized from the XML document given
		/// 
		/// </returns>
		/// <throws>  Exception if the object cannot be fully deserialized </throws>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private < T > T read(Class < ? extends T > type, InputNode node, Context context) throws Exception
		
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
		public < T > T read(T value, String source) throws Exception
		
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
		public < T > T read(T value, File source) throws Exception
		
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
		public < T > T read(T value, InputStream source) throws Exception
		
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
		public < T > T read(T value, Reader source) throws Exception
		
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
		public < T > T read(T value, InputNode source) throws Exception
		
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
		public < T > T read(T value, String source, boolean strict) throws Exception
		
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
		public < T > T read(T value, File source, boolean strict) throws Exception
		
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
		public < T > T read(T value, InputStream source, boolean strict) throws Exception
		
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
		public < T > T read(T value, Reader source, boolean strict) throws Exception
		
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
		/// <param name="node">this provides the source of the XML document
		/// </param>
		/// <param name="strict">this determines whether to read in strict mode
		/// 
		/// </param>
		/// <returns> the same instance provided is returned when finished 
		/// 
		/// </returns>
		/// <throws>  Exception if the object cannot be fully deserialized </throws>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T > T read(T value, InputNode node, boolean strict) throws Exception
		
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
		/// <param name="node">this provides the source of the XML document
		/// </param>
		/// <param name="context">the contextual object used for deserialization
		/// 
		/// </param>
		/// <returns> the same instance provided is returned when finished 
		/// 
		/// </returns>
		/// <throws>  Exception if the object cannot be fully deserialized </throws>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private < T > T read(T value, InputNode node, Context context) throws Exception
		
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
		public virtual bool validate(System.Type type, System.String source)
		{
			return validate(type, source, true);
		}
		
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
		public virtual bool validate(System.Type type, System.IO.FileInfo source)
		{
			return validate(type, source, true);
		}
		
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
		public virtual bool validate(System.Type type, System.IO.Stream source)
		{
			return validate(type, source, true);
		}
		
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
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual bool validate(System.Type type, System.IO.StreamReader source)
		{
			return validate(type, source, true);
		}
		
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
		public virtual bool validate(System.Type type, InputNode source)
		{
			return validate(type, source, true);
		}
		
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
		public virtual bool validate(System.Type type, System.String source, bool strict)
		{
			return validate(type, new System.IO.StringReader(source), strict);
		}
		
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
		public virtual bool validate(System.Type type, System.IO.FileInfo source, bool strict)
		{
			//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
			System.IO.Stream file = new System.IO.FileStream(source.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			
			try
			{
				return validate(type, file, strict);
			}
			finally
			{
				file.Close();
			}
		}
		
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
		public virtual bool validate(System.Type type, System.IO.Stream source, bool strict)
		{
			return validate(type, NodeBuilder.read(source), strict);
		}
		
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
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual bool validate(System.Type type, System.IO.StreamReader source, bool strict)
		{
			return validate(type, NodeBuilder.read(source), strict);
		}
		
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
		/// <param name="node">this provides the source of the XML document
		/// </param>
		/// <param name="strict">this determines whether to read in strict mode
		/// 
		/// </param>
		/// <returns> true if the document matches the class XML schema 
		/// 
		/// </returns>
		/// <throws>  Exception if the class XML schema does not fully match </throws>
		public virtual bool validate(System.Type type, InputNode node, bool strict)
		{
			return validate(type, node, new Source(strategy, support, style, strict));
		}
		
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
		/// <param name="node">this provides the source of the XML document
		/// </param>
		/// <param name="context">the contextual object used for deserialization  
		/// 
		/// </param>
		/// <returns> true if the document matches the class XML schema 
		/// 
		/// </returns>
		/// <throws>  Exception if the class XML schema does not fully match </throws>
		private bool validate(System.Type type, InputNode node, Context context)
		{
			return new Traverser(context).validate(node, type);
		}
		
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
		public virtual void  write(System.Object source, OutputNode root)
		{
			write(source, root, support);
		}
		
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
		/// </param>
		/// <param name="support">this is the support used to process strings
		/// 
		/// </param>
		/// <throws>  Exception if the schema for the object is not valid </throws>
		private void  write(System.Object source, OutputNode root, Support support)
		{
			write(source, root, new Source(strategy, support, style));
		}
		
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
		/// <param name="context">this is a contextual object used for serialization
		/// 
		/// </param>
		/// <throws>  Exception if the schema for the object is not valid </throws>
		private void  write(System.Object source, OutputNode node, Context context)
		{
			new Traverser(context).write(node, source);
		}
		
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
		public virtual void  write(System.Object source, System.IO.FileInfo out_Renamed)
		{
			//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javaioFile'"
			System.IO.Stream file = new System.IO.FileStream(out_Renamed.FullName, System.IO.FileMode.Create);
			
			try
			{
				write(source, file);
			}
			finally
			{
				file.Close();
			}
		}
		
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
		public virtual void  write(System.Object source, System.IO.Stream out_Renamed)
		{
			write(source, out_Renamed, "utf-8");
		}
		
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
		/// </param>
		/// <param name="charset">this is the character encoding to be used
		/// 
		/// </param>
		/// <throws>  Exception if the schema for the object is not valid </throws>
		public virtual void  write(System.Object source, System.IO.Stream out_Renamed, System.String charset)
		{
			//UPGRADE_TODO: Constructor 'java.io.OutputStreamWriter.OutputStreamWriter' was converted to 'System.IO.StreamWriter.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioOutputStreamWriterOutputStreamWriter_javaioOutputStream_javalangString'"
			write(source, new System.IO.StreamWriter(out_Renamed, System.Text.Encoding.GetEncoding(charset)));
		}
		
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
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual void  write(System.Object source, System.IO.StreamWriter out_Renamed)
		{
			write(source, NodeBuilder.write(out_Renamed, format));
		}
	}
}