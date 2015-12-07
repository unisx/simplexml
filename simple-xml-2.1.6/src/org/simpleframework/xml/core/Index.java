/*
 * Index.java April 2009
 *
 * Copyright (C) 2009, Niall Gallagher <niallg@users.sf.net>
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

import java.util.LinkedHashMap;

/**
 * The <code>Index</code> object is used to represent an index
 * of parameters iterable in declaration order. This is used so
 * that parameters can be acquired by name for validation. It is
 * also used to create an array of <code>Parameter</code> objects
 * that can be used to acquire the correct deserialized values
 * to use in order to instantiate the object.
 * 
 * @author Niall Gallagher
 */
class Index extends LinkedHashMap<String, Parameter> {
    
   /**
    * This represents a blank array of parameter objects.
    */
   private final Parameter[] blank;
   
   /**
    * This is the type that the parameters are created for.
    */
   private final Class type;
   
   /**
    * Constructor for the <code>Index</code> object. This is 
    * used to create a hash map that can be used to acquire
    * parameters by name. It also provides the parameters in
    * declaration order within a for each loop.
    * 
    * @param type this is the type the map is created for
    */
   public Index(Class type) {
      this.blank = new Parameter[]{};
      this.type = type;
   }
   
   /**
    * This is used to acquire a <code>Parameter</code> using the
    * position of that parameter within the constructor. This 
    * allows a builder to determine which parameters to use..
    * 
    * @param ordinal this is the position of the parameter
    * 
    * @return this returns the parameter for the position
    */
   public Parameter getParameter(int ordinal) {
      return getParameters()[ordinal];
   }
    
   /**
    * This is used to acquire an array of <code>Parameter</code>
    * objects in declaration order. This array will help with the
    * resolution of the correct constructor for deserialization
    * of the XML. It also provides a faster method of iteration.
    * 
    * @return this returns the parameters in declaration order
    */
   public Parameter[] getParameters() {
      return values().toArray(blank);
   }
 
   /**
    * This is the type that this class map represents. It can be 
    * used to determine where the parameters stored are declared.
    * 
    * @return returns the type that the parameters are created for
    */
   public Class getType() {
      return type;
   }
}