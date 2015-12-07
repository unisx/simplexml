/*
 * Reference.java May 2006
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

package org.simpleframework.xml.strategy;

/**
 * The <code>Reference</code> object represents an object that 
 * is used to provide a reference to an already instantiated value.
 * This is what is used if there is a cycle in the object graph. 
 * The <code>getValue</code> method of this object will simply
 * return the object instance that was previously created.
 * 
 * @author Niall Gallagher
 */
class Reference implements Value {
   
   /**
    * This is the object instance that has already be created.
    */
   private Object value;
   
   /**
    * This is the type of the object that this references.
    */
   private Class type;
   
   /**
    * Constructor for the <code>Reference</code> object. This 
    * is used to create a value that will produce the specified 
    * value when the <code>getValue</code> method is invoked.
    * 
    * @param value the value for the reference this represents
    * @param type this is the type value for the instance
    */
   public Reference(Object value, Class type) {      
      this.value = value;      
      this.type = type;
   }
   
   /**
    * This is used to acquire a reference to the instance that is
    * taken from the created object graph. This enables any cycles
    * in the graph to be reestablished from the persisted XML.
    * 
    * @return this returns a reference to the created instance
    */
   public Object getValue() {     
      return value;      
   }
   
   /**
    * This method is used set the value within this object. Once
    * this is set then the <code>getValue</code> method will return
    * the object that has been provided. Typically this will not 
    * be set as this represents a reference value.
    * 
    * @param value this is the value to insert as the type
    */
   public void setValue(Object value) {
      this.value = value;
   }
   
   /**
    * This returns the type for the object that this references.
    * This will basically return the <code>getClass</code> class
    * from the referenced instance. This is used to ensure that
    * the type this represents is compatible to the object field.
    * 
    * @return this returns the type for the referenced object
    */
   public Class getType() {
      return type;
   }
   
   /**
    * This returns zero as this is a reference and will typically
    * not be used to instantiate anything. If the reference is an
    * an array then this can not be used to instantiate it.
    * 
    * @return this returns zero regardless of the value type
    */
   public int getLength() {
      return 0;
   }
   
   /**
    * This always returns true for this object. This indicates to
    * the deserialization process that there should be not further
    * deserialization of the object from the XML source stream.
    * 
    * @return because this is a reference this is always true 
    */
   public boolean isReference() {      
      return true;
   }
}
