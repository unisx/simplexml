/*
 * Allocate.java January 2007
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

import java.util.Map;

/**
 * The <code>Allocate</code> object is used to represent an entity 
 * that has not yet been created and needs to be allocated to the
 * the object graph. This is given a map that contains each node 
 * in the graph keyed via a unique identifier. When an instance is 
 * created and set then it is added to the object graph.
 * 
 * @author Niall Gallagher
 */
class Allocate implements Value {
   
   /**
    * This is used to create an instance of the specified type.
    */
   private Value value;
   
   /**
    * This is the unique key that is used to store the value.
    */
   private String key;
   
   /**
    * This is used to store each instance in the object graph.
    */
   private Map map;
   
   /**
    * Constructor for the <code>Allocate</code> object. This is used
    * to create a value that can be used to set any object in to the
    * internal object graph so references can be discovered.
    * 
    * @param value this is the value used to describe the instance
    * @param map this contains each instance mapped with a key
    * @param key this is the unique key representing this instance
    */
   public Allocate(Value value, Map map, String key) {
      this.value = value;
      this.map = map;
      this.key = key;
   }
   
   /**
    * This method is used to acquire an instance of the type that
    * is defined by this object. If the object is not set in the
    * graph then this will return null.
    * 
    * @return an instance of the type this object represents
    */
   public Object getValue() {
      return map.get(key);
   }
   
   /**
    * This method is used to set the provided object in to the graph
    * so that it can later be retrieved. If the key for this value
    * is null then no object is set in the object graph.
    * 
    * @param object this is the value to insert to the graph
    */
   public void setValue(Object object) {
      if(key != null) {
         map.put(key, object);
      }     
      value.setValue(object);
   }
   
   /**
    * This is the type of the object instance that will be created
    * and set on this value. If this represents an array then this
    * is the component type for the array to be created.
    * 
    * @return the type of the object that will be instantiated
    */
   public Class getType() {
      return value.getType();
   }
   
   /**
    * This returns the length of an array if this value represents
    * an array. If this does not represent an array then this will
    * return zero. It is up to the deserialization process to 
    * determine if the annotated field or method is an array.
    * 
    * @return this returns the length of the array object
    */
   public int getLength() {
      return value.getLength();
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
