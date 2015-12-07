/*
 * TemplateEngine.java May 2005
 *
 * Copyright (C) 2005, Niall Gallagher <niallg@users.sf.net>
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General 
 * Public License along with this library; if not, write to the 
 * Free Software Foundation, Inc., 59 Temple Place, Suite 330, 
 * Boston, MA  02111-1307  USA
 */

package simple.xml.load;

import simple.xml.filter.PlatformFilter;
import simple.xml.filter.Filter;
import java.util.HashMap;
import java.util.Map;

/**
 * The <code>TemplateEngine</code> object is used to create strings
 * which have system variable names replaced with their values.
 * This is used by the <code>Source</code> context object to ensure
 * that values taken from an XML element or attribute can be values
 * values augmented with system or environment variable values.
 * <pre>
 *
 *    tools=${java.home}/lib/tools.jar
 * 
 * </pre>
 * Above is an example of the use of an system variable that
 * has been inserted into a plain Java properties file. This will
 * be converted to the full path to tools.jar when the system
 * variable "java.home" is replaced with the matching value.
 * 
 * @author Niall Gallagher
 */ 
final class TemplateEngine extends Template {
   
   /**
    * This is used to accumulate the bytes for the variable name.
    */         
   private Template name;

   /**
    * This is used to accumulate the transformed text value.
    */ 
   private Template text;

   /**
    * This is the filter used to replace templated variables.
    */
   private Filter filter;

   /**
    * This is used to keep track of the buffer seek offset.
    */ 
   private int off;
   
   /**
    * Constructor for the <code>TemplateEngine</code> object. This is 
    * used to create a parsing buffer, which can be used to replace
    * filter variable names with their corrosponding values.
    * 
    * @param filter this is the filter used to provide replacements 
    */ 
   public TemplateEngine() {
      this(new HashMap());           
   }
   
   /**
    * Constructor for the <code>TemplateEngine</code> object. This is 
    * used to create a parsing buffer, which can be used to replace
    * filter variable names with their corrosponding values.
    * 
    * @param filter this is the filter used to provide replacements 
    */ 
   public TemplateEngine(Map map) {
      this(new PlatformFilter(map));           
   }
   
   /**
    * Constructor for the <code>TemplateEngine</code> object. This is 
    * used to create a parsing buffer, which can be used to replace
    * filter variable names with their corrosponding values.
    * 
    * @param filter this is the filter used to provide replacements 
    */ 
   public TemplateEngine(Filter filter) {
      this.name = new Template();            
      this.text = new Template();
      this.filter = filter;
   }

   /**
    * This method is used to parse the value of the buffered text 
    * and return the corrosponding string. The contents of this
    * buffer remain unmodified when this method is invoked. The
    * transformed value is stored in a seperate text buffer.
    *
    * @return this returns the value of the converted string
    */ 
   public String process() {
      if(text.length() <=0){
         parse();              
      }          
      try {
         return text.toString();
      } finally {
         clear();
      }
   }
   
   /**
    * This method is used to append the provided text and then it
    * converts the buffered text to return the corrosponding text.
    * The contents of the buffer remain unchanged after the value
    * is buffered. It must be cleared if used as replacement only. 
    * 
    * @param value this is the value to append to the buffer
    * 
    * @return returns the value of the buffer after the append
    */
   public String process(String value) {
      if(value != null) {
         append(value);
      }
      return process();
   }
   
   /**
    * This extracts the value from the Java properties text. This
    * will basically ready any text up to the first occurance of
    * an equal of a terminal. If a terminal character is read
    * this returns without adding the terminal to the value.
    */
   private void parse(){
      while(off < count){
         char next = buf[off++];
         
         if(next == '$') {
            if(off < count)
               if(buf[off++] == '{') {
                  name();         
                  continue;
               } else {
                  off--;                       
               }         
         } 
         text.append(next);
      }
   }

   /**
    * This method is used to extract text from the property value
    * that matches the pattern "${ *TEXT }". Such patterns within 
    * the properties file are considered to be system 
    * variables, this will replace instances of the text pattern
    * with the matching system variable, if a matching
    * variable does not exist the value remains unmodified.
    */ 
   private void name() {
      while(off < count) {
         char next = buf[off++];              

         if(next == '}') {
            replace();                 
            break;
         } else {
            name.append(next);
         }            
      }      
      if(name.length() >0){
         text.append("${"+name);              
      }     
   }

   /**
    * This will replace the accumulated for an system variable
    * name with the value of that system variable. If a value
    * does not exist for the variable name, then the name is put
    * into the value so that the value remains unmodified.
    */ 
   private void replace() {
      if(name.length() > 0) {
         replace(name);
      }           
      name.clear();
   }

   /**
    * This will replace the accumulated for an system variable
    * name with the value of that system variable. If a value
    * does not exist for the variable name, then the name is put
    * into the value so that the value remains unmodified.
    *
    * @param name this is the name of the system variable
    */ 
   private void replace(Template name) {
      replace(name.toString());
   }

   /**
    * This will replace the accumulated for an system variable
    * name with the value of that system variable. If a value
    * does not exist for the variable name, then the name is put
    * into the value so that the value remains unmodified.
    *
    * @param name this is the name of the system variable
    */ 
   private void replace(String name) {
      String value = filter.replace(name);
   
      if(value == null) {
         text.append("${"+name+"}");
      }else {
         text.append(value);              
      }
   }

   /**
    * This method is used to clear the contents of the buffer. This
    * includes the contents of all buffers used to transform the
    * value of the buffered text with system variable values.
    * Once invoked the instance can be reused as a clean buffer.
    */ 
   public void clear() {
      name.clear();
      text.clear();    
      super.clear();
      off = 0;  
   }

   /**
    * This method is used to parse the value of the buffered text 
    * and return the corrosponding string. The contents of this
    * buffer remain unmodified when this method is invoked. The
    * transformed value is stored in a seperate text buffer.
    *
    * @return this returns the value of the converted string
    */   
   public String toString() {
      return process();
   }  
}
