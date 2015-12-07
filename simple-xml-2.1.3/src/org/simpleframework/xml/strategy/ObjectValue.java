/*
 * ObjectValue.java January 2007
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

package org.simpleframework.xml.strategy;

/**
 * The <code>ObjectValue</code> is an implementation of a value
 * that represents a object other than an array. Objects described
 * by this can be instantiated and set in to the internal graph 
 * so that they can be later retrieved.
 * 
 * @author Niall Gallagher
 * 
 * @see org.simpleframework.xml.strategy.Allocate
 */
class ObjectValue implements Value {
   
   /**
    * This is the value that has been set for this instance.
    */
   private Object value;
   
   /**
    * This is the type that this object is used to represent.
    */
   private Class type;

   /**
    * Constructor for the <code>ObjectValue</code> object. This is
    * used to describe an object that can be instantiated by the
    * deserialization process and set on the internal graph used.
    * 
    * @param type this is the type of object that is described
    */
   public ObjectValue(Class type) {
      this.type = type;
   }        
   
   /**
    * This method is used to acquire an instance of the type that
    * is defined by this object. If the value has not been set
    * then this method will return null as this is not a reference.
    * 
    * @return an instance of the type this object represents
    */
   public Object getValue() {
      return value;
   }
   
   /**
    * This method is used set the value within this object. Once
    * this is set then the <code>getValue</code> method will return
    * the object that has been provided for consistency. 
    * 
    * @param value this is the value to insert as the type
    */
   public void setValue(Object value) {
      this.value = value;
   }
   
   /**
    * This is the type of the object instance this represents. The
    * type returned by this is used to instantiate an object which
    * will be set on this value and the internal graph maintained.
    * 
    * @return the type of the object that will be instantiated
    */
   public Class getType() {
      return type;
   }
   
   /**
    * This returns zero as this is an object and will typically
    * not be used to instantiate anything. If the reference is an
    * an array then this can not be used to instantiate it.
    * 
    * @return this returns zero regardless of the value type
    */
   public int getLength() {
      return 0;
   }
   
   /**
    * This method always returns false for the default type. This
    * is because by default all elements encountered within the 
    * XML are to be deserialized based on there XML annotations.
    * 
    * @return this returns false for each type encountered     
    */
   public boolean isReference() {
      return false;
   }
}
