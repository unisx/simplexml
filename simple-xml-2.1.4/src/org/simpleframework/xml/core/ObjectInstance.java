/*
 * ObjectInstance.java April 2007
 *
 * Copyright (C) 2007, Niall Gallagher <niallg@users.sf.net>
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

package org.simpleframework.xml.core;

import org.simpleframework.xml.strategy.Value;

/**
 * The <code>ObjectInstance</code> is used to instantiate an object
 * from the criteria provided in the given <code>Value</code>. If
 * the value contains a reference then the reference is provided
 * from this type. For performance the <code>Context</code> object
 * is used to instantiate the object as it contains a reflection
 * cache of constructors.
 * 
 * @author Niall Gallagher
 */
class ObjectInstance implements Instance {
   
   /**
    * This is the context that is used to create the instance.
    */
   private final Context context;
   
   /**
    * This is the value object that will be wrapped by this.
    */
   private final Value value;
   
   /**
    * This is the new class that is used for the instantiation.
    */
   private final Class type;
   
   /**
    * Constructor for the <code>ObjectInstance</code> object. This
    * is used to create an instance of the type described by the
    * value object. If the value object contains a reference then
    * this will simply provide that reference.
    * 
    * @param context this is used to instantiate the object
    * @param value this is the value describing the instance
    */
   public ObjectInstance(Context context, Value value) {
      this.type = value.getType();
      this.context = context;
      this.value = value;
   }
   
   /**
    * This method is used to acquire an instance of the type that
    * is defined by this object. If for some reason the type can
    * not be instantiated an exception is thrown from this.
    * 
    * @return an instance of the type this object represents
    */
   public Object getInstance() throws Exception {
      if(value.isReference()) {
         return value.getValue();
      }
      Object object = getInstance(type);
      
      if(value != null) {
         value.setValue(object);
      }
      return object;
   }
   
   /**
    * This method is used to acquire an instance of the type that
    * is defined by this object. If for some reason the type can
    * not be instantiated an exception is thrown from this.
    * 
    * @param type this is the type that is to be instantiated
    * 
    * @return an instance of the type this object represents
    */
   public Object getInstance(Class type) throws Exception {
      Instance value = context.getInstance(type);
      Object object = value.getInstance();
      
      return object;  
   }
   
   /**
    * This method is used acquire the value from the type and if
    * possible replace the value for the type. If the value can
    * not be replaced then an exception should be thrown. This 
    * is used to allow primitives to be inserted into a graph.
    * 
    * @param object this is the object to insert as the value
    * 
    * @return an instance of the type this object represents
    */
   public Object setInstance(Object object) {
      if(value != null) {
         value.setValue(object);
      }
      return object;
   }

   /**
    * This is used to determine if the type is a reference type.
    * A reference type is a type that does not require any XML
    * deserialization based on its annotations. Types that are
    * references could be substitutes objects are existing ones. 
    * 
    * @return this returns true if the object is a reference
    */
   public boolean isReference() {
      return value.isReference();
   }

   /**
    * This is the type of the object instance that will be created
    * by the <code>getInstance</code> method. This allows the 
    * deserialization process to perform checks against the field.
    * 
    * @return the type of the object that will be instantiated
    */
   public Class getType() {
      return type;
   } 
}
