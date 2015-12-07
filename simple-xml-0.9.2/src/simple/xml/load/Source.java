/*
 * Source.java July 2006
 *
 * Copyright (C) 2006, Niall Gallagher <niallg@users.sf.net>
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
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import java.util.HashMap;
import java.util.Map;

/**
 * The <code>Source</code> object acts as a contextual object that is
 * used to store all information regarding an instance of serialization
 * or deserialization. This maintains the DOM <code>Document</code> as
 * well as the <code>Filter</code> used to replace template variables.
 * When serialization and deserialization are performed the source is
 * required as tt acts as a factory for objects used in the process.
 * <p>
 * For serialization the source object is required as a factory for
 * <code>Schema</code> objects, which are used to visit each field 
 * in the class that can be serialized. Also, it acts as a contextual
 * factory for <code>Element</code> objects that are used to build a
 * DOM document.
 * <p>
 * When deserializing the source object provides the contextual data
 * used to replace template variables extracted from the XML source.
 * This is performed using the <code>Filter</code> object. Also, as 
 * in serialization it acts as a factory for the <code>Schema</code> 
 * objects used to examine the serializable fields of an object.
 * 
 * @author Niall Gallagher
 * 
 * @see simple.xml.filter.Filter
 */ 
final class Source {

   /**
    * This is used to cache all schemas built to represent a class.
    * 
    * @see simple.xml.load.Scanner
    */
   private static ScannerCache cache;

   static {
      cache = new ScannerCache();           
   }
   
   /**
    * This is used to replace variables within the XML source.
    */
   private TemplateEngine engine;
   
   /**
    * This is used as a factory for creating DOM element objects.
    */
   private Document root;

   /**
    * Constructor for the <code>Source</code> object. This is used to
    * maintain a context during the serialization process. It holds 
    * the <code>Document</code> and <code>Filter</code> used in the
    * serialization process. The same source instance is used for 
    * each XML element evaluated in a the serialization process.   
    * 
    * @param root this is the document object used in serialization
    */
   public Source(Document root) {
      this(root, new HashMap());           
   }
   
   /**
    * Constructor for the <code>Source</code> object. This is used to
    * maintain a context during the serialization process. It holds 
    * the <code>Document</code> and <code>Filter</code> used in the
    * serialization process. The same source instance is used for 
    * each XML element evaluated in a the serialization process. 
    * 
    * @param root this is the document object used in serialization
    * @param data this is used to provide mappings for the filter
    */
   public Source(Document root, Map data) {
      this(root, new PlatformFilter(data));           
   }
   
   /**
    * Constructor for the <code>Source</code> object. This is used to
    * maintain a context during the serialization process. It holds 
    * the <code>Document</code> and <code>Filter</code> used in the
    * serialization process. The same source instance is used for 
    * each XML element evaluated in a the serialization process. 
    * 
    * @param root this is the document object used in serialization
    * @param data this is used for replacing template variables
    */       
   public Source(Document root, Filter data) {
      this.engine = new TemplateEngine(data);           
      this.root = root;
   }        

   /**
    * This is used to create <code>Element</code> objects that can
    * be used to build a document when serializing an object. The
    * elements created with this object are linked together as each
    * field within the schema object is traversed and completed.
    * 
    * @param name this is the name of the element to be created
    * 
    * @return returns an element from the document being created
    */
   public Element getElement(String name) {
      return root.createElement(name);           
   }

   /**
    * This creates a <code>Schema</code> object that can be used to
    * examine the fields within the XML class schema. The schema
    * maintains information when a field from within the schema is
    * visited, this allows the serialization and deserialization
    * process to determine if all required XML annotations are used.
    * 
    * @param source the source object the schema is created for
    * 
    * @return a new schema that can track visits within the schema
    */
   public Schema getSchema(Object source) {
      return getSchema(source.getClass());           
   }

   /**
    * This creates a <code>Schema</code> object that can be used to
    * examine the fields within the XML class schema. The schema
    * maintains information when a field from within the schema is
    * visited, this allows the serialization and deserialization
    * process to determine if all required XML annotations are used.
    * 
    * @param type the schema class the schema is created for
    * 
    * @return a new schema that can track visits within the schema
    */   
   public Schema getSchema(Class type) {
      Scanner schema = cache.get(type);
      
      if(schema == null) {
         schema = new Scanner(type);
         cache.put(type, schema);
      }
      return new Schema(schema);
   }

   /**
    * Replaces any template variables within the provided string. 
    * This is used in the deserialization process to replace 
    * variables with system properties, environment variables, or
    * used specified mappings. If a template variable does not have
    * a mapping from the <code>Filter</code> is is left unchanged.  
    * 
    * @param text this is text that will have template variables
    *        replaced with filter mappings 
    * 
    * @return this returns the text will all variables replaced
    */
   public String getProperty(String text) {
      return engine.process(text);
   }
}