/*
 * ArrayValue.java April 2007
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
 * The <code>ArrayValue</code> object is a value used for describing
 * arrays for a specified component type object. This provides the
 * component type for the array as well as the length of the array,
 * which allows the deserialization process to build a suitable length
 * array from the criteria taken from the XML element.
 * 
 * @author Niall Gallagher
 * 
 * @see org.simpleframework.xml.strategy.Allocate
 */
class ArrayValue implements Value {
   
   /**
    * This is the value that has been set within this value object.
    */
   private Object value;
   
   /**
    * This is the optional field type for the array to be created. 
    */
   private Class type;
   
   /**
    * This is used to determine the size of the array to be created.
    */
   private int size;
   
   /**
    * Constructor for the <code>ArrayValue</code> object. This will
    * provide sufficient criteria to the deserialization process 
    * to instantiate an array of the specified size an type.
    * 
    * @param type this is the component type for the array
    * @param size this is the size of the array to instantiate
    */
   public ArrayValue(Class type, int size) {
      this.type = type;      
      this.size = size;
   }
   
   /**
    * This is the instance that is acquired from this value. This is
    * typically used if the <code>isReference</code> method is true.
    * If there was no value reference provided then this returns null.
    * 
    * @return this returns a reference to an existing array
    */
   public Object getValue() {
      return value;
   }
   
   /**
    * This method is set the value so that future calls provide the
    * value that was provided. Setting the value ensures that the
    * value used is consistent across invocations of this object.
    * 
    * @param value this is the value to inserted to this object
    */
   public void setValue(Object value) {
      this.value = value;
   }
   
   /**
    * This will return the component type for the array instance 
    * that is described by this object. This is used to ensure that
    * an array with the correct component type can be instantiated.
    * 
    * @return this returns the component type for the array
    */
   public Class getType() {
      return type;
   }

   /**
    * This returns the length of the array that is to be allocated.
    * For various <code>Strategy</code> implementations the length
    * is provided as an attribute on the array XML element.
    * 
    * @return this returns the number of elements for the array
    */
   public int getLength() {
      return size;
   } 
   
   /**
    * This will return false for the array value because the array
    * is not a reference type. Only <code>Reference</code> values 
    * will have this set to true as they read from the graph.
    * 
    * @return this returns false as this is not a reference value
    */
   public boolean isReference() {
      return false;
   }  
}