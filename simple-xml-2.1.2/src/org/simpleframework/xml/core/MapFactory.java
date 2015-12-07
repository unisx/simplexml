/*
 * MapFactory.java July 2007
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

import java.util.HashMap;
import java.util.Map;
import java.util.TreeMap;

import org.simpleframework.xml.strategy.Value;
import org.simpleframework.xml.stream.InputNode;

/**
 * The <code>MapFactory</code> is used to create map instances that
 * are compatible with the field type. This performs resolution of 
 * the map class by consulting the specified <code>Strategy</code> 
 * implementation. If the strategy cannot resolve the map class 
 * then this will select a type from the Java Collections framework, 
 * if a compatible one exists.
 * 
 * @author Niall Gallagher
 */ 
class MapFactory extends Factory {
   
   /**
    * Constructor for the <code>MapFactory</code> object. This is 
    * given the field type as taken from the owning object. The
    * given type is used to determine the map instance created.
    * 
    * @param context this is the context object for this factory
    * @param field this is the class for the owning object
    */
   public MapFactory(Context context, Class field) {
      super(context, field);           
   }
   
   /**
    * Creates a map object that is determined from the field type. 
    * This is used for the <code>ElementMap</code> to get a map 
    * that does not have any overrides. This must be done as the 
    * inline list does not contain an outer element.
    * 
    * @return a type which is used to instantiate the map     
    */
   public Object getInstance() throws Exception {
      Class type = field;

      if(!isInstantiable(type)) {
         type = getConversion(field);   
      }
      if(!isMap(type)) {
         throw new InstantiationException("Type is not a collection %s", field);
      }
      return type.newInstance();
   }
   
   /**
    * Creates the map object to use. The <code>Strategy</code> object
    * is consulted for the map object class, if one is not resolved
    * by the strategy implementation or if the collection resolved is
    * abstract then the Java Collections framework is consulted.
    * 
    * @param node this is the input node representing the list
    * 
    * @return this is the map object instantiated for the field
    */       
   public Instance getInstance(InputNode node) throws Exception {
      Value value = getOverride(node);
     
      if(value != null) {              
         return getInstance(value);
      }
      if(!isInstantiable(field)) {
         field = getConversion(field);
      }
      if(!isMap(field)) {
         throw new InstantiationException("Type is not a map %s", field);
      }
      return context.getInstance(field);         
   }  
   
   /**
    * This creates a <code>Map</code> object instance from the type
    * provided. If the type provided is abstract or an interface then
    * this can promote the type to a map object type that can be 
    * instantiated. This is done by asking the type to convert itself.
    * 
    * @param value the type used to instantiate the map object
    * 
    * @return this returns a compatible map object instance 
    */
   public Instance getInstance(Value value) throws Exception {
      Class type = value.getType();

      if(!isInstantiable(type)) {
         type = getConversion(type);
      }
      if(!isMap(type)) {
         throw new InstantiationException("Type is not a map %s", type);              
      }
      return new ConversionInstance(context, value, type);         
   } 
   
   /**
    * This is used to convert the provided type to a map object type
    * from the Java Collections framework. This will check to see if
    * the type is a <code>Map</code> or <code>SortedMap</code> and 
    * return a <code>HashMap</code> or <code>TreeSet</code> type. If 
    * no suitable match can be found this throws an exception.
    * 
    * @param type this is the type that is to be converted
    * 
    * @return a collection that is assignable to the provided type
    */ 
   public Class getConversion(Class type) throws Exception {
      if(type.isAssignableFrom(HashMap.class)) {
         return HashMap.class;
      }
      if(type.isAssignableFrom(TreeMap.class)) {
         return TreeMap.class;                 
      }      
      throw new InstantiationException("Cannot instantiate %s", type);
   }
   
   /**
    * This determines whether the type provided is a object map type.
    * If the type is assignable to a <code> Map</code> object then 
    * this returns true, otherwise this returns false.
    * 
    * @param type given to determine whether it is a map type  
    * 
    * @return true if the provided type is a map object type
    */
   private boolean isMap(Class type) {
      return Map.class.isAssignableFrom(type);           
   }
}