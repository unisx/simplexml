/*
 * ValueInstance.java January 2007
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
 * The <code>ValueInstance</code> object is used to create an object
 * by using a <code>Value</code> instance to determine the type. If
 * the provided value instance represents a reference then this will
 * simply provide the value of the reference, otherwise it will
 * instantiate a new object and return that.
 * 
 * @author Niall Gallagher
 */
class ValueInstance implements Instance {
   
   /**
    * This is the instantiator used to create the objects.
    */
   private final Instantiator creator;
   
   /**
    * This is the internal value that contains the criteria.
    */
   private final Value value;
   
   /**
    * This is the type that is to be instantiated by this.
    */
   private final Class type;
   
   /**
    * Constructor for the <code>ValueInstance</code> object. This 
    * is used to represent an instance that delegates to the given
    * value object in order to acquire the object. 
    * 
    * @param creator this is the instantiator used to create objects
    * @param value this is the value object that contains the data
    */
   public ValueInstance(Instantiator creator, Value value) {
      this.type = value.getType();
      this.creator = creator;
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
      Object object = creator.getObject(type);
      
      if(value != null) {
         value.setValue(object);
      }
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
    * deserialization based on its annotations. Values that are
    * references could be substitutes objects or existing ones. 
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
