package org.simpleframework.xml.core;

import java.io.ByteArrayOutputStream;

import junit.framework.TestCase;

import org.simpleframework.xml.Element;
import org.simpleframework.xml.Namespace;
import org.simpleframework.xml.Serializer;

public class NamespaceInheritanceTest extends TestCase {
   
   public void testNamespace() throws Exception {
      Aaa parent = new Aaa();
      Bbb child = new Bbb();
      parent.bbb = child;
      Aaa grandchild = new Aaa();
      child.aaa = grandchild;
      grandchild.bbb = new Bbb();
 
      ByteArrayOutputStream tmp = new ByteArrayOutputStream();
      Serializer serializer = new Persister();
      serializer.write(parent, tmp);
 
      String result = new String(tmp.toByteArray());
 
      System.out.println(result);
   }
 
   @Namespace(prefix="aaa", reference = "namespace1")
   private static class Aaa {
      @Element(name = "bbb", required=false)
      public Bbb bbb;
   }
 
   @Namespace(prefix="bbb", reference = "namespace2")
   private static class Bbb {
      @Element(name = "aaa", required=false)
      public Aaa aaa;
   }
}