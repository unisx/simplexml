/*
 * Persistable.java July 2006
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

package simple.xml.load;

/**
 * The <code>Persistable</code> interface is used to receive callbacks 
 * from the deserialization of an object. This is an optional interface
 * that an object can implement if it wants to perform any validation
 * or post processing of deserialization. 
 * <p>
 * The callbacks received by an object implementing this interface 
 * are <code>validate</code> followed by <code>commit</code>. These
 * callbacks give the object an opportunity to validate the values that
 * have been deserialized before the object performs actions to build
 * further data structures with the deserialized fields.
 *
 * @author Niall Gallagher
 * 
 * @see simple.xml.load.Persister
 */ 
public interface Persistable {

   /**
    * The <code>validate</code> method is invoked immediately after the
    * object has been deserialized. This is used to validate the data
    * that has been read from the XML document. If any values do not
    * pass validation this can throw an exception, stopping the process
    * of deserialization. 
    *
    * @throws Exception thrown if the deserialized values are invalid
    */         
   public void validate() throws Exception;

   /**
    * The <code>commit</code> method is invoked if validation passes
    * and no exception is thrown from the <Code>validate</code> method.
    * This is used for post processing, it allows the object to build
    * further data structures when the annotated fields have been 
    * read from the XML document. If the post processing cannot be
    * performed this can thrown an exception to stop deserialization.
    *
    * @throws Exception thrown if post processing caused an error
    */ 
   public void commit() throws Exception;   
}
