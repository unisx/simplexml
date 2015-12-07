/*
 * Comparer.java December 2009
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

import java.lang.annotation.Annotation;
import java.lang.reflect.Method;

/**
 * The <code>Comparer</code> is used to compare annotations on the
 * attributes of that annotation. Unlike the <code>equals</code>
 * method, this can ignore some attributes based on the name of the
 * attributes. This is useful if some annotations have overridden
 * values, such as the field or method name.
 * 
 * @author Niall Gallagher
 */
class Comparer {
   
   /**
    * This is the default attribute to ignore for the comparer.
    */
   private static final String NAME = "name";
   
   /**
    * This is the list of names to ignore for this instance.
    */
   private final String[] ignore;
   
   /**
    * Constructor for the <code>Comparer</code> object. This is
    * used to create a comparer that has a default set of names
    * to be ignored during the comparison of annotations.
    */
   public Comparer() {
      this(NAME);
   }

   /**
    * Constructor for the <code>Comparer</code> object. This is
    * used to create a comparer that has a default set of names
    * to be ignored during the comparison of annotations.
    * 
    * @param ignore this is the set of attributes to be ignored
    */
   public Comparer(String... ignore) {
      this.ignore = ignore;
   }
   
   /**
    * This is used to determine if two annotations are equals based
    * on the attributes of the annotation. The comparison done can
    * ignore specific attributes, for instance the name attribute.
    * 
    * @param left this is the left side of the comparison done
    * @param right this is the right side of the comparison done
    * 
    * @return this returns true if the annotations are equal
    */
   public boolean equals(Annotation left, Annotation right) throws Exception {
      Class type = left.annotationType();
      Method[] list = type.getDeclaredMethods();

      for(Method method : list) {
         if(!isIgnore(method)) {
            Object value = method.invoke(left);
            Object other = method.invoke(right);
            
            if(!value.equals(other)) {
               return false;
            }
         }
      }
      return true;
   }
   
   /**
    * This is used to determine if the method for an attribute is 
    * to be ignore. To determine if it should be ignore the method
    * name is compared against the list of attributes to ignore.
    * 
    * @param method this is the method to be evaluated
    * 
    * @return this returns true if the method should be ignored
    */
   private boolean isIgnore(Method method) {
      String name = method.getName();
      
      for(String value : ignore) {
         if(name.equals(value)) {
            return true;
         }
      }
      return false;
   }
}
