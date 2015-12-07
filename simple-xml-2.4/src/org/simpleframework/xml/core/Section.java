/*
 * Section.java November 2010
 *
 * Copyright (C) 2010, Niall Gallagher <niallg@users.sf.net>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or 
 * implied. See the License for the specific language governing 
 * permissions and limitations under the License.
 */

package org.simpleframework.xml.core;

/**
 * The <code>Section</code> interface is used to represent a section
 * of XML that is to be generated. A section is a tree structure in
 * that it can contain other sections. Each section contains the
 * elements and attributes associated with it. This is used so that
 * complex XML structures can be written for a single object.
 * <p>
 * For convenience all the element names, including other section
 * names, can be iterated over. Element and section names can be
 * differentiated using the source section.
 * 
 * @author Niall Gallagher
 * 
 * @see org.simpleframework.xml.core.Structure
 */
interface Section extends Iterable<String> {

   /**
    * Returns a <code>LabelMap</code> that contains the details for
    * all fields and methods marked with XML annotations. All of the
    * element annotations are considered and gathered by name in 
    * this map. Also, if there is an associated <code>Style</code> 
    * for serialization the element names are renamed with this.
    * 
    * @return returns the elements associated with this section
    */
   public LabelMap getElements() throws Exception;
   
   /**
    * Returns a <code>LabelMap</code> that contains the details for
    * all fields and methods marked with XML annotations. All of the
    * attribute annotations are considered and gathered by name in 
    * this map. Also, if there is an associated <code>Style</code> 
    * for serialization the attribute names are renamed with this.
    * 
    * @return returns the attributes associated with this section
    */
   public LabelMap getAttributes() throws Exception;
   
   /**
    * Returns the named element as a <code>Label</code> object.
    * For convenience this method is provided so that when iterating
    * over the names of the elements in the section a specific one
    * of interest can be acquired.
    * <p>
    * To ensure that elements of the same name are not referenced
    * more than once this will remove the element once acquired. 
    * This ensures that they are visited only once in serialization.      
    * 
    * @param name the name of the element that is to be acquired
    * 
    * @return this returns the label associated with the name
    */
   public Label getElement(String name) throws Exception;
   
   /**
    * Returns the named section as a <code>Section</code> object.
    * For convenience this method is provided so that when iterating
    * over the names of the elements in the section a specific one
    * of interest can be acquired. 
    * <p>
    * To ensure that models of the same name are not referenced
    * more than once this will remove the model once acquired. 
    * This ensures that they are visited only once in serialization.
    * 
    * @param name the name of the element that is to be acquired
    * 
    * @return this returns the section associated with the name
    */
   public Section getSection(String name) throws Exception;
   
   /**
    * To differentiate between a section and an element this can be
    * used. When iterating over the elements within the section the
    * names of both elements and sections are provided. So in order
    * to determine how to interpret the structure this can be used.
    * 
    * @param name this is the name of the element to be determined
    * 
    * @return this returns true if the name represents a section
    */
   public boolean isSection(String name) throws Exception;
}
