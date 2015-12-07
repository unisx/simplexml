/*
* DocumentProvider.java January 2010
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
namespace org.simpleframework.xml.stream
{
	
	/// <summary> The <code>DocumentProvider</code> object is used to provide event
	/// reader implementations for DOM. Wrapping the mechanics of the
	/// DOM framework within a <code>Provider</code> ensures that it can
	/// be plugged in without any dependencies. This allows other parsers
	/// to be swapped in should there be such a requirement.
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// 
	/// </author>
	/// <seealso cref="org.simpleframework.xml.stream.DocumentProvider">
	/// </seealso>
	class DocumentProvider : Provider
	{
		
		/// <summary> This is the factory that is used to create DOM parsers.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_ISSUE: Class 'javax.xml.parsers.DocumentBuilderFactory' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmlparsersDocumentBuilderFactory'"
		private DocumentBuilderFactory factory;
		
		/// <summary> Constructor for the <code>DocumentProvider</code> object. This
		/// is used to instantiate a parser factory that will be used to
		/// create parsers when requested. Instantiating the factory up
		/// front also checks that the framework is fully supported.
		/// </summary>
		public DocumentProvider()
		{
			//UPGRADE_ISSUE: Method 'javax.xml.parsers.DocumentBuilderFactory.newInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmlparsersDocumentBuilderFactory'"
			this.factory = DocumentBuilderFactory.newInstance();
			//UPGRADE_ISSUE: Method 'javax.xml.parsers.DocumentBuilderFactory.setNamespaceAware' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmlparsersDocumentBuilderFactory'"
			this.factory.setNamespaceAware(true);
		}
		
		/// <summary> This provides an <code>EventReader</code> that will read from
		/// the specified input stream. When reading from an input stream
		/// the character encoding should be taken from the XML prolog or
		/// it should default to the UTF-8 character encoding.
		/// 
		/// </summary>
		/// <param name="source">this is the stream to read the document with
		/// 
		/// </param>
		/// <returns> this is used to return the event reader implementation
		/// </returns>
		public virtual EventReader provide(System.IO.Stream source)
		{
			return provide(new XmlSourceSupport(source));
		}
		
		/// <summary> This provides an <code>EventReader</code> that will read from
		/// the specified reader. When reading from a reader the character
		/// encoding should be the same as the source XML document.
		/// 
		/// </summary>
		/// <param name="source">this is the reader to read the document with
		/// 
		/// </param>
		/// <returns> this is used to return the event reader implementation
		/// </returns>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual EventReader provide(System.IO.StreamReader source)
		{
			return provide(new XmlSourceSupport(source));
		}
		
		/// <summary> This provides an <code>EventReader</code> that will read from
		/// the specified source. When reading from a source the character
		/// encoding should be the same as the source XML document.
		/// 
		/// </summary>
		/// <param name="source">this is the source to read the document with
		/// 
		/// </param>
		/// <returns> this is used to return the event reader implementation
		/// </returns>
		private EventReader provide(XmlSourceSupport source)
		{
			//UPGRADE_TODO: Class 'javax.xml.parsers.DocumentBuilder' was converted to 'System.Xml.XmlDocument' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmlparsersDocumentBuilder'"
			System.Xml.XmlDocument builder = new System.Xml.XmlDocument();
			//UPGRADE_TODO: Method 'javax.xml.parsers.DocumentBuilder.parse' was converted to 'System.Xml.XmlDocument.Load' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmlparsersDocumentBuilderparse_orgxmlsaxInputSource'"
			System.Xml.XmlDocument document = SupportClass.ParseDocument(builder, source);
			
			return new DocumentReader(document);
		}
	}
}