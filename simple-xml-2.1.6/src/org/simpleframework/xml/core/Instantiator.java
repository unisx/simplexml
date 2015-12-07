/*
 * Instantiator.java July 2006
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

package org.simpleframework.xml.core;

import java.lang.reflect.Constructor;

import org.simpleframework.xml.strategy.Value;

/**
 * The <code>Instantiator</code> is used to instantiate types that 
 * will leverage a constructor cache to quickly create the objects.
 * This is used by the various object factories to return type 
 * instances that can be used by converters to create the objects 
 * that will later be deserialized.
 *
 * @author Niall Gallagher
 * 
 * @see org.simpleframework.xml.core.Instance
 */
class Instantiator {

   /**
    * This is used to cache the constructors for the given types.
    */
   private final ConstructorCache cache;
   
   /**
    * Constructor for the <code>Instantiator</code> object. This will
    * create a constructor cache that can be used to cache all of 
    * the constructors instantiated for the required types. 
    */
   public Instantiator() {
      this.cache = new ConstructorCache();
   }
   
   /**
    * This will create an <code>Instance</code> that can be used
    * to instantiate objects of the specified class. This leverages
    * an internal constructor cache to ensure creation is quicker.
    * 
    * @param value this contains information on the object instance
    * 
    * @return this will return an object for instantiating objects
    */
   public Instance getInstance(Value value) {
      return new ValueInstance(this, value);
   }

   /**
    * This will create an <code>Instance</code> that can be used
    * to instantiate objects of the specified class. This leverages
    * an internal constructor cache to ensure creation is quicker.
    * 
    * @param type this is the type that is to be instantiated
    * 
    * @return this will return an object for instantiating objects
    */
   public Instance getInstance(Class type) {
      return new ClassInstance(this, type);
   }
   
   /**
    * This method will instantiate an object of the provided type. If
    * the object or constructor does not have public access then this
    * will ensure the constructor is accessible and can be used.
    * 
    * @param type this is used to ensure the object is accessible
    *
    * @return this returns an instance of the specific class type
    */ 
   public Object getObject(Class type) throws Exception {
      Constructor method = cache.get(type);
      
      if(method == null) {
         method = type.getDeclaredConstructor();      

         if(!method.isAccessible()) {
            method.setAccessible(true);              
         }
         cache.put(type, method);
      }
      return method.newInstance();   
   }
}