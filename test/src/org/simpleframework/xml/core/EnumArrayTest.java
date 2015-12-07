package org.simpleframework.xml.core;

import junit.framework.TestCase;

import org.simpleframework.xml.Element;
import org.simpleframework.xml.Root;

public class EnumArrayTest extends TestCase {
   
   private static final String SOURCE =
   "<example>"+
   "  <array>ONE,TWO,FOUR</array>"+
   "</example>";
   
   private static enum Number {
      ONE,
      TWO,
      THREE,
      FOUR
   }
   
   @Root(name="example")
   private static class NumberArray {
      
      @Element(name="array")
      private final Number[] array;
      
      public NumberArray(@Element(name="array") Number[] array) {
         this.array = array;
      }
   }
   
   public void testArrayElement() throws Exception {
      Persister persister = new Persister();
      NumberArray array = persister.read(NumberArray.class, SOURCE);
      
      assertEquals(array.array.length, 3);
      assertEquals(array.array[0], Number.ONE);
      assertEquals(array.array[1], Number.TWO);
      assertEquals(array.array[2], Number.FOUR);
   }

}
