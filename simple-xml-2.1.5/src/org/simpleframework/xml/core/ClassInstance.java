/*
 * ClassInstance.java January 2007
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

/**
 * The <code>ClassInstance</code> object is used to create an object
 * by using a <code>Class</code> to determine the type. If the given
 * class can not be instantiated then this throws an exception when
 * the instance is requested. For performance an instantiator is
 * given as it contains a reflection cache for constructors.
 * 
 * @author Niall Gallagher
 */
class ClassInstance implements Instance {
   
   /**
    * This is the instantiator used to create the objects.
    */
   private Instantiator creator;
   
   /**
    * This represents the value of the instance if it is set.
    */
   private Object value;
   
   /**
    * This is the type of the instance that is to be created.
    */
   private Class type;
   
   /**
    * Constructor for the <code>ClassInstance</code> object. This is
    * used to create an instance of the specified type. If the given
    * type can not be instantiated then an exception is thrown.
    * 
    * @param creator this is the creator used for the instantiation
    * @param type this is the type that is to be instantiated
    */
   public ClassInstance(Instantiator creator, Class type) {
      this.creator = creator;
      this.type = type;
   }

   /**
    * This method is used to acquire an instance of the type that
    * is defined by this object. If for some reason the type can
    * not be instantiated an exception is thrown from this.
    * 
    * @return an instance of the type this object represents
    */
   public Object getInstance() throws Exception {
      if(value == null) {
         value = creator.getObject(type);
      }
      return value;
   }
   
   /**
    * This method is used acquire the value from the type and if
    * possible replace the value for the type. If the value can
    * not be replaced then an exception should be thrown. This 
    * is used to allow primitives to be inserted into a graph.
    * 
    * @param value this is the value to insert as the type
    * 
    * @return an instance of the type this object represents
    */
   public Object setInstance(Object value) throws Exception {
      return this.value = value;
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

   /**
    * This is used to determine if the type is a reference type.
    * A reference type is a type that does not require any XML
    * deserialization based on its annotations. Values that are
    * references could be substitutes objects or existing ones. 
    * 
    * @return this returns true if the object is a reference
    */
   public boolean isReference() {
      return false;
   }
}
