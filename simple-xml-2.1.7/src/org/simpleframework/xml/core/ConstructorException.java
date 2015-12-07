/*
 * ConstructorException.java July 2009
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

/**
 * The <code>ConstructorException</code> is used to represent any
 * errors where an annotated constructor parameter is invalid. This
 * is thrown when constructor injection is used and the schema is
 * invalid. Invalid schemas are schemas where an annotated method
 * or field does not match an annotated constructor parameter.
 * 
 * @author Niall Gallagher
 */
public class ConstructorException extends PersistenceException {

   /**
    * Constructor for the <code>ConstructorException</code> object. 
    * This constructor takes a format string an a variable number of
    *  object arguments, which can be inserted into the format string. 
    * 
    * @param text a format string used to present the error message
    * @param list a list of arguments to insert into the string
    */
   public ConstructorException(String text, Object... list) {
      super(text, list);           
   }        

   /**
    * Constructor for the <code>ConstructorException</code> object.
    * This constructor takes a format string an a variable number of 
    * object arguments, which can be inserted into the format string. 
    * 
    * @param cause the source exception this is used to represent
    * @param text a format string used to present the error message
    * @param list a list of arguments to insert into the string 
    */
   public ConstructorException(Throwable cause, String text, Object... list) {
      super(cause, text, list);           
   }
}