package org.simpleframework.xml.stream;

import java.io.StringWriter;
import java.util.LinkedHashMap;

import org.simpleframework.xml.ValidationTestCase;

public class PrefixResolverTest extends ValidationTestCase {
   
   public void testPrefixResolver() throws Exception {
      StringWriter writer = new StringWriter();
      OutputNode node = NodeBuilder.write(writer);
      
      // <root xmlns="ns1">
      OutputNode root = node.getChild("root");
      root.setReference("ns1");
      root.getNamespaces().put("ns1", "n");
      
      // <child xmlns="ns2">
      OutputNode child = root.getChild("child");
      child.setReference("ns2");
      child.getNamespaces().put("ns2", "n");

      // <grandchild xmlns="ns1">
      OutputNode grandchild = child.getChild("grandchild");
      grandchild.setReference("ns1");
      grandchild.getNamespaces().put("ns1", "n");
      
      root.commit();
      
      String text = writer.toString();
      System.out.println(text);
   }
   public static class KeyMap extends LinkedHashMap<String, String>{
      public KeyMap() {
         super();
      }
   }
   public static class NodeResolver {
      private final KeyMap references;
      private final KeyMap prefixes;
      private final Node parent;
      public NodeResolver(Node parent) {
         this.references = new KeyMap();
         this.prefixes = new KeyMap();
         this.parent = parent;
      }
      public String getPrefix(String reference){
         String value = prefixes.get(reference);
         if(value == null) {
            NodeResolver resolver = parent.getResolver();
            String prefix = resolver.getPrefix(reference);
            if(prefix != null){
               return prefix;
            }
         }
         return value;
      }
      public String getReference(String prefix){
         String value = references.get(prefix);
         if(value == null) {
            NodeResolver resolver = parent.getResolver();
            String reference = resolver.getReference(prefix);
            if(reference != null){
               return reference;
            }
         }
         return value;
      }
      public void addNamespace(String reference, String prefix){
         String result = getPrefix(reference);
         if(result != null) {
            String prefixReference = getReference(prefix);
            if(prefixReference != null && prefixReference.equals(reference)) {
               return; // don't bother as parent already has it
            }
         }
         this.references.put(reference, prefix);
         this.prefixes.put(prefix, reference);
      }
   }
   public static class Node {
      private NodeResolver resolver;
      private String reference;
      private Node parent;
      public Node() {
         this(null);
      }
      public Node(Node parent) {
         this.resolver = new NodeResolver(this);
         this.parent = parent;
      }
      public NodeResolver getResolver() {
         return resolver;
      }
      public void setReference(String reference){
         this.reference = reference;
      }
      public String getReference(){
         return reference;
      }
      public String getPrefix(){
         String prefix = resolver.getPrefix(reference);
         if(prefix != null) {
            return prefix;
         }
         if(parent != null) {
            return parent.getPrefix();
         }
         return null;
      }
   }
}
