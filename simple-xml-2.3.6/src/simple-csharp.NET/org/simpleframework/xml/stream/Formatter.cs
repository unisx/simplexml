/*
* Formatter.java July 2006
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
	
	/// <summary> The <code>Formatter</code> object is used to format output as XML
	/// indented with a configurable indent level. This is used to write
	/// start and end tags, as well as attributes and values to the given
	/// writer. The output is written directly to the stream with and
	/// indentation for each element appropriate to its position in the
	/// document hierarchy. If the indent is set to zero then no indent
	/// is performed and all XML will appear on the same line.
	/// 
	/// </summary>
	/// <seealso cref="org.simpleframework.xml.stream.Indenter">
	/// </seealso>
	class Formatter
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			COMMENT, 
			START, 
			TEXT, 
			END
		}
		
		/// <summary> Represents the prefix used when declaring an XML namespace.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'NAMESPACE'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly char[] NAMESPACE = new char[]{'x', 'm', 'l', 'n', 's'};
		
		/// <summary> Represents the XML escape sequence for the less than sign.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'LESS'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly char[] LESS = new char[]{'&', 'l', 't', ';'};
		
		/// <summary> Represents the XML escape sequence for the greater than sign.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'GREATER'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly char[] GREATER = new char[]{'&', 'g', 't', ';'};
		
		/// <summary> Represents the XML escape sequence for the double quote.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'DOUBLE'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly char[] DOUBLE = new char[]{'&', 'q', 'u', 'o', 't', ';'};
		
		/// <summary> Represents the XML escape sequence for the single quote.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'SINGLE'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly char[] SINGLE = new char[]{'&', 'a', 'p', 'o', 's', ';'};
		
		/// <summary> Represents the XML escape sequence for the ampersand sign.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'AND'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly char[] AND = new char[]{'&', 'a', 'm', 'p', ';'};
		
		/// <summary> This is used to open a comment section within the document.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'OPEN'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly char[] OPEN = new char[]{'<', '!', '-', '-', ' '};
		
		/// <summary> This is used to close a comment section within the document.</summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'CLOSE'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly char[] CLOSE = new char[]{' ', '-', '-', '>'};
		
		/// <summary> Output buffer used to write the generated XML result to.</summary>
		private OutputBuffer buffer;
		
		/// <summary> Creates the indentations that are used buffer the XML file.</summary>
		private Indenter indenter;
		
		/// <summary> This is the writer that is used to write the XML document.</summary>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.IO.StreamWriter result;
		
		/// <summary> Represents the prolog to insert at the start of the document.</summary>
		private System.String prolog;
		
		/// <summary> Represents the last type of content that was written.</summary>
		private Tag last;
		
		/// <summary> Constructor for the <code>Formatter</code> object. This creates
		/// an object that can be used to write XML in an indented format
		/// to the specified writer. The XML written will be well formed.
		/// 
		/// </summary>
		/// <param name="result">this is where the XML should be written to
		/// </param>
		/// <param name="format">this is the format object to use 
		/// </param>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public Formatter(System.IO.StreamWriter result, Format format)
		{
			InitBlock();
			this.result = new System.IO.StreamWriter(result.BaseStream, result.Encoding);
			this.indenter = new Indenter(format);
			this.buffer = new OutputBuffer();
			this.prolog = format.Prolog;
		}
		
		/// <summary> This is used to write a prolog to the specified output. This is
		/// only written if the specified <code>Format</code> object has
		/// been given a non null prolog. If no prolog is specified then no
		/// prolog is written to the generated XML.
		/// 
		/// </summary>
		/// <throws>  Exception thrown if there is an I/O problem  </throws>
		public virtual void  writeProlog()
		{
			if (prolog != null)
			{
				write(prolog);
				write("\n");
			}
		}
		
		/// <summary> This is used to write any comments that have been set. The
		/// comment will typically be written at the start of an element
		/// to describe the purpose of the element or include debug data
		/// that can be used to determine any issues in serialization.
		/// 
		/// </summary>
		/// <param name="comment">this is the comment that is to be written
		/// </param>
		public virtual void  writeComment(System.String comment)
		{
			System.String text = indenter.top();
			
			if (last == Tag.START)
			{
				append('>');
			}
			if (text != null)
			{
				append(text);
				append(OPEN);
				append(comment);
				append(CLOSE);
			}
			last = Tag.COMMENT;
		}
		
		/// <summary> This method is used to write a start tag for an element. If a
		/// start tag was written before this then it is closed. Before
		/// the start tag is written an indent is generated and placed in
		/// front of the tag, this is done for all but the first start tag.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the start tag to be written
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		public virtual void  writeStart(System.String name, System.String prefix)
		{
			System.String text = indenter.push();
			
			if (last == Tag.START)
			{
				append('>');
			}
			flush();
			append(text);
			append('<');
			
			if (!isEmpty(prefix))
			{
				append(prefix);
				append(':');
			}
			append(name);
			last = Tag.START;
		}
		
		/// <summary> This is used to write a name value attribute pair. If the last
		/// tag written was not a start tag then this throws an exception.
		/// All attribute values written are enclosed in double quotes.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the attribute to be written
		/// </param>
		/// <param name="value">this is the value to assigne to the attribute
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		public virtual void  writeAttribute(System.String name, System.String value_Renamed, System.String prefix)
		{
			if (last != Tag.START)
			{
				throw new NodeException("Start element required");
			}
			write(' ');
			write(name, prefix);
			write('=');
			write('"');
			escape(value_Renamed);
			write('"');
		}
		
		/// <summary> This is used to write the namespace to the element. This will
		/// write the special attribute using the prefix and reference
		/// specified. This will escape the reference if it is required.
		/// 
		/// </summary>
		/// <param name="reference">this is the namespace URI reference to use
		/// </param>
		/// <param name="prefix">this is the prefix to used for the namespace
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		public virtual void  writeNamespace(System.String reference, System.String prefix)
		{
			if (last != Tag.START)
			{
				throw new NodeException("Start element required");
			}
			write(' ');
			write(NAMESPACE);
			
			if (!isEmpty(prefix))
			{
				write(':');
				write(prefix);
			}
			write('=');
			write('"');
			escape(reference);
			write('"');
		}
		
		/// <summary> This is used to write the specified text value to the writer.
		/// If the last tag written was a start tag then it is closed.
		/// By default this will escape any illegal XML characters. 
		/// 
		/// </summary>
		/// <param name="text">this is the text to write to the output
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		public virtual void  writeText(System.String text)
		{
			writeText(text, Mode.ESCAPE);
		}
		
		/// <summary> This is used to write the specified text value to the writer.
		/// If the last tag written was a start tag then it is closed.
		/// This will use the output mode specified. 
		/// 
		/// </summary>
		/// <param name="text">this is the text to write to the output
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		public virtual void  writeText(System.String text, Mode mode)
		{
			if (last == Tag.START)
			{
				write('>');
			}
			if (mode == Mode.DATA)
			{
				data(text);
			}
			else
			{
				escape(text);
			}
			last = Tag.TEXT;
		}
		
		/// <summary> This is used to write an end element tag to the writer. This
		/// will close the element with a short <code>/&gt;</code> if the
		/// last tag written was a start tag. However if an end tag or 
		/// some text was written then a full end tag is written.
		/// 
		/// </summary>
		/// <param name="name">this is the name of the element to be closed
		/// 
		/// </param>
		/// <throws>  Exception thrown if there is an I/O exception </throws>
		public virtual void  writeEnd(System.String name, System.String prefix)
		{
			System.String text = indenter.pop();
			
			if (last == Tag.START)
			{
				write('/');
				write('>');
			}
			else
			{
				if (last != Tag.TEXT)
				{
					write(text);
				}
				if (last != Tag.START)
				{
					write('<');
					write('/');
					write(name, prefix);
					write('>');
				}
			}
			last = Tag.END;
		}
		
		/// <summary> This is used to write a character to the output stream without
		/// any translation. This is used when writing the start tags and
		/// end tags, this is also used to write attribute names.
		/// 
		/// </summary>
		/// <param name="ch">this is the character to be written to the output
		/// </param>
		private void  write(char ch)
		{
			buffer.write(result);
			buffer.clear();
			result.Write(ch);
		}
		
		/// <summary> This is used to write plain text to the output stream without
		/// any translation. This is used when writing the start tags and
		/// end tags, this is also used to write attribute names.
		/// 
		/// </summary>
		/// <param name="plain">this is the text to be written to the output
		/// </param>
		private void  write(char[] plain)
		{
			buffer.write(result);
			buffer.clear();
			result.Write(plain);
		}
		
		/// <summary> This is used to write plain text to the output stream without
		/// any translation. This is used when writing the start tags and
		/// end tags, this is also used to write attribute names.
		/// 
		/// </summary>
		/// <param name="plain">this is the text to be written to the output
		/// </param>
		private void  write(System.String plain)
		{
			buffer.write(result);
			buffer.clear();
			result.Write(plain);
		}
		
		/// <summary> This is used to write plain text to the output stream without
		/// any translation. This is used when writing the start tags and
		/// end tags, this is also used to write attribute names.
		/// 
		/// </summary>
		/// <param name="plain">this is the text to be written to the output
		/// </param>
		/// <param name="prefix">this is the namespace prefix to be written
		/// </param>
		private void  write(System.String plain, System.String prefix)
		{
			buffer.write(result);
			buffer.clear();
			
			if (!isEmpty(prefix))
			{
				result.Write(prefix);
				result.Write(':');
			}
			result.Write(plain);
		}
		
		/// <summary> This is used to buffer a character to the output stream without
		/// any translation. This is used when buffering the start tags so
		/// that they can be reset without affecting the resulting document.
		/// 
		/// </summary>
		/// <param name="ch">this is the character to be written to the output
		/// </param>
		private void  append(char ch)
		{
			buffer.append(ch);
		}
		
		/// <summary> This is used to buffer characters to the output stream without
		/// any translation. This is used when buffering the start tags so
		/// that they can be reset without affecting the resulting document.
		/// 
		/// </summary>
		/// <param name="plain">this is the string that is to be buffered
		/// </param>
		private void  append(char[] plain)
		{
			buffer.append(plain);
		}
		
		/// <summary> This is used to buffer characters to the output stream without
		/// any translation. This is used when buffering the start tags so
		/// that they can be reset without affecting the resulting document.
		/// 
		/// </summary>
		/// <param name="plain">this is the string that is to be buffered
		/// </param>
		private void  append(System.String plain)
		{
			buffer.append(plain);
		}
		
		/// <summary> This method is used to write the specified text as a CDATA block
		/// within the XML element. This is typically used when the value is
		/// large or if it must be preserved in a format that will not be
		/// affected by other XML parsers. For large text values this is 
		/// also faster than performing a character by character escaping.
		/// 
		/// </summary>
		/// <param name="value">this is the text value to be written as CDATA
		/// </param>
		private void  data(System.String value_Renamed)
		{
			write("<![CDATA[");
			write(value_Renamed);
			write("]]>");
		}
		
		/// <summary> This is used to write the specified value to the output with
		/// translation to any symbol characters or non text characters.
		/// This will translate the symbol characters such as "&amp;",
		/// "&gt;", "&lt;", and "&quot;". This also writes any non text
		/// and non symbol characters as integer values like "&#123;".
		/// 
		/// </summary>
		/// <param name="value">the text value to be escaped and written
		/// </param>
		private void  escape(System.String value_Renamed)
		{
			int size = value_Renamed.Length;
			
			for (int i = 0; i < size; i++)
			{
				escape(value_Renamed[i]);
			}
		}
		
		/// <summary> This is used to write the specified value to the output with
		/// translation to any symbol characters or non text characters.
		/// This will translate the symbol characters such as "&amp;",
		/// "&gt;", "&lt;", and "&quot;". This also writes any non text
		/// and non symbol characters as integer values like "&#123;".
		/// 
		/// </summary>
		/// <param name="ch">the text character to be escaped and written
		/// </param>
		private void  escape(char ch)
		{
			char[] text = symbol(ch);
			
			if (text != null)
			{
				write(text);
			}
			else
			{
				write(ch);
			}
		}
		
		/// <summary> This is used to flush the writer when the XML if it has been
		/// buffered. The flush method is used by the node writer after an
		/// end element has been written. Flushing ensures that buffering
		/// does not affect the result of the node writer.
		/// </summary>
		public virtual void  flush()
		{
			buffer.write(result);
			buffer.clear();
			result.Flush();
		}
		
		/// <summary> This is used to convert the the specified character to unicode.
		/// This will simply get the decimal representation of the given
		/// character as a string so it can be written as an escape.
		/// 
		/// </summary>
		/// <param name="ch">this is the character that is to be converted
		/// 
		/// </param>
		/// <returns> this is the decimal value of the given character
		/// </returns>
		private System.String unicode(char ch)
		{
			return System.Convert.ToString(ch);
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
			if (value_Renamed != null)
			{
				return value_Renamed.Length == 0;
			}
			return true;
		}
		
		/// <summary> This is used to determine if the specified character is a text
		/// character. If the character specified is not a text value then
		/// this returls true, otherwise this returns false.
		/// 
		/// </summary>
		/// <param name="ch">this is the character to be evaluated as text
		/// 
		/// </param>
		/// <returns> this returns the true if the character is textual
		/// </returns>
		private bool isText(char ch)
		{
			switch (ch)
			{
				
				case ' ': 
				case '\n': 
				case '\r': 
				case '\t': 
					return true;
				}
			if (ch > ' ' && ch <= 0x7E)
			{
				return ch != 0xF7;
			}
			return false;
		}
		
		/// <summary> This is used to convert the specified character to an XML text
		/// symbol if the specified character can be converted. If the
		/// character cannot be converted to a symbol null is returned.
		/// 
		/// </summary>
		/// <param name="ch">this is the character that is to be converted
		/// 
		/// </param>
		/// <returns> this is the symbol character that has been resolved
		/// </returns>
		private char[] symbol(char ch)
		{
			switch (ch)
			{
				
				case '<': 
					return LESS;
				
				case '>': 
					return GREATER;
				
				case '"': 
					return DOUBLE;
				
				case '\'': 
					return SINGLE;
				
				case '&': 
					return AND;
				}
			return null;
		}
		
		/// <summary> This is used to enumerate the different types of tag that can
		/// be written. Each tag represents a state for the writer. After
		/// a specific tag type has been written the state of the writer
		/// is updated. This is needed to write well formed XML text.
		/// </summary>
		private enum_Renamed Tag;
	}
}