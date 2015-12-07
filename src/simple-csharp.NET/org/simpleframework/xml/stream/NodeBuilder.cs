/*
* NodeBuilder.java July 2006
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
namespace org.simpleframework.xml.stream
{
	
	/// <summary> The <code>NodeBuilder</code> object is used to create either an
	/// input node or an output node for a given source or destination. 
	/// If an <code>InputNode</code> is required for reading an XML
	/// document then a reader must be provided to read the content from.
	/// <p>
	/// If an <code>OutputNode</code> is required then a destination is
	/// required. The provided output node can be used to generate well
	/// formed XML to the specified writer. 
	/// 
	/// </summary>
	/// <author>  Niall Gallagher
	/// </author>
	public sealed class NodeBuilder
	{
		
		/// <summary> This is the XML provider implementation that creates readers.</summary>
		private static Provider provider;
		
		/// <summary> This is used to create an <code>InputNode</code> that can be 
		/// used to read XML from the specified stream. The stream will
		/// be positioned at the root element in the XML document.
		/// 
		/// </summary>
		/// <param name="source">this contains the contents of the XML source
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		public static InputNode read(System.IO.Stream source)
		{
			return read(provider.provide(source));
		}
		
		/// <summary> This is used to create an <code>InputNode</code> that can be 
		/// used to read XML from the specified reader. The reader will
		/// be positioned at the root element in the XML document.
		/// 
		/// </summary>
		/// <param name="source">this contains the contents of the XML source
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static InputNode read(System.IO.StreamReader source)
		{
			return read(provider.provide(source));
		}
		
		/// <summary> This is used to create an <code>InputNode</code> that can be 
		/// used to read XML from the specified reader. The reader will
		/// be positioned at the root element in the XML document.
		/// 
		/// </summary>
		/// <param name="source">this contains the contents of the XML source
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		private static InputNode read(EventReader source)
		{
			return new NodeReader(source).readRoot();
		}
		
		/// <summary> This is used to create an <code>OutputNode</code> that can be
		/// used to write a well formed XML document. The writer specified
		/// will have XML elements, attributes, and text written to it as
		/// output nodes are created and populated.
		/// 
		/// </summary>
		/// <param name="result">this contains the result of the generated XML
		/// 
		/// </param>
		/// <throws>  Exception this is thrown if there is an I/O error </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static OutputNode write(System.IO.StreamWriter result)
		{
			return write(result, new Format());
		}
		
		/// <summary> This is used to create an <code>OutputNode</code> that can be
		/// used to write a well formed XML document. The writer specified
		/// will have XML elements, attributes, and text written to it as
		/// output nodes are created and populated.
		/// 
		/// </summary>
		/// <param name="result">this contains the result of the generated XML
		/// </param>
		/// <param name="format">this is the format to use for the document
		/// 
		/// </param>
		/// <throws>  Exception this is thrown if there is an I/O error </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static OutputNode write(System.IO.StreamWriter result, Format format)
		{
			return new NodeWriter(result, format).writeRoot();
		}
		static NodeBuilder()
		{
			{
				provider = ProviderFactory.Instance;
			}
		}
	}
}